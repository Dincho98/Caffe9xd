using System.Data;

namespace PCPOS.Sinkronizacija
{
    internal class synPokretac
    {
        private synArtikli Artikli = new synArtikli(false);
        private synRobaProdaja RobaProdaja = new synRobaProdaja(false);
        private synRacuni Racuni = new synRacuni(false);
        private synPrimka Primka = new synPrimka(false);
        private synGrupe Grupe = new synGrupe(false);
        private synZaposlenici Zaposlenici = new synZaposlenici(false);
        private synNormativ Normativi = new synNormativ(false);
        private synOtpisRobe OtpisRobe = new synOtpisRobe(false);
        private synInventura Inventura = new synInventura(false);
        private synPocetnoStanje PocetnoStanje = new synPocetnoStanje(false);

        private synIzdatnice Izdatnice = new synIzdatnice(false);

        private synPromjena_cijene PromjenaCijena = new synPromjena_cijene(false);
        private synPartner Partneri = new synPartner(false);
        private synPoslovnice Poslovnice = new synPoslovnice(false);
        private synMeduskladisnica Meduskl = new synMeduskladisnica(false);
        private synPotrosniMaterijal PotrosniMat = new synPotrosniMaterijal(false);
        private synBlagajnickiIzvjestaj BlagajnickiIzvjestaj = new synBlagajnickiIzvjestaj(false);
        private synPredracuni Predracuni = new synPredracuni(false);
        private synSkladiste Skladista = new synSkladiste(false);

        private synFaktura Faktura = new synFaktura(false);
        private synOtpremnica Otpremnica = new synOtpremnica(false);
        private synnaplatniUredaji Naplatni = new synnaplatniUredaji(false);

        private newSql SqlPostgres = new newSql();
        private DataTable DTpostavke;

        public synPokretac()
        {
            DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        }

        /// <summary>
        /// FUNKCIJA KOJA POZIVA SINKRONIZACIJU SA WEBOM
        /// </summary>
        /// <param name="_artikli"></param>
        /// <param name="_roba_prodaja"></param>
        /// <param name="_racuni"></param>
        /// <param name="_primke"></param>
        /// <param name="_grupe"></param>
        /// <param name="_zaposlenici"></param>
        /// <param name="_normativi"></param>
        /// <param name="_otpis_robe"></param>
        /// <param name="_inventura"></param>
        /// <param name="_pocetno_stanje"></param>
        /// <param name="_Promjena_cijene"></param>
        /// <param name="_partneri"></param>
        /// <param name="_poslovnice"></param>
        /// <param name="_meduskl"></param>
        /// <param name="_predracun"></param>
        /// <param name="_skladiste"></param>
        /// <param name="_fakture"></param>
        /// <param name="_otpremnice"></param>
        /// <param name="_izdatnice"></param>
        public void PokreniSinkronizaciju(bool _artikli, bool _roba_prodaja, bool _racuni, bool _primke,
            bool _grupe, bool _zaposlenici, bool _normativi, bool _otpis_robe, bool _inventura, bool _pocetno_stanje, bool _Promjena_cijene, bool _partneri, bool _poslovnice, bool _meduskl, bool _predracun, bool _skladiste, bool _fakture, bool _otpremnice, bool _izdatnice)
        {
            try
            {
                if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "1" && Util.Korisno.CheckForInternetConnection())
                {
                    if (System.Environment.MachineName != "POWER-RAC")
                    {
                        Util.Korisno.RadimSinkronizaciju = true;

                        if (_poslovnice) { Poslovnice.Send(); Naplatni.Send(); }

                        if (_roba_prodaja) { RobaProdaja.Send(); RobaProdaja.UzmiPodatkeSaWeba(); }
                        if (_artikli) { Artikli.Send(); Artikli.UzmiPodatkeSaWeba(); }
                        if (_racuni) { Racuni.Send(); }
                        if (_primke) { Primka.Send(); Primka.UzmiPodatkeSaWeba(); }
                        if (_grupe) { Grupe.Send(); }
                        if (_zaposlenici) { Zaposlenici.Send(); Zaposlenici.UzmiPodatkeSaWeba(); }
                        if (_normativi) { Normativi.Send(); Normativi.UzmiPodatkeSaWeba(); }
                        if (_otpis_robe) { OtpisRobe.Send(); OtpisRobe.UzmiPodatkeSaWeba(); }
                        if (_inventura) { Inventura.Send(); Inventura.UzmiPodatkeSaWeba(); }
                        if (_pocetno_stanje) { PocetnoStanje.Send(); PocetnoStanje.UzmiPodatkeSaWeba(); }
                        if (_Promjena_cijene) { PromjenaCijena.Send(); PromjenaCijena.UzmiPodatkeSaWeba(); }
                        if (_partneri) { Partneri.Send(); Partneri.UzmiPodatkeSaWeba(); PotrosniMat.Send(); PotrosniMat.UzmiPodatkeSaWeba(); }

                        if (_partneri) { BlagajnickiIzvjestaj.Send(); BlagajnickiIzvjestaj.UzmiPodatkeSaWeba(); }
                        if (_meduskl) { Meduskl.Send(); Meduskl.UzmiPodatkeSaWeba(); }
                        if (_predracun) { Predracuni.Send(); }
                        if (_skladiste) { Skladista.Send(); }
                        if (_fakture) { Faktura.Send(); Faktura.UzmiPodatkeSaWeba(); }
                        if (_otpremnice) { Otpremnica.Send(); Otpremnica.UzmiPodatkeSaWeba(); }
                        if (_izdatnice) { Izdatnice.Send(); }

                        Util.Korisno.RadimSinkronizaciju = false;
                    }
                }
            }
            catch
            {
                Util.Korisno.RadimSinkronizaciju = false;
            }
        }
    }
}