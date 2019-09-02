using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmNewInfo : Form
    {
        public frmNewInfo()
        {
            InitializeComponent();
        }

        private void frmNewInfo_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = LoadDataFromServer();
        }

        private string LoadDataFromServer()
        {
            string fileString = "";
            string fileName = @"promjene.txt";
            string url = $"ftp://5.189.154.50/CodeCaffe/{fileName}";
            using (WebClient req = new WebClient())
            {
                try
                {
                    req.Credentials = new NetworkCredential("codeadmin", "Eqws64%2");
                    byte[] fileData = req.DownloadData(url);
                    fileString = Encoding.UTF8.GetString(fileData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception 1:" + ex);
                }
            }
            return fileString;
        }
    }
}