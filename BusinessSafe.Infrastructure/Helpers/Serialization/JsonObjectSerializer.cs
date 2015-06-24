using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Infrastructure.Helpers.Serialization
{
    [CoverageExclude]
    public class JsonObjectSerializer 
    {
        public static object Deserialize(string data, Type objectType)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(data)))
            {
                var serializer =
                    new DataContractJsonSerializer(objectType);
                return serializer.ReadObject(ms);
            }
        }

        public static string Serialize(object objectToSerialize)
        {
            using (var ms = new MemoryStream())
            {
                var serializer =
                    new DataContractJsonSerializer(objectToSerialize.GetType());
                serializer.WriteObject(ms, objectToSerialize);
                ms.Position = 0;
                using (var reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
