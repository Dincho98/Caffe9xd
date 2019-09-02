using System.Data;

namespace PCPOS.Sinkronizacija.uzmi_sve_artikle
{
    internal class synArtikli
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Util.Korisno.domena_za_sinkronizaciju;
        private string poslovnica = "1";

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba()
        {
            try
            {
                DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
                DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

                poslovnica = "1";
                if (DTposlovnica.Rows.Count > 0)
                {
                    poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                }

                //*************************WEB ARTIKLI******************************
                string query = "SELECT * FROM roba WHERE " +
                    "oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                //*************************LOCAL ARTIKLI******************************
                query = "SELECT * FROM  roba";
                DataTable DTlocal = SqlPostgres.select(query, "loc").Tables[0];
                decimal porez, pp;

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        DataRow[] row = DTlocal.Select("sifra='" + r["sifra"].ToString() + "'");

                        decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
                        decimal.TryParse(r["pp"].ToString().Replace(".", ","), out pp);

                        if (row.Length == 0)
                        {
                            query = "INSERT INTO roba (" +
                                        "naziv,id_grupa,jm,mpc,sifra,ean,porez,porez_potrosnja,aktivnost,porezna_grupa,id_podgrupa,novo,editirano" +
                                        ") VALUES (" +
                                            "'" + r["naziv"].ToString() + "'," +
                                            "'" + r["id_grupa"].ToString() + "'," +
                                            "'" + r["jm"].ToString() + "'," +
                                            "'" + r["mpc"].ToString().Replace(".", ",") + "'," +
                                            "'" + r["sifra"].ToString() + "'," +
                                            "'" + r["ean"].ToString() + "'," +
                                            "'" + porez.ToString("#0.0").Replace(".", ",") + "'," +
                                            "'" + pp.ToString("#0.0").Replace(".", ",") + "'," +
                                            "'" + r["aktivnost"].ToString() + "'," +
                                            "'" + r["porezna_grupa"].ToString() + "'," +
                                            "'" + r["id_podgrupa"].ToString() + "'," +
                                            "'0'," +
                                            "'0'" +
                                            ");";
                            classSQL.insert(query);
                        }
                        else
                        {
                            query = "UPDATE roba SET " +
                                                " naziv='" + r["naziv"].ToString() + "'," +
                                                " id_grupa='" + r["id_grupa"].ToString() + "'," +
                                                " jm='" + r["jm"].ToString() + "'," +
                                                " porez='" + porez.ToString("#0.0").Replace(".", ",") + "'," +
                                                " porez_potrosnja='" + pp.ToString("#0.0").Replace(".", ",") + "'," +
                                                " aktivnost='" + r["aktivnost"].ToString() + "'," +
                                                " porezna_grupa='" + r["porezna_grupa"].ToString() + "'," +
                                                " id_podgrupa='" + r["id_podgrupa"].ToString() + "'" +
                                                " WHERE sifra='" + r["sifra"].ToString() + "'";
                            classSQL.insert(query);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion PRIMI PODATKE
    }
}