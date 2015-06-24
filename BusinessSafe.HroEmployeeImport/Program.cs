using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using BusinessSafe.Application.Common;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.NHibernate.Helpers;
using NHibernate;
using NHibernate.Event;
using NServiceBus;
using Peninsula.Online.Data.NHibernate;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using NHibernate.Context;
using RestSharp;

using Configuration = NHibernate.Cfg.Configuration;

namespace BusinessSafe.HroEmployeeImport
{
    /// <summary>
    /// 
    /// </summary>
    public class ImportedRecord
    {
        public List<string> fields = new List<string>();

        private List<string> CSVToList(string line)
        {
            return line.Split(',').ToList();
        }

        public ImportedRecord(string line)
        {
            fields = CSVToList(line);
        }

        public Dictionary<string, string> GetRecordAsDictionary(ImportedRecord HeaderRow)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            for (int x = 0; x < HeaderRow.fields.Count; x++)
            {
                result.Add(HeaderRow.fields[x], fields[x]);
            }

            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ImportedData
    {
        public ImportedRecord HeaderRow;

        public List<ImportedRecord> Records = new List<ImportedRecord>();

        public void AddHeaderRow(string line)
        {
            HeaderRow = new ImportedRecord(line);            
        }

        public void AddRecord(string line)
        {            
            Records.Add(new ImportedRecord(line) );           
        }      
       
        public Dictionary<string, string> GetRecordAsDictionary(int recordIndex)
        {
            return Records[recordIndex].GetRecordAsDictionary(HeaderRow);                       
        }       
    }

    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        private string _connectionString;
        private char _key;
        private string _CAN;
        private Int64 _clientID;
        private List<EmployeeDto> _existingEmployeesList = null;
        private IEmployeeService _employeeService = null;        
        private IRestClient _restClient= null;

            
        private static readonly Guid SystemUserId = new Guid("B03C83EE-39F2-4F88-B4C4-7C276B1AAD99");

