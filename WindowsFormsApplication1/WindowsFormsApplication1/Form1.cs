using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Visible = false;
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Visible = false;
            form3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            this.Visible = false;
            form4.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult der = MessageBox.Show("Вы действительно хотите выйти?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            if (der == DialogResult.Yes)  { }  /* Application.Exit();*/
            else if (der == DialogResult.No) e.Cancel = true; 
        }

        private void правилаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Игрок должен найти как можно больше парных картинок за отведенное время. В окне главного меню можно выбрать уровень сложности, при его изменении меняется не только количество картинок, но и отведенное время)","*****Правила игры*****");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            this.Visible = false;
            f5.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
