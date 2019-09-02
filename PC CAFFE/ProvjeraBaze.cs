
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    internal class ProvjeraBaze
    {
        private string put_za_stavke = "";
        private string put_za_stavkeWeb = "";

        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private static DataTable DTtvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTcompact = classSQL.select_settings("SELECT * FROM INFORMATION_SCHEMA.COLUMNS", "postapodaci_tvrtkavke").Tables[0];

        public static void IncomingWithUpdate()
        {
            DataTable DTremote = classSQL.select("select table_name, column_name, data_type, character_maximum_length from information_schema.columns", "coltypes").Tables[0];
            DodajProvjeruGreske(DTremote);
            DodajPotrosni_materijal(DTremote);
            Porezi();

            string sql = @"SELECT pg_namespace.nspname, pg_proc.proname
    FROM pg_proc, pg_namespace
    WHERE pg_proc.pronamespace=pg_namespace.oid
       AND pg_proc.proname LIKE '%dblink%';";
            DataSet dsDbLink = classSQL.select(sql, "dblink");

            if (dsDbLink == null || dsDbLink.Tables.Count == 0 || dsDbLink.Tables[0] == null || dsDbLink.Tables[0].Rows.Count == 0)
            {
                sql = @"CREATE EXTENSION dblink;";
                classSQL.update(sql);
            }

            sql = @"drop function if exists selectOsobeKartica(id_partner integer);
CREATE OR REPLACE FUNCTION selectOsobeKartica(in id_partner integer, in server character varying, out usluga character varying, out datum timestamp without time zone , out izradio character varying)
RETURNS SETOF record
AS
$BODY$
DECLARE
    REC RECORD;
            stmt text;
            BEGIN

            FOR REC IN

    SELECT datname FROM pg_database
WHERE datistemplate = false
and length(datname) = (SELECT length(current_database())) and
substring(datname, 1, length(datname) - 4) = (SELECT substring(current_database(), 1, length(current_database()::varchar) - 4))
LOOP

    IF(LENGTH(stmt) > 0) THEN
          stmt := CONCAT(stmt, chr(10), 'UNION', chr(10));
            END IF;
            stmt:= CONCAT(stmt, 'select * from dblink(', CHR(39), 'host=', server, ' dbname=', rec.datname, ' user=postgres password=q1w2e3r4', CHR(39), ',

           ', CHR(39), 'select ro.naziv as usluga, r.datum_racuna as datum, concat(z.ime, ', CHR(39), CHR(39), ' ', CHR(39), CHR(39), ', z.prezime) as izradio
       from racun_stavke rs
       left
       join racuni r on rs.broj_racuna = r.broj_racuna and rs.id_ducan = r.id_kasa and rs.id_ducan = r.id_ducan
       left join roba ro on rs.sifra_robe = ro.sifra
       left join zaposlenici z on rs.id_izradio = z.id_zaposlenik
       where r.beauty_partner = ',id_partner, CHR(39), ')
       as t', rec.datname, '(
       usluga character varying,
       datum timestamp without time zone,
       izradio character varying
       )');

END LOOP;

            RETURN QUERY EXECUTE stmt;

            END
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
           ";

            classSQL.update(sql);

            sql = @"CREATE OR REPLACE FUNCTION selectOsobeBrojDugZadnji(IN id_partner integer, IN server character varying, OUT br_posjeta bigint, OUT zadnji_posjet timestamp without time zone, OUT dug numeric)
  RETURNS SETOF record AS
$BODY$
DECLARE
    REC RECORD;
            stmt text;
            BEGIN

            FOR REC IN
		SELECT datname FROM pg_database
		WHERE datistemplate = false
		and length(datname) = (SELECT length(current_database())) and
		substring(datname, 1, length(datname) - 4) = (SELECT substring(current_database(), 1, length(current_database()::varchar) - 4))
LOOP

    IF(LENGTH(stmt) > 0) THEN
          stmt := CONCAT(stmt, chr(10), 'UNION', chr(10));
            END IF;
            stmt:= CONCAT(stmt, 'select * from dblink(', CHR(39), 'host=', server, ' dbname=', rec.datname, ' user=postgres password=q1w2e3r4', CHR(39), ',', CHR(39),
             'select (
	select count(*)
	from racuni
	where beauty_partner = ', id_partner, '
	and storno = ', CHR(39),CHR(39), 'NE', CHR(39),CHR(39), ' and ukupno::numeric > 0
) as br_posjeta,
(select max(datum_racuna) from racuni where beauty_partner = 3157 and storno = ', CHR(39),CHR(39), 'NE', CHR(39),CHR(39), ' and ukupno::numeric > 0) as zadnji_posjet,
round(coalesce(sum(bd.kolicina * (bd.mpc - (bd.mpc * bd.rabat / 100))), 0), 2) as dug
from racuni r
left join beauty_dug bd on r.beauty_partner = bd.id_partner
where r.beauty_partner = ', id_partner, '
and bd.naplaceno_broj_racunom = 0', CHR(39), ')
       as t', rec.datname, '(
       br_posjeta bigint,
       zadnji_posjet timestamp without time zone,
       dug numeric
       )');

END LOOP;

            RETURN QUERY EXECUTE concat('select sum(br_posjeta)::bigint as br_posjeta, max(zadnji_posjet) as zadnji_posjet, sum(dug) as dug
from (', stmt, ') x');

            END
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;";

            classSQL.update(sql);

            sql = @"CREATE OR REPLACE FUNCTION selectOsobeDugDetaljno(IN id_partner integer, IN server character varying,
 OUT Datum timestamp without time zone, OUT id_djelatnik integer, OUT Izradio character varying, OUT Naziv character varying,
 OUT Kol numeric, OUT Cijena numeric, out vpc numeric, OUT nbc numeric, OUT pdv numeric, OUT sifra character varying,
 OUT porez_potrosnja numeric, OUT dod integer, OUT id_grupa integer, OUT id integer, OUT naplaceno_broj_racunom integer)
  RETURNS SETOF record AS
$BODY$
DECLARE
    REC RECORD;
            stmt text;
            BEGIN

            FOR REC IN
		SELECT datname FROM pg_database
		WHERE datistemplate = false
		and length(datname) = (SELECT length(current_database())) and
		substring(datname, 1, length(datname) - 4) = (SELECT substring(current_database(), 1, length(current_database()::varchar) - 4))
LOOP

    IF(LENGTH(stmt) > 0) THEN
          stmt := CONCAT(stmt, chr(10), 'UNION', chr(10));
            END IF;
            stmt:= CONCAT(stmt, 'select * from dblink(', CHR(39), 'host=', server, ' dbname=', rec.datname, ' user=postgres password=q1w2e3r4', CHR(39), ',', CHR(39),
             'select cast(bd.datum_duga as date) as datum, bd.id_djelatnik, CONCAT(z.ime, ', CHR(39), CHR(39), ' ', CHR(39), CHR(39), ', z.prezime) as izradio, roba.naziv as naziv, bd.kolicina as kol, bd.mpc as cijena, bd.vpc, bd.nbc, bd.pdv, bd.sifra, bd.porez_potrosnja, 0 as dod, bd.id_grupa, bd.id, bd.naplaceno_broj_racunom
from beauty_dug bd
left join roba on bd.sifra = roba.sifra
left join zaposlenici z on bd.id_djelatnik = id_zaposlenik
where bd.id_partner = ', id_partner, '
and bd.naplaceno_broj_racunom = 0
order by bd.datum_duga desc', CHR(39), ')
       as t', rec.datname, '(
       datum timestamp without time zone,
       id_djelatnik integer,
       izradio character varying,
       naziv character varying,
       kol numeric,
       cijena numeric,
       vpc numeric,
       nbc numeric,
       pdv numeric,
       sifra character varying,
       porez_potrosnja numeric,
       dod integer,
       id_grupa integer,
       id integer,
       naplaceno_broj_racunom integer
       )');

END LOOP;

            RETURN QUERY EXECUTE stmt;

            END
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;";

            classSQL.update(sql);

            DataRow[] REGISTRACIJA = DTcompact.Select("table_name = 'registracija'");
            if (REGISTRACIJA.Length == 0)
            {
                sql = @"create table registracija (
productKey nchar(36),
activationCode nchar(34)
)";
                classSQL.select_settings(sql, "registracija");
            }

            REGISTRACIJA = DTcompact.Select("table_name = 'registracija' and column_name='broj'");
            if (REGISTRACIJA.Length == 0)
            {
                sql = @"alter table registracija add column broj int;";
                classSQL.select_settings(sql, "registracija");
                sql = @"update registracija set broj = 0;";
                classSQL.select_settings(sql, "registracija");
            }

            DataRow[] Rpodaci = DTremote.Select("TABLE_NAME='roba_prodaja' AND COLUMN_NAME='kolicina'");

            if (Rpodaci.Length > 0)
            {
                if (Rpodaci[0]["CHARACTER_MAXIMUM_LENGTH"].ToString() == "10")
                {
                    string Qsql = "ALTER TABLE roba_prodaja ALTER COLUMN kolicina type character varying(30);";
                    classSQL.update(Qsql);
                }
            }

            Rpodaci = DTremote.Select("TABLE_NAME='roba_prodaja' AND COLUMN_NAME='nc'");

            if (Rpodaci.Length > 0 && Rpodaci[0]["data_type"].ToString() == "money")
            {
                string sql1 = @"ALTER TABLE roba_prodaja ALTER COLUMN nc TYPE numeric USING nc::numeric;";
                classSQL.insert(sql1);
            }

            Rpodaci = DTremote.Select("TABLE_NAME='racun_stavke' AND COLUMN_NAME='nbc'");

            if (Rpodaci.Length > 0)
            {
                if (Rpodaci[0]["data_type"].ToString() == "money")
                {
                    string Qsql = "ALTER TABLE racun_stavke ALTER COLUMN nbc type numeric;";
                    //classSQL.update(Qsql);
                }
            }

            string path = "DBbackup";
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Neke datoteke fale u Vašem programu, molimo kontaktirajte korisničku podršku iz Code-iT.\r\nFajlovi koji fale bitni su za sigurnost baze i za kreiranje nove godine.");
            }

            try
            {
                DataTable DTdodamPorez13 = classSQL.select("SELECT * FROM porezi", "porezi").Tables[0];
                if (DTdodamPorez13.Rows.Count > 0)
                {
                    bool porez_postoji = false;
                    foreach (DataRow RowPorez in DTdodamPorez13.Rows)
                    {
                        decimal porez;
                        decimal.TryParse(RowPorez["iznos"].ToString(), out porez);

                        if (porez == 13)
                        {
                            porez_postoji = true;
                        }
                    }

                    if (!porez_postoji)
                    {
                        classSQL.insert("INSERT INTO porezi (naziv,iznos) VALUES ('PDV 13','13');");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (DTremote.Select("table_name='promjena_cijene_auto'").Length == 0)
            {
                sql = "CREATE TABLE promjena_cijene_auto " +
                   "(" +
                     "id serial NOT NULL," +
                     "sifra character varying," +
                     "stara_cijena decimal," +
                     "nova_cijena decimal," +
                     "kolicina decimal," +
                     "porez decimal," +
                     "datum timestamp without time zone," +
                     "poslovnica character varying," +
                     "kalkulacija int," +
                     "id_skladiste integer," +
                     "novo boolean DEFAULT '1'," +
                     "editirano boolean DEFAULT '0'," +
                     "CONSTRAINT promjena_cijene_auto_pkey PRIMARY KEY (id )" +
                   ")";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='na_stol_naplaceno'").Length == 0)
            {
                sql = $@"CREATE TABLE na_stol_naplaceno(
                      id_stol integer,
                      sifra character varying(25),
                      broj_narudzbe character varying(7),
                      kom numeric,
                      id_poslovnica integer,
                      id_skladiste character varying(100),
                      mpc numeric,
                      vpc numeric,
                      porez numeric,
                      porez_potrosnja numeric,
                      br numeric,
                      jelo character varying(50),
                      skinuto integer,
                      id_zaposlenik integer,
                      dod integer,
                      pol integer,
                      id_adresa_dostave numeric,
                      rabat numeric,
                      id_foreign_key_na_stol int
                      )";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='medu_poslovnice'").Length == 0)
            {
                sql = "CREATE TABLE medu_poslovnice " +
                   "(" +
                     "id serial NOT NULL," +
                     "sifra character varying," +
                     "mpc decimal," +
                     "pdv decimal," +
                     "kolicina decimal," +
                     "pnp decimal," +
                     "pp decimal," +
                     "id_skladiste integer," +
                     "broj int," +
                     "godina int," +
                     "datum timestamp without time zone," +
                     "iz_poslovnice character varying," +
                     "u_poslovnicu character varying," +
                     "id_izradio int," +
                     "napomena text," +
                     "novo_izskl boolean DEFAULT '1'," +
                     "novo_uskl boolean DEFAULT '0'," +
                     "CONSTRAINT medu_poslovnice_auto_pkey PRIMARY KEY (id )" +
                   ");";
                classSQL.insert(sql);
            }

            //----------------------------------------------adresa_dostave----------------------------------------

            if (DTremote.Select("table_name='adresa_dostave'").Length == 0)
            {
                sql = "CREATE TABLE adresa_dostave " +
                   "(" +
                     "id serial NOT NULL," +
                     "mjesto character varying," +
                     "ulica character varying," +
                     "kbr character varying," +
                     "telefon character varying," +
                     "editirano boolean DEFAULT '0'," +
                     "novo boolean DEFAULT '1'," +
                     "datum_syn timestamp without time zone," +
                     "CONSTRAINT adresa_dostave_primary_key PRIMARY KEY (id)" +
                   ");";

                classSQL.insert(sql);
            }
            //DTremote.Select("table_name = 'izdatnica'");
            if (DTremote.Select("table_name = 'izdatnica'").Length == 0)
            {
                sql = "CREATE TABLE izdatnica" +
                   "(broj bigint NOT NULL," +
                   "id_partner bigint," +
                   "originalni_dokument character varying(100)," +
                   "id_izradio integer," +
                   "datum timestamp without time zone," +
                   "napomena text," +
                   "id_skladiste integer," +
                   "id_izdatnica serial NOT NULL," +
                   "godina character varying(6)," +
                   "id_mjesto integer," +
                   "CONSTRAINT izdatnica_pkey PRIMARY KEY (id_izdatnica))";
                classSQL.select(sql, "izdatnica");
            }

            if (DTremote.Select("table_name = 'izdatnica' AND column_name='editirano'").Length == 0)
            {
                sql = @"alter table izdatnica add column editirano boolean default false;
alter table izdatnica add column novo boolean default true;";
                classSQL.select(sql, "izdatnica");
            }

            //dataROW = DTremote.Select("table_name = 'izdatnica_stavke'");
            if (DTremote.Select("table_name = 'izdatnica_stavke'").Length == 0)
            {
                sql = "CREATE TABLE izdatnica_stavke" +
                   "(sifra character varying(25)," +
                   "vpc numeric," +
                   "mpc numeric," +
                   "rabat character varying(15)," +
                   "broj bigint," +
                   "kolicina character(15)," +
                   "nbc numeric," +
                   "id_stavka serial NOT NULL," +
                   "pdv character varying(5)," +
                   "id_izdatnica integer NOT NULL," +
                   "ukupno numeric," +
                   "iznos numeric," +
                   "CONSTRAINT izdatnica_stavke_pkey PRIMARY KEY (id_stavka));";
                classSQL.select(sql, "izdatnica_stavke");
            }

            DataTable DTnaStol = classSQL.select("SELECT * FROM na_stol LIMIT 1", "fakture").Tables[0];
            {
                if (DTnaStol.Columns["id_adresa_dostave"] == null)
                {
                    sql = "ALTER TABLE na_stol ADD COLUMN id_adresa_dostave numeric DEFAULT null;";
                    classSQL.select(sql, "na_stol");
                }
            }

            DataTable DTracun = classSQL.select("SELECT * FROM racuni LIMIT 1", "fakture").Tables[0];
            {
                if (DTracun.Columns["id_adresa_dostave"] == null)
                {
                    sql = "ALTER TABLE racuni ADD COLUMN id_adresa_dostave numeric DEFAULT null;";
                    classSQL.select(sql, "na_stol");
                }
            }

            DataTable DTnapomene = classSQL.select("SELECT * FROM napomene LIMIT 1", "podgrupa").Tables[0];
            {
                if(DTnapomene.Columns["id_podgrupa"] == null)
                {
                    sql = "ALTER TABLE napomene ADD COLUMN id_podgrupa numeric DEFAULT 2;";
                    classSQL.select(sql, "napomene");
                }
            }

            DataTable DTotpremnicaStavke = classSQL.select("SELECT * FROM otpremnica_stavke LIMIT 1", "otpremnica_stavke").Tables[0];
            {
                if (DTotpremnicaStavke.Columns["naplaceno_fakturom"] == null)
                {
                    /*sql = @"alter table otpremnica_stavke
	                        alter column kolicina type numeric(15,6) using replace(kolicina, ',','.')::numeric,
	                        alter column porez type numeric(6,3) using replace(porez, ',','.')::numeric,
	                        alter column rabat type numeric(15,6) using replace(rabat, ',','.')::numeric,
	                        alter column godina_otpremnice type integer using replace(godina_otpremnice, ',','.')::integer,
	                        alter column nbc type numeric(15,6),
	                        alter column porez_potrosnja type numeric(15,6) using replace(porez_potrosnja,',','.')::numeric,
                            alter table otpremnica_stavke add column naplaceno_fakturom boolean default false;";*/
                    sql = @"alter table otpremnica_stavke add column naplaceno_fakturom boolean default false;";
                    classSQL.select(sql, "otpremnica_stavke");
                }
            }

            //---------------------------------------------------end adresa_dostave------------------------------------

            // ------------------------------------------------- karticakupci_racuni --------------------------------

            if (DTremote.Select("table_name = 'karticakupci_racuni'").Length == 0)
            {
                sql = "CREATE TABLE karticakupci_racuni " +
                                "( " +
                                  "id serial NOT NULL, " +
                                  "oib character varying(11), " +
                                  "poslovnica character varying(30), " +
                                  "naplatni_uredaj character varying(30), " +
                                  "kartica_kupca character varying(10), " +
                                  "broj_racuna bigint, " +
                                  "datum_racun timestamp without time zone, " +
                                  "iznos numeric, " +
                                  "CONSTRAINT karticakupci_racuni_primary_key PRIMARY KEY (id) " +
                                ")";

                classSQL.select(sql, "karticaKupci_racuni");
            }
            if (DTremote.Select("table_name='usklada_robe'").Length == 0)
            {
                sql= "CREATE TABLE usklada_robe(id_usklade integer NOT NULL,datum timestamp with time zone,godina character varying,izradio integer,primka_id integer,izdatnica_id integer,"+
                     "napomena character varying,zakljuceno integer,obrisano integer,novo boolean,editirano boolean,CONSTRAINT PK_uskladaRobe PRIMARY KEY(id_usklade),"+
                     "CONSTRAINT FK_uskladaRobe_Izradio FOREIGN KEY(izradio) REFERENCES zaposlenici(id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION,"+
                     "CONSTRAINT FK_uskladaRobe_izdatnica FOREIGN KEY(izdatnica_id) REFERENCES izdatnica(id_izdatnica) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION,"+
                     "CONSTRAINT FK_uskladaRobe_primka FOREIGN KEY(primka_id) REFERENCES primka(id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION)";
                classSQL.select(sql, "UskladaRobe");
            }
            if (DTremote.Select("table_name='usklada_robe_stavke'").Length == 0)
            {
                sql = "CREATE TABLE usklada_robe_stavke(usklada_id integer,roba_id integer,usklada_idpk serial," +
                      "nova_kolicina character varying(30),stara_kolicina character varying(30),CONSTRAINT PK_uskladeStavke PRIMARY KEY(usklada_idpk)," +
                      "CONSTRAINT FK_uskladaRobeStavke_usklada FOREIGN KEY(usklada_id) REFERENCES usklada_robe(id_usklade) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION)";
                classSQL.select(sql, "UskladaRobeStavke");
            }

            if (DTremote.Select("table_name = 'napomene'").Length == 0)
            {
                sql = "CREATE TABLE napomene " +
                                "( " +
                                  "id serial NOT NULL, " +
                                  "napomena character varying(80), " +
                                  "aktivno boolean DEFAULT '1', " +
                                  "editirano boolean DEFAULT '0', " +
                                  "novo boolean DEFAULT '1', " +
                                  "CONSTRAINT napomene_primary_key PRIMARY KEY (id) " +
                                ")";

                classSQL.select(sql, "napomene");
            }



            if (DTremote.Select("table_name = 'grupe'").Length == 0)
            {
                sql = "CREATE TABLE grupe " +
                                    "( " +
                              "id_grupa integer PRIMARY KEY,"+
                              "grupa character varying(50)," +
                              "opis character varying(150)," +
                              "id_podgrupa integer," +
                              "aktivnost smallint," +
                              "editirano boolean DEFAULT false," +
                              "novo boolean DEFAULT true," +
                              "is_dodatak boolean DEFAULT false," +
                              "printer3 integer DEFAULT 0," +
                             " is_polpol boolean DEFAULT false,"+
                                ")";
                classSQL.select(sql, "grupe");
            }

            if (DTremote.Select("table_name = 'beauty_dug'").Length == 0)
            {
                sql = @"alter table racun_stavke add column id_izradio integer default 0;
alter table racuni add column beauty_partner integer default 0;

create table beauty_dug
(
	id serial not null PRIMARY KEY,
	id_ducan integer not null,
	id_blagajna integer not null,
	godina integer not null,
	id_izradio integer default 0,
	id_partner integer not null,
	datum_duga timestamp without time zone,
	sifra character varying (50),
	id_grupa integer,
	id_skladiste integer,
	id_djelatnik integer default 0,
	kolicina numeric(15,6) default 0,
	nbc numeric(15,6) default 0,
	vpc numeric(15,6) default 0,
	pdv numeric(6,2) default 0,
	mpc numeric(15,6) default 0,
	rabat numeric(12,6) default 0,
	porez_potrosnja numeric(6,2) default 0,
	povratna_naknada numeric(7,3) default 0,
	naplaceno_broj_racunom integer default 0,
	naplaceno_id_ducan integer default 0,
	naplaceno_id_blagajna integer default 0
);";

                classSQL.select(sql, "beauty_dug");
            }

            DataTable DTkarticakupca_racuni = classSQL.select("SELECT * FROM karticakupci_racuni LIMIT 1", "karticaKupci_racuni").Tables[0];
            {
                if (DTkarticakupca_racuni.Columns["bodovi"] == null)
                {
                    //string sss = "ALTER TABLE roba ADD COLUMN bodovi numeric(15,2)";
                    sql = "ALTER TABLE karticakupci_racuni ADD COLUMN bodovi numeric default 0;";
                    classSQL.select(sql, "karticaKupci_racuni");

                    //classSQL.select_settings(sql, "karticakupci_racuni");
                    //classSQL.update("UPDATE roba SET border_color='0'");
                }
            }

            //------------------------------------------------- end karticakupci_racuni ----------------------------
            //----------------------------------- create table kartice ---------------------------------------------
            if (DTremote.Select("table_name='kartice'").Length == 0)
            {
                sql = "CREATE TABLE kartice " +
                   "(" +
                     "id serial NOT NULL," +
                     "naziv character varying," +
                     "aktivnost boolean DEFAULT '1'," +
                     "novo boolean DEFAULT '1'," +
                     "editirano boolean DEFAULT '0'," +
                     "datum_syn timestamp without time zone," +
                     "CONSTRAINT kartice_auto_pkey PRIMARY KEY (id )" +
                   ")";
                classSQL.insert(sql);
            }
            //---------------------------- end create table kartice -----------------------------------------------
            DataTable DTFakture = classSQL.select("SELECT * FROM fakture LIMIT 1", "fakture").Tables[0];
            {
                if (DTFakture.Columns["id_kasa"] == null)
                {
                    sql = "ALTER TABLE fakture ADD COLUMN id_kasa numeric DEFAULT 0;";
                    classSQL.select(sql, "fakture");
                }

                if (DTFakture.Columns["id_ducan"] == null)
                {
                    sql = "ALTER TABLE fakture ADD COLUMN id_ducan NUMERIC DEFAULT 0;";
                    classSQL.select(sql, "fakture");
                }

                if (DTFakture.Columns["editirano"] == null)
                {
                    sql = "ALTER TABLE fakture ADD COLUMN editirano boolean DEFAULT false;";
                    classSQL.select(sql, "fakture");
                }

                if (DTFakture.Columns["novo"] == null)
                {
                    sql = "ALTER TABLE fakture ADD COLUMN novo boolean DEFAULT true;";
                    classSQL.select(sql, "fakture");
                }
            }

            DataTable DTOtpremnice = classSQL.select("SELECT * FROM otpremnice LIMIT 1", "otpremnice").Tables[0];
            {
                //if (DTOtpremnice.Columns["id_kasa"] == null) {
                //    sql = "ALTER TABLE fakture ADD COLUMN id_kasa numeric DEFAULT 0;";
                //    classSQL.select(sql, "fakture");
                //}

                //if (DTOtpremnice.Columns["id_ducan"] == null) {
                //    sql = "ALTER TABLE fakture ADD COLUMN id_ducan NUMERIC DEFAULT 0;";
                //    classSQL.select(sql, "fakture");
                //}

                if (DTOtpremnice.Columns["editirano"] == null)
                {
                    sql = "ALTER TABLE otpremnice ADD COLUMN editirano boolean DEFAULT false;";
                    classSQL.select(sql, "fakture");
                }

                if (DTOtpremnice.Columns["novo"] == null)
                {
                    sql = "ALTER TABLE otpremnice ADD COLUMN novo boolean DEFAULT true;";
                    classSQL.select(sql, "fakture");
                }

                if (DTOtpremnice.Columns["na_sobu"] == null)
                {
                    sql = "ALTER TABLE otpremnice ADD COLUMN na_sobu boolean DEFAULT false;";
                    classSQL.select(sql, "fakture");
                }

                if (DTOtpremnice.Columns["naplaceno_fakturom"] == null)
                {
                    sql = "ALTER TABLE otpremnice ADD COLUMN naplaceno_fakturom int DEFAULT 1;";
                    classSQL.select(sql, "fakture");
                }

                if (DTOtpremnice.Columns["rfaktura_broj"] == null)
                {
                    sql = @"alter table otpremnice add column rfaktura_broj int default 0;
                            alter table otpremnice add column rfaktura_poslovnica int default 0;
                            alter table otpremnice add column rfaktura_naplatni_uredaj int default 0; ";
                    classSQL.select(sql, "otpremnice");
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
                    classSQL.select(sqlagencija, "sobe");
                    classSQL.insert("INSERT INTO sobe (broj_sobe,id_tip_sobe,naziv_sobe,broj_lezaja,napomena,cijena_nocenja,aktivnost) VALUES ('1','1','Soba broj 1','2','','100','1')");
                    classSQL.insert("INSERT INTO sobe (broj_sobe,id_tip_sobe,naziv_sobe,broj_lezaja,napomena,cijena_nocenja,aktivnost) VALUES ('2','1','Soba broj 2','3','','100','1')");
                }
            }

            DataTable DTcaffe_narudzbe = classSQL.select("select * from caffe_narudzbe limit 1;", "caffe_narudzbe").Tables[0];
            {
                if (DTcaffe_narudzbe.Columns["broj_narudzbe"] == null)
                {
                    sql = "ALTER TABLE caffe_narudzbe ADD COLUMN broj_narudzbe bigint default 0;";
                    classSQL.select(sql, "caffe_narudzbe");
                }
                //sql = "ALTER TABLE broj_narudzbe DROP COLUMN mjesto, ulica, kbr;";
                //classSQL.select(sql, "caffe_narudzbe");
                //sql = "ALTER TABLE caffe_narudzbe DROP COLUMN mjesto;";
                //classSQL.select(sql, "caffe_narudzbe");
                //sql = "ALTER TABLE caffe_narudzbe DROP COLUMN ulica;";
                //classSQL.select(sql, "caffe_narudzbe");
                //sql = "ALTER TABLE caffe_narudzbe DROP COLUMN kbr;";
                //classSQL.select(sql, "caffe_narudzbe");

                if (DTcaffe_narudzbe.Columns["mjesto"] == null)
                {
                    sql = "ALTER TABLE caffe_narudzbe ADD COLUMN mjesto character varying(100) default null;";
                    classSQL.select(sql, "caffe_narudzbe");
                }

                if (DTcaffe_narudzbe.Columns["ulica"] == null)
                {
                    sql = "ALTER TABLE caffe_narudzbe ADD COLUMN ulica character varying(100) default null;";
                    classSQL.select(sql, "caffe_narudzbe");
                }

                if (DTcaffe_narudzbe.Columns["kbr"] == null)
                {
                    sql = "ALTER TABLE caffe_narudzbe ADD COLUMN kbr numeric default 0;";
                    classSQL.select(sql, "caffe_narudzbe");
                }
                else
                {
                    sql = "ALTER TABLE caffe_narudzbe ALTER COLUMN kbr TYPE character varying(20);";
                    classSQL.select(sql, "caffe_narudzbe");
                }

                if (DTcaffe_narudzbe.Columns["telefon"] == null)
                {
                    sql = "ALTER TABLE caffe_narudzbe ADD COLUMN telefon character varying(50) default null;";
                    classSQL.select(sql, "caffe_narudzbe");
                }

                if (DTcaffe_narudzbe.Columns["id_ducan"] == null)
                {
                    sql = "ALTER TABLE caffe_narudzbe ADD COLUMN id_ducan integer default 0;";
                    classSQL.select(sql, "caffe_narudzbe");
                }
                //if (DTcaffe_narudzbe.Columns["broj_narudzbe"] == null) {
                //    sql = "ALTER TABLE caffe_narudzbe ADD COLUMN broj_narudzbe bigint default 0;";
                //    classSQL.select(sql, "caffe_narudzbe");
                //}
                //if (DTcaffe_narudzbe.Columns["broj_narudzbe"] == null) {
                //    sql = "ALTER TABLE caffe_narudzbe ADD COLUMN broj_narudzbe bigint default 0;";
                //    classSQL.select(sql, "caffe_narudzbe");
                //}
            }

            DataTable DTskladiste = classSQL.select("SELECT * FROM skladiste LIMIT 1", "skladiste").Tables[0];
            {
                if (DTskladiste.Columns["editirano"] == null)
                {
                    //string sss = "ALTER TABLE roba ADD COLUMN bodovi numeric(15,2)";
                    sql = "ALTER TABLE skladiste ADD COLUMN editirano boolean DEFAULT true;";
                    classSQL.select(sql, "skladiste");

                    //classSQL.select_settings(sql, "karticakupci_racuni");
                    //classSQL.update("UPDATE roba SET border_color='0'");
                }

                if(DTskladiste.Columns["kalkulacija"] == null)
                {
                    sql = "ALTER TABLE skladiste ADD COLUMN kalkulacija character varying DEFAULT 'NE'";
                    classSQL.select(sql, "skladiste");
                }
            }

            if (DTremote.Select("table_name='medu_poslovnice' AND column_name='nbc'").Length == 0)
            {
                sql = "ALTER TABLE medu_poslovnice RENAME COLUMN mpc to nbc;" +
                              "ALTER TABLE medu_poslovnice ADD COLUMN mpc numeric DEFAULT '0';";
                classSQL.insert(sql);

                sql = @"DROP FUNCTION IF EXISTS UzmiProdajnuCijenuPremaRepromaterijalu(character varying,timestamp without time zone);
                        CREATE OR REPLACE FUNCTION UzmiProdajnuCijenuPremaRepromaterijalu(_sifra character varying,_datum timestamp without time zone) RETURNS decimal AS
                        $BODY$
                        DECLARE
                          _mpc numeric;
                        BEGIN

                        _mpc=(CASE
	                        WHEN
		                        COALESCE((SELECT primka_stavke.prodajna_cijena_sa_porezom FROM primka_stavke
		                        LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke
		                        WHERE primka_stavke.is_kalkulacija='1'
		                        AND primka.datum<=_datum AND primka_stavke.sifra=_sifra LIMIT 1),0)
		                        <> 0 THEN
		                        COALESCE((SELECT primka_stavke.prodajna_cijena_sa_porezom FROM primka_stavke
		                        LEFT JOIN primka ON primka.broj_primke=primka_stavke.broj_primke
		                        WHERE primka_stavke.is_kalkulacija='1'
		                        AND primka.datum<=_datum AND primka_stavke.sifra=_sifra LIMIT 1),0)
	                        ELSE
		                        COALESCE((SELECT mpc::numeric FROM roba WHERE roba.sifra=_sifra LIMIT 1),0)
	                        END);

                        RETURN _mpc;

                        END
                        $BODY$ LANGUAGE plpgsql;";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='poslovnice'").Length == 0)
            {
                sql = "CREATE TABLE poslovnice " +
                   "(" +
                     "id serial NOT NULL," +
                     "adresa character varying," +
                     "aktivno int," +
                     "fiskalna_oznaka_poslovnice character varying," +
                     "grad_opcina character varying," +
                     "iban character varying," +
                     "ispostava character varying," +
                     "knjigovodstvo decimal," +
                     "najam decimal," +
                     "naziv_poslovnice character varying," +
                     "oib character varying," +
                     "osoblje character varying," +
                     "podrucni_ured character varying," +
                     "CONSTRAINT poslovnice_auto_pkey PRIMARY KEY (id )" +
                   ");";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='blagajnicki_izvjestaj'").Length == 0)
            {
                sql = "CREATE TABLE blagajnicki_izvjestaj " +
                   "(" +
                     "id serial NOT NULL," +
                     "datum timestamp without time zone," +
                     "dokumenat character varying," +
                     "oznaka_dokumenta character varying," +
                     "uplaceno decimal," +
                     "izdatak decimal," +
                     "novo boolean DEFAULT '1'," +
                     "editirano boolean DEFAULT '0'," +
                     "CONSTRAINT blagajnicki_izvjestaj_auto_pkey PRIMARY KEY (id )" +
                   ")";
                classSQL.insert(sql);
            }

            DataRow[] a = DTremote.Select("table_name = 'blagajnicki_izvjestaj' and column_name = 'id_partner'");
            if (a.Length == 0)
            {
                sql = "ALTER TABLE blagajnicki_izvjestaj add column id_partner bigint default null;";
                classSQL.select(sql, "izdatnica_stavke");
            }

            if (DTremote.Select("table_name = 'blagajnicki_izvjestaj' and column_name = 'rb'").Length == 0)
            {
                sql = "ALTER TABLE blagajnicki_izvjestaj add column rb bigint default 0;";
                classSQL.select(sql, "blagajnicki_izvjestaj");

                sql = string.Format(@"SELECT ROW_NUMBER() OVER(PARTITION BY uplata ORDER BY datum ASC) as rb, * FROM
(
    SELECT
CASE WHEN dokumenat in ('POČETNO STANJE', 'POZAJMNICA', 'POCETNI POLOG BLAGAJNE')
    THEN 1
    ELSE 0 END as uplata,

    id, datum, dokumenat,
    CASE WHEN oznaka_dokumenta is null OR length(oznaka_dokumenta) = 0
    THEN partners.ime_tvrtke
    ELSE concat(oznaka_dokumenta, ' - ', partners.ime_tvrtke)
    END AS oznaka_dokumenta,
    CASE WHEN dokumenat in ('POČETNO STANJE', 'POZAJMNICA', 'POCETNI POLOG BLAGAJNE')
    THEN uplaceno
    ELSE '0' END as uplaceno, izdatak
    FROM blagajnicki_izvjestaj
    LEFT JOIN partners ON blagajnicki_izvjestaj.id_partner = partners.id_partner
    WHERE dokumenat <> 'PROMET BLAGAJNE'

    UNION ALL

    SELECT 1 as uplata, '-1' as id,date_trunc('day', datum_racuna) as datum,'PROMET BLAGAJNE' as dokumenat,
    concat(MIN(CAST(racuni.broj_racuna AS INT)), '-', MAX(CAST(racuni.broj_racuna AS INT))) AS oznaka_dokumenta,

    SUM((CAST(racun_stavke.mpc AS NUMERIC) - (CAST(racun_stavke.mpc AS NUMERIC) * CAST(REPLACE(racun_stavke.rabat, ',', '.') AS NUMERIC) / 100)) * CAST(REPLACE(racun_stavke.kolicina, ',', '.') AS NUMERIC)) AS uplaceno, '0' as izdatak

    FROM racuni
    LEFT JOIN racun_stavke ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan = racun_stavke.id_ducan AND racuni.id_kasa = racun_stavke.id_blagajna
    WHERE(racuni.nacin_placanja = 'G' OR racuni.nacin_placanja is null)
    GROUP BY date_trunc('day', datum_racuna)
)
bm
WHERE cast(datum as date) >= '{0}-01-01' AND cast(datum as date) <= '{0}-12-31'
ORDER BY datum ASC;", Util.Korisno.GodinaKojaSeKoristiUbazi);
                DataSet dsBlagajnicki = classSQL.select(sql, "blagajnicki_izvjestaj");
                if (dsBlagajnicki != null && dsBlagajnicki.Tables.Count > 0 && dsBlagajnicki.Tables[0] != null && dsBlagajnicki.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsBlagajnicki.Tables[0].Rows)
                    {
                        int id = -1, rb = 0;
                        if (int.TryParse(dRow["id"].ToString(), out id))
                        {
                            int.TryParse(dRow["rb"].ToString(), out rb);

                            if (id == -1)
                            {
                                sql = string.Format(@"INSERT INTO blagajnicki_izvjestaj(
        rb, datum, dokumenat, oznaka_dokumenta, uplaceno, izdatak)
VALUES ({0}, '{1}', '{2}', '{3}', {4}, {5});",
rb,
((DateTime)dRow["datum"]).ToString("yyyy-MM-dd HH:mm:ss"),
dRow["dokumenat"],
dRow["oznaka_dokumenta"],
dRow["uplaceno"].ToString().Replace(',', '.'),
dRow["izdatak"].ToString().Replace(',', '.')
);
                            }
                            else
                            {
                                sql = string.Format(@"update blagajnicki_izvjestaj
set rb = {0}, editirano = '1'
where id = {1};", rb, id);
                            }
                            classSQL.update(sql);
                        }
                    }
                }
            }

            if (DTremote.Select("table_name='roba' AND column_name='nbc'").Length == 0)
            {
                sql = "ALTER TABLE roba ADD COLUMN nbc numeric DEFAULT '0';";
                classSQL.insert(sql);
            }

            try
            {
                int ss;
                DataRow[] row = DTcompact.Select("table_name = 'podaci_tvrtka' and column_name = 'skraceno_ime'");

                if (row.Length > 0)
                {
                    int.TryParse(row[0]["CHARACTER_MAXIMUM_LENGTH"].ToString(), out ss);
                    if (ss < 300)
                    {
                        classSQL.select_settings("alter table podaci_tvrtka alter column skraceno_ime ntext", "podaci_tvrtka");
                        classSQL.update("alter table podaci_tvrtka alter column skraceno_ime type character varying");
                    }
                }
            }
            catch { }

            //if (DTremote.Select("table_name='inventura' AND column_name='postavi_kao_pocetno_stanje'").Length == 0)
            //{
            //    sql = "ALTER TABLE inventura ADD COLUMN postavi_kao_pocetno_stanje INT DEFAULT '0';";
            //    classSQL.insert(sql);

            //    sql = "ALTER TABLE inventura ADD COLUMN postavi_kao_trenutno_stanje INT DEFAULT '0';";
            //    classSQL.insert(sql);

            //    sql = "UPDATE inventura SET postavi_kao_pocetno_stanje = '1' WHERE broj_inventure='1';";
            //    classSQL.insert(sql);

            //}

            if (DTremote.Select("table_name='grupa' AND column_name='is_dodatak'").Length == 0)
            {
                sql = "ALTER TABLE grupa ADD COLUMN is_dodatak boolean DEFAULT '0';";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='grupa' AND column_name='is_polpol'").Length == 0)
            {
                sql = "ALTER TABLE grupa ADD COLUMN is_polpol boolean DEFAULT '0';";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='grupa' AND column_name='printer3'").Length == 0)
            {  
                sql = "ALTER TABLE grupa ADD COLUMN printer3 INT DEFAULT 0;";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='partners' AND column_name='novo'").Length == 0)
            {
                sql = "ALTER TABLE partners ADD COLUMN novo numeric DEFAULT '1';";
                classSQL.insert(sql);

                sql = "ALTER TABLE partners ADD COLUMN editirano numeric DEFAULT '0';";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='partners' AND column_name='kartica_kupca'").Length == 0)
            {
                string _sql = "ALTER TABLE partners ADD COLUMN kartica_kupca character varying(10) default null;";
                classSQL.insert(_sql);
            }
            if (DTremote.Select("table_name='partners' AND column_name = 'spol'").Length == 0)
            {
                string _sql = @"alter table partners add column spol character varying(1) default 'O';
alter table partners add column zanimanje character varying (250);
alter table partners add column datum_upisa timestamp without time zone;
alter table partners add column trudnoca character varying (500);
alter table partners add column kontracepcija character varying (500);
alter table partners add column hormonalni_nadomjestak character varying (500);";
                classSQL.insert(_sql);
            }

            if (DTremote.Select("table_name='pocetno' AND column_name='prodajna_cijena'").Length == 0)
            {
                sql = "ALTER TABLE pocetno ADD COLUMN prodajna_cijena numeric DEFAULT '0';";
                classSQL.insert(sql);

                sql = "ALTER TABLE povrat_robe_stavke ADD COLUMN prodajna_cijena numeric DEFAULT '0';";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='povrat_robe_stavke' AND column_name='kolicina'").Length != 0)
            {
                sql = "ALTER TABLE povrat_robe_stavke ALTER COLUMN kolicina TYPE character varying(20);";
                classSQL.insert(sql);

                //sql = "ALTER TABLE povrat_robe_stavke ADD COLUMN prodajna_cijena numeric DEFAULT '0';";
                //classSQL.insert(sql);
            }

            //id_ducan='" + Util.Korisno.idDucan + "' AND smjene.id_kasa
            if (DTremote.Select("table_name='smjene' AND column_name='id_ducan'").Length == 0)
            {
                try
                {
                    DataSet DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");
                    string id_kasa = DSpostavke.Tables[0].Rows[0]["default_blagajna"].ToString();
                    string id_ducan = DSpostavke.Tables[0].Rows[0]["default_ducan"].ToString();

                    sql = "ALTER TABLE smjene ADD COLUMN id_ducan int DEFAULT '" + id_ducan + "';";
                    classSQL.insert(sql);

                    sql = "ALTER TABLE smjene ADD COLUMN id_kasa int DEFAULT '" + id_kasa + "';";
                    classSQL.insert(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            if (DTremote.Select("table_name='pocetno' AND column_name='povratna_naknada'").Length == 0)
            {
                sql = "ALTER TABLE pocetno ADD COLUMN povratna_naknada numeric DEFAULT '0';";
                classSQL.insert(sql);

                //sql = "ALTER TABLE pocetno ADD COLUMN  numeric DEFAULT '0';";
                //classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='inventura_stavke' AND column_name='povratna_naknada'").Length == 0)
            {
                sql = @"ALTER TABLE inventura_stavke ADD COLUMN povratna_naknada numeric DEFAULT '0';
                                ALTER TABLE inventura_stavke ADD COLUMN mpc numeric DEFAULT '0';";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='inventura' AND column_name='zakljucano'").Length == 0)
            {
                sql = @"ALTER TABLE inventura ADD COLUMN zakljucano int DEFAULT '0';";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='inventura' AND column_name='obrisano'").Length == 0)
            {
                sql = @"ALTER TABLE inventura ADD COLUMN obrisano int DEFAULT '0';";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='promjena_cijene' AND column_name='kalkulacija'").Length == 0)
            {
                sql = @"ALTER TABLE promjena_cijene ADD COLUMN kalkulacija integer;";
                classSQL.insert(sql);
            }
            if (DTremote.Select("table_name='promjena_cijene' AND column_name='novo'").Length == 0)
            {
                sql = @"ALTER TABLE promjena_cijene ADD COLUMN novo boolean DEFAULT true;";
                classSQL.insert(sql);
            }
            if (DTremote.Select("table_name='promjena_cijene' AND column_name='editirano'").Length == 0)
            {
                sql = @"ALTER TABLE promjena_cijene ADD COLUMN editirano boolean DEFAULT true;";
                classSQL.insert(sql);
            }
            if (DTremote.Select("table_name='promjena_cijene_stavke' AND column_name='kolicina'").Length == 0)
            {
                sql = @"ALTER TABLE promjena_cijene_stavke ADD COLUMN kolicina numeric;";
                classSQL.insert(sql);
              //  Sinkronizacija.pomagala_syn Pomagala = new pomagala_syn();
                sql = "SELECT * FROM promjena_cijene WHERE oib = '" + Util.Korisno.oibTvrtke + "' and poslovnica = '" + Util.Korisno.getPoslovnicaNaziv() + "';";
               // DataTable DTWeb = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, Util.Korisno.domena_za_sinkronizaciju + "uzmi_podatke_xml/web_request.php");

               /* sql = "";
                if (DTWeb != null && DTWeb.Rows.Count > 0)
                {
                    foreach (DataRow dr in DTWeb.Rows)
                    {
                        sql += "UPDATE promjena_cijene_stavke SET kolicina = '" + dr["kolicina"] + "' WHERE broj = '" + dr["broj"] + "' AND sifra = '" + dr["sifra"] + "';\n";
                    }
                    classSQL.insert(sql);
                }*/
            }

            //zakljucavanje dokumenata
            if (DTremote.Select("table_name='primka' AND column_name='zakljucano'").Length == 0)
            {
                sql = @"ALTER TABLE primka ADD COLUMN zakljucano boolean DEFAULT false;";
                classSQL.insert(sql);
            }
            if (DTremote.Select("table_name='medu_poslovnice' AND column_name='zakljucano'").Length == 0)
            {
                sql = @"ALTER TABLE medu_poslovnice ADD COLUMN zakljucano boolean DEFAULT false;";
                classSQL.insert(sql);
            }
            if (DTremote.Select("table_name='povrat_robe' AND column_name='zakljucano'").Length == 0)
            {
                sql = @"ALTER TABLE povrat_robe ADD COLUMN zakljucano boolean DEFAULT false;";
                classSQL.insert(sql);
            }
            if (DTremote.Select("table_name='na_stol' AND column_name='id'").Length == 0)
            {
                sql = @"ALTER TABLE na_stol ADD COLUMN id SERIAL";
                classSQL.insert(sql);
            }
            if (DTremote.Select("table_name='promjena_cijene' AND column_name='zakljucano'").Length == 0)
            {
                sql = @"ALTER TABLE promjena_cijene ADD COLUMN zakljucano boolean DEFAULT false;";
                classSQL.insert(sql);
            }
            //kraj zakljucavanja dokumenata

            if (DTremote.Select("table_name='partners_bon'").Length == 0)
            {
                sql = @"CREATE TABLE partners_bon
(
	id serial NOT NULL,
	id_partner bigint,
	datum timestamp without time zone,
	bon boolean default true,
	iznos numeric(15,6),
  CONSTRAINT partners_bon_primary_key PRIMARY KEY (id)
)";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='racuni' AND column_name='popust_cijeli_racun'").Length == 0)
            {
                sql = @"ALTER TABLE racuni ADD COLUMN popust_cijeli_racun numeric DEFAULT '0';";
                classSQL.insert(sql);
            }
            if (DTremote.Select("table_name='racuni' AND column_name='popust_racun_kartica_kupca'").Length == 0)
            {
                sql = @"ALTER TABLE racuni ADD COLUMN popust_racun_kartica_kupca numeric DEFAULT '0';";
                classSQL.insert(sql);
            }
            if (DTremote.Select("table_name='racuni' AND column_name='karticaID'").Length == 0)
            {
                sql = @"ALTER TABLE racuni ADD COLUMN karticaID numeric DEFAULT '0';";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='zid'").Length == 0)
            {
                sql = "CREATE TABLE zid " +
                "(" +
                 "id serial NOT NULL," +
                 "x_pozicija int," +
                 "y_pozicija int," +
                 "height int," +
                 "width int," +
                 "CONSTRAINT zid_pkey PRIMARY KEY (id )" +
               ")";
                classSQL.insert(sql);
            }

            //---------------------------------------RACUNI------------------------------------
            if (DTremote.Select("table_name='racuni' AND column_name='editirano'").Length == 0)
            {
                sql = "ALTER TABLE racuni ADD COLUMN editirano boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE racuni SET editirano='0'");
            }

            if (DTremote.Select("table_name='racuni' AND column_name='novo'").Length == 0)
            {
                sql = "ALTER TABLE racuni ADD COLUMN novo boolean DEFAULT '1';";
                classSQL.insert(sql);
                classSQL.update("UPDATE racuni SET novo='1'");
            }

            if (DTremote.Select("table_name='racuni' AND column_name='id'").Length == 0)
            {
                sql = " ALTER TABLE racuni DROP CONSTRAINT racuni_primary_key;" +
                       " ALTER TABLE racuni ADD COLUMN id serial;" +
                       " ALTER TABLE racuni ADD PRIMARY KEY (id);";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='racuni' AND column_name='godina'").Length == 0)
            {
                sql = "ALTER TABLE racuni ADD COLUMN godina int;";
                classSQL.insert(sql);
                classSQL.update("UPDATE racuni SET godina= EXTRACT(YEAR FROM datum_racuna)");
            }

            if (DTremote.Select("table_name='racun_stavke' AND column_name='godina'").Length == 0)
            {
                sql = "ALTER TABLE racun_stavke ADD COLUMN godina int;";
                classSQL.insert(sql);
                classSQL.update("UPDATE racun_stavke SET godina=racuni.godina " +
                    " FROM racuni WHERE racuni.broj_racuna=racun_stavke.broj_racuna");
            }

            if (DTremote.Select("table_name='pocetno'").Length == 0)
            {
                sql = "CREATE TABLE pocetno " +
                "(" +
                  "id serial NOT NULL," +
                  "sifra character varying," +
                  "id_skladiste integer," +
                  "novo boolean DEFAULT '1'," +
                  "editirano boolean DEFAULT '0'," +
                  "kolicina decimal," +
                  "nc decimal," +
                  "datum timestamp without time zone," +
                  "CONSTRAINT pocetno_pkey PRIMARY KEY (id )" +
                ")";
                classSQL.insert(sql);
            }

            //---------------------------------------PRIMKE------------------------------------
            if (DTremote.Select("table_name='primka' AND column_name='is_kalkulacija'").Length == 0)
            {
                sql = "ALTER TABLE primka ADD COLUMN is_kalkulacija boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka SET is_kalkulacija='0'");
            }

            if (DTremote.Select("table_name='primka_stavke' AND column_name='is_kalkulacija'").Length == 0)
            {
                sql = "ALTER TABLE primka_stavke ADD COLUMN is_kalkulacija boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka_stavke SET is_kalkulacija='0'");
            }

            if (DTremote.Select("table_name='primka_stavke' AND column_name='marza'").Length == 0)
            {
                sql = "ALTER TABLE primka_stavke ADD COLUMN marza numeric DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka_stavke SET marza='0'");
            }

            if (DTremote.Select("table_name='primka_stavke' AND column_name='iznos_marze'").Length == 0)
            {
                sql = "ALTER TABLE primka_stavke ADD COLUMN iznos_marze numeric DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka_stavke SET iznos_marze='0'");
            }

            if (DTremote.Select("table_name='primka_stavke' AND column_name='prodajna_cijena'").Length == 0)
            {
                sql = "ALTER TABLE primka_stavke ADD COLUMN prodajna_cijena numeric DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka_stavke SET prodajna_cijena='0'");
            }

            if (DTremote.Select("table_name='primka_stavke' AND column_name='prodajna_cijena_sa_porezom'").Length == 0)
            {
                sql = "ALTER TABLE primka_stavke ADD COLUMN prodajna_cijena_sa_porezom numeric DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka_stavke SET prodajna_cijena_sa_porezom='0'");
            }

            if (DTremote.Select("table_name='primka_stavke' AND column_name='povratna_naknada'").Length == 0)
            {
                sql = "ALTER TABLE primka_stavke ADD COLUMN povratna_naknada numeric DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka_stavke SET povratna_naknada='0'");
            }

            if (DTremote.Select("table_name='primka' AND column_name='editirano'").Length == 0)
            {
                sql = "ALTER TABLE primka ADD COLUMN editirano boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka SET editirano='0'");
            }

            if (DTremote.Select("table_name='primka_stavke' AND column_name='povratna_naknada'").Length == 0)
            {
                sql = "ALTER TABLE primka_stavke ADD COLUMN povratna_naknada numeric DEFAULT 0;";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka_stavke SET povratna_naknada='0'");
            }

            if (DTremote.Select("table_name='primka' AND column_name='id_poslovnica'").Length == 0)
            {
                sql = "ALTER TABLE primka ADD COLUMN id_poslovnica character varying;";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka SET id_poslovnica='0'");
            }

            if (DTremote.Select("table_name='primka_stavke' AND column_name='id_poslovnica'").Length == 0)
            {
                sql = "ALTER TABLE primka_stavke ADD COLUMN id_poslovnica character varying;";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka_stavke SET id_poslovnica='0'");
            }

            if (DTremote.Select("table_name='primka' AND column_name='novo'").Length == 0)
            {
                sql = "ALTER TABLE primka ADD COLUMN novo boolean DEFAULT '1';";
                classSQL.insert(sql);
                classSQL.update("UPDATE primka SET novo='0'");
            }

            if (DTremote.Select("table_name='primka' AND column_name='id'").Length == 0)
            {
                sql = " ALTER TABLE primka DROP CONSTRAINT primka_primary_key;" +
                       " ALTER TABLE primka ADD COLUMN id serial;" +
                       " ALTER TABLE primka ADD PRIMARY KEY (id);";
                classSQL.insert(sql);
            }

            //--------------------------------ARTIKLI----------------------------------------
            if (DTremote.Select("table_name='roba' AND column_name='editirano'").Length == 0)
            {
                sql = "ALTER TABLE roba ADD COLUMN editirano boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE roba SET editirano='0'");
            }

            if (DTremote.Select("table_name='roba' AND column_name='novo'").Length == 0)
            {
                sql = "ALTER TABLE roba ADD COLUMN novo boolean DEFAULT '1';";
                classSQL.insert(sql);
                classSQL.update("UPDATE roba SET novo='1'");
            }

            //--------------------------------ROBA PRODAJA----------------------------------------
            if (DTremote.Select("table_name='roba_prodaja' AND column_name='editirano'").Length == 0)
            {
                sql = "ALTER TABLE roba_prodaja ADD COLUMN editirano boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE roba_prodaja SET editirano='0'");
            }

            if (DTremote.Select("table_name='roba_prodaja' AND column_name='novo'").Length == 0)
            {
                sql = "ALTER TABLE roba_prodaja ADD COLUMN novo boolean DEFAULT '1';";
                classSQL.insert(sql);
                classSQL.update("UPDATE roba_prodaja SET novo='1'");
            }

            //--------------------------------GRUPE----------------------------------------
            if (DTremote.Select("table_name='grupa' AND column_name='editirano'").Length == 0)
            {
                sql = "ALTER TABLE grupa ADD COLUMN editirano boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE grupa SET editirano='0'");
            }

            if (DTremote.Select("table_name='grupa' AND column_name='novo'").Length == 0)
            {
                sql = "ALTER TABLE grupa ADD COLUMN novo boolean DEFAULT '1';";
                classSQL.insert(sql);
                classSQL.update("UPDATE grupa SET novo='1'");
            }

            //--------------------------------ZAPOSLENICI----------------------------------------
            if (DTremote.Select("table_name='zaposlenici' AND column_name='editirano'").Length == 0)
            {
                sql = "ALTER TABLE zaposlenici ADD COLUMN editirano boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE zaposlenici SET editirano='0'");
            }

            if (DTremote.Select("table_name='zaposlenici' AND column_name='novo'").Length == 0)
            {
                sql = "ALTER TABLE zaposlenici ADD COLUMN novo boolean DEFAULT '1';";
                classSQL.insert(sql);
                classSQL.update("UPDATE zaposlenici SET novo='1'");
            }

            if (DTremote.Select("table_name='zaposlenici' AND column_name='id'").Length == 0)
            {
                sql = " ALTER TABLE zaposlenici DROP CONSTRAINT zaposlenici_primary_key;" +
                       " ALTER TABLE zaposlenici ADD COLUMN id serial;" +
                       " ALTER TABLE zaposlenici ALTER COLUMN id_zaposlenik type integer;" +
                       " ALTER TABLE zaposlenici ALTER COLUMN id_zaposlenik DROP DEFAULT;" +
                       " ALTER TABLE zaposlenici ADD PRIMARY KEY (id);";
                classSQL.insert(sql);
            }

            //--------------------------------NORMATIVI----------------------------------------
            if (DTremote.Select("table_name='caffe_normativ' AND column_name='editirano'").Length == 0)
            {
                sql = "ALTER TABLE caffe_normativ ADD COLUMN editirano boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE caffe_normativ SET editirano='0'");
            }

            if (DTremote.Select("table_name='caffe_normativ' AND column_name='editirano'").Length == 0)
            {
                sql = "ALTER TABLE caffe_normativ ADD COLUMN novo boolean DEFAULT '1';";
                classSQL.insert(sql);
                classSQL.update("UPDATE caffe_normativ SET novo='1'");
            }

            //--------------------------------ODJAVA ROBE----------------------------------------
            if (DTremote.Select("table_name='povrat_robe' AND column_name='editirano'").Length == 0)
            {
                sql = "ALTER TABLE povrat_robe ADD COLUMN editirano boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE povrat_robe SET editirano='0'");
            }

            if (DTremote.Select("table_name='povrat_robe' AND column_name='novo'").Length == 0)
            {
                sql = "ALTER TABLE povrat_robe ADD COLUMN novo boolean DEFAULT '1';";
                classSQL.insert(sql);
                classSQL.update("UPDATE povrat_robe SET novo='1'");
            }

            //--------------------------------INVENTURA----------------------------------------
            if (DTremote.Select("table_name='inventura' AND column_name='editirano'").Length == 0)
            {
                sql = "ALTER TABLE inventura ADD COLUMN editirano boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE inventura SET editirano='0'");
            }

            if (DTremote.Select("table_name='inventura' AND column_name='novo'").Length == 0)
            {
                sql = "ALTER TABLE inventura ADD COLUMN novo boolean DEFAULT '1';";
                classSQL.insert(sql);
                classSQL.update("UPDATE inventura SET novo='1'");
            }

            if (DTremote.Select("table_name='inventura' AND column_name='is_pocetno_stanje'").Length == 0)
            {
                sql = "ALTER TABLE inventura ADD COLUMN is_pocetno_stanje boolean DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE inventura SET is_pocetno_stanje='0'");
            }

            if (DTremote.Select("table_name='inventura_stavke' AND column_name='kolicina_koja_je_bila_na_skl'").Length == 0)
            {
                sql = "ALTER TABLE inventura_stavke ADD COLUMN kolicina_koja_je_bila_na_skl numeric DEFAULT '0';";
                classSQL.insert(sql);
                classSQL.update("UPDATE inventura_stavke SET kolicina_koja_je_bila_na_skl='0'");
            }

            //--------------------------------ROBA------------------------------
            if (DTremote.Select("table_name='roba' AND column_name='porezna_grupa'").Length == 0)
            {
                sql = "ALTER TABLE roba ADD COLUMN porezna_grupa character varying(1) DEFAULT 'O';";
                classSQL.insert(sql);
                classSQL.update("UPDATE roba SET porezna_grupa='O'");
            }

            if (DTpostavke.Columns["direct_print"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN direct_print int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.update("UPDATE postavke SET direct_print='0'");
                classSQL.Setings_Update("UPDATE postavke SET direct_print='0'");
            }

            if (DTpostavke.Columns["print_narudzbe_bez_pitanja"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN print_narudzbe_bez_pitanja int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.update("UPDATE postavke SET print_narudzbe_bez_pitanja='0'");
                classSQL.Setings_Update("UPDATE postavke SET print_narudzbe_bez_pitanja='0'");
            }

            if (DTpostavke.Columns["default_kasa_fakture"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN default_kasa_fakture int;";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                //classSQL.update("UPDATE postavke SET default_kasa_fakture = '0'");
                classSQL.Setings_Update("UPDATE postavke SET default_kasa_fakture = '0'");
            }

            if (DTpostavke.Columns["default_jezik"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN default_jezik int default 1";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Rows[0]["default_skladiste"].ToString() != "1")
            {
                sql = "UPDATE postavke SET default_skladiste='1'";
                classSQL.Setings_Update(sql);
            }

            if (DTpostavke.Columns["domena_za_sinkronizaciju"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN domena_za_sinkronizaciju nvarchar(300)";
                classSQL.Setings_Update(sql);
                string oib = Util.Korisno.oibTvrtke;
                string[] oibs = new string[] { "05593216962", "77566209058", "82552175118" };
                if (oibs.Contains(oib))
                {
                    sql = "update postavke set domena_za_sinkronizaciju = 'http://cloud.pc1.hr/'";
                }
                else
                {
                    sql = "update postavke set domena_za_sinkronizaciju = 'http://pos.pc1.hr/'";
                }
                classSQL.Setings_Update(sql);
            }

            if (DTpostavke.Columns["stolovi_razvrstavanje"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN stolovi_razvrstavanje int DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["koristi_popust_prema_stavki"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN koristi_popust_prema_stavki int DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["ladicaOn"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN ladicaOn int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.update("UPDATE postavke SET ladicaOn='0'");
                classSQL.Setings_Update("UPDATE postavke SET ladicaOn='0'");
            }

            if (DTpostavke.Columns["zabrana_zaposleniku_da_vidi_druge_promete"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN zabrana_zaposleniku_da_vidi_druge_promete int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.update("UPDATE postavke SET zabrana_zaposleniku_da_vidi_druge_promete='0'");
                classSQL.Setings_Update("UPDATE postavke SET zabrana_zaposleniku_da_vidi_druge_promete='0'");
            }

            if (DTpostavke.Columns["dodatno_upozorenje"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN dodatno_upozorenje int DEFAULT 1;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["stol_konobar"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN stol_konobar int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.update("UPDATE postavke SET stol_konobar='0'");
                classSQL.Setings_Update("UPDATE postavke SET stol_konobar='0'");
            }

            if (DTpostavke.Columns["zabraniUvidSkladiste"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN zabraniUvidSkladiste int DEFAULT 0;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");

                if (DTtvrtka.Rows[0]["oib"].ToString() == "05593216962" || DTtvrtka.Rows[0]["oib"].ToString() == "77566209058")
                {
                    classSQL.update("UPDATE postavke SET zabraniUvidSkladiste='1'");
                    classSQL.Setings_Update("UPDATE postavke SET zabraniUvidSkladiste='1'");
                }
            }

            //if (DTpostavke.Columns["domena_za_cloud"] == null) {
            //    sql = "ALTER TABLE postavke ADD COLUMN domena_za_cloud nvarchar(300);";
            //    classSQL.select_settings(sql, "postavke");
            //}

            if (DTpostavke.Columns["nacini_placanja"] == null)
            {
                try
                {
                    sql = "ALTER TABLE postavke ADD COLUMN nacini_placanja nvarchar(12);";

                    //sql = "ALTER TABLE postavke DROP COLUMN nacini_placanja";

                    classSQL.select_settings(sql, "postavke");

                    if (DTtvrtka.Rows[0]["oib"].ToString() == "05593216962")
                        classSQL.Setings_Update("UPDATE postavke SET nacini_placanja='1;0;0;1'");
                    else
                        classSQL.Setings_Update("UPDATE postavke SET nacini_placanja='1;1;1;1'");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            if (DTpostavke.Columns["backup_aktivnost"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN backup_aktivnost int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET backup_aktivnost='1'");
            }

            if (DTpostavke.Columns["skidaj_sa_predracuna"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN skidaj_sa_predracuna int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET skidaj_sa_predracuna='0'");
            }

            if (DTpostavke.Columns["is_caffe"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN is_caffe int DEFAULT 1;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }
            if (DTpostavke.Columns["is_beauty"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN is_beauty bit DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["napomena_na_kraju_racuna"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN napomena_na_kraju_racuna bit DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["napomena_na_kraju_predracuna"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN napomena_na_kraju_predracuna bit DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["useUdsGame"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN useUdsGame BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
                sql = "ALTER TABLE postavke ADD COLUMN useUdsGameEmployees BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
                sql = "ALTER TABLE postavke ADD COLUMN useUdsGameApiKey NVARCHAR(1500);";
                classSQL.select_settings(sql, "postavke");
            }


            //rad_sa_tabletima
            if (DTpostavke.Columns["rad_sa_tabletima"] == null) {
                sql = "ALTER TABLE postavke ADD COLUMN rad_sa_tabletima BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["test_fiskalizacija"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN test_fiskalizacija BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["obavjeti_ako_nema_repromaterijala"] == null ||
                DTpostavke.Columns["skidaj_kolicinu_po_dokumentima"] == null ||
                DTpostavke.Columns["posalji_dokumente_na_web"] == null)
            {
                try
                {
                    sql = "ALTER TABLE postavke ADD COLUMN obavjeti_ako_nema_repromaterijala int;";
                    classSQL.insert(sql);
                    classSQL.select_settings(sql, "postavke");
                    classSQL.Setings_Update("UPDATE postavke SET obavjeti_ako_nema_repromaterijala='0'");
                }
                catch { }

                try
                {
                    sql = "ALTER TABLE postavke ADD COLUMN skidaj_kolicinu_po_dokumentima int;";
                    classSQL.insert(sql);
                    classSQL.select_settings(sql, "postavke");
                    classSQL.Setings_Update("UPDATE postavke SET skidaj_kolicinu_po_dokumentima='0'");
                }
                catch { }

                try
                {
                    sql = "ALTER TABLE postavke ADD COLUMN posalji_dokumente_na_web int;";
                    classSQL.insert(sql);
                    classSQL.select_settings(sql, "postavke");
                    classSQL.Setings_Update("UPDATE postavke SET posalji_dokumente_na_web='0'");
                }
                catch { }
            }

            if (DTpostavke.Columns["gasenje_racunala"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN gasenje_racunala int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET gasenje_racunala='0'");
            }

            //----------------------------------------------- kartica & frmCaffe buttons design -------------------

            if (DTpostavke.Columns["kartica_kupca"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN kartica_kupca smallint DEFAULT 0;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET kartica_kupca='0'");
            }

            if (DTpostavke.Columns["kartica_popust"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN kartica_popust numeric DEFAULT 0;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET kartica_popust='0'");
            }

            //ALTER TABLE postavke drop COLUMN reader_prefix;
            //sql = "ALTER TABLE postavke drop COLUMN reader_prefix;";
            //classSQL.insert(sql);//postgres
            //classSQL.select_settings(sql, "postavke");

            if (DTpostavke.Columns["reader_prefix"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN reader_prefix nvarchar(10) DEFAULT '';";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET reader_prefix=''");
            }
            //sql = "ALTER TABLE postavke drop COLUMN readere_sufix;";
            //classSQL.insert(sql);//postgres
            //classSQL.select_settings(sql, "postavke");
            if (DTpostavke.Columns["reader_sufix"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN reader_sufix nvarchar(10) DEFAULT '';";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET reader_sufix=''");
            }

            //ALTER TABLE postavke drop COLUMN button_color_icona;
            //sql = "ALTER TABLE postavke drop COLUMN button_color_icona;";
            //classSQL.insert(sql);//postgres
            //classSQL.select_settings(sql, "postavke");
            if (DTpostavke.Columns["font_size_icona"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN font_size_icona int DEFAULT 10;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET font_size_icona='10'");
            }
            if (DTpostavke.Columns["button_color_icona"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN button_color_icona nchar(9) default '#FF1F5763';";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET button_color_icona='#FF1F5763';");
            }
            else if (DTpostavke.Columns["button_color_icona"].DataType == typeof(System.Int16))
            {
                sql = "ALTER TABLE postavke drop COLUMN button_color_icona;";
                classSQL.select_settings(sql, "postavke");

                sql = "ALTER TABLE postavke ADD COLUMN button_color_icona nchar(9) default '#FF1F5763';";
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET button_color_icona='#FF1F5763';");
            }

            if (DTpostavke.Columns["font_color_icona"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN font_color_icona nchar(9) default '#FFFFFFFF';";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET font_color_icona='#FFFFFFFF';");
            }
            if (DTpostavke.Columns["bui_icona"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN bui_icona nchar(5) default '1;0;0';";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET bui_icona='1;0;0';");
            }
            if (DTpostavke.Columns["dodatak_na_artikl"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN dodatak_na_artikl numeric DEFAULT 0;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET dodatak_na_artikl='0';");
            }
            if (DTpostavke.Columns["zaposlenici_vide_samo_danasnju_prodaju"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN zaposlenici_vide_samo_danasnju_prodaju numeric DEFAULT 0;";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                //classSQL.Setings_Update("UPDATE postavke SET bui_icona='1;0;0';");
            }
            //------------------------------------------------------------------------------------------------------

            if (DTpostavke.Columns["lokacija_sigurnosne_kopije"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN lokacija_sigurnosne_kopije ntext;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["backup_vrijeme"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN backup_vrijeme nvarchar;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["putanja_certifikat"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN putanja_certifikat nvarchar(500);";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["certifikat_zaporka"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN certifikat_zaporka nvarchar(100);";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["bool_direct_print_kuhinja"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN bool_direct_print_kuhinja int;";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["direct_print"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN direct_print int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET direct_print='0'");
            }

            if (DTtvrtka.Columns["napomenaTransa"] == null)
            {
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN napomenaTransa nvarchar(1000);";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "podaci_tvrtka");
            }

            DataRow[] rows = DTcompact.Select("table_name = 'podaci_tvrtka' and column_name = 'skracenoImeTvrtke'");
            if (rows.Count() == 0)
            {
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN skracenoImeTvrtke nvarchar(500) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }
            else if (rows[0]["data_type"].ToString().ToLower() == "nchar")
            {
                sql = @"alter table podaci_tvrtka drop column skracenoImeTvrtke;";
                classSQL.select_settings(sql, "podaci_tvrtka");
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN skracenoImeTvrtke nvarchar(500) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }

            rows = DTcompact.Select("table_name = 'podaci_tvrtka' and column_name = 'nazivPoslovnice'");
            if (rows.Count() == 0)
            {
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN nazivPoslovnice nvarchar(500) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }
            else if (rows[0]["data_type"].ToString().ToLower() == "nchar")
            {
                sql = @"alter table podaci_tvrtka drop column nazivPoslovnice;";
                classSQL.select_settings(sql, "podaci_tvrtka");
                sql = @"ALTER TABLE podaci_tvrtka ADD COLUMN nazivPoslovnice nvarchar(500) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }

            rows = DTcompact.Select("table_name = 'podaci_tvrtka' and column_name = 'swift'");
            if (rows.Count() == 0)
            {
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN swift nvarchar(500) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }
            else if (rows[0]["data_type"].ToString().ToLower() == "nchar")
            {
                sql = @"alter table podaci_tvrtka drop column swift;";
                classSQL.select_settings(sql, "podaci_tvrtka");
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN swift nvarchar(500) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }

            rows = DTcompact.Select("table_name = 'podaci_tvrtka' and column_name = 'pdvBr'");
            if (rows.Count() == 0)
            {
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN pdvBr nvarchar(500) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }
            else if (rows[0]["data_type"].ToString().ToLower() == "nchar")
            {
                sql = @"alter table podaci_tvrtka drop column pdvBr;";
                classSQL.select_settings(sql, "podaci_tvrtka");
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN pdvBr nvarchar(500) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }

            // sifra_ppmipo
            rows = DTcompact.Select("table_name = 'podaci_tvrtka' and column_name = 'sifra_ppmipo'");
            if (rows.Count() == 0)
            {
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN sifra_ppmipo nvarchar(25) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }
            else if (rows[0]["data_type"].ToString().ToLower() == "nchar")
            {
                sql = @"alter table podaci_tvrtka drop column sifra_ppmipo;";
                classSQL.select_settings(sql, "podaci_tvrtka");
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN sifra_ppmipo nvarchar(25) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }

            //email za slanje dokumentacije u knjigovodstvo, katija knjigovođa 
            rows = DTcompact.Select("table_name = 'podaci_tvrtka' and column_name = 'email_knjigovodstvo'");
            if (rows.Count() == 0)
            {
                sql = "ALTER TABLE podaci_tvrtka ADD COLUMN email_knjigovodstvo nvarchar(35) default '-';";
                classSQL.select_settings(sql, "podaci_tvrtka");
            }

            if (DTpostavke.Columns["samo_prodaja"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN samo_prodaja int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET samo_prodaja='0'");
            }

            if (DTpostavke.Columns["ladicaOn"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN ladicaOn int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET ladicaOn='0'");
            }

            if (DTpostavke.Columns["prijava_nakon_racuna"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN prijava_nakon_racuna int DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
                //classSQL.Setings_Update("UPDATE postavke SET prijava_nakon_racuna='0'");
            }

            if (DTpostavke.Columns["preferirajSifre"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN preferirajSifre int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET preferirajSifre='0'");
            }

            if (DTpostavke.Columns["upozori_iskljucenu_fiskalizaciju"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN upozori_iskljucenu_fiskalizaciju INT default 1;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["zadnji_racun"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN zadnji_racun nvarchar(30);";
                classSQL.select_settings(sql, "postavke");
                //DEFAULTNA VRIJEDNOST JE '0;0';
                classSQL.Setings_Update("UPDATE postavke SET zadnji_racun='0;0'");
            }

            if (DTpostavke.Columns["magnetska_kartica"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN magnetska_kartica int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET magnetska_kartica='0'");
            }

            if (DTpostavke.Columns["font_print_size"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN font_print_size numeric;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET font_print_size='10'");
            }

            if (DTpostavke.Columns["ispis_cijele_stavke"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN ispis_cijele_stavke int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["ispis_na_kopiji_racuna"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN ispis_na_kopiji_racuna bit default 1;";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            DataTable DT = classSQL.select_settings("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];

            DataRow[] dataROW = DT.Select("table_name = 'podaci_poslovnica_fiskal'");
            if (dataROW.Length == 0)
            {
                string sqlTab = "CREATE TABLE podaci_poslovnica_fiskal" +
                " (OIB nvarchar(12), " +
                " oznakaPP nvarchar(30)," +
                " ulica nvarchar(50)," +
                " broj nvarchar(25)," +
                " broj_dodatak nvarchar(25)," +
                " posta nvarchar(10)," +
                " naselje nvarchar(50)," +
                " opcina nvarchar(25)," +
                " datum nvarchar(25)," +
                " r_vrijeme nvarchar(500)," +
                " zatvaranje nvarchar(1)" +
                " )";
                classSQL.select_settings(sqlTab, "podaci_poslovnica_fiskal");

                classSQL.select_settings("INSERT INTO podaci_poslovnica_fiskal (OIB) VALUES ('00000000000')", "podaci_poslovnica_fiskal");
            }

            DataTable DT_1 = classSQL.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];

            dataROW = DT_1.Select("table_name = 'svi_predracuni'");
            if (dataROW.Length == 0)
            {
                string sss = "CREATE TABLE svi_predracuni" +
                " (id serial NOT NULL," +
                " sifra character varying(50), " +
                " mpc numeric," +
                " porez numeric," +
                " id_stol int," +
                " id_zaposlenik int," +
                " datum_ispisa timestamp without time zone," +
                " kolicina numeric," +
                " vpc numeric," +
                " porez_potrosnja numeric," +
                " naziv character varying(50)," +
                " broj int," +
                " id_ducan character varying(10)," +
                " id_blagajna character varying(10),CONSTRAINT id_svi_predracuni_primary_key PRIMARY KEY (id )" +
                " )";
                classSQL.insert(sss);
            }

            dataROW = DT_1.Select("table_name = 'maping'");
            if (dataROW.Length == 0)
            {
                sql = @"CREATE TABLE maping
                                (
                                  nasa_grupa character varying(100),
                                  dobavljac_grupa character varying(100),
                                  partner INT,
                                  id serial NOT NULL,
                                  CONSTRAINT maping_primary_key PRIMARY KEY (id)
                                )";
                classSQL.insert(sql);
            }

            dataROW = DT_1.Select("table_name = 'smjene'");
            if (dataROW.Length == 0)
            {
                string sqlTabR = "CREATE TABLE smjene" +
                " (id serial NOT NULL," +
                "pocetno_stanje numeric, " +
                " konobar character varying(10)," +
                " pocetak timestamp without time zone," +
                " zavrsetak timestamp without time zone," +
                " zavrsno_stanje numeric," +
                " napomena text," +
                " CONSTRAINT id_primary_key PRIMARY KEY (id )" +
                " )";

                classSQL.select(sqlTabR, "smjene");

                string sqlTabC = "CREATE TABLE smjene" +
                    " (id BIGINT IDENTITY NOT NULL PRIMARY KEY," +
                    " pocetno_stanje numeric, " +
                    " konobar nvarchar(10)," +
                    " pocetak datetime," +
                    " zavrsetak datetime," +
                    " napomena ntext," +
                    " zavrsno_stanje numeric" +
                    " )";

                classSQL.select_settings(sqlTabC, "smjene");
            }

            DataTable DTsmjene = classSQL.select("SELECT * FROM smjene LIMIT 2", "smjene").Tables[0];
            if (DTsmjene.Rows.Count > 0)
            {
                if (DTsmjene.Columns["konobarZ"] == null)
                {
                    sql = "ALTER TABLE smjene ADD COLUMN konobarZ int;";
                    classSQL.insert(sql);
                    classSQL.select_settings(sql, "smjene");
                }
            }

            DataTable DTfaktura_stavke = classSQL.select("SELECT * FROM faktura_stavke LIMIT 1", "faktura_stavke").Tables[0];
            if (DTfaktura_stavke.Columns["mpc"] == null)
            {
                sql = "ALTER TABLE faktura_stavke ADD COLUMN mpc numeric;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "faktura_stavke");
                classSQL.update("UPDATE faktura_stavke SET mpc='0'");
            }

            DataTable DTponude_stavke = classSQL.select("SELECT * FROM ponude_stavke LIMIT 1", "ponude_stavke").Tables[0];
            if (DTfaktura_stavke.Columns["mpc"] == null)
            {
                sql = "ALTER TABLE ponude_stavke ADD COLUMN mpc numeric;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "ponude_stavke");
                classSQL.update("UPDATE ponude_stavke SET mpc='0'");
            }

            DataTable DTracuni = classSQL.select("SELECT * FROM racuni LIMIT 1", "racuni").Tables[0];
            //if (DTracuni.Rows.Count > 0)
            {
                if (DTracuni.Columns["ukupno_ostalo"] == null)
                {
                    sql = "ALTER TABLE racuni ADD COLUMN ukupno_ostalo numeric;";
                    classSQL.insert(sql);
                    classSQL.select_settings(sql, "racuni");
                    classSQL.update("UPDATE racuni SET ukupno_ostalo='0'");
                }

                if (DTracuni.Columns["broj_ispisa"] == null)
                {
                    sql = "ALTER TABLE racuni ADD COLUMN broj_ispisa integer DEFAULT 1;";
                    classSQL.insert(sql);
                    classSQL.select_settings(sql, "racuni");
                    classSQL.update("UPDATE racuni SET broj_ispisa='1'");
                }

                if (DTracuni.Columns["napomena"] == null)
                {
                    sql = "ALTER TABLE racuni ADD COLUMN napomena text default '';";
                    classSQL.insert(sql);
                    //classSQL.select_settings(sql, "racuni");
                    //classSQL.update("UPDATE racuni SET broj_ispisa='1'");
                }

                //////////////////////////////////////////////////////////////////
                DataTable DTracuni_stavke = classSQL.select("SELECT * FROM racun_stavke LIMIT 1", "racuni").Tables[0];
                if (DTracuni_stavke.Columns["povratna_naknada"] == null)
                {
                    sql = "ALTER TABLE racun_stavke ADD COLUMN povratna_naknada numeric DEFAULT 0;";
                    classSQL.insert(sql);
                    classSQL.update("UPDATE racun_stavke SET povratna_naknada='0'");
                }
            }

            if (DTremote.Select("table_name='svi_predracuni' AND column_name='novo'").Length == 0)
            {
                sql = @"ALTER TABLE svi_predracuni ADD COLUMN novo numeric DEFAULT '1';";
                classSQL.insert(sql);
            }

            if (DTremote.Select("table_name='svi_predracuni' AND column_name='napomena'").Length == 0)
            {
                sql = @"ALTER TABLE svi_predracuni ADD COLUMN napomena text DEFAULT '';";
                classSQL.insert(sql);
            }

            try
            {
                DataTable DTpos_print = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
                if (DTpos_print.Rows.Count > 0)
                {
                    if (DTpos_print.Columns["port_display"] == null || DTpos_print.Columns["port_display_enable"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN port_display character varying(10);";
                        classSQL.insert(sql);
                        classSQL.select_settings(sql.Replace("character varying(10)", "nvarchar(10)"), "pos_print");
                        classSQL.update("UPDATE pos_print SET port_display='COM10'");

                        sql = "ALTER TABLE pos_print ADD COLUMN port_display_enable int;";
                        classSQL.insert(sql);
                        classSQL.select_settings(sql, "pos_print");
                        classSQL.update("UPDATE pos_print SET port_display_enable='0'");
                    }

                    if (DTpostavke.Columns["prviPrint"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN prviPrint NVARCHAR(30) DEFAULT 'Sank';";
                        classSQL.select_settings(sql, "pos_print");
                    }

                    if (DTpostavke.Columns["drugiPrint"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN drugiPrint NVARCHAR(30) DEFAULT 'Kuhinja';";
                        classSQL.select_settings(sql, "pos_print");
                    }

                    if (DTpostavke.Columns["treciPrint"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN treciPrint NVARCHAR(30) DEFAULT 'Pizzeria';";
                        classSQL.select_settings(sql, "pos_print");
                    }

                    if (DTpos_print.Columns["windows_printer_sank"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN windows_printer_sank nvarchar(100);";
                        classSQL.select_settings(sql, "pos_print");
                    }

                    if (DTpos_print.Columns["windows_printer_name3"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN windows_printer_name3 nvarchar(100);";
                        classSQL.select_settings(sql, "a");

                        sql = "ALTER TABLE grupa ADD COLUMN printer3 int DEFAULT 0;";
                        classSQL.insert(sql);
                    }

                    if (DTpos_print.Columns["adresa_narudzbe_racun_kraj"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN adresa_narudzbe_racun_kraj int DEFAULT 0;";
                        classSQL.select_settings(sql, "a");

                        //sql = "ALTER TABLE grupa ADD COLUMN printer3 int DEFAULT 0;";
                        //classSQL.insert(sql);
                    }

                    if (DTpos_print.Columns["ispisGotovina"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN ispisGotovina int DEFAULT 1;";
                        classSQL.select_settings(sql, "a");
                    }

                    if (DTpos_print.Columns["ispisKartica"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN ispisKartica int DEFAULT 1;";
                        classSQL.select_settings(sql, "a");
                    }

                    if (DTpos_print.Columns["ispisVirman"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN ispisVirman int DEFAULT 1;";
                        classSQL.select_settings(sql, "a");
                    }

                    if (DTpos_print.Columns["ispisOstalo"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN ispisOstalo int DEFAULT 1;";
                        classSQL.select_settings(sql, "a");
                    }

                    if (DTpos_print.Columns["ispisOtpremnica"] == null)
                    {
                        sql = "ALTER TABLE pos_print ADD COLUMN ispisOtpremnica int DEFAULT 1;";
                        classSQL.select_settings(sql, "a");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ne mogu kreirati kolonue za pos_printer.");
            }

            DataTable DTzaposlenici = classSQL.select("SELECT * FROM zaposlenici LIMIT 1", "racuni").Tables[0];
            if (DTzaposlenici.Rows.Count > 0)
            {
                if (DTzaposlenici.Columns["kartica"] == null)
                {
                    sql = "ALTER TABLE zaposlenici ADD COLUMN kartica character varying(50);";
                    classSQL.insert(sql);
                    //classSQL.select_settings(sql, "racuni");
                }
            }

            DataTable DTblagajna = classSQL.select("SELECT * FROM blagajna LIMIT 1", "racuni").Tables[0];
            if (DTblagajna.Columns["editirano"] == null)
            {
                sql = @"alter table blagajna add column editirano boolean default false;
alter table blagajna add column novo boolean default true;";
                classSQL.insert(sql);
            }
            DataTable DTducan = classSQL.select("SELECT * FROM ducan LIMIT 1", "racuni").Tables[0];
            if (DTducan.Columns["editirano"] == null)
            {
                sql = @"alter table ducan add column editirano boolean default false;
alter table ducan add column novo boolean default true;
create sequence fakture_broj_fakture_seq start 1;
SELECT setval('fakture_broj_fakture_seq', (SELECT MAX(broj_fakture) FROM fakture)+1)";
                classSQL.insert(sql);
            }

            DataTable DTjelo = classSQL.select("SELECT * FROM na_stol LIMIT 1", "na_stol").Tables[0];
            if (DTjelo.Columns["jelo"] == null)
            {
                sql = "ALTER TABLE na_stol ADD COLUMN jelo character varying(50);";
                classSQL.insert(sql);
            }

            if (DTjelo.Columns["skinuto"] == null)
            {
                sql = "ALTER TABLE na_stol ADD COLUMN skinuto int;";
                classSQL.insert(sql);
            }

            if (DTjelo.Columns["id_zaposlenik"] == null)
            {
                sql = "ALTER TABLE na_stol ADD COLUMN id_zaposlenik int;";
                classSQL.insert(sql);
            }

            if (DTjelo.Columns["dod"] == null)
            {
                sql = "ALTER TABLE na_stol ADD COLUMN dod int;";
                classSQL.insert(sql);
            }
            if (DTjelo.Columns["pol"] == null)
            {
                sql = "ALTER TABLE na_stol ADD COLUMN pol int DEFAULT null;";
                classSQL.insert(sql);
            }
            if (DTjelo.Columns["rabat"] == null)
            {
                sql = "ALTER TABLE na_stol ADD COLUMN rabat numeric DEFAULT null;";
                classSQL.insert(sql);
            }

            DataTable DTstolovi = classSQL.select("SELECT * FROM stolovi LIMIT 1", "na_stol").Tables[0];
            if (DTstolovi.Columns["x_pozicija"] == null)
            {
                sql = "ALTER TABLE stolovi ADD COLUMN x_pozicija int DEFAULT 0;";
                classSQL.insert(sql);
            }

            if (DTstolovi.Columns["y_pozicija"] == null)
            {
                sql = "ALTER TABLE stolovi ADD COLUMN y_pozicija int DEFAULT 0;";
                classSQL.insert(sql);
            }

            DataTable DTkucani_predracuni = classSQL.select("SELECT * FROM kucani_predracuni LIMIT 1", "roba").Tables[0];
            {
                if (DTkucani_predracuni.Columns["id_stol"] == null)
                {
                    string sss = "ALTER TABLE kucani_predracuni ADD COLUMN id_stol int";
                    sql = "ALTER TABLE kucani_predracuni ADD COLUMN id_stol int;";
                    classSQL.insert(sss);
                    classSQL.select_settings(sql, "roba");
                }
            }

            DataTable DTartikli = classSQL.select("SELECT * FROM roba LIMIT 1", "roba").Tables[0];
            {
                if (DTartikli.Columns["border_color"] == null)
                {
                    string sss = "ALTER TABLE roba ADD COLUMN border_color character varying(50)";
                    sql = "ALTER TABLE roba ADD COLUMN border_color nvarchar(50);";
                    classSQL.insert(sss);
                    classSQL.select_settings(sql, "roba");
                    classSQL.update("UPDATE roba SET border_color='0'");
                }

                if (DTartikli.Columns["button_style"] == null)
                {
                    string sss = "ALTER TABLE roba ADD COLUMN button_style character varying(50)";
                    sql = "ALTER TABLE roba ADD COLUMN button_style nvarchar(50);";
                    classSQL.insert(sss);
                    classSQL.select_settings(sql, "roba");
                    classSQL.update("UPDATE roba SET button_style='standard'");
                }

                if (DTartikli.Columns["brojcanik"] == null)
                {
                    string sss = "ALTER TABLE roba ADD COLUMN brojcanik decimal";
                    sql = "ALTER TABLE roba ADD COLUMN brojcanik decimal;";
                    classSQL.insert(sss);
                    classSQL.select_settings(sql, "roba");
                    classSQL.update("UPDATE roba SET brojcanik='0'");
                }
            }
        }

        private static void Porezi()
        {
            //classSQL.Setings_Update("UPDATE postavke SET promjena_poreza='1'");
            if (DTpostavke.Columns["promjena_poreza"] == null)
            {
                classSQL.Setings_Update("ALTER TABLE postavke ADD COLUMN promjena_poreza int;");
                classSQL.Setings_Update("UPDATE postavke SET promjena_poreza='1'");
            }
        }

        private static void DodajProvjeruGreske(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'ispravljene_greske'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE ispravljene_greske " +
                            "(" +
                            "id serial NOT NULL," +
                            "opis character varying UNIQUE," +
                            "CONSTRAINT ispravljene_greske_primary_key PRIMARY KEY (id )" +
                            ")" +
                            "";

                classSQL.select(sql, "partners_odrzavanje");
            }
        }

        private static void DodajPotrosni_materijal(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'potrosni_materijal'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE potrosni_materijal " +
                            "(" +
                            " id serial NOT NULL," +
                            " id_partner int, " +
                            " id_zaposlenik int, " +
                            " broj int, " +
                            " godina int, " +
                            " datum timestamp without time zone," +
                            " placanje character varying(25)," +
                            " napomena text," +

                            " sifra character varying," +
                            " naziv character varying," +
                            " jmj character varying," +
                            " kolicina numeric, " +
                            " porez numeric, " +
                            " cijena numeric, " +
                            " rabat numeric, " +
                            " novo int  DEFAULT 1," +
                            "CONSTRAINT potrosni_materijal_primary_key PRIMARY KEY (id )" +
                            ")" +
                            "";

                classSQL.select(sql, "potrosni_materijal");
            }

            dataROW = DTremote.Select("table_name = 'potrosni_materijal_artikli'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE potrosni_materijal_artikli " +
                            "(" +
                            " id serial NOT NULL," +
                            " sifra character varying," +
                            " naziv character varying," +
                            " jmj character varying," +
                            " kolicina numeric, " +
                            " porez numeric, " +
                            " cijena numeric, " +
                            " rabat numeric, " +
                            "CONSTRAINT ppotrosni_materijal_artikli_primary_key PRIMARY KEY (id )" +
                            ")" +
                            "";

                classSQL.select(sql, "potrosni_materijal");

                sql = @"INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'1','Sredstvo za pranje poda','ml','1','25','12','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'2','Sredstvo za pranje prozora','ml','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'3','Spužvice za suđe','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'4','Krpa truleks','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'5','Krpa obična','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'6','Sredstvo za dezinfekciju','ml','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'7','Slamke','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'8','Vreće za smeće','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'9','Sol','kg','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'10','Osvježivač prostora','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'11','WC papir','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'12','Papirnati ručnici','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'13','WC osvježivač školjke','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'14','Bombice za šlag','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'15','Čaša','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'16','Metla/Partviš','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'17','Đoger','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'18','Šalica','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'19','Kanta za smeće','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'20','Čaše za kavu za van','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'21','Poklopci za čaše za kavu za van','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'22','Žličice metalne','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'23','Žlićice plastićne','kom','1','25','27','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'24','Mjerači za žesticu','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'25','Tekuća čokolada','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'26','Nož za limun','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'27','Daska za rezanje','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'28','Posuda','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'29','Ukrasi za dekoriranje','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'30','Svijeće/lučice','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'31','Biljka','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'32','Tegla za cvijeće','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'33','Stolnjak','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'34','Sredstvo za pranje suđa','ml','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'35','Sredstvo za perilicu','ml','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'36','Deterđent za rublje','kg','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'37','Omekšivač za rublje','ml','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'38','TV','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'39','DVBT','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'40','Linija','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'42','Natren','kom','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'43','Klinčići ','kom/vrećica','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'44','Cimet','kom/vrećica','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'45','Tekuća čokolada','ml','1','25','0','0');
                        INSERT INTO potrosni_materijal_artikli VALUES (DEFAULT,'41','Ruter','kom','1','25','0','0');";

                classSQL.insert(sql);
            }
        }
    }
}