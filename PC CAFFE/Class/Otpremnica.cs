using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Class
{
    internal class Otpremnica
    {
        public int BrojOtpremnice { get; set; }
        public int Godina { get; set; }
        public int IdSkladiste { get; set; }
        public int IdOdrediste { get; set; }
        public int IdPartner { get; set; }
        public int IdKomercijalista { get; set; }
        public int IdDjelatnik { get; set; }

        public string Napomena { get; set; }
        public string SveUkupno { get; set; }

        public DateTime Datum { get; set; }

        public DataTable DtOtpremnicaStavke { get; set; }

        public bool Osoba { get; set; }
        public bool na_sobu { get; private set; }

        private bool MiniOtpremnica = false;

        public Otpremnica()
        {
        }

        public Otpremnica(int _idPartner, DateTime _datum, string _napomena = null, bool _na_sobu = false)
        {
            MiniOtpremnica = true;
            na_sobu = _na_sobu;
            IdSkladiste = 1;
            BrojOtpremnice = Convert.ToInt32(brojOtpremnice(IdSkladiste.ToString()));
            Datum = _datum;
            Godina = Util.Korisno.GodinaKojaSeKoristiUbazi;
            IdPartner = _idPartner;
            IdOdrediste = IdPartner;
            IdDjelatnik = Convert.ToInt32(Properties.Settings.Default.id_zaposlenik);
            IdKomercijalista = IdDjelatnik;
            Napomena = _napomena;
            try
            {
                Osoba = !Convert.ToBoolean(classSQL.select("select vrsta_korisnika from partners where id_partner = '" + IdPartner + "'", "partners").Tables[0].Rows[0]["vrsta_korisnika"]);
            }
            catch
            {
                Osoba = false;
            }
        }

        public void otpremnicaPripremaZaPrint(string broj_otpremnice)
        {
            try
            {
                DataTable DTsend = new DataTable();
                //DTsend.Columns.Add("kolicina");
                //DTsend.Columns.Add("vpc");
                //DTsend.Columns.Add("nbc");
                //DTsend.Columns.Add("godina_otpremnice");
                //DTsend.Columns.Add("broj_otpremnice");
                //DTsend.Columns.Add("porez");
                //DTsend.Columns.Add("sifra_robe");
                //DTsend.Columns.Add("naziv");
                //DTsend.Columns.Add("porez_potrosnja");
                //DTsend.Columns.Add("rabat");
                //DTsend.Columns.Add("oduzmi");
                //DTsend.Columns.Add("id_skladiste");

                string sql = @"select sum(os.kolicina) as kolicina, os.vpc, os.nbc, o.godina_otpremnice, o.broj_otpremnice, os.porez, os.sifra_robe, r.naziv, os.porez_potrosnja, os.rabat, os.oduzmi, o.id_skladiste, o.osoba_partner
from otpremnica_stavke os
left join otpremnice o on os.broj_otpremnice = o.broj_otpremnice
left join roba r on os.sifra_robe = r.sifra
where o.broj_otpremnice = '" + broj_otpremnice + @"'
group by os.vpc, os.nbc, o.godina_otpremnice, o.broj_otpremnice, os.porez, os.sifra_robe, r.naziv, os.porez_potrosnja, os.rabat, os.oduzmi, o.id_skladiste, o.osoba_partner
order by r.naziv asc;";

                DataSet dsOtpremnicaStavke = classSQL.select(sql, "otpremnica_stavke");
                if (dsOtpremnicaStavke != null && dsOtpremnicaStavke.Tables.Count > 0 && dsOtpremnicaStavke.Tables[0] != null && dsOtpremnicaStavke.Tables[0].Rows.Count > 0)
                {
                    DTsend = dsOtpremnicaStavke.Tables[0];
                    PCPOS.PosPrint.classPosPrintOtpremnice.PrintReceipt(DTsend, Properties.Settings.Default.id_zaposlenik, broj_otpremnice + "/" + DateTime.Now.Year.ToString(), DTsend.Rows[0]["osoba_partner"].ToString(), "", broj_otpremnice, "", 0, false, false);
                }
                else
                {
                    MessageBox.Show("Krivi broj otpremnice.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Metoda za brisanje otpremnice iz baze podataka
        /// </summary>
        /// <param name="dgv">Proslijeduje DataGridView u kojem su zapisane stavke</param>
        /// <param name="dtOtpremnicaStavke"></param>
        /// <param name="brojOtpremnice"></param>
        /// <returns>vraca true ako je obrisana i false ako nije</returns>
        public bool otpremnicaBrisi(DataGridView dgv, DataTable dtOtpremnicaStavke, string brojOtpremnice)
        {
            if (MessageBox.Show("Brisanjem ove otpremnice vraćate količinu robe sa skladišta. Dali ste sigurni da želite obrisai ovu otpremnicu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    double skl = 0;
                    double fa_kolicina = 0;
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if (dgv.Rows[i].Cells["id_stavka"].Value != null)
                        {
                            if (dgv.Rows[i].Cells["oduzmi"].Value.ToString() == "DA")
                            {
                                if (Class.Postavke.is_caffe)
                                {
                                    if (Class.Postavke.skidaj_kolicinu_po_dokumentima)
                                    {
                                        string kol = SQL.ClassSkladiste.GetAmountCaffe(dgv.Rows[i].Cells["sifra"].Value.ToString(), dgv.Rows[i].Cells["id_skladiste"].Value.ToString(), dgv.Rows[i].Cells["kolicina"].Value.ToString(), "+");
                                    }
                                    SQL.ClassSkladiste.SetBrojcanik(dgv.Rows[i].Cells["sifra"].Value.ToString(), dgv.Rows[i].Cells["id_skladiste"].Value.ToString(), dgv.Rows[i].Cells["kolicina"].Value.ToString(), "-");
                                }
                                else
                                {
                                    DataRow[] dataROW = dtOtpremnicaStavke.Select("id_stavka = " + dgv.Rows[i].Cells["id_stavka"].Value.ToString());
                                    skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgv.Rows[i].Cells["sifra"].Value + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                                    fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                                    skl = skl + fa_kolicina;
                                    classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dgv.Rows[i].Cells["sifra"].Value + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                                }
                            }
                        }
                    }

                    classSQL.delete("DELETE FROM otpremnica_stavke WHERE broj_otpremnice='" + brojOtpremnice + "'");
                    classSQL.delete("DELETE FROM otpremnice WHERE broj_otpremnice='" + brojOtpremnice + "'");
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele otpremnice br." + brojOtpremnice + "')");

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Metoda za spremanje otpremnice u bazu
        /// </summary>
        /// <param name="dgv">Proslijeduje DataGridView u kojem su zapisane stavke</param>
        /// <param name="broj_otpremnice">Broj otpremnice prethodno izracunat</param>
        /// <param name="id_skladiste">ID skladiste na koje se otpremnica odnosi</param>
        /// <param name="id_odrediste"></param>
        /// <param name="id_partner">ID partnera na kojeg glasi otpremnica</param>
        /// <param name="osoba"></param>
        /// <returns></returns>
        public bool otpremnicaSpremi(DataGridView dgv, ref string broj_otpremnice, string id_skladiste, DateTime datum, string id_zaposlenik, string id_odrediste = null, string id_partner = null, bool osoba = false, string napomena = null, string godina = null, string id_komercijalist = null)
        {
            try
            {
                DataTable DTsend = new DataTable();
                DTsend.Columns.Add("kolicina");
                DTsend.Columns.Add("vpc");
                DTsend.Columns.Add("nbc");
                DTsend.Columns.Add("godina_otpremnice");
                DTsend.Columns.Add("broj_otpremnice");
                DTsend.Columns.Add("porez");
                DTsend.Columns.Add("sifra_robe");
                DTsend.Columns.Add("naziv");
                DTsend.Columns.Add("porez_potrosnja");
                DTsend.Columns.Add("rabat");
                DTsend.Columns.Add("oduzmi");
                DTsend.Columns.Add("id_skladiste");
                DataRow row;

                string partner_osoba = "";
                if (osoba)
                {
                    partner_osoba = "O";
                }
                else
                {
                    partner_osoba = "P";
                }

                string broj = brojOtpremnice(id_skladiste);
                if (broj.Trim() != broj_otpremnice.Trim())
                {
                    MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
                    broj_otpremnice = broj;
                }

                if (classSQL.remoteConnectionString == "")
                {
                    SveUkupno = SveUkupno.Replace(",", ".");
                }
                else
                {
                    SveUkupno = SveUkupno.Replace(".", ",");
                }

                //insert header
                string sql = "INSERT INTO otpremnice (broj_otpremnice, partner_osoba, godina_otpremnice, id_skladiste, osoba_partner, id_odrediste, " +
                    "vrsta_dok, datum, id_izjava, napomena, id_kom, id_izradio, id_otprema, mj_otpreme, adr_otpreme, isprave, id_prijevoznik, registracija, " +
                    "istovarno_mj, istovarni_rok, troskovi_prijevoza, ukupno, editirano, novo) VALUES (" +
                    "'" + broj_otpremnice + "'," +
                    "'" + partner_osoba + "'," +
                    "'" + (godina != null ? godina : datum.ToString("yyyy")) + "'," +
                    "'" + id_skladiste + "'," +
                    "'" + (id_partner != null ? id_partner : "") + "'," +
                    "'" + (id_odrediste != null ? id_odrediste : "") + "'," +
                    "null," + //" + cbVD.SelectedValue + "
                    "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "null," + //" + cbIzjava.SelectedValue + "
                    "'" + (napomena != null ? napomena : "") + "'," +
                    "'" + (id_komercijalist != null ? id_komercijalist : id_zaposlenik) + "'," +
                    "'" + id_zaposlenik + "'," +
                    "null," + //" + cbOtprema.SelectedValue + "
                    "null," + //" + txtMjOtpreme.Text + "
                    "null," + //" + txtAdresaOtp.Text + "
                    "null," + //" + txtIsprave1.Text + "
                    "null," + //" + txtSifraPrijevoznik.Text + "
                    "null," + //" + txtReg.Text + "
                    "null," + //" + txtIstovarnoMJ.Text + "
                    "null," + //" + dtpIstovarniRok.Value.ToString("yyyy-MM-dd H:mm:ss") + "
                    "null," + //" + txtTroskovi.Text + "
                    "'" + SveUkupno + "'," +
                    "false," +
                    "true" +
                    ")";

                if (na_sobu)
                {
                    sql = "INSERT INTO otpremnice (broj_otpremnice, partner_osoba, godina_otpremnice, id_skladiste, osoba_partner, id_odrediste, " +
                    "vrsta_dok, datum, id_izjava, napomena, id_kom, id_izradio, id_otprema, mj_otpreme, adr_otpreme, isprave, id_prijevoznik, registracija, " +
                    "istovarno_mj, istovarni_rok, troskovi_prijevoza, ukupno, editirano, novo, na_sobu) VALUES (" +
                    "'" + broj_otpremnice + "'," +
                    "'" + partner_osoba + "'," +
                    "'" + (godina != null ? godina : datum.ToString("yyyy")) + "'," +
                    "'" + id_skladiste + "'," +
                    "'" + (id_partner != null ? id_partner : "") + "'," +
                    "'" + (id_odrediste != null ? id_odrediste : "") + "'," +
                    "null," + //" + cbVD.SelectedValue + "
                    "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "null," + //" + cbIzjava.SelectedValue + "
                    "'" + (napomena != null ? napomena : "") + "'," +
                    "'" + (id_komercijalist != null ? id_komercijalist : id_zaposlenik) + "'," +
                    "'" + id_zaposlenik + "'," +
                    "null," + //" + cbOtprema.SelectedValue + "
                    "null," + //" + txtMjOtpreme.Text + "
                    "null," + //" + txtAdresaOtp.Text + "
                    "null," + //" + txtIsprave1.Text + "
                    "null," + //" + txtSifraPrijevoznik.Text + "
                    "null," + //" + txtReg.Text + "
                    "null," + //" + txtIstovarnoMJ.Text + "
                    "null," + //" + dtpIstovarniRok.Value.ToString("yyyy-MM-dd H:mm:ss") + "
                    "null," + //" + txtTroskovi.Text + "
                    "'" + SveUkupno + "'," +
                    "false," +
                    "true," +
                    "true" +
                    ")";
                }

                classSQL.insert(sql);

                string kol = "";

                for (int i = 0; i < dgv.RowCount; i++)
                {
                    DataGridViewRow dRow = dgv.Rows[i];

                    //if (classSQL.select_settings("select oduzmi_s_otpremnicom from postavke", "postavke").Tables[0].Rows[0]["oduzmi_s_otpremnicom"].ToString() == "1"){
                    //    kol = SQL.ClassSkladiste.GetAmount(dRow.Cells["sifra"].Value.ToString(), id_skladiste, dRow.Cells["kolicina"].Value.ToString(), "1", "-");
                    //    SQL.SQLroba_prodaja.UpdateRows(id_skladiste, kol, dRow.Cells["sifra"].Value.ToString());
                    //}

                    row = DTsend.NewRow();
                    row["kolicina"] = dRow.Cells["kolicina"].Value;
                    row["vpc"] = dRow.Cells["vpc"].Value.ToString().Replace(",", ".");
                    if (dgv.Columns.Contains("nc"))
                    {
                        row["nbc"] = dRow.Cells["nc"].Value;
                    }
                    else if (dgv.Columns.Contains("nbc"))
                    {
                        row["nbc"] = dRow.Cells["nbc"].Value;
                    }
                    else
                    {
                        row["nbc"] = 0;
                    }
                    try
                    {
                        row["naziv"] = classSQL.select("select naziv from roba where sifra = '" + dRow.Cells["sifra"].Value + "'", "roba").Tables[0].Rows[0]["naziv"];
                    }
                    catch
                    {
                        row["naziv"] = "";
                    }

                    try
                    {
                        row["porez_potrosnja"] = classSQL.select("select porez_potrosnja from roba where sifra = '" + dRow.Cells["sifra"].Value + "'", "roba").Tables[0].Rows[0]["porez_potrosnja"];
                    }
                    catch
                    {
                        row["porez_potrosnja"] = "";
                    }

                    row["godina_otpremnice"] = (godina != null ? godina : datum.ToString("yyyy"));
                    row["broj_otpremnice"] = broj_otpremnice;
                    row["porez"] = dRow.Cells["porez"].Value;
                    row["sifra_robe"] = dRow.Cells["sifra"].Value;
                    if (dgv.Columns.Contains("rabat"))
                    {
                        row["rabat"] = dRow.Cells["rabat"].Value;
                    }
                    else
                    {
                        row["rabat"] = 0;
                    }
                    //row["oduzmi"] = dRow.Cells["oduzmi"].Value;
                    row["id_skladiste"] = id_skladiste;
                    DTsend.Rows.Add(row);

                    if (Convert.ToInt32(dRow.Cells["dod"].Value.ToString()) != 2)
                    {
                        if (Class.Postavke.skidaj_kolicinu_po_dokumentima)
                        {
                            kol = SQL.ClassSkladiste.GetAmountCaffe(dRow.Cells["sifra"].Value.ToString(), id_skladiste, dRow.Cells["kolicina"].Value.ToString(), "-");
                        }
                        SQL.ClassSkladiste.SetBrojcanik(dRow.Cells["sifra"].Value.ToString(), id_skladiste, dRow.Cells["kolicina"].Value.ToString(), "+");
                    }
                }

                DtOtpremnicaStavke = DTsend;

                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik, datum, radnja) VALUES ('" + id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova otpremnica br." + broj_otpremnice + "')");
                SQL.SQLotpremnica.InsertStavke(DTsend);
                //if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokumenat?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                //printaj(broj_otpremnice, id_skladiste);
                //}
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string brojOtpremnice(string id_skladiste)
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj_otpremnice) FROM otpremnice  WHERE id_skladiste='" + id_skladiste + "'", "otpremnice").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        public IzracunRrezultat izracun(DataGridView dgv)
        {
            IzracunRrezultat izracunRezultat = new IzracunRrezultat();
            izracunRezultat.ukupnpPdv = 0;
            izracunRezultat.ukupnoVpc = 0;
            izracunRezultat.ukupnoMpc = 0;
            DataTable dtNest = new DataTable();
            //dgv.
            if (dgv.RowCount > 0)
            {
                if (MiniOtpremnica)
                {
                    double mpcUkp = 0, vpcUkp = 0, pdvUkp = 0;
                    foreach (DataGridViewRow drow in dgv.Rows)
                    {
                        double mpc = 0, vpc = 0, kolicina = 0, pdv = 0, rabat = 0;

                        vpc = Convert.ToDouble(drow.Cells["vpc"].FormattedValue.ToString());
                        kolicina = Convert.ToDouble(drow.Cells["kolicina"].FormattedValue.ToString());
                        pdv = Convert.ToDouble(drow.Cells["porez"].FormattedValue.ToString());
                        if (dgv.Columns.Contains("rabat"))
                            rabat = Convert.ToDouble(drow.Cells["rabat"].FormattedValue.ToString());
                        if (dgv.Columns.Contains("mpc"))
                        {
                            mpc = Convert.ToDouble(drow.Cells["mpc"].FormattedValue.ToString());
                        }
                        else
                        {
                            mpc = vpc * (1 + (pdv / 100));
                        }

                        double porez_ukupno = vpc * pdv / 100;
                        //double mpc = porez_ukupno + vpc;
                        double mpc_sa_kolicinom = mpc * kolicina;
                        double rabat_iznos = mpc * rabat / 100;

                        izracunRezultat.ukupnoMpc += (((mpc - rabat_iznos) * kolicina));
                        izracunRezultat.ukupnoVpc += (((mpc - rabat_iznos) * kolicina) / (1 + (pdv / 100)));
                        izracunRezultat.ukupnpPdv = izracunRezultat.ukupnoMpc - izracunRezultat.ukupnoVpc;
                    }
                    SveUkupno = izracunRezultat.ukupnoMpc.ToString();
                }
                else
                {
                    int rowBR = dgv.CurrentRow.Index;

                    if (isNumeric(dgv.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgv.Rows[rowBR].Cells["kolicina"].Value = "1"; MessageBox.Show("Greška kod upisa količine.", "Greška"); }
                    if (isNumeric(dgv.Rows[rowBR].Cells["rabat"].FormattedValue.ToString().ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgv.Rows[rowBR].Cells["rabat"].Value = "0"; MessageBox.Show("Greška kod upisa rabata.", "Greška"); }
                    if (isNumeric(dgv.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString().ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgv.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00"; MessageBox.Show("Greška kod upisa rabata.", "Greška"); }

                    double kol = Convert.ToDouble(dgv.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString());
                    double vpc = Convert.ToDouble(dgv.Rows[rowBR].Cells["vpc"].FormattedValue.ToString());
                    double porez = Convert.ToDouble(dgv.Rows[rowBR].Cells["porez"].FormattedValue.ToString());
                    double rbt = Convert.ToDouble(dgv.Rows[rowBR].Cells["rabat"].FormattedValue.ToString());

                    double porez_ukupno = vpc * porez / 100;
                    double mpc = porez_ukupno + vpc;
                    double mpc_sa_kolicinom = mpc * kol;
                    double rabat = mpc * rbt / 100;

                    //dgw.Rows[rowBR].Cells["mpc"].Value = String.Format("{0:0.00}", mpc);
                    dgv.Rows[rowBR].Cells["rabat_iznos"].Value = String.Format("{0:0.00}", (rabat * kol));
                    dgv.Rows[rowBR].Cells["iznos_bez_pdva"].Value = String.Format("{0:0.00}", (((mpc - rabat) * kol) / Convert.ToDouble("1," + porez)));
                    dgv.Rows[rowBR].Cells["iznos_ukupno"].Value = String.Format("{0:0.00}", ((mpc - rabat) * kol));
                    dgv.Rows[rowBR].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", ((mpc - rabat) / Convert.ToDouble("1," + porez)));

                    double pdv = 0;
                    double B_pdv = 0;
                    double u = 0;

                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        u = Convert.ToDouble(dgv.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString()) + u;
                        B_pdv = (Convert.ToDouble(dgv.Rows[i].Cells["cijena_bez_pdva"].FormattedValue.ToString()) * kol) + B_pdv;
                        pdv = u - B_pdv;
                    }

                    SveUkupno = u.ToString();

                    izracunRezultat.ukupnoMpc = u;
                    izracunRezultat.ukupnoVpc = B_pdv;
                    izracunRezultat.ukupnpPdv = pdv;
                }

                return izracunRezultat;
            }

            return izracunRezultat;
        }
    }

    public class IzracunRrezultat
    {
        public double ukupnoMpc { get; set; }
        public double ukupnoVpc { get; set; }
        public double ukupnpPdv { get; set; }
    }
}