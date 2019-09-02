using System;
using System.Data;

namespace RESORT.Class
{
    internal class PostavkeFiskalizacije
    {
        public static int poslovnicaId { get; private set; }
        public static int naplatniUredajId { get; private set; }
        public static int naplatniUredajAvansId { get; private set; }
        public static bool sustavPdv { get; private set; }
        public static bool testFiskalizacija { get; private set; }
        public static string nazivCertifikata { get; private set; }
        public static string oznakaSlijednosti { get; private set; }

        public static void getPodaci()
        {
            DataTable DT = classDBlite.LiteSelect("SELECT * FROM podaci_fiskalizacija", "podaci_fiskalizacija").Tables[0];

            sustavPdv = Convert.ToBoolean((DT.Rows[0]["sustav_pdv"].ToString() == "DA" ? 1 : 0));
            testFiskalizacija = Convert.ToBoolean(DT.Rows[0]["test_Yes"]);
            poslovnicaId = Convert.ToInt32(DT.Rows[0]["oznakaPP"].ToString());
            naplatniUredajId = Convert.ToInt32(DT.Rows[0]["oznaka_prodajnog_mj"].ToString());
            naplatniUredajAvansId = Convert.ToInt32(DT.Rows[0]["oznaka_prodajnog_mj_avans"].ToString());
            nazivCertifikata = DT.Rows[0]["naziv_certifikata"].ToString();
            oznakaSlijednosti = DT.Rows[0]["oznaka_slijednosti"].ToString();
        }
    }
}