using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace AutoLogon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox1.Enabled = true;
                string userName = Environment.UserName;
                textBox1.Text = userName;
            }
            else
            {
                groupBox1.Enabled = false;
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string winlogon = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
                if (checkBox1.Checked == true)
                {
                    if (string.IsNullOrEmpty(textBox1.Text))
                    {
                        MessageBox.Show("No username specified. Operation aborted", "AutoLogon - ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Restart();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(textBox2.Text))
                        {
                            RegistryKey rk = Registry.LocalMachine.OpenSubKey(winlogon,true);
                            rk.SetValue("AutoAdminLogon", 1, RegistryValueKind.String);
                            rk.SetValue("DefaultUserName", textBox1.Text, RegistryValueKind.String);
                            rk.SetValue("DefaultPassword","", RegistryValueKind.String);
                            rk.Close();
                        }
                        else
                        {
                            RegistryKey rk = Registry.LocalMachine.OpenSubKey(winlogon,true);
                            rk.SetValue("AutoAdminLogon", 1, RegistryValueKind.String);
                            rk.SetValue("DefaultUserName", textBox1.Text, RegistryValueKind.String);
                            rk.SetValue("DefaultPassword", textBox2.Text, RegistryValueKind.String);
                            rk.Close();
                        }
                    }
                    MessageBox.Show("Done. We set this user as AutoLogon user: " + textBox1.Text, "AutoLogon", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else
                {
                    RegistryKey rk = Registry.LocalMachine.OpenSubKey(winlogon,true);
                    rk.SetValue("AutoAdminLogon", 0, RegistryValueKind.String);
                    rk.Close();
                    MessageBox.Show("Done, AutoLogon disabled", "AutoLogon", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"AutoLogon - Application Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string userName = Environment.UserName;
            groupBox1.Enabled = false;
            textBox1.Text = userName;
            textBox2.Text = string.Empty;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            about();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Here you have to type your username","AutoLogon - Help Guide");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Here you have to type your password","AutoLogon - Help Guide");
        }

        private void checkStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
            string autologon = (string)rk.GetValue("AutoAdminLogon");
            if (autologon == "1")
            {
                MessageBox.Show("AutoLogon is enabled","AutoLogon",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("AutoLogon is disabled", "AutoLogon", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void sourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Vichingo455/AutoLogon");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about();
        }
        public void about()
        {
            MessageBox.Show("AutoLogon\nVersion 1.0\nMaded by Vichingo455\nCopyright (C) 2022 Vichingo455. All rights reserved.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void resetFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string userName = Environment.UserName;
            groupBox1.Enabled = false;
            textBox1.Text = userName;
            textBox2.Text = string.Empty;
            checkBox1.Checked = false;
        }

        private void giveAFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Vichingo455/AutoLogon/discussions/new?category=feedback");
        }
    }
}
