using System.Data;

namespace PCPOS.Sinkronizacija.uzmi_sve_artikle
{
    internal class synRobaProdaja
    {
        private Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private Until.FunkcijeRobno FunkcijeRobno = new Until.FunkcijeRobno();

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

                string sql_za_web = "sql=";

                //*************************WEB ARTIKLI******************************
                string query = "SELECT * FROM  roba_prodaja WHERE " +
                    "oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

                //*************************LOCAL ARTIKLI******************************
                query = "SELECT * FROM  roba_prodaja";
                DataTable DTlocal = SqlPostgres.select(query, "loc").Tables[0];
                decimal porez;

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        DataRow[] row = DTlocal.Select("sifra='" + r["sifra"].ToString() + "'");
                        decimal.TryParse(r["ulazni_porez"].ToString().Replace(".", ","), out porez);

                        int id_partner;
                        decimal mpc, kolicina_predracun, cijena2;

                        int.TryParse(r["id_partner"].ToString(), out id_partner);
                        decimal.TryParse(r["mpc"].ToString().Replace(".", ","), out mpc);
                        decimal.TryParse(r["kolicina_predracun"].ToString().Replace(".", ","), out kolicina_predracun);
                        decimal.TryParse(r["cijena2"].ToString().Replace(".", ","), out cijena2);

                        if (row.Length == 0)
                        {
                            query = "INSERT INTO roba_prodaja (" +
                                            "id_skladiste,kolicina,nc,vpc,sifra,porez_potrosnja,id_grupa,id_podgrupa,mjera,aktivnost," +
                                            "povratna_naknada,poticajna_naknada,ulazni_porez,izlazni_porez,naziv,novo,editirano" +
                                            ") VALUES (" +
                                                "'" + r["id_skladiste"].ToString() + "'," +
                                                "'0'," +
                                                "'" + r["nc"].ToString().Replace(",", ".") + "'," +
                                                "'0'," +
                                                "'" + r["sifra"].ToString() + "'," +
                                                "'" + r["porez_potrosnja"].ToString().Replace(".", ",") + "'," +
                                                "'" + r["id_grupa"].ToString() + "'," +
                                                "'" + r["id_podgrupa"].ToString() + "'," +
                                                "'" + r["mjera"].ToString() + "'," +
                                                "'1'," +
                                                "'" + r["povratna_naknada"].ToString().Replace(".", ",") + "'," +
                                                "'" + r["poticajna_naknada"].ToString().Replace(".", ",") + "'," +
                                                "'" + porez.ToString("#0.0").Replace(".", ",") + "'," +
                                                "'" + r["izlazni_porez"].ToString().Replace(".", ",") + "'," +
                                                "'" + r["naziv"].ToString() + "'," +
                                                "'0','0'" +
                                                ");";
                            classSQL.insert(query);
                        }
                        else
                        {
                            query = "UPDATE roba_prodaja SET " +
                                       " id_grupa='" + r["id_grupa"].ToString() + "'," +
                                       " id_podgrupa='" + r["id_podgrupa"].ToString() + "'," +
                                       " mjera='" + r["mjera"].ToString() + "'," +
                                       " aktivnost='" + r["aktivnost"].ToString() + "'," +
                                       " ulazni_porez='" + porez.ToString("#0.0").Replace(".", ",") + "'," +
                                       " povratna_naknada='" + r["povratna_naknada"].ToString().Replace(".", ",") + "'," +
                                       " naziv='" + r["naziv"].ToString() + "'" +
                                       " WHERE sifra='" + r["sifra"].ToString() + "'";
                            classSQL.update(query);
                        }
                    }

                    //*********PORAVNAJ SKLADIŠTE**********
                    FunkcijeRobno.PostaviStanjeSkladista();
                }
            }
            catch
            {
            }
        }

        #endregion PRIMI PODATKE
    }
}