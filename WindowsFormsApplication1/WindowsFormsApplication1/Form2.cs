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
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        bool allowClick = false;
        PictureBox firstGuess;
        Random rnd = new Random();
        Timer clickTimer = new Timer();
        int time = 60;
        SoundPlayer sp = new SoundPlayer("1.wav");

        Timer timer = new Timer { Interval = 1000 };

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            sp.Play();
        }

        private PictureBox[] pictureBoxes
        {
            get { return Controls.OfType<PictureBox>().ToArray(); }
        }

        private static IEnumerable<Image> images
        {
            get
            {
                return new Image[]
                {
                   Properties.Resources._1,
                   Properties.Resources._2,
                   Properties.Resources._3,
                   Properties.Resources._4,
                   Properties.Resources._5,
                   Properties.Resources._6,
                };
            }
        }

        private void clickImage(object sender, EventArgs e)
        {
            if (!allowClick) return;
            var pic = (PictureBox)sender;
            if (firstGuess == null)
            {
                firstGuess = pic;
                pic.Image = (Image)pic.Tag;
                return;
            }
            pic.Image = (Image)pic.Tag;
            if (pic.Image == firstGuess.Image && pic != firstGuess)
            {
                    pic.Visible = firstGuess.Visible = false;
                    {
                        firstGuess = pic;
                    }
                HideImages();
            }
            else
            {
                allowClick = false;
                clickTimer.Start();
            }
            firstGuess = null;
            if (pictureBoxes.Any(p => p.Visible)) return;
            timer.Stop();

            Form5 f5 = new Form5();

            string[] d =File.ReadAllLines("Рекорд.txt");

            using (StreamWriter sr = new StreamWriter("Рекорд.txt",false, System.Text.Encoding.Default))
            {
                if (Convert.ToDateTime(label1.Text) > Convert.ToDateTime(d[0]))
                {
                    d[0] = label1.Text;
                    for (int i = 0; i < d.Length; i++)
                    {
                        sr.WriteLine(d[i]);
                    }
                }
            }
            DialogResult der=MessageBox.Show("Вы Выйграли!!!) \nПопробуете снова?","Победа!",MessageBoxButtons.YesNo,MessageBoxIcon.None);
            if (der == DialogResult.Yes) { ResetImages(); sp.Play(); }
            else if (der == DialogResult.No)
            {
                Form q = Application.OpenForms[0];
                sp.Stop();
                q.Visible = true;
                Close();
            }

        }

        private void CLICKTIMER_TICK(object sender, EventArgs e)
        {
            HideImages();
            allowClick = true;
            clickTimer.Stop();
        }

        private void startGameTimer()
        {
            timer.Start();
            timer.Tick += delegate
            {
                time--;
                if (time < 0)
                {
                    timer.Stop();

                    DialogResult der = MessageBox.Show("Время вышло!( \nПопробуете еще раз?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                    if (der == DialogResult.Yes) { ResetImages(); sp.Play(); }
                    else if (der == DialogResult.No)
                    {
                        Form d = Application.OpenForms[0];
                        d.Visible = true;
                        sp.Stop();
                        this.Close();
                    }
                }
                var ssTime = TimeSpan.FromSeconds(time);
                label1.Text = "00: " + time.ToString();
            };
        }

        private void ResetImages()
        {
            foreach (var pic in pictureBoxes)
            {
                pic.Tag = null;
                pic.Visible = true;
            }
            HideImages();
            setRandomImages();
            time = 60;
            timer.Start();
        }

        private void HideImages()
        {
            foreach (var pic in pictureBoxes)
            {
                pic.Image = Properties.Resources.Обложка;
            }
        }

        private void setRandomImages()
        {
            foreach (var image in images)
            {
                getFreeSlot().Tag = image;
                getFreeSlot().Tag = image;
            }
        }

        private PictureBox getFreeSlot()
        {
            int num;
            do
            {
                num = rnd.Next(0, pictureBoxes.Count());
            }
            while (pictureBoxes[num].Tag != null);
            return pictureBoxes[num];
        }

        private void startGame(object sender, EventArgs e)
        {
            allowClick = true;
            setRandomImages();
            HideImages();
            startGameTimer();
            clickTimer.Interval = 1000;
            clickTimer.Tick += CLICKTIMER_TICK;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == true)
            {
                timer.Stop();
                DialogResult der = MessageBox.Show("Закончить попытку?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (der == DialogResult.Yes)
                {
                    Form d = Application.OpenForms[0];
                    d.Visible = true;
                    sp.Stop();
                    this.Close();
                }
                else if (der == DialogResult.No)
                {
                }
            }
            else
            {
                Form d = Application.OpenForms[0];
                d.Visible = true;
                sp.Stop();
                this.Close();
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        int ss = 1;
        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap image1 = new Bitmap("54309.png");
            Bitmap image2 = new Bitmap("573.png");
            if (ss == 1)
            {
                button3.BackgroundImage = image1;
                sp.Stop();
                ss = 0;
            }
            else
            {
                button3.BackgroundImage = image2;
                sp.Play();
                ss = 1;
            }
        }
    }
}

