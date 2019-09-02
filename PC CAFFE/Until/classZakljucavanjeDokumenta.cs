using System;
using System.Windows.Forms;

namespace PCPOS.Until
{
    internal class classZakljucavanjeDokumenta
    {
        private string sql = "";

        public static bool isLockPrimka(int br, int skl)
        {
            try
            {
                string sql = "SELECT zakljucano FROM primka WHERE broj_primke = '" + br + "' AND id_skladiste = '" + skl + "' AND is_kalkulacija = '0';";

                return Convert.ToBoolean(classSQL.select(sql, "primka").Tables[0].Rows[0]["zakljucano"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
        }

        public static bool isLockKalkulacija(int br, int skl)
        {
            try
            {
                string sql = "SELECT zakljucano FROM primka WHERE broj_primke = '" + br + "' AND id_skladiste = '" + skl + "' AND is_kalkulacija = '1';";

                return Convert.ToBoolean(classSQL.select(sql, "primka").Tables[0].Rows[0]["zakljucano"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
        }

        public static bool isLockMeduskladisnica(int br, string izs, int god)
        {
            try
            {
                string sql = "SELECT zakljucano FROM medu_poslovnice WHERE broj = '" + br + "' AND iz_poslovnice = '" + izs + "' AND godina = '" + god + "';";

                return Convert.ToBoolean(classSQL.select(sql, "primka").Tables[0].Rows[0]["zakljucano"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
        }

        public static bool isLockPromjenaCijene(int br, string izs, int god)
        {
            try
            {
                string sql = "SELECT zakljucano FROM promjena_cijene_auto WHERE broj = '" + br + "' AND iz_poslovnice = '" + izs + "' AND godina = '" + god + "';";

                return Convert.ToBoolean(classSQL.select(sql, "primka").Tables[0].Rows[0]["zakljucano"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
        }

        public static bool isLockOtpisRobe(int br)
        {
            try
            {
                string sql = "SELECT zakljucano FROM povrat_robe WHERE broj = '" + br + "';";

                return Convert.ToBoolean(classSQL.select(sql, "primka").Tables[0].Rows[0]["zakljucano"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
        }
    }
}