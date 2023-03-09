using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ClientViTrader.Utils
{
    public static class Util
    {
        public static string Serialize<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public static T Deserialize<T>(string text)
        {
            using (TextReader reader = new StringReader(text))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        public static string GetAuthHeaderValue(string username, string password)
        {
            string usernamePassword = username + ":" + password;
            var bytes = Encoding.UTF8.GetBytes(usernamePassword);

            return "Basic " + Convert.ToBase64String(bytes);
        }
    }
}
