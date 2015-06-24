using System;
using System.Data;
using System.Data.SqlClient;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Infrastructure.Attributes;
using BusinessSafe.Infrastructure.Helpers.Serialization;

using StructureMap;

namespace BusinessSafe.Infrastructure.Logging
{
    [LogIgnore]
    [CoverageExclude]
    public class SqlPeninsulaLog : PeninsulaLogBase
    {
        private readonly string _connectionString;

        public SqlPeninsulaLog(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override string WriteEvent(LogEvent logEvent)
        {
            Console.WriteLine(logEvent.Message);

            var cn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("INSERT INTO LogEvent " +
                "([Date], [Level], [Message], [User], [Exception], [Objects], " +
                    "[ExecutingMachine], [CallingAssembly], [CallingClass], [CallingMethod], " +
                        "[ContextGuid]) VALUES (" +
                            "@Date, @Level, @Message, @User, @Exception, @Objects, " +
                                "@ExecutingMachine, @CallingAssembly, @CallingClass, @CallingMethod, " +
                                    "@ContextGuid); SELECT SCOPE_IDENTITY() AS LogEventId; ", cn);

            cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = logEvent.Date;

            cmd.Parameters.Add("@Level", SqlDbType.Int).Value = logEvent.Level;

            cmd.Parameters.Add("@Message", SqlDbType.VarChar, 100).Value = SetObjectToNull(logEvent.Message);

            cmd.Parameters.Add("@User", SqlDbType.VarChar, 50).Value = SetObjectToNull(logEvent.User);

            cmd.Parameters.Add("@Exception", SqlDbType.VarChar, 2147483647);
            if (logEvent.Exception != null)
            {
                cmd.Parameters["@Exception"].Value = logEvent.Exception.ToString();
            }
            else
            {
                cmd.Parameters["@Exception"].Value = DBNull.Value;
            }

            cmd.Parameters.Add("@Objects", SqlDbType.Xml);
            if (logEvent.Objects != null)
            {
                cmd.Parameters["@Objects"].Value = DataContractObjectSerializer.Serialize(logEvent.Objects);
            }
            else
            {
                cmd.Parameters["@Objects"].Value = DBNull.Value;
            }

            cmd.Parameters.Add("@ExecutingMachine", SqlDbType.VarChar, 50).Value =
                SetObjectToNull(logEvent.ExecutingMachine);

            cmd.Parameters.Add("@CallingAssembly", SqlDbType.VarChar, 50).Value =
                SetObjectToNull(logEvent.CallingAssembly);

            cmd.Parameters.Add("@CallingClass", SqlDbType.VarChar, 50).Value = SetObjectToNull(logEvent.CallingClass);

            cmd.Parameters.Add("@CallingMethod", SqlDbType.VarChar, 50).Value = SetObjectToNull(logEvent.CallingMethod);

            cmd.Parameters.Add("@ContextGuid", SqlDbType.VarChar, 50).Value =
                SetObjectToNull(logEvent.ContextGuid.ToString());

            string logEventId = null;
            try
            {
                cn.Open();
                var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    logEventId = dr["LogEventId"].ToString();
                }
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
            }

            return logEventId;
        }

        private static object SetObjectToNull(object value)
        {
            if (value != null)
            {
                return value;
            }
            return DBNull.Value;
        }
    }
}