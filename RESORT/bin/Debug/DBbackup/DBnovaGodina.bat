pg_dump --host localhost --port 5432 --username "postgres"  --role "postgres" --no-password -c db2012 > db_name_dump
 createdb --host localhost --port 5432 --username postgres db2013
 psql --host localhost --port 5432 --username postgres db2013 < db_name_dump
 psql --host localhost --port 5432 --username postgres -d db2013 -c "DELETE FROM racuni" 
 psql --host localhost --port 5432 --username postgres -d db2013 -c "DELETE FROM racun_stavke" 
 psql --host localhost --port 5432 --username postgres -d db2013 -c "DELETE FROM primka" 
 psql --host localhost --port 5432 --username postgres -d db2013 -c "DELETE FROM primka_stavke" 
 psql --host localhost --port 5432 --username postgres -d db2013 -c "DELETE FROM faktura_stavke" 
 psql --host localhost --port 5432 --username postgres -d db2013 -c "DELETE FROM fakture" 
 psql --host localhost --port 5432 --username postgres -d db2013 -c "DELETE FROM aktivnost_zaposlenici" 
pause