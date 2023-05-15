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
            modelOsoba m1 = new modelOsoba();
            m1.Add(textBox1.Text, textBox2.Text);
            form.m1.Start(wyszukaj, nrStrony);
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Zatwierdzanie dodawania
            modelOsoba m1 = new modelOsoba();
            m1.Add(textBox1.Text, textBox2.Text);
            form.m1.Start(wyszukaj, nrStrony);
            form.update();
            this.Close();
        }

        private void FormAdd_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
        }
    }
}
