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
    public partial class FormLoan : Form
    {
        private int osobaID;
        private string dane;
        private modelPozyczka p1 = new modelPozyczka();
        private Form1 form;
        public FormLoan(int osobaID, string dane, Form1 form)
        {
            this.dane = dane;
            this.osobaID = osobaID;
            this.form = form;
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            update();
            numericUpDown1.Value = 6;
        }
        public void update()
        {
            //Resetowanie listy
            dataGridView1.DataSource = null;
            p1.Start(osobaID, System.Convert.ToInt32(label1.Text));
            dataGridView1.DataSource = p1.lista;
            dataGridView1.Columns["id"].Width = 35;
            dataGridView1.Columns["stan"].Width = 65;
            dataGridView1.Columns["osoba_id"].Visible = false;
            dataGridView1.Columns["zabezpieczenie"].Visible = false;
            dataGridView1.Columns["details"].DisplayIndex = 7;

            label2.Text = dane;
        }

        private void FormLoan_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
        }

        private void dodajToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Otwieranie Formularza do dodawania i przekazanie numeru strony
            FormLoanAdd add = new FormLoanAdd(System.Convert.ToInt32(label1.Text), this, osobaID);
            add.Show();
            this.Enabled = false;
            update();
        }


        private void usuńToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Usuwanie elementu
            try
            {
                //Zebranie ID z wybranego elementu
                var idToDeleteStrip = dataGridView1.SelectedRows[0].Index;
                idToDeleteStrip = System.Convert.ToInt32(dataGridView1.Rows[idToDeleteStrip].Cells[1].Value);

                //Usuwanie rekordu z użyciem modelu
                p1.Delete(System.Convert.ToInt32(idToDeleteStrip));
                p1.Start(osobaID, System.Convert.ToInt32(label1.Text));
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
            if (dataGridView1.RowCount == 0)
            {
                label1.Text = (System.Convert.ToInt32(label1.Text) - 1).ToString();
                update();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            p1.offSetNumber = Convert.ToInt32(numericUpDown1.Value);
            update();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["details"].Index)
            {
                //Otwieranie Formularza do pożyczek
                try
                {
                    //Zebranie ID z wybranego elementu
                    var idToModifyStrip = dataGridView1.SelectedRows[0].Index;
                    //dataGridView1.SelectedRows[0].ContextMenuStrip = contextMenuStrip1;
                    var idToModifyStripCell = System.Convert.ToInt32(dataGridView1.Rows[idToModifyStrip].Cells[1].Value);
                    var total = System.Convert.ToInt32(dataGridView1.Rows[idToModifyStrip].Cells[4].Value);

                    //Wygenerowanie formularza wraz z przekazaniem danych
                    FormRepay loan = new FormRepay(idToModifyStripCell, total, this);
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
