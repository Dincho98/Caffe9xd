using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //Dino (14.8.2019.): "Dejan je rekel ka moram napraviti kaj se od 1.10.2019. ne moze pokrenuti program."
            DateTime TodaysDate = DateTime.Now;
            DateTime NeDelaDate = new DateTime(2019, 10, 1, 0, 0, 1);
            if (TodaysDate.CompareTo(NeDelaDate) > 0)
            {
                MessageBox.Show("Javite se tvrtci Code-IT.","Informacija",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Environment.Exit(1);
            }

            if (!File.Exists("NeProvjeravjVisePrograma"))
            {
                try
                {
                    Process[] pro = Process.GetProcessesByName("PC POS");

                    if (pro.Count() > 1)
                    {
                        MessageBox.Show("Program je več upaljen. Potražite ga na alatnoj traci.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new Caffe.frmOdabirStolaCustom());
                //Application.Run(new Caffe.frmStoloviZaNaplatuCustom());

                Application.Run(new frmMenu());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}