        public Program()
        {                        
            _connectionString = string.Empty;
            _key = 'L'; // Default to live database
            _clientID = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public char Key
        {
            get { return _key; }
            set
            {
                char tmp = Char.ToUpper(value);
                if ( !(tmp == 'D' || tmp == 'U' || tmp == 'L')) 
                {
                    throw new Exception("Invalid Database: should be 'D', 'U' or 'L'");
                }

                _key = tmp;
                SetConnectionString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ClientAccountNumber
        {
            get { return _CAN; }

            set
            {
                _CAN = value;

                _clientID = GetCompanyIDFromCAN(_CAN);

                if (_clientID == -1)
                {
                    throw new Exception("Invalid Client Account Number");
                }
            }                            
        }

        public int GetCompanyIDFromCAN(string clientAccountNumber)
        {
            var clientDetailsServicesUrl = ConfigurationManager.AppSettings["ClientDetailsServices.Rest.BaseUrl"];
            _restClient = new RestClient(clientDetailsServicesUrl);

            var request = new RestRequest("Client?can={CAN}", Method.GET)
                              {
                                  RequestFormat = DataFormat.Xml
                              };

            request.AddUrlSegment("CAN", clientAccountNumber);

            var resp = _restClient.Execute<CompanyDetailsResponse>(request).Data;

            return resp.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetConnectionString()
        {
            switch (_key)
            {
                case ('D'): _connectionString = ConfigurationManager.ConnectionStrings["DevDB"].ConnectionString; break;
                case ('U'): _connectionString = ConfigurationManager.ConnectionStrings["UATDB"].ConnectionString; break;
                case ('L'): _connectionString = ConfigurationManager.ConnectionStrings["LiveDB"].ConnectionString; break;

                default: throw new Exception("Must select database (U, D or L)");
            };                       
        }

        /// <summary>
        /// 
        /// </summary>
        public void Go()
        {           
            if (_clientID == 0)
            {
                throw new Exception("Must define Client Account Number" );
            }

            Setup();
            ReadDirectory();
        }

        private void Setup()
        {
            Console.WriteLine("Import operation starting");

            var sessionFactory = GetSessionFactory(_connectionString);
            var session = sessionFactory.OpenSession();

            CurrentSessionContext.Bind(session);
            
            ObjectFactory.Container.Configure(x =>
                                                  {
                                                      x.ForSingletonOf<IBusinessSafeSessionFactory>().Use<BusinessSafeSessionFactory>();
                                                      x.For<IBusinessSafeSessionManager>().HybridHttpOrThreadLocalScoped().Use<CurrentContextSessionManager>();                                                      
                                                      x.AddRegistry<ApplicationRegistry>();
                                                      x.For<IBus>().Use<StubBus>();
                                                  });

            _employeeService = ObjectFactory.GetInstance<IEmployeeService>();
            
            
            //populates the existing employee list only once based on client. i.e. The clientId must be same for all the records on excel sheet.
            //so do not need to query the database again & again for the same client.
            if (_existingEmployeesList == null)
            {
                _existingEmployeesList = _employeeService.GetAll(_clientID).ToList();
            }

            session.Flush();
            sessionFactory.GetCurrentSession().Dispose();
            CurrentSessionContext.Unbind(sessionFactory);
        }

        private void ReadDirectory()
        {            
            var directory = new DirectoryInfo( ConfigurationManager.AppSettings.Get("EmployeeImportFileDirectory") );            

            if (!directory.GetFiles().Any())
            {
                throw new Exception("No file to process. Could not find input file in directory " + directory.FullName);                
            }

            if (!Directory.Exists(directory.FullName + "\\Done"))
            {
                Directory.CreateDirectory(directory.FullName + "\\Done");
            }
                 
            foreach (var file in directory.GetFiles())
            {
                Console.WriteLine("Reading file '" + file.Name + "'");
                ImportedData importedData = ReadFile(file);

                Console.WriteLine("Writing to Business Safe database ");
                WriteToBso(importedData);

                Console.WriteLine("Moving file to Done");
                file.MoveTo(directory.FullName + "\\Done\\" + file.Name);
            }

            Console.WriteLine("Import operation completed.");
            Console.WriteLine("Press RETURN to finish.");
            Console.Read();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private ImportedData ReadFile(FileInfo file)
        {
            ImportedData importedData =  new ImportedData();

            using (var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    importedData.AddHeaderRow(streamReader.ReadLine());

                    while (streamReader.Peek() >= 0)
                    {
                        importedData.AddRecord(streamReader.ReadLine());
                    }
                }

                fileStream.Close();
            }
            
            return importedData;
        }            

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private ISessionFactory GetSessionFactory(string connectionString)
        {
            var hibernateConfigFilePath = new NHibernateConfigPathGenerator(Database.BusinessSafe).GetConfigFilePath();

            var configuration = new Configuration();
            configuration.Configure(hibernateConfigFilePath);

            configuration.SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);

            configuration.EventListeners.PostUpdateEventListeners = new IPostUpdateEventListener[] {new AuditUpdateListener() };
            configuration.EventListeners.PostInsertEventListeners = new IPostInsertEventListener[] { new AuditUpdateListener() };
            configuration.EventListeners.PostDeleteEventListeners = new IPostDeleteEventListener[] { new AuditUpdateListener() };
            configuration.EventListeners.PostCollectionUpdateEventListeners = new IPostCollectionUpdateEventListener[] { new AuditUpdateListener() };
            return configuration.BuildSessionFactory();
        }

        // TODO: move into imported record class
        private AddEmployeeRequest MapEmployeeRecordToAddEmployeeRequest(ImportedData importedData, int idx)
        {
            var addEmployeeRequest = new AddEmployeeRequest();

            var dictionary = importedData.GetRecordAsDictionary(idx);

            addEmployeeRequest.CompanyId = _clientID;
            addEmployeeRequest.UserId = SystemUserId;


            // Mandatory fields
            addEmployeeRequest.Forename = dictionary["Forename"];
            if (String.IsNullOrEmpty(addEmployeeRequest.Forename))
            {
                throw new Exception("Record " + (idx + 1).ToString() + " - Forename cannot be empty");
            }

            addEmployeeRequest.Surname = dictionary["Surname"];
            if (String.IsNullOrEmpty(addEmployeeRequest.Surname))
            {
                throw new Exception("Record " + (idx + 1).ToString() + " - Surname cannot be empty");
            }

            // Non-mandatory fields
            addEmployeeRequest.EmployeeReference = GetStringValueByKeyReturnNullIfNotFound(dictionary, "EmployeeReference");
            addEmployeeRequest.Title = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Title");
            addEmployeeRequest.Sex = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Gender");
            addEmployeeRequest.DateOfBirth = DateOfBirth(dictionary);
            addEmployeeRequest.NationalityId = GetIntValueByKeyReturnNullIfNotFound(dictionary, "NationalityId");
            addEmployeeRequest.NINumber = GetStringValueByKeyReturnNullIfNotFound(dictionary, "NI Number");
            addEmployeeRequest.DrivingLicenseNumber = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Driving Licence No.");
            addEmployeeRequest.PPSNumber = GetStringValueByKeyReturnNullIfNotFound(dictionary, "PPSNumber");
            addEmployeeRequest.PassportNumber = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Passport No");
            addEmployeeRequest.WorkVisaNumber = GetStringValueByKeyReturnNullIfNotFound(dictionary, "WorkVisaNumber");
            addEmployeeRequest.WorkVisaExpirationDate = GetDateTimeValueByKeyReturnNullIfNotFound(dictionary, "WorkVisaExpiryDate");
            addEmployeeRequest.HasDisability = GetBooleanValueByKeyReturnFalseIfNotFound(dictionary, "Disabled");
            addEmployeeRequest.HasCompanyVehicle = GetBooleanValueByKeyReturnFalseIfNotFound(dictionary, "CompanyCar");
            addEmployeeRequest.Address1 = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Address Line 1");
            addEmployeeRequest.Address2 = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Address Line 2");
            addEmployeeRequest.Address3 = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Address Line 3");
            addEmployeeRequest.Town = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Town");
            addEmployeeRequest.County = GetStringValueByKeyReturnNullIfNotFound(dictionary, "County");
            addEmployeeRequest.Postcode = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Postcode");
            addEmployeeRequest.Telephone = GetStringValueByKeyReturnNullIfNotFound(dictionary,
                                                                                   new string[]
                                                                                                   {
                                                                                                       "Telephone",
                                                                                                       "Home Telephone"
                                                                                                   });
            addEmployeeRequest.Mobile = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Mobile");
            addEmployeeRequest.Mobile = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Mobile No.");
            addEmployeeRequest.Email = GetStringValueByKeyReturnNullIfNotFound(dictionary, "Email");
            addEmployeeRequest.JobTitle = GetStringValueByKeyReturnNullIfNotFound(dictionary,
                                                                                  new string[]
                                                                                                  {
                                                                                                      "Job Title",
                                                                                                      "Job Role"
                                                                                                  });

            return addEmployeeRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="importedData"></param>
        private void WriteToBso( ImportedData importedData)
        {          
            var sessionFactory = GetSessionFactory(_connectionString);
            var session = sessionFactory.OpenSession();

            CurrentSessionContext.Bind(session);

            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    for (int idx = 0; idx < importedData.Records.Count; idx++ )
                    {                        
                        AddEmployeeRequest addEmployeeRequest = MapEmployeeRecordToAddEmployeeRequest(importedData, idx);
                                                                     
                        if (!DoesEmployeeAlreadyExistForThisClient(addEmployeeRequest))
                        {
                           _employeeService.Add(addEmployeeRequest);

                           Console.WriteLine(string.Format("Client {0} import - added employee {1} {2}",
                                                            _CAN, addEmployeeRequest.Forename.ToUpper(), addEmployeeRequest.Surname.ToUpper() ));
                        }
                        else
                        {
                            Console.WriteLine(string.Format("{0} {1} already exists for client {2} and cannot be imported.",
                                                        addEmployeeRequest.Forename.ToUpper(), addEmployeeRequest.Surname.ToUpper(), _CAN ));                          
                        }
                    }
                    session.Flush();
                    transaction.Commit();
                }
                finally
                {
                    sessionFactory.GetCurrentSession().Dispose();
                    CurrentSessionContext.Unbind(sessionFactory);
                }                
            }
        }

        private bool DoesEmployeeAlreadyExistForThisClient(AddEmployeeRequest addEmployeeRequest)
        {
            var employeeAlreadyExistsForThisClient =
                _existingEmployeesList.Count(
                    e =>
                    e.Surname.Equals(addEmployeeRequest.Surname) &&
                    e.Forename.Equals(addEmployeeRequest.Forename)) > 0;
            return employeeAlreadyExistsForThisClient;
        }

        private static DateTime? DateOfBirth(Dictionary<string, string> dictionary)
        {
            DateTime? value = null;
            var keys = new string[] {"DOB", "Date of Birth"};

            keys.ToList()
                .ForEach(key =>
                             {
                                 if (dictionary.ContainsKey(key))
                                 {
                                     value = !String.IsNullOrEmpty(dictionary[key])
                                                 ? DateTime.ParseExact(dictionary[key], "dd/MM/yyyy", null)
                                                 : (DateTime?) null;
                                 }
                             });

            return value;
        }

        
        private static String GetStringValueByKeyReturnNullIfNotFound(Dictionary<string, string> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return !String.IsNullOrEmpty(dictionary[key]) ? dictionary[key] : null;
            }

            return null;
        }

        private static String GetStringValueByKeyReturnNullIfNotFound(Dictionary<string, string> dictionary,
                                                                      string[] keys)
        {
            String value = null;
            keys.ToList()
                .ForEach(key =>
                             {
                                 if (dictionary.ContainsKey(key))
                                 {
                                     value = !String.IsNullOrEmpty(dictionary[key]) ? dictionary[key] : null;
                                 }
                             });

            return value;
        }

        private static Boolean GetBooleanValueByKeyReturnFalseIfNotFound(Dictionary<string, string> dictionary,
                                                                         string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return !String.IsNullOrEmpty(dictionary[key]) && Convert.ToBoolean(dictionary[key]);
            }

            return false;

        }

