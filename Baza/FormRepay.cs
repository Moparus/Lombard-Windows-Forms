using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Baza
{
    public partial class FormRepay : Form
    {
        private modelSplata s1 = new modelSplata();
        private int loan_id;
        private int kwota_pozyczki;
        private FormLoan form;

        public FormRepay(int _loan_id, int _kwota_pozyczki, FormLoan form)
        {
            loan_id = _loan_id;
            kwota_pozyczki = _kwota_pozyczki;
            this.form = form;
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            update();
        }
        public void update()
        {
            //Resetowanie listy
            if ((kwota_pozyczki - s1.RepayedTotal(loan_id) == 0))
            {
                s1.LoanPaid(loan_id, 2);
            }
            else
            {
                s1.LoanPaid(loan_id, 1);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = s1.Start(loan_id, 1);
            label2.Text = "Pożyczka: " + kwota_pozyczki;
            label3.Text = "Spłacone: " + s1.RepayedTotal(loan_id);
            label4.Text = "Należność: " + (kwota_pozyczki - s1.RepayedTotal(loan_id));
            dataGridView1.Columns["pozyczki_id"].Visible = false;
            dataGridView1.Columns["id"].Width = 25; 
            /* dataGridView1.Columns["osoba_id"].Visible = false;
            dataGridView1.Columns["zabezpieczenie"].Visible = false;
            dataGridView1.Columns["details"].DisplayIndex = 7;*/
        }


        private void dodajToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Otwieranie Formularza do dodawania i przekazanie numeru strony
            FormRepayAdd add = new FormRepayAdd(loan_id, this,kwota_pozyczki,s1.RepayedTotal(loan_id));
            this.Enabled = false;
            add.Show();
            update();
        }

        private void usuńToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //Zebranie ID z wybranego elementu
                var idToDeleteStrip = dataGridView1.SelectedRows[0].Index;
                idToDeleteStrip = System.Convert.ToInt32(dataGridView1.Rows[idToDeleteStrip].Cells[0].Value);
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //Usuwanie rekordu z użyciem modelu
                s1.Delete(System.Convert.ToInt32(idToDeleteStrip));
                update();
            }
            catch
            {
                MessageBox.Show("Nie wybrano ID", "Błąd");
            }
        }

        private void FormRepay_Activated(object sender, EventArgs e)
        {
            form.update();
        }

        private void FormRepay_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.update();
        }
    }
}
