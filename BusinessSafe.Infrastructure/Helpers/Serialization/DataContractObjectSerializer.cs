using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Infrastructure.Helpers.Serialization
{
    [CoverageExclude]
    public class DataContractObjectSerializer
    {
        public static object Deserialize(string data, Type objectType)
        {
            //Needs completing.
            return null;
        }

        public static string Serialize(object obj)
        {
            string returnValue;
            var dataContractSerializer = new DataContractSerializer(obj.GetType());
            using (var backing = new StringWriter())
            {
                using (var writer = new XmlTextWriter(backing))
                {
                    dataContractSerializer.WriteObject(writer, obj);
                    returnValue = backing.ToString();
                }
            }
            return returnValue;
        }
    }
}