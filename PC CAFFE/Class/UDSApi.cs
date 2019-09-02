using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PCPOS.Class
{
    public class UDS
    {
        private string ApiKey = "";

        public UDS(string apiKey = null)
        {
            if (apiKey != null)
            {
                ApiKey = apiKey;
            }
        }

        public decimal[] getScoreToSubstractFromCustomerAccount(decimal ukupnoBezRabata, decimal maxScoresDiscount)
        {
            decimal[] rez = new decimal[2];
            rez[0] = 0;
            rez[1] = 0;
            var input = Microsoft.VisualBasic.Interaction.InputBox("Score to substract from customer account.");

            if (decimal.TryParse(input.ToString(), out rez[1]))
            {
                rez[0] = 1;

                if (rez[1] > Math.Round((ukupnoBezRabata * maxScoresDiscount / 100), 2, MidpointRounding.AwayFromZero))
                {
                    MessageBox.Show("Upisan prevelik popust.");
                }
            }

            return rez;
        }

        private string getCurentDateTimeZone()
        {
            return DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss,fffzzzz");
        }

        private string getNewUUID()
        {
            return Guid.NewGuid().ToString();
        }

        public PartnerCompanyInfoType getCompany()
        {
            HttpWebRequest httpWebRequest;
            WebResponse webResponse;
            Stream stream;
            StreamReader streamReader;

            try
            {
                string url = "https://udsgame.com/v1/partner/company";

                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Headers["Accept-Charset"] = "utf-8";
                httpWebRequest.ContentType = "application/json";

                httpWebRequest.Headers["X-Api-Key"] = ApiKey;
                httpWebRequest.Headers["X-Origin-Request-Id"] = getNewUUID();
                httpWebRequest.Headers["X-Timestamp"] = getCurentDateTimeZone();
                httpWebRequest.Timeout = 90000;
                httpWebRequest.Method = "GET";

                webResponse = httpWebRequest.GetResponse();
                stream = webResponse.GetResponseStream();
                streamReader = new StreamReader(stream);

                var result = streamReader.ReadToEnd();

                return JsonConvert.DeserializeObject<PartnerCompanyInfoType>(result);
            }
            catch (WebException we)
            {
                errors(((HttpWebResponse)we.Response).StatusCode);

                return null;
            }
            finally
            {
                streamReader = null;
                stream = null;
                webResponse = null;
                httpWebRequest = null;
            }
        }

        public CustomerInfoType getCustomer(int customerCode = 0)
        {
            HttpWebRequest httpWebRequest;
            WebResponse webResponse;
            Stream stream;
            StreamReader streamReader;

            try
            {
                string url = string.Format("https://udsgame.com/v1/partner/customer?code={0}", customerCode);

                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Headers["Accept-Charset"] = "utf-8";
                httpWebRequest.ContentType = "application/json";

                httpWebRequest.Headers["X-Api-Key"] = ApiKey;
                httpWebRequest.Headers["X-Origin-Request-Id"] = getNewUUID();
                httpWebRequest.Headers["X-Timestamp"] = getCurentDateTimeZone();
                httpWebRequest.Timeout = 90000;
                httpWebRequest.Method = "GET";

                webResponse = httpWebRequest.GetResponse();
                stream = webResponse.GetResponseStream();
                streamReader = new StreamReader(stream);

                var result = streamReader.ReadToEnd();

                return JsonConvert.DeserializeObject<CustomerInfoType>(result);
            }
            catch (WebException we)
            {
                errors(((HttpWebResponse)we.Response).StatusCode);

                return null;
            }
            finally
            {
                streamReader = null;
                stream = null;
                webResponse = null;
                httpWebRequest = null;
            }
        }

        public PurchaseResponseType postPurchase(PartnerCompanyInfoType company, int CODE, ulong? participantId = 0, string cashierExternalId = "", string invoiceNumber = "", decimal totalOrderCost = 0, decimal scoresToSubstractFromCustomerAccount = 0)
        {
            HttpWebRequest httpWebRequest;
            HttpWebResponse httpWebResponse;
            Stream stream;
            StreamReader streamReader;

            PurchaseRequestType purchaseRequest;

            bool APPLY_DISCOUNT = (company.baseDiscountPolicy == "APPLY_DISCOUNT" ? true : false);
            decimal orderCostIncludingBaseDiscountAndSubstractedScores = 0;
            decimal companyDiscountBase = company.marketingSettings.discountBase;
            if (APPLY_DISCOUNT)
            {
                scoresToSubstractFromCustomerAccount = 0;
                orderCostIncludingBaseDiscountAndSubstractedScores = totalOrderCost - Math.Round((totalOrderCost * companyDiscountBase / 100), 2, MidpointRounding.AwayFromZero);
                orderCostIncludingBaseDiscountAndSubstractedScores = Math.Round(orderCostIncludingBaseDiscountAndSubstractedScores, 2, MidpointRounding.AwayFromZero);
                //orderCostIncludingBaseDiscountAndSubstractedScores = orderCostIncludingBaseDiscountAndSubstractedScores - scoresToSubstractFromCustomerAccount;
                //orderCostIncludingBaseDiscountAndSubstractedScores = Math.Round(orderCostIncludingBaseDiscountAndSubstractedScores, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                orderCostIncludingBaseDiscountAndSubstractedScores = totalOrderCost - scoresToSubstractFromCustomerAccount;
                orderCostIncludingBaseDiscountAndSubstractedScores = Math.Round(orderCostIncludingBaseDiscountAndSubstractedScores, 2, MidpointRounding.AwayFromZero);
            }

            purchaseRequest = new PurchaseRequestType();
            purchaseRequest.code = CODE.ToString();
            purchaseRequest.customerId = participantId;
            purchaseRequest.cashierExternalId = cashierExternalId;
            purchaseRequest.total = totalOrderCost;
            purchaseRequest.scores = scoresToSubstractFromCustomerAccount;
            purchaseRequest.cash = orderCostIncludingBaseDiscountAndSubstractedScores;
            purchaseRequest.invoiceNumber = invoiceNumber;
            purchaseRequest.phone = "";

            try
            {
                string url = "https://udsgame.com/v1/partner/purchase";

                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Headers["Accept-Charset"] = "utf-8";
                httpWebRequest.ContentType = "application/json";

                httpWebRequest.Headers["X-Api-Key"] = ApiKey;
                string uuid = getNewUUID();
                string datum = getCurentDateTimeZone();
                httpWebRequest.Headers["X-Origin-Request-Id"] = uuid;
                httpWebRequest.Headers["X-Timestamp"] = datum;
                httpWebRequest.Timeout = 90000;
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var bodyData = JsonConvert.SerializeObject(purchaseRequest);

                    streamWriter.Write(bodyData);
                }

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                stream = httpWebResponse.GetResponseStream();
                streamReader = new StreamReader(stream);

                var result = streamReader.ReadToEnd();

                return JsonConvert.DeserializeObject<PurchaseResponseType>(result);
            }
            catch (WebException we)
            {
                errors(((HttpWebResponse)we.Response).StatusCode);

                return null;
            }
            finally
            {
                streamReader = null;
                stream = null;
                httpWebResponse = null;
                httpWebRequest = null;
            }
        }

        public void errors(HttpStatusCode statusCode)
        {
            int iStatusCode = (int)statusCode;

            string apiName = "UDS Game";

            if (iStatusCode == 200)
                return;

            switch (iStatusCode)
            {
                case 400:
                    MessageBox.Show(string.Format("{0}: Dogodila se pogreška provjere valjanosti ili pogreška poslovne logike.", apiName));
                    break;

                case 403:
                    //Forbiden
                    MessageBox.Show(string.Format("{0}: Tvrtka je neaktivna. Pretplata na tvrtku je istekla ili moderator blokira tvrtku.", apiName));
                    break;

                case 404:
                    MessageBox.Show(string.Format("{0}: Traženi entitet ili traženi put api-metode ne postoji.", apiName));
                    break;

                default:
                    MessageBox.Show(string.Format("{0}:  Nepoznata greška.", apiName));
                    break;
            }
        }
    }

    public class PartnerCompanyInfoType
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public string promoCode { get; set; }
        public MarketingSettingsType marketingSettings { get; set; }
        public string currency { get; set; }
        public string baseDiscountPolicy { get; set; }
    }

    public class MarketingSettingsType
    {
        public decimal discountBase { get; set; }
        public decimal discountLevel1 { get; set; }
        public decimal discountLevel2 { get; set; }
        public decimal discountLevel3 { get; set; }
        public decimal maxScoresDiscount { get; set; }
    }

    public class CustomerInfoType
    {
        public ulong id { get; set; }
        public ulong? participantId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string dateCreated { get; set; }
        public string skype { get; set; }
        public string instagram { get; set; }
        public bool participant { get; set; }
        public decimal scores { get; set; }
        public bool vip { get; set; }
        public decimal discountRate { get; set; }
        public string birthday { get; set; }
    }

    public class PurchaseResponseType
    {
        public OperationInfoType operation { get; set; }
    }

    public class OperationInfoType
    {
        public ulong id { get; set; }
        public string dateCreated { get; set; }
        public decimal scoresDelta { get; set; }
        public decimal cash { get; set; }
        public decimal total { get; set; }
        public MarketingSettingsType marketingSettings { get; set; }
        public ulong participantId { get; set; }
        public OperationCustomerType customer { get; set; }
        public OperationCashierType cashier { get; set; }
    }

    public class OperationCustomerType
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }

    public class OperationCashierType
    {
        public ulong externalId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class PurchaseRequestType
    {
        public ulong? customerId { get; set; }
        public string code { get; set; }
        public decimal total { get; set; }
        public decimal scores { get; set; }
        public decimal cash { get; set; }
        public string invoiceNumber { get; set; }
        public string cashierExternalId { get; set; }
        public string phone { get; set; }

        public PurchaseRequestType()
        {
            customerId = 0;
            code = "";
            total = 0;
            scores = 0;
            cash = 0;
            invoiceNumber = "";
            cashierExternalId = "";
            phone = "";
        }
    }
}