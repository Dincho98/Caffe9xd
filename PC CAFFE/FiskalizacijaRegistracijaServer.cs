using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace PCPOS
{
    internal class FiskalizacijaRegistracijaServer
    {
        public class dllFiskalizacijaRegistracijaServer
        {
            public static DataSet SendDataOnServer(string web_adress, string oib, string tvrtka, string tel, string backup_baze, string blagajna, string ducan, string verzija, string robnoBool, string ukupno)
            {
                DataSet DSpodaci = new DataSet();
                try
                {
                    if (Convert.ToDecimal(ukupno) > 100)
                    {
                        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                        {
                            ASCIIEncoding encoding = new ASCIIEncoding();

                            string postData = "oib=" + oib + "&tvrtka=" + tvrtka + "&tel=" + tel + "&backup=" + backup_baze + "&blagajna=" + blagajna + "&ukupno=" + ukupno + "&ducan=" + ducan + "&verzija=" + verzija + "&robno=" + robnoBool;
                            byte[] data = encoding.GetBytes(postData);

                            WebRequest reguest = WebRequest.Create(web_adress);
                            reguest.Method = "POST";
                            reguest.ContentType = "application/x-www-form-urlencoded";
                            reguest.ContentLength = data.Length;

                            Stream stream = reguest.GetRequestStream();
                            stream.Write(data, 0, data.Length);
                            stream.Close();

                            WebResponse response = reguest.GetResponse();
                            stream = response.GetResponseStream();

                            StreamReader sr = new StreamReader(stream);
                            try
                            {
                                // string a = sr.ReadToEnd();
                                DSpodaci.ReadXml(sr);
                                sr.Close();
                                stream.Close();
                                return DSpodaci;
                            }
                            catch
                            {
                                return DSpodaci;
                            }
                        }
                    }
                    return DSpodaci;
                }
                catch
                {
                    return DSpodaci;
                }
            }
        }
    }
}