using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Baza
{
    public partial class FormAdd : Form
    {
        Form1 form;
        int nrStrony;
        string wyszukaj;
        public FormAdd(int _nrStrony, string _wyszukaj, Form1 Form1)
        {
            form = Form1;
            nrStrony = _nrStrony;
            wyszukaj = _wyszukaj;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Zatwierdzanie dodawania
            try
            {
                modelOsoba m1 = new modelOsoba();
                int gender_id = 0;
                if (!checkBox1.Checked)
                {
                    MessageBox.Show("Akceptacja", "Nie zatwierdzono");
                    return;
                }
                if (radioButton2.Checked)
                {
                    gender_id = 1;
                }
                m1.Add(textBox1.Text, textBox2.Text, gender_id, comboBox1.SelectedIndex, richTextBox1.Text);
                form.m1.Start(wyszukaj, nrStrony);
                form.update();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Podano złe dane", "Błąd");
            }

        }

        private void FormAdd_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
        }

    }
}
