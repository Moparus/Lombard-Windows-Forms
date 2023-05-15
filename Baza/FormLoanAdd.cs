using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Baza
{
    public partial class FormLoanAdd : Form
    {
        FormLoan form;
        int nrStrony;
        int osobaID;
        public FormLoanAdd(int _nrStrony, FormLoan Form2, int _osobaID)
        {
            form = Form2;
            nrStrony = _nrStrony;
            osobaID = _osobaID;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Zatwierdzanie dodawania
            try
            {
                modelPozyczka p1 = new modelPozyczka();
                if (!checkBox1.Checked)
                {
                    MessageBox.Show("Akceptacja", "Nie zatwierdzono");
                    return;
                }

                p1.Add(osobaID, dateTimePicker1.Value.ToString("dd-MM-yyyy"), Convert.ToInt32(numericUpDown1.Value), dateTimePicker2.Value.ToString("dd-MM-yyyy"), textBox1.Text);
                form.update();
                this.Close();
            }
            catch
            {
                MessageBox.Show("podano złe dane", "błąd");
            }

        }

        private void FormLoanAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
        }
    }
}
