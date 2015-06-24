using System;
using Peninsula.Online.Data.NHibernate;

namespace BusinessSafe.Data.NHibernate.Helpers
{
    public class NHibernateConfigPathGenerator
    {
        private readonly Database _database;

        public NHibernateConfigPathGenerator(Database database)
        {
            _database = database;
        }

        public string GetConfigFilePath()
        {
            string configFileName = string.Format("hibernate_{0}.cfg.xml",_database.ToString().ToLower());
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            //When hosted in WCF the base directory ends with \ . When hosted in a test app this does not occur
            if (baseDirectory.EndsWith(@"\") == false)
            {
                baseDirectory = baseDirectory + @"\";
            }

            return String.Format(@"{0}{1}", baseDirectory, configFileName);
        }  
    }
}