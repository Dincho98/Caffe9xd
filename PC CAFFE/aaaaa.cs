using Raverus.FiskalizacijaDEV;
using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;

namespace PCPOS
{
    public partial class aaaaa : Form
    {
        public aaaaa()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Class.Postavke.backGround;
        }

        private void aaaaa_Load(object sender, EventArgs e)
        {
            Fiskalizacija("47165970760", "74119445654");
        }

        private DataTable DTfis = classSQL.select_settings("SELECT oznaka_slijednosti FROM fiskalizacija", "fiskalizacija").Tables[0];

        private void Fiskalizacija(string oib, string oib_operatera)
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                             @"<tns:RacunZahtjev xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" Id=""signXmlId"" xmlns:tns=""http://www.apis-it.hr/fin/2012/types/f73"">" +

                             @"<tns:Zaglavlje>" +
                             @"<tns:IdPoruke>cff93023-850b-403c-ac8b-277619e81dc9</tns:IdPoruke>" +
                             @"<tns:DatumVrijeme>" + DateTime.Now.ToString("dd.MM.yyyyThh:mm:ss") + "</tns:DatumVrijeme>" +
                             @"</tns:Zaglavlje>" +

                             @"<tns:Racun>" +
                             @"<tns:Oib>" + oib + "</tns:Oib>" +
                             @"<tns:USustPdv>true</tns:USustPdv>" +
                             @"<tns:DatVrijeme>09.11.2012T12:25:28</tns:DatVrijeme>" +
                             @"<tns:OznSlijed>" + DTfis.Rows[0]["oznaka_slijednosti"].ToString() + "</tns:OznSlijed>" +

                             @"<tns:BrRac>" +
                             @"<tns:BrOznRac>1</tns:BrOznRac>" +
                             @"<tns:OznPosPr>123</tns:OznPosPr>" +
                             @"<tns:OznNapUr>1</tns:OznNapUr>" +
                             @"</tns:BrRac>" +

                             @"<tns:OstaliPor>" +
                             @"<tns:Porez>" +
                             @"<tns:Naziv>Porez na luksuz</tns:Naziv>" +
                             @"<tns:Stopa>15.00</tns:Stopa>" +
                             @"<tns:Osnovica>10.00</tns:Osnovica>" +
                             @"<tns:Iznos>1.50</tns:Iznos>" +
                             @"</tns:Porez>" +
                             @"</tns:OstaliPor>" +
                             @"<tns:IznosOslobPdv>12.00</tns:IznosOslobPdv>" +
                             @"<tns:IznosMarza>13.00</tns:IznosMarza>" +
                             @"<tns:Naknade>" +
                             @"<tns:Naknada>" +
                             @"<tns:NazivN>Povratna naknada</tns:NazivN>" +
                             @"<tns:IznosN>1.00</tns:IznosN>" +
                             @"</tns:Naknada>" +
                             @"</tns:Naknade>" +

                             @"<tns:IznosUkupno>12.50</tns:IznosUkupno>" +
                             @"<tns:NacinPlac>G</tns:NacinPlac>" +
                             @"<tns:OibOper>" + oib_operatera + "</tns:OibOper>" +
                             @"<tns:ZastKod>e4d909c290d0fb1ca068ffaddf22cbd0</tns:ZastKod>" +
                             @"<tns:NakDost>false</tns:NakDost>" +
                             @"</tns:Racun>" +

                             @"</tns:RacunZahtjev>";

            XmlDocument dokument = new XmlDocument();
            dokument.LoadXml(xml);

            Raverus.FiskalizacijaDEV.CentralniInformacijskiSustav cis = new CentralniInformacijskiSustav();

            X509Certificate2 certifikat = Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.DohvatiCertifikat("FISKAL 1");
            Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.PotpisiXmlDokument(dokument, certifikat);

            Raverus.FiskalizacijaDEV.PopratneFunkcije.XmlDokumenti.DodajSoapEnvelope(ref dokument);

