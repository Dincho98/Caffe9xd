using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Until
{
    public class classFukcijeZaUpravljanjeBazom
    {
        private string query;
        private string program;
        private string nastavak_na_bazu;
        private string nova_baza;
        private string postojeca_baza;

        /// <summary>
        /// VARIJABLE KOJE TREBA OBAVEZNO POPUNITI SU "program" i nastavak_na_bazu
        ///
        /// VARIJABLA nastavak_na_bazu: npr ako se baza zove DB2014 varijabla se šalje samo sa "DB"
        /// VARIJABLA nastavak_na_bazu: npr ako se baza zove POS2014 varijabla se šalje samo sa "POS"
        ///
        /// VARIJABLA "program" treba dobiti vrijednost sa "CAFFE","MALOPRODAJA","RESORT"
        /// </summary>
        public classFukcijeZaUpravljanjeBazom(string _program, string _nastavak_na_bazu)
        {
            program = _program;
            nastavak_na_bazu = _nastavak_na_bazu;
        }

        ~classFukcijeZaUpravljanjeBazom()
        {
            query = null;
            program = null;
            nastavak_na_bazu = null;
        }

        #region KreirajNovuGodinu(): Funkcija koja kreira novu godinu

        public string KreirajNovuGodinu()
        {
            BackupSvihBaza();
            MessageBox.Show("VAŽNO!!!\r\nPRIČEKAJTE DA PROGRAM DOVRŠI BACKUP SVIH BAZA PODATAKA I DA SE ZATVORE SVI PROZORI COMMAND PROMPT!", "VAŽNO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            PCPOS.Until.frmPotvrdaZaNovuGodinu ng = new PCPOS.Until.frmPotvrdaZaNovuGodinu();
            ng.frmNG = this;
            ng.ShowDialog();

            if (GodinaPostoji(nova_baza))
            {
                return "GREŠKA!!!\r\nUpisana baza već postoji!";
            }

            if (nova_baza.ToUpper().Trim() == postojeca_baza.ToUpper().Trim())
            {
                MessageBox.Show("GREŠKA\r\nKrivo upisana nova baza podataka!", "GREŠKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "GREŠKA\r\nKrivo upisana nova baza podataka!";
            }

            ///PROVJERA DALI SU BAZE DOBRO UPISANE AKO JESU KREIRA SE NOVA BAZA
            if (nova_baza == "" || postojeca_baza == "" || !GodinaPostoji(postojeca_baza))
            {
                return "GREŠKA!!!\r\nKrivo su postavljene baze podataka!";
            }
            else
            {
                //JOŠ JEDNOM PROVJERAVAM DALI JE KONEKCIJA PREKINUTA
                if (classSQL.remoteConnection.State.ToString() != "Closed") { classSQL.remoteConnection.Close(); }
                //OVA FUNKCIJA ODJAVLJUJE SVE KORISNIKE
                OdspojiSveUsere();

                try
                {
                    if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }
                    NpgsqlCommand comm = new NpgsqlCommand("CREATE DATABASE " + nova_baza + " TEMPLATE = " + postojeca_baza + ";", classSQL.remoteConnection);
                    comm.CommandTimeout = 500000;
                    comm.ExecuteNonQuery();
                    classSQL.remoteConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return "";
                }

                ObrisiSvePodatkeIzZadanihTablica();
                return "";
            }
        }

        #endregion KreirajNovuGodinu(): Funkcija koja kreira novu godinu

        #region OdspojiSveUsere()

        private void OdspojiSveUsere()
        {
            string sql;

            try
            {
                DataTable DTpid = classSQL.select("SELECT * FROM pg_stat_activity;", "pid").Tables[0];
                if (DTpid.Rows.Count > 0)
                {
                    if (DTpid.Columns["pid"] != null)
                    {
                        sql = "SELECT pg_terminate_backend(pg_stat_activity.pid)" +
                        " FROM pg_stat_activity" +
                        " WHERE pid <> pg_backend_pid();";
                        classSQL.insert(sql);
                    }
                    else
                    {
                        sql = "SELECT pg_terminate_backend(pg_stat_activity.procpid)" +
                        " FROM pg_stat_activity" +
                        " WHERE procpid <> pg_backend_pid();";
                        classSQL.insert(sql);
                    }
                }
                else
                {
                    MessageBox.Show("Greška, nisu uklonjeni svi useri i baze");
                }
            }
            catch
            {
            }
        }

        #endregion OdspojiSveUsere()

        #region ProvjeriDaliTrebaKreiratiNovuGodinu(): Funkcija provjerava dali treba kreirati godinu i upozorava korisnika da se javi u Code-iT

        /// <summary>
        /// Funkcija provjerava dali treba kreirati godinu i upozorava korisnika
        /// </summary>
        public void ProvjeriDaliTrebaKreiratiNovuGodinu()
        {
            int trenutna_godina = UzmiTrenutnuGodinu();
            int godina_koja_se_koristi_u_bazi = UzmiGodinuKojaSeKoristi();

            if (godina_koja_se_koristi_u_bazi == 0)
            {
                MessageBox.Show("UPOZORENJE!!!\r\nBazu podataka koju koristite nije pravilno nazvana. " +
                    "Baza podataka morala bi biti nazvana '" + nastavak_na_bazu + DateTime.Now.Year + "'.\r\n" +
                    "Za više informacija nazovite Code-iT.", "UPOZORENJE!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!GodinaPostoji(nastavak_na_bazu + trenutna_godina.ToString()))
            {
                //OVAJ IF SLUŽI DA PROVJERI DALI JE VEC KREIRANA GODINA U BAZI
                MessageBox.Show("UPOZORENJE!!!\r\nNemate kreiranu bazu na tekuću godinu!\r\n" +
                    "Zakonom od 01.01.2013 obavezni ste prvi račun u novoj godini započeti od broja 1, " +
                    "arhivirati prethodne godine, \r\n" +
                    "a ako koristite robno izraditi početno stanje prema zadnjoj inventuri.\r\n" +
                    "Za više informacija kontaktirajte tvrtku Code-iT.",
                    "UPOZORENJE", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else if (godina_koja_se_koristi_u_bazi != DateTime.Now.Year)
            {
                MessageBox.Show("UPOZORENJE!!!\r\nGodina koju koristite u programu različita je od tekuče godine.\r\n" +
                    "U godini koju trenutno koristite ne smijete raditi račune, fakrure, niti bilo koje druge dokumente.\r\n" +
                    "Za promjenu godine na programu trebate slijediti putanju:\r\n" +
                    "Prijavite se kao administrator->na lijevoj strani programa imate promijeni godinu.\r\n" +
                    "Za više informacija nazovite Code-iT.", "UPOZORENJE!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion ProvjeriDaliTrebaKreiratiNovuGodinu(): Funkcija provjerava dali treba kreirati godinu i upozorava korisnika da se javi u Code-iT

        #region UzmiTrenutnuGodinu(): Funkcija samo uzima trenutnu godinu

        /// <summary>
        /// FUNKCIJA uzima trenutnu godinu i vrača kao integer
        /// npr: 2014
        /// </summary>
        /// <returns></returns>
        private int UzmiTrenutnuGodinu()
        {
            return Convert.ToInt16(DateTime.Now.Year);
        }

        #endregion UzmiTrenutnuGodinu(): Funkcija samo uzima trenutnu godinu

        #region UzmiGodinuKojaSeKoristi(): FUNKCIJA vraca integer kao godinu koja se trenutno korisi u remoteConnectionString-u

        /// <summary>
        /// FUNKCIJA vraca integer kao godinu koja se trenutno korisi u remoteConnectionString-u
        /// npr: 2013
        /// </summary>
        /// <returns></returns>
        public int UzmiGodinuKojaSeKoristi()
        {
            string StringZaGodinu = classSQL.remoteConnectionString;
            string[] ArrZaGodinu = StringZaGodinu.Split(';');
            ArrZaGodinu[4] = ArrZaGodinu[4].Replace("Database=", "");
            string baza = ArrZaGodinu[4];
            int godina = 0;
            if (baza.Length > 4)
            {
                if (!int.TryParse(baza.Remove(0, nastavak_na_bazu.Length), out godina))
                {
                    return 0;
                }
            }
            return godina;
        }

        #endregion UzmiGodinuKojaSeKoristi(): FUNKCIJA vraca integer kao godinu koja se trenutno korisi u remoteConnectionString-u

        #region UzmiBazuKojaSeKoristi(): FUNKCIJA vraca naziv baze koja se trenutno korisi u remoteConnectionString-u

        /// <summary>
        /// FUNKCIJA vraca naziv baze koja se trenutno korisi u remoteConnectionString-u
        /// npr: 2013
        /// </summary>
        /// <returns></returns>
        public string UzmiBazuKojaSeKoristi()
        {
            string StringZaGodinu = classSQL.remoteConnectionString;
            string[] ArrZaGodinu = StringZaGodinu.Split(';');
            ArrZaGodinu[4] = ArrZaGodinu[4].Replace("Database=", "");
            string baza = ArrZaGodinu[4];
            return baza;
        }

        #endregion UzmiBazuKojaSeKoristi(): FUNKCIJA vraca naziv baze koja se trenutno korisi u remoteConnectionString-u

        #region GodinaPostoji(): FUNKCIJA vraču true|false ovisno dali tražena godina postoji

        /// <summary>
        /// FUNKCIJA vraču true|false ovisno dali tražena godina postoji
        /// Argumenti su: lista svih godina u bazi i tražena baza npr "DB2014"
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="trazena_godina"></param>
        /// <returns></returns>
        public bool GodinaPostoji(string trazena_godina)
        {
            List<string> lista = UzmiSveBazeIzPostgressa();

            foreach (string l in lista)
            {
                if (l.ToUpper() == trazena_godina.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        #endregion GodinaPostoji(): FUNKCIJA vraču true|false ovisno dali tražena godina postoji

        #region UzmiSveBazeIzPostgressa(): Funkcija vrača List tipa string

        /// <summary>
        /// Funkcija vrača List tipa string
        /// </summary>
        /// <returns></returns>
        public List<string> UzmiSveBazeIzPostgressa()
        {
            List<string> l = new List<string>();
            string sql = @"SELECT datname FROM pg_database WHERE datistemplate IS FALSE AND datallowconn IS TRUE AND datname!='postgres';";

            DataSet dsBaze = classSQL.select(sql, "baze");
            if (dsBaze != null && dsBaze.Tables.Count > 0 && dsBaze.Tables[0] != null && dsBaze.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBaze.Tables[0].Rows)
                {
                    l.Add(r["datname"].ToString());
                }
            }
            return l;
        }

        #endregion UzmiSveBazeIzPostgressa(): Funkcija vrača List tipa string

        #region FUNKCIJA BRIŠE SVE UNOSE IZ ODABRANIH TABLICA I POSTAVLJA u XML novu bazu

        private void ObrisiSvePodatkeIzZadanihTablica()
        {
            string query = "";

            if (program == "CAFFE")
            {
                query = "BEGIN; " +
                   " DELETE FROM blagajnicki_izvjestaj;" +
                   " DELETE FROM caffe_narudzbe;" +
                   " DELETE FROM faktura_stavke;" +
                   " DELETE FROM fakture;" +
                   " DELETE FROM inventura_stavke;" +
                   " DELETE FROM inventura;" +
                   " DELETE FROM izdatnica_stavke;" +
                   " DELETE FROM izdatnica;" +
                   " DELETE FROM kucani_predracuni;" +
                   " DELETE FROM meduskladisnica_stavke;" +
                   " DELETE FROM meduskladisnica;" +
                   " DELETE FROM medu_poslovnice;" +
                   //" DELETE FROM na_stol;" +
                   " DELETE FROM otpremnica_stavke;" +
                   " DELETE FROM otpremnice;" +
                   " DELETE FROM ponude;" +
                   " DELETE FROM ponude_stavke;" +
                   " DELETE FROM pocetno;" +
                   " DELETE FROM pocetno_stanje_stavke;" +
                   " DELETE FROM pocetno_stanje;" +
                   " DELETE FROM ispravljene_greske;" +
                   " DELETE FROM potrosni_materijal;" +
                   " DELETE FROM potrosni_materijal_artikli;" +
                   " DELETE FROM povrat_robe;" +
                   " DELETE FROM povrat_robe_stavke;" +
                   " DELETE FROM primka;" +
                   " DELETE FROM primka_stavke;" +
                   //" DELETE FROM promjena_cijene_komadno;" +
                   //" DELETE FROM promjena_cijene_komadno_stavke;" +
                   " DELETE FROM promjena_cijene;" +
                   " DELETE FROM promjena_cijene_stavke;" +
                   " DELETE FROM promjena_cijene_auto;" +
                   " DELETE FROM racun_popust_kod_sljedece_kupnje;" +
                   " DELETE FROM racun_stavke;" +
                   " DELETE FROM racuni;" +
                   " DELETE FROM svi_predracuni;" +
                   " DELETE FROM smjene;" +
                   " COMMIT;";
            }
            /*else if (program == "PCPOS")
            {
                query = "BEGIN; " +
                   " DELETE FROM avansi;" +
                   " DELETE FROM faktura_stavke;" +
                   " DELETE FROM fakture;" +
                   " DELETE FROM faktura_van_stavke;" +
                   " DELETE FROM fakture_van;" +
                   " DELETE FROM ifb;" +
                   " DELETE FROM ifb_stavke;" +
                   " DELETE FROM inventura;" +
                   " DELETE FROM inventura_stavke;" +
                   " DELETE FROM ispis_faktura_stavke;" +
                   " DELETE FROM ispis_fakture;" +
                   " DELETE FROM ispis_racun_stavke;" +
                   " DELETE FROM ispis_racuni;" +
                   " DELETE FROM izdatnica;" +
                   " DELETE FROM izdatnica_stavke;" +
                   " DELETE FROM ispravljene_greske;" +
                   " DELETE FROM kalkulacija;" +
                   " DELETE FROM kalkulacija_stavke;" +
                   " DELETE FROM ispis_racuni;" +
                   " DELETE FROM meduskladisnica;" +
                   " DELETE FROM meduskladisnica_stavke;" +
                   " DELETE FROM normativi;" +
                   " DELETE FROM normativi_stavke;" +
                   " DELETE FROM odjava_komisione;" +
                   " DELETE FROM odjava_komisione_stavke;" +
                   " DELETE FROM otpis_robe;" +
                   " DELETE FROM otpis_robe_stavke;" +
                   " DELETE FROM pocetno_stanje;" +
                   " DELETE FROM pocetno_stanje_stavke;" +
                   " DELETE FROM otpremnica_stavke;" +
                   " DELETE FROM otpremnice;" +
                   " DELETE FROM ponude;" +
                   " DELETE FROM ponude_stavke;" +
                   " DELETE FROM povrat_robe;" +
                   " DELETE FROM povrat_robe_stavke;" +
                   " DELETE FROM primka;" +
                   " DELETE FROM primka_stavke;" +
                   " DELETE FROM promjena_cijene_komadno;" +
                   " DELETE FROM promjena_cijene_komadno_stavke;" +
                   " DELETE FROM promjena_cijene;" +
                   " DELETE FROM promjena_cijene_stavke;" +
                   " DELETE FROM racun_popust_kod_sljedece_kupnje;" +
                   " DELETE FROM radni_nalog;" +
                   " DELETE FROM radni_nalog_normativ;" +
                   " DELETE FROM radni_nalog_servis;" +
                   " DELETE FROM radni_nalog_servis_stavke;" +
                   " DELETE FROM radni_nalog_stavke;" +
                   " DELETE FROM ufa;" +
                   " DELETE FROM racun_stavke;" +
                   " DELETE FROM racuni;" +
                   "COMMIT;";
            }*/

            try
            {
                //AKO JE OTVOREN POSTOJEČI REMOTE CONNECTION
                if (classSQL.remoteConnection.State.ToString() != "Closed") { classSQL.remoteConnection.Close(); }

                NpgsqlConnection remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(postojeca_baza, nova_baza));
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlCommand comm = new NpgsqlCommand(query, remoteConnection);
                comm.ExecuteNonQuery();
                remoteConnection.Close();

                #region AKO NE POSTOJE OVE TABLICE DESILA SE BUDE GREŠKA (TRY/CATCH) TE TABLICE PRIPADAJU RESORTU

                try
                {
                    DataSet dataSet = new DataSet();
                    if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", remoteConnection);
                    dataSet.Reset();
                    da.Fill(dataSet);
                    remoteConnection.Close();

                    DataRow[] dataROW = dataSet.Tables[0].Select("table_name = 'Rfakture'");

                    if (dataROW.Length > 0)
                    {
                        query = "BEGIN; " +
                           " DELETE FROM Rfakture;" +
                           " DELETE FROM Rfaktura_stavke;" +
                           " DELETE FROM Rponude;" +
                           " DELETE FROM Rponude_stavke;" +
                           " DELETE FROM avansi;" +
                           "COMMIT;";

                        if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                        comm = new NpgsqlCommand(query, remoteConnection);
                        comm.ExecuteNonQuery();
                        remoteConnection.Close();
                    }
                }
                catch
                {
                }

                #endregion AKO NE POSTOJE OVE TABLICE DESILA SE BUDE GREŠKA (TRY/CATCH) TE TABLICE PRIPADAJU RESORTU

                MessageBox.Show("KREIRANO!!!\r\nBaza za novu godinu uspješno je kreirana.\r\nPROGRAM JE PREBAČEN NA NOVU tek kreiranu GODINU.", "Kreirano!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PostaviGodinu_U_XML(nova_baza);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion FUNKCIJA BRIŠE SVE UNOSE IZ ODABRANIH TABLICA I POSTAVLJA u XML novu bazu

        #region OVA FUNKCIJA RADI BACKUP SVIH BAZA U POSTGRESU

        public void BackupSvihBaza()
        {
            string ime_servera = "";
            string port = "";

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                ime_servera = book.Attribute("server").Value;
                port = book.Attribute("port").Value;
            }

            string pathDesk = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija " + DateTime.Now.Year.ToString();
            if (!Directory.Exists(pathDesk))
            {
                Directory.CreateDirectory(pathDesk);
            }

            foreach (string DB in UzmiSveBazeIzPostgressa())
            {
                //File.WriteAllText("DBbackup//DB" + DB + ".bat", "pg_dump.exe --host " + ime_servera + " --port 5432 --username postgres --format custom --blobs --verbose --file \"" + pathDesk + "\\" + DB + DateTime.Now.ToString("yyyy-MM-dd-H-mm-ss") + ".backup\" \"" + DB + "\"");
                path = System.Environment.CurrentDirectory;

                /*System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.WorkingDirectory = path + "\\DBbackup";
                proc.StartInfo.FileName = path + "\\DBbackup\\DB" + DB + ".bat";
                proc.Start();*/
                System.Diagnostics.Process.Start(path + "\\DBbackup\\pg_dump.exe", "--host " + ime_servera + " --port " + port + " --username postgres --format custom --blobs --verbose --file \"" + pathDesk + "\\" + DB + "_" + DateTime.Now.ToString("yyyy-MM-dd-H-mm-ss") + ".backup\" \"" + DB + "\"");
            }
        }

        #endregion OVA FUNKCIJA RADI BACKUP SVIH BAZA U POSTGRESU

        #region DODACI

        public void PostaviVarijabluZaPostojecuBazu(string _name)
        {
            postojeca_baza = _name;
        }

        public void PostaviVarijabluZaNovuBazu(string _name)
        {
            nova_baza = _name;
        }

        public void PostaviGodinu_U_XML(string __nova_baza)
        {
            //OVAJ DIO ZAPISUJE NOVU BAZU U XML za postavke i restarta program
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var queryXML = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in queryXML)
            {
                book.Attribute("database").Value = __nova_baza;
            }
            xmlFile.Save(path);

            classSQL.remoteConnectionString = SQL.claasConnectDatabase.GetRemoteConnectionString();
            classSQL.remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString);

            DataTable DT = classSQL.select("SELECT * FROM ispravljene_greske WHERE opis='prvi_prvi'", "greske").Tables[0];

            //bool p = PostojiProslaGodina();
            // bool d = (UzmiGodinuKojaSeKoristi() == UzmiTrenutnuGodinu());

            DataTable racuni = classSQL.select("SELECT " +
                " zbroj (SELECT coalesce(MAX(broj_fakture),0) FROM fakture) zbroj (SELECT coalesce(MAX(CAST(broj_racuna AS int)),0) FROM racuni)", "kolicine").Tables[0];

            int x;
            int.TryParse(racuni.Rows[0][0].ToString(), out x);

            if ((DT.Rows.Count == 0) && (UzmiGodinuKojaSeKoristi() == UzmiTrenutnuGodinu()))
            {
                if (PostojiProslaGodina() && x == 0)
                {
                    BackupSvihBaza();
                    MessageBox.Show("PRIČEKAJTE!!!\r\nOvo može potrajato do 30 sekundi molimo pričekajte prebacivanje stanja u odabranu godinu!", "PRIČEKAJTE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GetRoba();
                    GetRobaProdaja();
                    PoravnavanjeIdSerialBazi();
                }

                classSQL.insert("INSERT INTO ispravljene_greske (opis) VALUES ('prvi_prvi');");
            }

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Application.OpenForms[i].Close();
            }
            Application.Restart();
        }

        #endregion DODACI

        #region PostojiProslaGodina(): FUNKCIJA vrača true|false ovisno dali postoji prošla godina

        /// <summary>
        /// FUNKCIJA vraču true|false ovisno dali postoji prošla godina
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="trazena_godina"></param>
        /// <returns></returns>
        public bool PostojiProslaGodina()
        {
            int trenutna_g = UzmiGodinuKojaSeKoristi();
            if (trenutna_g == 0)
                return false;
            return GodinaPostoji(UzmiBazuKojaSeKoristi().Replace(trenutna_g.ToString(), (trenutna_g - 1).ToString()));
        }

        #endregion PostojiProslaGodina(): FUNKCIJA vrača true|false ovisno dali postoji prošla godina

        #region GetRoba*************************************************************

        public struct Roba
        {
            public string naziv;
            public string id_grupa;
            public string jm;
            public string mpc;
            public string id_roba;
            public string sifra;
            public string ean;
            public string porez;
            public string porez_potrosnja;
            public string aktivnost;
            public string id_podgrupa;
            public string border_color;
            public string button_style;
            public string brojcanik;

            public string nbc;
            public string editirano;
            public string novo;
            public string porezna_grupa;
        }

        private void GetRoba()
        {
            NpgsqlConnection remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(UzmiTrenutnuGodinu().ToString(), (UzmiTrenutnuGodinu() - 1).ToString()));
            string sql = "SELECT * FROM roba";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, remoteConnection);
            DataSet DS = new DataSet();
            da.Fill(DS);
            DataTable DTk = DS.Tables[0];

            Roba r = new Roba();

            classSQL.insert("DELETE FROM roba");

            PoravnavanjeIdSerialBazi();
            for (int i = 0; i < DTk.Rows.Count; i++)
            {
                //*************************************************PROVJERA ROBE I SPREMANJE U STRUCT************************************************
                r.naziv = DTk.Rows[i]["naziv"].ToString();
                r.id_grupa = DTk.Rows[i]["id_grupa"].ToString() == "" ? "0" : DTk.Rows[i]["id_grupa"].ToString();
                r.jm = DTk.Rows[i]["jm"].ToString() == "" ? "kom" : DTk.Rows[i]["jm"].ToString();
                r.mpc = DTk.Rows[i]["mpc"].ToString() == "" ? "0" : DTk.Rows[i]["mpc"].ToString().Replace(".", ",");
                r.id_roba = DTk.Rows[i]["id_roba"].ToString() == "" ? "0" : DTk.Rows[i]["id_roba"].ToString();
                r.sifra = DTk.Rows[i]["sifra"].ToString() == "" ? "greska" : DTk.Rows[i]["sifra"].ToString();
                r.ean = DTk.Rows[i]["ean"].ToString() == "" ? "-1" : DTk.Rows[i]["ean"].ToString();
                r.porez = DTk.Rows[i]["porez"].ToString() == "" ? "0" : DTk.Rows[i]["porez"].ToString();
                r.porez_potrosnja = DTk.Rows[i]["porez_potrosnja"].ToString() == "" ? "0" : DTk.Rows[i]["porez_potrosnja"].ToString();
                r.aktivnost = DTk.Rows[i]["aktivnost"].ToString() == "" ? "0" : DTk.Rows[i]["aktivnost"].ToString();
                r.id_podgrupa = DTk.Rows[i]["id_podgrupa"].ToString() == "" ? "0" : DTk.Rows[i]["id_podgrupa"].ToString();
                r.border_color = DTk.Rows[i]["border_color"].ToString() == "" ? "" : DTk.Rows[i]["border_color"].ToString();
                r.button_style = DTk.Rows[i]["button_style"].ToString() == "standard" ? "" : DTk.Rows[i]["button_style"].ToString();
                r.brojcanik = DTk.Rows[i]["brojcanik"].ToString() == "" ? "0" : DTk.Rows[i]["brojcanik"].ToString();
                r.nbc = DTk.Rows[i]["nbc"].ToString() == "" ? "0" : DTk.Rows[i]["nbc"].ToString().Replace(",", ".");
                r.editirano = DTk.Rows[i]["editirano"].ToString() == "" ? "0" : DTk.Rows[i]["editirano"].ToString();
                r.novo = DTk.Rows[i]["brojcanik"].ToString() == "" ? "0" : DTk.Rows[i]["novo"].ToString();
                r.porezna_grupa = DTk.Rows[i]["porezna_grupa"].ToString() == "" ? "0" : DTk.Rows[i]["porezna_grupa"].ToString();

                r.naziv = r.naziv.Replace("\'", "");
                r.naziv = r.naziv.Replace("\"", "");
                r.naziv = r.naziv.Replace("&", " AND ");

                //**************************************************************************************************************************************

                sql = "INSERT INTO roba (naziv,id_grupa,jm,mpc,id_roba,sifra,ean,porez,porez_potrosnja,aktivnost,id_podgrupa,border_color,button_style,brojcanik" +
                    ",nbc,editirano,novo,porezna_grupa) VALUES (" +
                    "'" + r.naziv + "'," +
                    "'" + r.id_grupa + "'," +
                    "'" + r.jm + "'," +
                    "'" + r.mpc + "'," +
                    "'" + r.id_roba + "'," +
                    "'" + r.sifra + "'," +
                    "'" + r.ean + "'," +
                    "'" + r.porez + "'," +
                    "'" + r.porez_potrosnja + "'," +
                    "'" + r.aktivnost + "'," +
                    "'" + r.id_podgrupa + "'," +
                    "'" + r.border_color + "'," +
                    "'" + r.button_style + "'," +
                    "'" + r.brojcanik + "'," +
                    "'" + r.nbc + "'," +
                    "'" + r.editirano + "'," +
                    "'" + r.novo + "'," +
                    "'" + r.porezna_grupa + "'" +
                    ");";
                classSQL.insert(sql);
            }
        }

        #endregion GetRoba*************************************************************

        #region GetRobaProdaja**********************************************

        public struct RobaProdaja
        {
            public string id_roba_prodaja;
            public string id_skladiste;
            public string kolicina;
            public string nc;
            public string vpc;
            public string porez;
            public string sifra;
            public string porez_potrosnja;
            public string id_grupa;
            public string id_podgrupa;
            public string mjera;
            public string aktivnost;
            public string povratna_naknada;
            public string poticajna_naknada;
            public string ulazni_porez;
            public string izlazni_porez;
            public string naziv;
            public string mpc;
            public string id_partner;
            public string kolicina_predracun;
            public string editirano;
            public string novo;
            public string cijena2;
            public string u_pakiranju;
            public string brojcanik;
        }

        private void GetRobaProdaja()
        {
            //classSQL.remoteConnectionString.Replace(UzmiTrenutnuGodinu().ToString(), (UzmiTrenutnuGodinu() - 1).ToString())
            NpgsqlConnection remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString);
            string sql = "SELECT * FROM roba_prodaja";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, remoteConnection);
            DataSet DS = new DataSet();
            da.Fill(DS);
            DataTable DTrp = DS.Tables[0];

            classSQL.insert("DELETE FROM roba_prodaja");

            PoravnavanjeIdSerialBazi();

            RobaProdaja rp = new RobaProdaja();
            for (int i = 0; i < DTrp.Rows.Count; i++)
            {
                //*************************************************PROVJERA ROBE I SPREMANJE U STRUCT***************************************************
                decimal kol = 0;
                rp.id_roba_prodaja = DTrp.Rows[i]["id_roba_prodaja"].ToString() == "" ? "1" : DTrp.Rows[i]["id_roba_prodaja"].ToString();
                rp.id_skladiste = DTrp.Rows[i]["id_skladiste"].ToString() == "" ? "1" : DTrp.Rows[i]["id_skladiste"].ToString();
                rp.kolicina = decimal.TryParse(DTrp.Rows[i]["kolicina"].ToString(), out kol) ? DTrp.Rows[i]["kolicina"].ToString() : "0";
                rp.nc = DTrp.Rows[i]["nc"].ToString() == "" ? "0" : DTrp.Rows[i]["nc"].ToString().Replace(",", ".");
                rp.vpc = DTrp.Rows[i]["vpc"].ToString() == "" ? "0" : DTrp.Rows[i]["vpc"].ToString().Replace(",", ".");
                rp.sifra = DTrp.Rows[i]["sifra"].ToString() == "" ? "greska" : DTrp.Rows[i]["sifra"].ToString();
                rp.porez_potrosnja = DTrp.Rows[i]["porez_potrosnja"].ToString() == "" ? "0" : DTrp.Rows[i]["porez_potrosnja"].ToString();
                rp.id_grupa = DTrp.Rows[i]["id_grupa"].ToString() == "" ? "0" : DTrp.Rows[i]["id_grupa"].ToString();
                rp.id_podgrupa = DTrp.Rows[i]["id_podgrupa"].ToString() == "" ? "0" : DTrp.Rows[i]["id_podgrupa"].ToString();
                rp.mjera = DTrp.Rows[i]["mjera"].ToString() == "" ? "0" : DTrp.Rows[i]["mjera"].ToString();
                rp.aktivnost = DTrp.Rows[i]["aktivnost"].ToString() == "" ? "0" : DTrp.Rows[i]["aktivnost"].ToString();
                rp.povratna_naknada = DTrp.Rows[i]["povratna_naknada"].ToString() == "" ? "0" : DTrp.Rows[i]["povratna_naknada"].ToString();
                rp.poticajna_naknada = DTrp.Rows[i]["poticajna_naknada"].ToString() == "" ? "0" : DTrp.Rows[i]["poticajna_naknada"].ToString();
                rp.ulazni_porez = DTrp.Rows[i]["ulazni_porez"].ToString() == "" ? "0" : DTrp.Rows[i]["ulazni_porez"].ToString();
                rp.izlazni_porez = DTrp.Rows[i]["izlazni_porez"].ToString() == "" ? "0" : DTrp.Rows[i]["izlazni_porez"].ToString();
                rp.naziv = DTrp.Rows[i]["naziv"].ToString() == "" ? "0" : DTrp.Rows[i]["naziv"].ToString();
                rp.mpc = DTrp.Rows[i]["mpc"].ToString() == "" ? "0" : DTrp.Rows[i]["mpc"].ToString().Replace(",", ".");
                rp.id_partner = DTrp.Rows[i]["id_partner"].ToString() == "" ? "0" : DTrp.Rows[i]["id_partner"].ToString();
                rp.kolicina_predracun = DTrp.Rows[i]["kolicina_predracun"].ToString() == "" ? "0" : DTrp.Rows[i]["kolicina_predracun"].ToString().Replace(",", ".");
                rp.brojcanik = DTrp.Rows[i]["brojcanik"].ToString() == "" ? "0" : DTrp.Rows[i]["brojcanik"].ToString().Replace(",", ".");
                rp.editirano = DTrp.Rows[i]["editirano"].ToString() == "" ? "0" : DTrp.Rows[i]["editirano"].ToString();
                rp.novo = DTrp.Rows[i]["brojcanik"].ToString() == "" ? "0" : DTrp.Rows[i]["novo"].ToString();
                rp.cijena2 = DTrp.Rows[i]["cijena2"].ToString() == "" ? "0" : DTrp.Rows[i]["cijena2"].ToString().Replace(",", ".");
                rp.u_pakiranju = DTrp.Rows[i]["u_pakiranju"].ToString() == "" ? "0" : DTrp.Rows[i]["u_pakiranju"].ToString().Replace(",", ".");
                //**************************************************************************************************************************************

                sql = "INSERT INTO roba_prodaja (id_roba_prodaja,id_skladiste,kolicina,nc,vpc,sifra,porez_potrosnja,id_grupa,id_podgrupa,mjera,aktivnost" +
                    ",povratna_naknada,poticajna_naknada,ulazni_porez,izlazni_porez,naziv,mpc,id_partner,kolicina_predracun,brojcanik,editirano, novo,cijena2,u_pakiranju" +
                    ") VALUES (" +
                    "'" + rp.id_roba_prodaja + "'," +
                    "'" + rp.id_skladiste + "'," +
                    "'" + rp.kolicina + "'," +
                    "'" + rp.nc + "'," +
                    "'" + rp.vpc + "'," +
                    "'" + rp.sifra + "'," +
                    "'" + rp.porez_potrosnja + "'," +
                    "'" + rp.id_grupa + "'," +
                    "'" + rp.id_podgrupa + "'," +
                    "'" + rp.mjera + "'," +
                    "'" + rp.aktivnost + "'," +
                    "'" + rp.povratna_naknada + "'," +
                    "'" + rp.poticajna_naknada + "'," +
                    "'" + rp.ulazni_porez + "'," +
                    "'" + rp.izlazni_porez + "'," +
                    "'" + rp.naziv + "'," +
                    "'" + rp.mpc + "'," +
                    "'" + rp.id_partner + "'," +
                    "'" + rp.kolicina_predracun + "'," +
                    "'" + rp.brojcanik + "'," +
                    "'" + rp.editirano + "'," +
                    "'" + rp.novo + "'," +
                    "'" + rp.cijena2 + "'," +
                    "'" + rp.u_pakiranju + "'" +
                    ")";
                classSQL.insert(sql);
            }
        }

        #endregion GetRobaProdaja**********************************************

        #region Poravnavanje ID_SERIAL u Bazi

        private void PoravnavanjeIdSerialBazi()
        {
            string sql = "BEGIN; " +
            " SELECT setval('roba_prodaja_id_roba_prodaja_seq', (SELECT MAX(id_roba_prodaja) FROM roba_prodaja)+1); " +
            " SELECT setval('roba_id_roba_seq', (SELECT MAX(id_roba) FROM roba)+1); " +
            " COMMIT;";

            classSQL.insert(sql);
        }

        #endregion Poravnavanje ID_SERIAL u Bazi
    }
}