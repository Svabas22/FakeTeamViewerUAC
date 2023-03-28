using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uac
{
    public partial class Teamviewer : Form
    {
        public Teamviewer()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Icon = Properties.Resources.logo2;
            this.Text = "TeamViewer Setup";
        }
        public void SetProgressValue(int value)
        {
            progressBar1.Value = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
