using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PCPOS.Until
{
    internal class classDownloadFiles
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

                Stream strRemote = client.OpenRead(url);

                FileStream strLocal = new FileStream(sFileSavePath, FileMode.Create, FileAccess.Write, FileShare.None);

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

                strRemote.Close();
            }
            catch (Exception exM)
            {
                MessageBox.Show("Error: " + exM.Message);
            }
        }
    }
}