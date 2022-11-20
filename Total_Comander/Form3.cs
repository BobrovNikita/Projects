using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Total_Comander
{
    public partial class Form3 : Form
    {
        private string? fullNameFile;
        private string? fullNameDir;
        int number;
        public Form3(string? fullNameFile, string? fullNameDir, int number)
        {
            InitializeComponent();
            this.fullNameFile = fullNameFile;
            this.fullNameDir = fullNameDir;
            this.number = number;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (number == 0)
            {
                DirectoryInfo dir = new DirectoryInfo(fullNameFile);
                textBox1.Text = dir.Name;
            }
            else
            {
                FileInfo file = new FileInfo(fullNameFile);
                textBox1.Text = file.Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = textBox1.Text.Trim();
                if (number == 0)
                {
                    if (textBox1 != null && textBox1.Text != "")
                    {
                        DirectoryInfo dir = new DirectoryInfo(fullNameFile);

                        if (dir.Name == textBox1.Text)
                        {
                            this.Close();
                            return;
                        }

                        if (dir.Exists)
                        {
                            string reName = Path.Combine(fullNameDir, textBox1.Text);
                            dir.MoveTo(reName);
                            this.Close();
                        }
                    }
                }
                else
                {
                    if (textBox1 != null && textBox1.Text != "")
                    {
                        FileInfo file = new FileInfo(fullNameFile);

                        if (file.Name == textBox1.Text)
                        {
                            this.Close();
                            return;
                        }

                        if (file.Exists)
                        {
                            string reName = Path.Combine(fullNameDir, textBox1.Text);
                            file.MoveTo(reName);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\\' || e.KeyChar == '.' || e.KeyChar == '/')
            {
                e.Handled = true;
            }
        }
    }
}
