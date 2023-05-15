using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Baza
{
    public partial class FormModify : Form
    {
        Form1 form;
        int nrStrony;
        string wyszukaj;
        string nameBeforeModify;
        string surnameBeforeModify;
        int idCell;
        public FormModify(int _nrStrony, string _wyszukaj, string _nameBeforeModify, string _surnameBeforeModify, int _idCell, Form1 Form1)
        {
            form = Form1;
            nrStrony = _nrStrony;
            wyszukaj = _wyszukaj;
            nameBeforeModify = _nameBeforeModify;
            surnameBeforeModify = _surnameBeforeModify;
            idCell = _idCell;
            InitializeComponent();
            textBox1.Text = nameBeforeModify;
            textBox2.Text = surnameBeforeModify;
        }

        private void FormModify_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Zatwierdzanie modyfikacji
            modelOsoba m1 = new modelOsoba();
            m1.Modify(idCell, textBox1.Text, textBox2.Text);
            form.m1.Start(wyszukaj, nrStrony);
            this.Close();
        }
    }
}
