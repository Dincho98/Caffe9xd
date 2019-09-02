using System.Xml;

namespace RESORT
{
    internal class weather
    {
        public static string[] SetRemoteFields(string ime_grada, XmlDocument doc)
        {
            string[] DataReturn = new string[6];

            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/Hrvatska/Grad/GradIme");

            foreach (XmlNode node in nodes)
            {
                if (node.InnerText == ime_grada)
                {
                    DataReturn[0] = node.NextSibling["Temp"].InnerText;
                    DataReturn[1] = node.NextSibling["Vlaga"].InnerText;
                    DataReturn[2] = node.NextSibling["Tlak"].InnerText;
                    DataReturn[3] = node.NextSibling["VjetarSmjer"].InnerText;
                    DataReturn[4] = node.NextSibling["VjetarBrzina"].InnerText;
                    DataReturn[5] = node.NextSibling["Vrijeme"].InnerText;
                }
            }

            return DataReturn;
        }
    }
}