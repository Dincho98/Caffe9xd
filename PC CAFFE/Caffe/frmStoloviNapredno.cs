using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmStoloviNapredno : Form
    {
        //Variables
        List<string> tablesInUse_Names;
        List<string> allTable_Names;

        //Constructor
        public frmStoloviNapredno()
        {
            InitializeComponent();
            this.Size = new Size(520, 118);
            HideAllPanels();
            LoadComboBoxValuesIntoLists();
            LoadAllComboBoxes();
        }

        #region MenuButtons
        private void buttonSpojiStolove_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            this.Size = new Size(520, 273);
            panelSpojiStolove.Visible = true;
        }

        private void buttonPrebaciStolove_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            this.Size = new Size(520, 462);
            panelPrebaciStolove.Visible = true;
        }

        private void HideAllPanels()
        {
            panelSpojiStolove.Visible = false;
            panelPrebaciStolove.Visible = false;
        }
        #endregion

        #region ComboBoxes
        private void LoadComboBoxValuesIntoLists()
        {
            //-----Load tables that are in use
            string sqlQuery = "select distinct on (na_stol.id_stol) na_stol.*,stolovi.naziv from na_stol,stolovi where na_stol.id_stol=stolovi.id_stol;";
            DataTable DTUpotrijebljeniStolovi = classSQL.select(sqlQuery, "na_stol").Tables[0];

            //Dodaj nazive stolova u listu
            tablesInUse_Names = new List<string>();
            foreach (DataRow row in DTUpotrijebljeniStolovi.Rows)
            {
                tablesInUse_Names.Add(row[0].ToString() + "|" + row["naziv"].ToString());
            }

            //-----Load all tables
            sqlQuery = "SELECT * FROM stolovi ORDER BY id_stol";
            DataTable DTSlobodniStolovi = classSQL.select(sqlQuery, "stolovi").Tables[0];

            //Dodaj nazive stolova u listu
            allTable_Names = new List<string>();
            foreach (DataRow row in DTSlobodniStolovi.Rows)
            {
                allTable_Names.Add(row["id_stol"].ToString() + "|" + row["naziv"].ToString());
            }
        }

        private void LoadSpecificComboBoxWithTablesInUse(ComboBox comboBox)
        {
            foreach (string item in tablesInUse_Names)
            {
                comboBox.Items.Add(item);
            }

            //Ako je baza prazna da se ne baci error
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private void LoadSpecificComboBoxWithAllTables(ComboBox comboBox)
        {
            foreach (string item in allTable_Names)
            {
                comboBox.Items.Add(item);
            }
        }

        private void LoadAllComboBoxes()
        {
            LoadSpecificComboBoxWithTablesInUse(comboBoxStol1Spoji);
            LoadSpecificComboBoxWithTablesInUse(comboBoxStol2Spoji);
            LoadSpecificComboBoxWithTablesInUse(comboBoxStol1Prebaci);
            LoadSpecificComboBoxWithAllTables(comboBoxStol2Prebaci);
        }
        #endregion

        #region Spojiti
        /// <summary>
        /// SPOJITI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSpoji_Click(object sender, EventArgs e)
        {
            if (CheckIfSameTablesAreSelected(comboBoxStol1Spoji.Text, comboBoxStol2Spoji.Text))
            {
                MessageBox.Show("Potrebno je odabrati različite stolove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxStol1Spoji.SelectedIndex == -1 || comboBoxStol2Spoji.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati stolove za spajanje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string idStol_1 = comboBoxStol1Spoji.SelectedItem.ToString().Split('|')[0];
            string idStol_2 = comboBoxStol2Spoji.SelectedItem.ToString().Split('|')[0];
            string imeStol_1 = comboBoxStol1Spoji.SelectedItem.ToString().Split('|')[1];
            string imeStol_2 = comboBoxStol2Spoji.SelectedItem.ToString().Split('|')[1];
            string sqlQuery = string.Empty;
            //U tablici na_stol mjenjamo id_stol iz comboboxa1 u id_stol iz comboboxa2
            sqlQuery = $@"UPDATE na_stol SET id_stol={idStol_2} WHERE id_stol={idStol_1}";
            classSQL.update(sqlQuery);

            MessageBox.Show($"Stol '{imeStol_1}'({idStol_1}) spojen na '{imeStol_2}'({idStol_2}).", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();

        }
        #endregion

        #region Prebacivanje
        /// <summary>
        /// PREBACITI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxStol1Prebaci_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxStol1.Items.Clear();
            listBoxStol1.Items.Clear();
            LoadItemsIntoListBox(comboBoxStol1Prebaci, listBoxStol1);
        }

        private void comboBoxStol2Prebaci_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxStol2.Items.Clear();
            LoadItemsIntoListBox(comboBoxStol2Prebaci, listBoxStol2);
        }

        private void LoadItemsIntoListBox(ComboBox comboBox, ListBox listBox)
        {
            string idStol = comboBox.SelectedItem.ToString().Split('|')[0];
            string sqlQuery = $"SELECT na_stol.*,roba.naziv FROM na_stol INNER JOIN roba ON na_stol.sifra=roba.sifra WHERE na_stol.id_stol={idStol};";
            DataTable DTStolPodaci = classSQL.select(sqlQuery, "na_stol").Tables[0];

            if (DTStolPodaci.Rows.Count > 0)
            {
                foreach (DataRow row in DTStolPodaci.Rows)
                {
                    listBox.Items.Add($@"{row["naziv"].ToString()} | {row["kom"].ToString()} kom                                                ID?{row["sifra"].ToString()}");
                }
            }

        }

        private void buttonPrebaciDesno_Click(object sender, EventArgs e)
        {
            //Checks
            if (!CheckIfComboBoxesAreSelected(comboBoxStol1Prebaci.SelectedIndex, comboBoxStol2Prebaci.SelectedIndex))
            {
                MessageBox.Show("Morate odabrati oba stola.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckIfSameTablesAreSelected(comboBoxStol1Prebaci.Text, comboBoxStol2Prebaci.Text))
            {
                MessageBox.Show("Ne možete prebacivati na istom stolu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listBoxStol1.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati piće/hranu za prijenos na drugi stol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TransferItem(listBoxStol1, true);
        }


        private void buttonPrebaciLijevo_Click(object sender, EventArgs e)
        {
            //Checks
            if (!CheckIfComboBoxesAreSelected(comboBoxStol1Prebaci.SelectedIndex, comboBoxStol2Prebaci.SelectedIndex))
            {
                MessageBox.Show("Morate odabrati oba stola.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckIfSameTablesAreSelected(comboBoxStol1Prebaci.Text, comboBoxStol2Prebaci.Text))
            {
                MessageBox.Show("Ne možete prebacivati na istom stolu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listBoxStol2.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati piće/hranu za prijenos na drugi stol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TransferItem(listBoxStol2, false);
        }


        private void buttonPrebaciDesnoSve_Click(object sender, EventArgs e)
        {
            //Checks
            if (!CheckIfComboBoxesAreSelected(comboBoxStol1Prebaci.SelectedIndex, comboBoxStol2Prebaci.SelectedIndex))
            {
                MessageBox.Show("Morate odabrati oba stola.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckIfSameTablesAreSelected(comboBoxStol1Prebaci.Text, comboBoxStol2Prebaci.Text))
            {
                MessageBox.Show("Ne možete prebacivati na istom stolu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TransferAllItems(true);
        }

        private void buttonPrebaciLijevoSve_Click(object sender, EventArgs e)
        {          
            //Checks
            if (!CheckIfComboBoxesAreSelected(comboBoxStol1Prebaci.SelectedIndex, comboBoxStol2Prebaci.SelectedIndex))
            {
                MessageBox.Show("Morate odabrati oba stola.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckIfSameTablesAreSelected(comboBoxStol1Prebaci.Text, comboBoxStol2Prebaci.Text))
            {
                MessageBox.Show("Ne možete prebacivati na istom stolu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TransferAllItems(false);

        }

        private void buttonPrebaci_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Završeno prebacivanje s stola na stol.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void TransferItem(ListBox listBox, bool stol1NaStol2)
        {
            string idStol_1 = comboBoxStol1Prebaci.Text.Split('|')[0];
            string idStol_2 = comboBoxStol2Prebaci.Text.Split('|')[0];

            string listBoxSelectedItem = listBox.SelectedItem.ToString();
            string listBoxSelectedItem_Sifra = listBoxSelectedItem.Split('?')[1];

            string sqlQuery = $"UPDATE na_stol SET id_stol={(stol1NaStol2 ? idStol_2 : idStol_1)} WHERE id=(SELECT id FROM na_stol WHERE id_stol={(stol1NaStol2 ? idStol_1 : idStol_2)} AND sifra='{listBoxSelectedItem_Sifra}' LIMIT 1);";
            classSQL.update(sqlQuery);

            MessageBox.Show("Prebačeno!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshListBox();
        }

        private void TransferAllItems(bool stol1NaStol2)
        {   
            string idStol_1 = comboBoxStol1Prebaci.Text.Split('|')[0];
            string idStol_2 = comboBoxStol2Prebaci.Text.Split('|')[0];

            string sqlQuery = $"UPDATE na_stol SET id_stol={(stol1NaStol2?idStol_2:idStol_1)} WHERE id_stol={(stol1NaStol2?idStol_1:idStol_2)}";
            classSQL.update(sqlQuery);

            MessageBox.Show("Prebačeno!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshListBox();
        }

        private void RefreshListBox()
        {
            listBoxStol1.Items.Clear();
            LoadItemsIntoListBox(comboBoxStol1Prebaci, listBoxStol1);
            listBoxStol2.Items.Clear();
            LoadItemsIntoListBox(comboBoxStol2Prebaci, listBoxStol2);
        }
        #endregion

        #region Checks
        private bool CheckIfSameTablesAreSelected(string cbText1, string cbText2)
        {
            return cbText1== cbText2;
        }

        private bool CheckIfComboBoxesAreSelected(int cb1Index, int cb2Index)
        {
            if (cb1Index >= 0 && cb2Index >= 0)
                return true;
            return false;
        }
        #endregion

    }
}
