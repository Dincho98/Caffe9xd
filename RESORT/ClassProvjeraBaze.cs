using System.Data;

namespace RESORT
{
    internal class ClassProvjeraBaze
    {
        public static void ProvjeraTablica()
        {
            try
            {
                DataSet DS_podaciTtvrtke = classDBlite.LiteSelect("SELECT id FROM temp_podaci_tvrtke;", "temp_podaci_tvrtke");
            }
            catch
            {
                string sqlPT = string.Format(@"create table temp_podaci_tvrtke as select * from podaci_tvrtke;
drop table podaci_tvrtke;
CREATE TABLE [podaci_tvrtke] (
    [id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
    [ime_tvrtke] varchar(100),
    [oib] varchar(50),
    [tel] varchar(100),
    [fax] varchar(100),
    [mob] varchar(100),
    [iban] varchar(100),
    [iban1] varchar(100),
    [adresa] varchar(200),
    [grad] varchar(100),
    [vl] varchar(100),
    [poslovnica_adresa] varchar(200),
    [poslovnica_grad] varchar(100),
    [email] varchar(200),
    [opis_na_kraju_fakture] text,
    [r1] varchar(50),
    [skraceno_ime] varchar(200)
);
insert into podaci_tvrtke select * from temp_podaci_tvrtke;
");
                classDBlite.LiteSqlCommand(sqlPT);
            }

            DataTable DTpostavke = classDBlite.LiteSelect("SELECT * FROM postavke", "postavke").Tables[0];
            if (DTpostavke.Columns["naplata_po_sobi"] == null)
            {
                string sql = "ALTER TABLE postavke ADD COLUMN naplata_po_sobi int;";
                classDBlite.LiteSqlCommand(sql);
            }

            if (DTpostavke.Columns["path_image"] == null)
            {
                string sql = "ALTER TABLE postavke ADD COLUMN path_image varchar;";
                classDBlite.LiteSqlCommand(sql);
            }

            if (DTpostavke.Columns["bool_path_image"] == null)
            {
                string sql = "ALTER TABLE postavke ADD COLUMN bool_path_image int;";
                classDBlite.LiteSqlCommand(sql);
            }

            DataTable DTremote = RemoteDB.select("select table_name, column_name, data_type, character_maximum_length from information_schema.columns", "Table").Tables[0];

            if (DTremote.Select("table_name='grad' AND column_name='id_drzava'").Length == 0)
            {
                string sql = "ALTER TABLE grad ADD COLUMN id_drzava INT DEFAULT 60;";
                RemoteDB.insert(sql);
            }

            if (DTremote.Rows.Count > 0)
            {
                if (DTremote.Select("table_name = 'Rfakture'").Length == 0)
                {
                    string sql = "CREATE TABLE Rfakture" +
                    " (id serial NOT NULL," +
                    " broj int," +
                    " godina int," +
                    " jir character varying(100)," +
                    " zik character varying(100)," +
                    " datumDVO  timestamp without time zone," +
                    " datum  timestamp without time zone," +
                    " datum_valute  timestamp without time zone," +
                    " valuta numeric," +
                    " id_valuta int," +
                    " id_partner int," +
                    " broj_lezaja numeric," +
                    " nacin_placanja int," +
                    " id_izradio int," +
                    " model character varying(10)," +
                    " storno character varying(2)," +
                    " napomena text," +
                    " ukupno numeric," +
                    " CONSTRAINT id_primary_key_Rfakture PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.select(sql, "Rfakture");
                }

                if (DTremote.Select("table_name = 'Rfaktura_stavke'").Length == 0)
                {
                    string sqlagencija = "CREATE TABLE Rfaktura_stavke" +
                    " (id serial NOT NULL," +
                    " broj int," +
                    " dana numeric," +
                    " ukupno numeric," +
                    " rabat numeric," +
                    " porez numeric," +
                    " id_unos_gosta int," +
                    " broj_sobe int," +
                    " avans numeric," +
                    " ime_gosta character varying(150)," +
                    " datumDolaska timestamp without time zone," +
                    " datumOdlaska timestamp without time zone," +
                    " boravisna_pristojba numeric," +
                    " iznos_usluge numeric," +
                    " cijena_sobe numeric," +
                    " CONSTRAINT id_primary_key_Rfakture_stavke PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.select(sqlagencija, "sobe");
                }

                if (DTremote.Select("table_name = 'rfaktura_stavke' and column_name = 'otpremnica_broj'").Length == 0)
                {
                    string sql = "alter table rfaktura_stavke add column otpremnica_broj integer default 0;";
                    RemoteDB.select(sql, "rfaktura_stavke");
                }

                if (DTremote.Select("table_name = 'rfaktura_stavke' and column_name = 'otpremnica_pnp'").Length == 0)
                {
                    string sql = "alter table rfaktura_stavke add column otpremnica_pnp character varying(20) default '';";
                    RemoteDB.select(sql, "rfaktura_stavke");
                }

                //if (DTremote.Select("table_name = 'rfaktura_stavke' and column_name = 'otpremnica_sifra_robe'").Length == 0)
                //{
                //    string sql = "alter table rfaktura_stavke add column otpremnica_sifra_robe numeric(15,6) default 0;";
                //    RemoteDB.select(sql, "rfaktura_stavke");
                //}

                if (DTremote.Select("table_name = 'otpremnice' and column_name = 'rfaktura_broj'").Length == 0)
                {
                    string sql = @"alter table otpremnice add column na_sobu boolean DEFAULT false;
alter table otpremnice add column rfaktura_broj integer DEFAULT 0;
  alter table otpremnice add column rfaktura_poslovnica integer DEFAULT 0;
  alter table otpremnice add column rfaktura_naplatni_uredaj integer DEFAULT 0;";
                    RemoteDB.select(sql, "rfaktura_stavke");
                }

                if (DTremote.Select("table_name = 'Rponude'").Length == 0)
                {
                    string sql = "CREATE TABLE Rponude" +
                    " (id serial NOT NULL," +
                    " broj int," +
                    " godina int," +
                    " datumDVO  timestamp without time zone," +
                    " datum  timestamp without time zone," +
                    " datum_valute  timestamp without time zone," +
                    " valuta numeric," +
                    " id_valuta int," +
                    " id_partner int," +
                    " broj_lezaja numeric," +
                    " nacin_placanja int," +
                    " id_izradio int," +
                    " model character varying(10)," +
                    " napomena text," +
                    " ukupno numeric," +
                    " CONSTRAINT id_primary_key_Rponude PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.select(sql, "Rfakture");
                }

                if (DTremote.Select("table_name = 'Rponude_stavke'").Length == 0)
                {
                    string sqlagencija = "CREATE TABLE Rponude_stavke" +
                    " (id serial NOT NULL," +
                    " broj int," +
                    " dana numeric," +
                    " ukupno numeric," +
                    " rabat numeric," +
                    " porez numeric," +
                    " cijena_sobe numeric," +
                    " avans numeric," +
                    " opis_usluge character varying(150)," +
                    " datumDolaska timestamp without time zone," +
                    " datumOdlaska timestamp without time zone," +
                    " boravisna_pristojba numeric," +
                    " iznos_usluge numeric," +
                    " CONSTRAINT id_primary_key_Rponude_stavke PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.select(sqlagencija, "sobe");
                }

                if (DTremote.Select("table_name = 'R_CijenaSoba'").Length == 0)
                {
                    string sqlagencija = "CREATE TABLE R_CijenaSoba" +
                    " (id serial NOT NULL," +
                    " broj_sobe character varying(50)," +
                    " id_soba int," +
                    " id_valuta int," +
                    " cijena_nocenja numeric," +
                    " od_datuma timestamp without time zone," +
                    " do_datuma timestamp without time zone," +
                    " CONSTRAINT id_primary_key_R_CijenaSoba PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.insert(sqlagencija);
                }

                if (DTremote.Select("table_name = 'avansi'").Length == 0)
                {
                    string sql = "CREATE TABLE avansi" +
                                            "(broj_avansa bigint NOT NULL," +
                                            "dat_dok timestamp without time zone," +
                                            "dat_knj timestamp without time zone," +
                                            "id_zaposlenik integer," +
                                            "id_zaposlenik_izradio integer," +
                                            "model character varying(10)," +
                                            "id_nacin_placanja bigint," +
                                            "id_valuta integer," +
                                            "opis text," +
                                            "id_vd character(5)," +
                                            "godina_avansa character(6)," +
                                            "ukupno numeric," +
                                            "nult_stp numeric," +
                                            "neoporezivo numeric," +
                                            "osnovica10 numeric," +
                                            "osnovica_var numeric," +
                                            "porez_var numeric," +
                                            "id_pdv integer," +
                                            "id_partner bigint," +
                                            "ziro bigint," +
                                            "jir character varying(100)," +
                                            "zki character varying(100)," +
                                            "storno character varying(2)," +
                                            "CONSTRAINT broj_avansa PRIMARY KEY (broj_avansa)" +
                                            ")";

                    RemoteDB.select(sql, "avansi");
                }

                if (DTremote.Select("table_name = 'avansi_vd'").Length == 0)
                {
                    string sql = "CREATE TABLE avansi_vd" +
                        "(id_vd serial NOT NULL," +
                        "vd character varying(30)," +
                        "grupa character varying(5)," +
                        "CONSTRAINT primary_key_id_vd PRIMARY KEY (id_vd )" +
                        ")";
                    RemoteDB.select(sql, "avansi_vd");
                    Funkcije.provjera_sql(RemoteDB.insert("INSERT INTO avansi_vd (vd,grupa) VALUES ('Predujam','IP')"));
                    Funkcije.provjera_sql(RemoteDB.insert("INSERT INTO avansi_vd (vd,grupa) VALUES ('Storno primljenog predujma','PRS')"));
                }

                if (DTremote.Select("table_name = 'tip_sobe'").Length == 0)
                {
                    string sql = "CREATE TABLE tip_sobe" +
                        " (id serial NOT NULL," +
                        " tip character varying(70)," +
                        " aktivnost int," +
                        " CONSTRAINT id_tip_sobe_primary_key PRIMARY KEY (id )" +
                        " )";
                    RemoteDB.select(sql, "tip_sobe");
                    RemoteDB.insert("INSERT INTO tip_sobe (tip,aktivnost) VALUES ('Sve sobe','1')");
                    RemoteDB.insert("INSERT INTO tip_sobe (tip,aktivnost) VALUES ('Jednokrevetna','1')");
                    RemoteDB.insert("INSERT INTO tip_sobe (tip,aktivnost) VALUES ('Dvokrevetna','1')");
                }

                if (DTremote.Select("table_name = 'agencija'").Length == 0)
                {
                    string sqlagencija = "CREATE TABLE agencija" +
                    " (id serial NOT NULL," +
                    " ime_agencije character varying(100)," +
                    " napomena text," +
                    " aktivnost int," +
                    " CONSTRAINT id_primary_key_agencija PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.select(sqlagencija, "agencija");
                    RemoteDB.insert("INSERT INTO agencija (ime_agencije,aktivnost) VALUES ('Bravo','1')");
                }

                if (DTremote.Select("table_name = 'sobe'").Length == 0)
                {
                    string sqlagencija = "CREATE TABLE sobe" +
                    " (id serial NOT NULL," +
                    " broj_sobe numeric," +
                    " id_tip_sobe int," +
                    " naziv_sobe character varying(100)," +
                    " broj_lezaja numeric," +
                    " cijena_nocenja numeric," +
                    " aktivnost int," +
                    " napomena text," +
                    " CONSTRAINT id_primary_key_sobe PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.select(sqlagencija, "sobe");
                    RemoteDB.insert("INSERT INTO sobe (broj_sobe,id_tip_sobe,naziv_sobe,broj_lezaja,napomena,cijena_nocenja,aktivnost) VALUES ('1','1','Soba broj 1','2','','100','1')");
                    RemoteDB.insert("INSERT INTO sobe (broj_sobe,id_tip_sobe,naziv_sobe,broj_lezaja,napomena,cijena_nocenja,aktivnost) VALUES ('2','1','Soba broj 2','3','','100','1')");
                }

                if (DTremote.Select("table_name = 'vrsta_usluge'").Length == 0)
                {
                    string sqlagencija = "CREATE TABLE vrsta_usluge" +
                    " (id serial NOT NULL," +
                    " naziv_usluge character varying(100)," +
                    " napomena text," +
                    " iznos decimal," +
                    " aktivnost int," +
                    " CONSTRAINT id_primary_key_vrsta_usluge PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.select(sqlagencija, "vrsta_usluge");
                    RemoteDB.insert("INSERT INTO vrsta_usluge (naziv_usluge,iznos,aktivnost) VALUES ('Samo noćenje','0','1')");
                    RemoteDB.insert("INSERT INTO vrsta_usluge (naziv_usluge,iznos,aktivnost) VALUES ('Noćenje s doručkom','20','1')");
                    RemoteDB.insert("INSERT INTO vrsta_usluge (naziv_usluge,iznos,aktivnost) VALUES ('Puni pansion','50','1')");
                }

                if (DTremote.Select("table_name = 'boravisna_pristojba'").Length == 0)
                {
                    string sqlagencija = "CREATE TABLE boravisna_pristojba" +
                    " (id serial NOT NULL," +
                    " oznaka character varying(10)," +
                    " iznos numeric," +
                    " boravisna_pristojba character varying(100)," +
                    " aktivnost int," +
                    " CONSTRAINT id_primary_key_boravisna_pristojba PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.select(sqlagencija, "boravisna_pristojba");
                    RemoteDB.insert("INSERT INTO boravisna_pristojba (boravisna_pristojba,iznos,oznaka,aktivnost) VALUES ('Glavna sezona','7','A','1')");
                    RemoteDB.insert("INSERT INTO boravisna_pristojba (boravisna_pristojba,iznos,oznaka,aktivnost) VALUES ('Predsezona','5.50','A','1')");
                    RemoteDB.insert("INSERT INTO boravisna_pristojba (boravisna_pristojba,iznos,oznaka,aktivnost) VALUES ('Izvan sezona','4.50','A','1')");
                }

                if (DTremote.Select("table_name = 'unos_gosta'").Length == 0)
                {
                    string sqlagencija = "CREATE TABLE unos_gosta" +
                    " (id serial NOT NULL," +
                    " broj bigint," +
                    " godina int," +
                    " vrijeme_unosa timestamp without time zone," +
                    " ime_gosta character varying(150)," +
                    " adresa character varying(250)," +
                    " broj_osobne character varying(50)," +
                    " broj_putovnice character varying(50)," +
                    " datum_dolaska timestamp without time zone," +
                    " datum_odlaska timestamp without time zone," +
                    " datum_rodenja timestamp without time zone," +
                    " id_agencija int," +
                    " id_soba int," +
                    " popust numeric," +
                    " porez numeric," +
                    " id_tip_sobe int," +
                    " ukupno numeric," +
                    " id_vrsta_usluge int," +
                    " id_vrsta_gosta int," +
                    " iznos_vu numeric," +
                    " id_boravisna_pristojba int," +
                    " cijena_sobe numeric," +
                    " iznos_bor_pristojbe numeric," +
                    " id_drzava character varying(5)," +
                    " avans numeric," +
                    " dorucak int," +
                    " rucak int," +
                    " vecera int," +
                    " napomena text," +
                    " boja character varying(100)," +
                    " odjava int," +
                    " CONSTRAINT id_primary_key_unos_gosta PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.select(sqlagencija, "unos_gosta");
                }

                if (DTremote.Select("table_name = 'unos_rezervacije'").Length == 0)
                {
                    string sqlagencija = "CREATE TABLE unos_rezervacije" +
                    " (id serial NOT NULL," +
                    " broj bigint," +
                    " godina int," +
                    " vrijeme_unosa timestamp without time zone," +
                    " ime_gosta character varying(150)," +
                    " broj_osobne character varying(50)," +
                    " broj_putovnice character varying(50)," +
                    " datum_dolaska timestamp without time zone," +
                    " datum_odlaska timestamp without time zone," +
                    " id_agencija int," +
                    " id_soba int," +
                    " id_vrsta_gosta int," +
                    " id_vrsta_usluge int," +
                    " id_drzava character varying(5)," +
                    " avans numeric," +
                    " dorucak int," +
                    " rucak int," +
                    " vecera int," +
                    " odrasli int," +
                    " djeca int," +
                    " bebe int," +
                    " boja character varying(100)," +
                    " napomena text," +
                    " odjava int," +
                    " CONSTRAINT id_primary_key_unos_rezervacije PRIMARY KEY (id)" +
                    " )";
                    RemoteDB.select(sqlagencija, "unos_gosta");
                }
            }
        }
    }
}