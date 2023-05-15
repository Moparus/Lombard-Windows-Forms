using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.Text.RegularExpressions;

namespace Baza
{

    public partial class Form1 : Form
    {
        public modelOsoba m1 = new modelOsoba();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            update();
            numericUpDown1.Value = 6;
        }
        public void update()
        {
            //Resetowanie listy
            dataGridView1.DataSource = null;
            m1.Start(textBox3.Text, System.Convert.ToInt32(label1.Text));
            dataGridView1.DataSource = m1.lista;
            dataGridView1.Columns["gender"].Visible = false;
            dataGridView1.Columns["voivodeship"].Visible = false;
            dataGridView1.Columns["loan"].DisplayIndex = 3;
            dataGridView1.Columns["description"].Visible = false;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Wyświetlanie menu przy kliknięciu na rekord
            try
            {
                //Zebranie ID z klikniętego elementu
                var idToModifyStrip = dataGridView1.SelectedRows[0].Index;
                dataGridView1.SelectedRows[0].ContextMenuStrip = contextMenuStrip1;
            }
            catch
            {
                return;
            }
        }

        private void dodajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Otwieranie Formularza do dodawania i przekazanie numeru strony
            FormAdd add = new FormAdd(System.Convert.ToInt32(label1.Text), textBox3.Text, this);
            add.Show();
            this.Enabled = false;
            update();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Usuwanie elementu
            try
            {
                //Zebranie ID z wybranego elementu
                var idToDeleteStrip = dataGridView1.SelectedRows[0].Index;
                idToDeleteStrip = System.Convert.ToInt32(dataGridView1.Rows[idToDeleteStrip].Cells[1].Value);

                //Usuwanie rekordu z użyciem modelu
                m1.Delete(System.Convert.ToInt32(idToDeleteStrip));
                m1.Start(textBox3.Text, System.Convert.ToInt32(label1.Text));
                update();
            }
            catch
            {
                MessageBox.Show("Nie wybrano ID", "Błąd");
            }
        }

        private void edytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Otwieranie Formularza do edycji
            try
            {
                //Zebranie ID z wybranego elementu
                var idToModifyStrip = dataGridView1.SelectedRows[0].Index;
                dataGridView1.SelectedRows[0].ContextMenuStrip = contextMenuStrip1;

                //Zebranie danych do FormModify
                var nameBeforeModify = dataGridView1.Rows[idToModifyStrip].Cells[2].Value.ToString();
                var surnameBeforeModify = dataGridView1.Rows[idToModifyStrip].Cells[3].Value.ToString();
                var genderBeforeModify = Convert.ToInt32(dataGridView1.Rows[idToModifyStrip].Cells[4].Value);
                var voivodeshipBeforeModify = Convert.ToInt32(dataGridView1.Rows[idToModifyStrip].Cells[5].Value);
                var descriptionBeforeModify = dataGridView1.Rows[idToModifyStrip].Cells[6].Value.ToString();

                var idToModifyStripCell = System.Convert.ToInt32(dataGridView1.Rows[idToModifyStrip].Cells[1].Value);

                //Wygenerowanie formularza wraz z przekazaniem danych
                FormModify modify = new FormModify(System.Convert.ToInt32(label1.Text), textBox3.Text, nameBeforeModify, surnameBeforeModify, idToModifyStripCell, this, genderBeforeModify, voivodeshipBeforeModify, descriptionBeforeModify);
                modify.Show();
                this.Enabled = false;
                update();
            }
            catch
            {
                MessageBox.Show("Nie wybrano ID", "Błąd");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Poprzednia Strona
            if (System.Convert.ToInt32(label1.Text) != 1)
            {
                label1.Text = (System.Convert.ToInt32(label1.Text) - 1).ToString();
            }
            update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Następna Strona
            label1.Text = (System.Convert.ToInt32(label1.Text) + 1).ToString();
            update();
            if (m1.lista.Count() == 0)
            {
                label1.Text = (System.Convert.ToInt32(label1.Text) - 1).ToString();
                update();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Wyszukiwanie
            m1.Start(textBox3.Text, 0);
            update();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            m1.offSetNumber = Convert.ToInt32(numericUpDown1.Value);
            update();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["loan"].Index)
            {
                //Otwieranie Formularza do pożyczek
                try
                {
                    //Zebranie ID z wybranego elementu
                    var idToModifyStrip = dataGridView1.SelectedRows[0].Index;
                    dataGridView1.SelectedRows[0].ContextMenuStrip = contextMenuStrip1;
                    var idToModifyStripCell = System.Convert.ToInt32(dataGridView1.Rows[idToModifyStrip].Cells[1].Value);
                    var imie = dataGridView1.Rows[idToModifyStrip].Cells[2].Value.ToString();
                    var nazwisko = dataGridView1.Rows[idToModifyStrip].Cells[3].Value.ToString();
                    var dane = imie + " " + nazwisko;

                    //Wygenerowanie formularza wraz z przekazaniem danych
                    FormLoan loan = new FormLoan(idToModifyStripCell, dane,this);
                    loan.Show();
                    this.Enabled = false;
                    update();
                }
                    catch
                {
                    MessageBox.Show("Nie wybrano ID", "Błąd");
                }
            }
        }
    }
}
