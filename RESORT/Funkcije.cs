using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RESORT
{
    internal class Funkcije
    {
        public static int ReturnDaysFromDate(DateTime Date1, DateTime Date2)
        {
            TimeSpan ts = Date2 - Date1.AddHours(-1);
            int differenceInDays = ts.Days;
            return differenceInDays;
        }

        public static string Mat(decimal d, int b)
        {
            if (b > 2)
            {
                return Math.Round(d, 3).ToString("#0.000");
            }
            else
            {
                return Math.Round(d, 3).ToString("#0.00");
            }
        }

        public static bool DodajPodatkeOfiskalizaciji(string brojRacuna, string[] oznake)
        {
            string sql = "UPDATE rfakture SET jir='" + oznake[0] + "',zik='" + oznake[1] + "' WHERE broj='" + brojRacuna + "'";
            provjera_sql(RemoteDB.update(sql));
            return true;
        }

        public static void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        public static void SetInLog(string greska, string linija, string forma)
        {
            try
            {
                if (File.Exists("log.txt"))
                {
                    string[] lines = { "\r\n**************************************************************",
                                         "DATUM:"+ DateTime.Now.ToString()+", FORMA: " + forma,
                                         "LINIJA KODA:"+linija,
                                         "GREŠKA:\r\n"+greska,
                                        "**************************************************************" };

                    System.IO.File.AppendAllLines("log.txt", lines);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void Form1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                System.Data.DataTable DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];

                Color x = Color.FromArgb(Convert.ToInt16(DTBojeForme.Rows[0]["x1"].ToString()), Convert.ToInt16(DTBojeForme.Rows[0]["x2"].ToString()), Convert.ToInt16(DTBojeForme.Rows[0]["x3"].ToString()));
                Color y = Color.FromArgb(Convert.ToInt16(DTBojeForme.Rows[0]["y1"].ToString()), Convert.ToInt16(DTBojeForme.Rows[0]["y2"].ToString()), Convert.ToInt16(DTBojeForme.Rows[0]["y3"].ToString()));

                Graphics c = e.Graphics;
                Brush bG = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, (sender as Control).Width, (sender as Control).Height), x, y, 250);
                c.FillRectangle(bG, 0, 0, (sender as Control).Width, (sender as Control).Height);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void btnScroll_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (sender as Button);
                string controlName = button.Tag.ToString().Trim();
                DataGridView dgv = (button.FindForm().Controls.Find(controlName, true)[0] as DataGridView);
                int step = 5;
                int.TryParse(dgv.Tag.ToString(), out step);
                if (button.Text == "▲")
                {
                    for (int i = 0; i < step; i++)
                    {
                        if (dgv.FirstDisplayedScrollingRowIndex - 1 >= 0)
                        {
                            dgv.FirstDisplayedScrollingRowIndex = dgv.FirstDisplayedScrollingRowIndex - 1;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < step; i++)
                    {
                        if (dgv.FirstDisplayedScrollingRowIndex + 1 < dgv.Rows.Count)
                        {
                            dgv.FirstDisplayedScrollingRowIndex = dgv.FirstDisplayedScrollingRowIndex + 1;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                controlName = null;
                dgv = null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}