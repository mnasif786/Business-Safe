using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Infrastructure.Helpers.Serialization
{
    [CoverageExclude]
    public class XMLObjectSerializer
    {
        public static string Serialize(object obj)
        {
            var settings = new XmlWriterSettings
            {Encoding = Encoding.Unicode, OmitXmlDeclaration = false};
            using (var memoryStream = new MemoryStream())
            {
                var xs = new XmlSerializer(obj.GetType());
                var xmlWriter = XmlWriter.Create(memoryStream, settings);
                xs.Serialize(xmlWriter, obj);
                string xml = Encoding.Unicode.GetString(memoryStream.GetBuffer());
                xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
                xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
                return xml;
            }
        }

        public static object Deserialize(string data, Type objectType)
        {
            var bytes = Encoding.UTF8.GetBytes(data);

            using (var memoryStream = new MemoryStream(bytes))
            {
                memoryStream.Position = 0;
                var xs = new XmlSerializer(objectType);
                var retval = xs.Deserialize(memoryStream);
                return retval;
            }
        }
    }
}