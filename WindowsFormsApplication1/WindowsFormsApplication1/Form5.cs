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

namespace WindowsFormsApplication1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form d = Application.OpenForms[0];
            d.Visible = true;
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string[] r = File.ReadAllLines("Рекорд.txt");
            label4.Text = r[0];
            label5.Text = r[1];
            label6.Text = r[2];
            
        }
    }
}
