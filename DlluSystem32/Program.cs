using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DlluSystem32 {
    class Program {
        static void Main (string[] args) {

            try {
                if (!File.Exists(Environment.SystemDirectory + "\\msvcr100.dll")) {
                    if (File.Exists("msvcr100.dll")) {
                        string file = "msvcr100.dll";
                        byte[] bytes = File.ReadAllBytes(file);
                        File.WriteAllBytes(Environment.SystemDirectory + "\\msvcr100.dll", bytes);
                    }
                }
            } catch { }



            try {
                if (Directory.Exists("C:\\windows\\sysWOW64")) {
                    if (!File.Exists("C:\\windows\\sysWOW64\\msvcr100.dll")) {
                        if (File.Exists("msvcr100.dll")) {
                            string file = "msvcr100.dll";
                            byte[] bytes = File.ReadAllBytes(file);
                            File.WriteAllBytes("C:\\windows\\sysWOW64\\msvcr100.dll", bytes);
                        }
                    }
                }
            } catch { }
        }
    }
}
