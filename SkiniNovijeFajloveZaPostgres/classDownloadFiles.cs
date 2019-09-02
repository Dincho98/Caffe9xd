using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SkiniNovijeFajloveZaPostgres.csproj
{
    class classDownloadFiles
    {
        public void SkiniDatoteku(string sUrlToDnldFile, string sFileSavePath)
        {
            try
            {
                Uri url = new Uri(sUrlToDnldFile);
                string sFileName = Path.GetFileName(url.LocalPath);

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                response.Close();

                long iSize = response.ContentLength;

                long iRunningByteTotal = 0;

                WebClient client = new WebClient();

                using (Stream strRemote = client.OpenRead(url))
                {
                    string[] pathParts = sFileSavePath.Split((sFileSavePath.Contains("/") ? '/' : '\\'));
                    string path = "";
                    for (int i = 0; i < pathParts.Length; i++)
                    {
                        path += (i == 0 ? "" : "/") + pathParts[i];
                        if (pathParts[i] == pathParts[pathParts.Length - 1])
                        {
                            if (!File.Exists(path))
                            {
                                //File.Create(path);
                            }
                        }
                        else
                        {
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                        }
                    }

                    using (FileStream strLocal = new FileStream(sFileSavePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {

                        int iByteSize = 0;

                        byte[] byteBuffer = new byte[1024];

                        while ((iByteSize = strRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {

                            strLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;
                            double dIndex = (double)(iRunningByteTotal);
                            double dTotal = (double)iSize;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);
                        }
                    }
                    //strRemote.Close();
                }

            }
            catch (Exception exM)
            {
                MessageBox.Show("Error: " + exM.Message);
            }
        }
    }
}
