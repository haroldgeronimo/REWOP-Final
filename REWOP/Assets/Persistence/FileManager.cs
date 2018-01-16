using System.IO;
using System.Xml.Serialization;
namespace Rewop.Systems
{
    public static class FileManager
    {
        //Xml Serialization
        public static string XmlSerialize<T>(this T toSerialize)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();
            xml.Serialize(writer, toSerialize);
            return writer.ToString();
        }
        //Xml Deserialization
        public static T XmlDeserialize<T>(this string toDeserialize)
        {

            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(toDeserialize);
            return (T)xml.Deserialize(reader);
        }
        //WritingFileToPath
        public static void WriteFileToPath(string fileName, string path, string data)
        {
            File.WriteAllText(path + "/" + fileName, data);
        }
        //ReadingFileFromPath
        public static string ReadFileFromPath(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return "";
        }

        public static bool IsExist(string path)
        {
            return (File.Exists(path));
        }
    }
}