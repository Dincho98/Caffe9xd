using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace PCPOS.Sinkronizacija
{
    internal class pomagala_syn
    {
        public string MyWebRequest(string arrPOST, string url)
        {
            // Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = arrPOST;
            byte[] byteArray = Encoding.GetEncoding("windows-1250").GetBytes(postData + KreirajSigurnuPoruku());
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            string statusDescription = ((HttpWebResponse)response).StatusDescription.ToString();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return statusDescription + ";" + responseFromServer;
        }

        public DataTable MyWebRequestXML(string arrPOST, string url)
        {
            try
            {
                try
                {
                    if (arrPOST.Length > 0)
                    {
                        string[] arrGodina = arrPOST.Split('&');
                        if (arrGodina.Length <= 1)
                            return new DataTable();

                        string godina = arrGodina[arrGodina.Length - 1];
                        if (godina == "godina=0")
                            return new DataTable();
                    }
                }
                catch
                {
                    return new DataTable();//[1].Split('=')[1]
                }

                Encoding encoding = Encoding.GetEncoding("windows-1250");

                byte[] data = encoding.GetBytes(arrPOST + KreirajSigurnuPoruku());

                WebRequest reguest = WebRequest.Create(url);

                reguest.Timeout = 90000;
                reguest.Method = "POST";
                reguest.ContentType = "application/x-www-form-urlencoded";
                reguest.ContentLength = data.Length;

                Stream stream = reguest.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                WebResponse response = reguest.GetResponse();
                stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("windows-1250"));
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                sr.Close();
                stream.Close();

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return new DataTable();
            }
            catch
            {
                return new DataTable();
            }
        }

        //ova metoda sluzi za kreiranje sigurnosne poruke koja se uspoređuje na web-u
        private string KreirajSigurnuPoruku()
        {
            decimal key = 123654789;
            decimal nekiBroj = 5463287658246;
            decimal br = 0;
            decimal izracun = 0;
            string dat = DateTime.Now.ToString("yyyyMMddHmmss");
            decimal.TryParse(dat, out br);
            izracun = br + nekiBroj;
            izracun = izracun / key;
            return "&izracun=" + Math.Round(izracun, 5) + "&datum=" + dat;
        }
    }
}