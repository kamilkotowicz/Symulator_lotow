﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Symulator_lotow
{
    
    
    public partial class MainForm : Form
    {
        private Ekran ekran = new Ekran();
        public MainForm()
        {
            InitializeComponent();
        }
        public PictureBox pictureBox1 = new PictureBox();
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BackColor = Color.White;
            this.Controls.Add(pictureBox1);
        }
        public void Redraw()
        {
            this.pictureBox1.Invalidate();
        }

    }

}
