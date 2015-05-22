﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ketoansoft.app.Components.clsVproUtility;
using ketoansoft.app.Class.Global;

namespace ketoansoft.app
{
    public partial class fTerm : Form
    {
        public static int _month = 0;
        public static int _year = 0;
        public fTerm()
        {
            InitializeComponent();
        }
        public fTerm(int month, int year)
        {
            month = _month;
            year = _year;
        }
        
        private void getTerm()
        {
            
            _month = Utils.CIntDef(cboThang.SelectedText, 0);
            _year = Utils.CIntDef(cboNam.SelectedText, 0);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            getTerm();
            this.Hide();
            fHome _form = new fHome();
            _form.ShowDialog();
            this.Close();
        }

    }
}