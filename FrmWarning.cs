﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleShutdown
{
    public partial class FrmWarning : Form
    {
        public FrmWarning()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Walang Paggalaw! \n Galawin ang Mouse upang hindi mag Shutdown ang Computer mo.";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
