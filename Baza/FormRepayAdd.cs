using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Baza
{
    public partial class FormRepayAdd : Form
    {
        private int pozyczka_id;
        private int kwota_pozyczki;
        private int repaid_total;
        private FormRepay form;
        public FormRepayAdd(int pozyczka_id, FormRepay form, int kwota_pozyczki, int repaid_total)
        {
            this.pozyczka_id = pozyczka_id;
            this.kwota_pozyczki = kwota_pozyczki;
            this.repaid_total = repaid_total;
            this.form = form;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (repaid_total + numericUpDown1.Value > kwota_pozyczki)
            {
                MessageBox.Show("Spłata przekracza należność");
                return;
            }
            if (numericUpDown1.Value < 0 || numericUpDown1.Value > 100000)
            {
                MessageBox.Show("Bledne dane");
                return;
            }

            modelSplata m1 = new modelSplata();

            m1.Add(pozyczka_id, Convert.ToInt32(numericUpDown1.Value),dateTimePicker1.Value.ToString("dd-MM-yyyy"));

            this.Close();
        }
        private void FormLoanAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.update();
        }
    }
}