            try
            {
                XmlDocument odgovor = cis.PosaljiSoapPoruku(dokument);
                if (odgovor != null)
                {
                    string jir = Raverus.FiskalizacijaDEV.PopratneFunkcije.XmlDokumenti.DohvatiJir(odgovor);
                    MessageBox.Show(jir);
                }
            }
            catch (Exception ex)
            {
                if (cis.OdgovorGreska != null)
                {
                    MessageBox.Show(cis.OdgovorGreska.InnerXml);
                }
                else
                    MessageBox.Show(String.Format("Greska: {0}", ex.Message));
            }
        }

        private void Fiskalizacija1()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                         @"<tns:RacunZahtjev xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" Id=""signXmlId"" xmlns:tns=""http://www.apis-it.hr/fin/2012/types/f73"">" +
                         @"<tns:Zaglavlje>" +
                         @"<tns:IdPoruke>cff93023-850b-403c-ac8b-277619e81dc9</tns:IdPoruke>" +
                         @"<tns:DatumVrijeme>21.10.2012T10:10:28</tns:DatumVrijeme>" +
                         @"</tns:Zaglavlje>" +
                         @"<tns:Racun>" +
                         @"<tns:Oib>47165970760</tns:Oib>" +
                         @"<tns:USustPdv>true</tns:USustPdv>" +
                         @"<tns:DatVrijeme>21.10.2012T10:10:22</tns:DatVrijeme>" +
                         @"<tns:OznSlijed>P</tns:OznSlijed>" +
                         @"<tns:BrRac><tns:BrOznRac>1</tns:BrOznRac>" +
                         @"<tns:OznPosPr>123</tns:OznPosPr>" +
                         @"<tns:OznNapUr>1</tns:OznNapUr>" +
                         @"</tns:BrRac>" +
                         @"<tns:Pdv>" +
                         @"<tns:Porez>" +
                         @"<tns:Stopa>25.00</tns:Stopa>" +
                         @"<tns:Osnovica>10.00</tns:Osnovica>" +
                         @"<tns:Iznos>2.50</tns:Iznos>" +
                         @"</tns:Porez>" +
                         @"</tns:Pdv>" +
                         @"<tns:IznosUkupno>12.50</tns:IznosUkupno>" +
                         @"<tns:NacinPlac>G</tns:NacinPlac>" +
                         @"<tns:OibOper>47165970760</tns:OibOper>" +
                         @"<tns:ZastKod>e4d909c290d0fb1ca068ffaddf22cbd0</tns:ZastKod>" +
                         @"<tns:NakDost>false</tns:NakDost>" +
                         @"</tns:Racun>" +
                         @"</tns:RacunZahtjev>";

            XmlDocument dokument = new XmlDocument();
            dokument.LoadXml(xml);

            Raverus.FiskalizacijaDEV.CentralniInformacijskiSustav cis = new CentralniInformacijskiSustav();

            X509Certificate2 certifikat = Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.DohvatiCertifikat("FISKAL 1");
            Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.PotpisiXmlDokument(dokument, certifikat);

            Raverus.FiskalizacijaDEV.PopratneFunkcije.XmlDokumenti.DodajSoapEnvelope(ref dokument);

            XmlDocument odgovor = cis.PosaljiSoapPoruku(dokument);
        }

        private void Fiskalizacija2()
        {
            Raverus.FiskalizacijaDEV.CentralniInformacijskiSustav cis = new CentralniInformacijskiSustav();

            Raverus.FiskalizacijaDEV.Schema.RacunType racun = new Raverus.FiskalizacijaDEV.Schema.RacunType();
            racun.Oib = "47165970760";
            racun.USustPdv = true;
            racun.DatVrijeme = Raverus.FiskalizacijaDEV.PopratneFunkcije.Razno.DohvatiFormatiranoTrenutnoDatumVrijeme();
            racun.OznSlijed = Raverus.FiskalizacijaDEV.Schema.OznakaSlijednostiType.P;

            Raverus.FiskalizacijaDEV.Schema.BrojRacunaType broj = new Raverus.FiskalizacijaDEV.Schema.BrojRacunaType();
            broj.BrOznRac = "1";
            broj.OznPosPr = "123";
            broj.OznNapUr = "1";
            racun.BrRac = broj;

            Raverus.FiskalizacijaDEV.Schema.PorezType porez25 = new Raverus.FiskalizacijaDEV.Schema.PorezType();
            porez25.Stopa = "25.00";
            porez25.Osnovica = "10.00";
            porez25.Iznos = "2.50";

            Raverus.FiskalizacijaDEV.Schema.PorezType porez0 = new Raverus.FiskalizacijaDEV.Schema.PorezType();
            porez0.Stopa = "0.00";
            porez0.Osnovica = "10.00";
            porez0.Iznos = "0.00";

            racun.Pdv.Add(porez25);
            racun.Pdv.Add(porez0);

            racun.IznosUkupno = "22.50";
            racun.NacinPlac = Raverus.FiskalizacijaDEV.Schema.NacinPlacanjaType.G;
            racun.OibOper = "47165970760";
            racun.ZastKod = "e4d909c290d0fb1ca068ffaddf22cbd0";
            racun.NakDost = false;

            XmlDocument doc = cis.PosaljiRacun(racun, "FISKAL 1");
            doc.Save("D:/ddd.xml");
        }
    }
}