        private static int? GetIntValueByKeyReturnNullIfNotFound(Dictionary<string, string> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return !String.IsNullOrEmpty(dictionary[key]) ? (int?) Convert.ToInt32(dictionary[key]) : null;
            }

            return null;

        }

        private static DateTime? GetDateTimeValueByKeyReturnNullIfNotFound(Dictionary<string, string> dictionary,
                                                                           string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return !String.IsNullOrEmpty(dictionary[key])
                           ? DateTime.ParseExact(dictionary[key], "dd/MM/yyyy", null)
                           : (DateTime?) null;
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Program program = new Program();

            try
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine(" BSO Employee Import Tool");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("");


                //
                // Parse Database selection
                //
                Console.Write("Dev (D), UAT (U) or Live (L)? :");
                string tmp = Console.ReadLine();
                if (String.IsNullOrEmpty(tmp))
                {
                    Console.WriteLine("Using default database (LIVE)");
                    tmp = "L";
                }

                program.Key = tmp[0];
               
                switch (Char.ToUpper(program.Key))
                {
                    case 'D':
                        Console.WriteLine("Writing to DEVELOPMENT database");
                        break;
                    case 'U':
                        Console.WriteLine("Writing to UAT database");
                        break;
                    case 'L':
                        Console.WriteLine("Writing to LIVE database");
                        break;
                }

                //
                // Get client ID
                //
                Console.WriteLine();
                Console.Write("Enter Client Account Number (CAN): ");
                program.ClientAccountNumber = Console.ReadLine();


                Console.WriteLine("Importing data for client " + program.ClientAccountNumber);
               
                //
                // Import data
                //
                program.Go();
            }           
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
                Console.WriteLine("Press RETURN to exit.");
                Console.Read();
            }
        }
    }
}