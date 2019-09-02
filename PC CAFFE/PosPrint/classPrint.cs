using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;

namespace PCPOS.PosPrint
{
    internal class classPrint
    {
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private static DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private static string tekst = "";

        public static void printaj(string tt)
        {
            tekst = tt;
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                tekst += Environment.NewLine;
            }

            //if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
            //{
            string ttx = "\r\n" + tekst;
            ttx = ttx.Replace("č", "c");
            ttx = ttx.Replace("Č", "C");
            ttx = ttx.Replace("ž", "z");
            ttx = ttx.Replace("Ž", "Z");
            ttx = ttx.Replace("ć", "c");
            ttx = ttx.Replace("Ć", "C");
            ttx = ttx.Replace("đ", "d");
            ttx = ttx.Replace("Đ", "D");
            ttx = ttx.Replace("š", "s");
            ttx = ttx.Replace("Š", "S");

            string GS = Convert.ToString((char)29);
            string ESC = Convert.ToString((char)27);

            string COMMAND = "";
            COMMAND = ESC + "@";
            COMMAND += GS + "V" + (char)1;

            RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);
        }

        private static void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 10);

            //header
            String drawString = tekst;
            Font drawFont = font;
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(tekst, drawFont);

            drawFont = font;
            float y = 0.0F;
            float x = 0.0F;

            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }
    }
}