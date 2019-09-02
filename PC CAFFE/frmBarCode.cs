using GenCode128;
using Microsoft.Win32;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmBarCode : Form
    {
        public string broj_dokumenta { get; set; }
        public double ukupno { get; set; }

        public frmBarCode()
        {
            InitializeComponent();
        }

        public Image ResizeBarCode(Image img, int heightINT)
        {
            //get the height and width of the image
            int originalW = img.Width;
            int originalH = img.Height;

            //get the new size based on the percentage change
            //int resizedW = (int)(originalW * percentage);
            //int resizedH = (int)(originalH * percentage);

            //create a new Bitmap the size of the new image
            Bitmap bmp = new Bitmap(originalW, heightINT);
            //create a new graphic from the Bitmap
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //draw the newly resized image
            graphic.DrawImage(img, 0, 0, originalW, heightINT);
            //dispose and free up the resources
            graphic.Dispose();
            //return the image
            return (Image)bmp;
        }

        private void frmBarCode_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = classSQL.select("SELECT traje_do,popust,aktivnost FROM promocija1", "promocija1").Tables[0];

                double UKpopust = ukupno * Convert.ToDouble(dt.Rows[0]["popust"].ToString()) / 100;

                DateTime RunsUntil;
                DateTime dvo = DateTime.Now;
                RunsUntil = dvo.AddDays(Convert.ToInt16(dt.Rows[0]["traje_do"].ToString()));
                ;

                Image myimg = Code128Rendering.MakeBarcodeImage("000" + broj_dokumenta, int.Parse("3"), true);
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\barcode.png";
                ResizeBarCode(myimg, 100).Save(path);

                string txt = "<html><body style=\"font-family:Verdana, Geneva, sans-serif\"><div>Naša zahvala za Vašu kupovinu:</div><div>" + UKpopust.ToString("#0.00") + " kn popusta.</div><br/><br/>";
                txt += "<img src=\"" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\barcode.png\" width=\"300\" height=\"100\" /><br/><br/><br/>";

                txt += "<div>Popust odgovara " + dt.Rows[0]["popust"].ToString() + "% vrijednosti kupovine koju dobivate kod iduće kupovine.</div>" +
                    "<div>Trajanje kupona vrijedi do " + RunsUntil.ToString() + "</div>" +
                    "<div>Gotovinska isplata nije moguća. \r\nIznos sljedeće kupovine mora biti jednak ili veći od vrijednosti bona.</div></html></body>";

                webBrowser1.DocumentText = txt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, this.Text);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                string keyName = @"Software\Microsoft\Internet Explorer\PageSetup";
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
                {
                    if (key != null)
                    {
                        string old_footer = key.GetValue("footer").ToString();
                        string old_header = key.GetValue("header").ToString();
                        key.SetValue("footer", "");
                        key.SetValue("header", "");
                    }
                }
            }
            catch (Exception)
            {
            }

            webBrowser1.ShowPrintDialog();
            this.Close();
        }
    }
}