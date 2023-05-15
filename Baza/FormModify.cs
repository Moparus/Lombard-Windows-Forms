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
        int genderBeforeModify;
        int voivodeshipBeforeModify;
        string descriptionBeforeModify;

        int idCell;
        public FormModify(int _nrStrony, string _wyszukaj, string _nameBeforeModify, string _surnameBeforeModify, int _idCell, Form1 Form1, int _genderBeforeModify, int _voivodeshipBeforeModify, string _descriptionBeforeModify)
        {
            form = Form1;
            nrStrony = _nrStrony;
            wyszukaj = _wyszukaj;
            nameBeforeModify = _nameBeforeModify;
            surnameBeforeModify = _surnameBeforeModify;
            genderBeforeModify = _genderBeforeModify;
            voivodeshipBeforeModify = _voivodeshipBeforeModify;
            descriptionBeforeModify = _descriptionBeforeModify;
            idCell = _idCell;
            InitializeComponent();
            comboBox1.SelectedIndex = voivodeshipBeforeModify;
            textBox1.Text = nameBeforeModify;
            textBox2.Text = surnameBeforeModify;
            richTextBox1.Text = descriptionBeforeModify;

            if (genderBeforeModify == 0)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            comboBox1.SelectedValue = voivodeshipBeforeModify;
        }

        private void FormModify_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int gender_id = 0;
            if (radioButton2.Checked)
            {
                gender_id = 1;
            }
            //Zatwierdzanie modyfikacji
            modelOsoba m1 = new modelOsoba();
            m1.Modify(idCell, textBox1.Text, textBox2.Text, gender_id, comboBox1.SelectedIndex, richTextBox1.Text);
            form.m1.Start(wyszukaj, nrStrony);
            this.Close();
        }
    }
}
