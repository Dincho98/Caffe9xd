using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS
{
    internal class variable
    {
        public static string modul = GetProgram();

        public static string GetProgram()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("program").Elements("run_progam") select c;
            foreach (XElement book in query)
            {
                return book.Attribute("run").Value;
            }

            return "";
        }
    }
}