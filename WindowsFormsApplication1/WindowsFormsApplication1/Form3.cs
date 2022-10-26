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
    public partial class Form3 : Form
    {
        bool allowClick = false;
        PictureBox firstGuess;
        Random rnd = new Random();
        Timer clickTimer = new Timer();
        int time = 90;

        Timer timer = new Timer { Interval = 1000 };
        SoundPlayer sp = new SoundPlayer("2.wav");

        public Form3()
        {
            InitializeComponent();
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
                   Properties.Resources._21,
                   Properties.Resources._22,
                   Properties.Resources._23,
                   Properties.Resources._24,
                   Properties.Resources._25,
                   Properties.Resources._26,
                   Properties.Resources._27,
                   Properties.Resources._28,
                   Properties.Resources._29,
                   Properties.Resources._210,
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
                //  MessageBox.Show("dfghjk");
                //System.Threading.Thread.Sleep(1000);
                for (int i = 0,  a=0; i < 1000; i++) { a++; }
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

            string[] q = File.ReadAllLines("Рекорд.txt");

            using (StreamWriter sr = new StreamWriter("Рекорд.txt", false, System.Text.Encoding.Default))
            {
                if (Convert.ToDateTime(label1.Text) > Convert.ToDateTime(q[1]))
                {
                    q[1] = label1.Text;
                    for (int i = 0; i < q.Length; i++)
                    {
                        sr.WriteLine(q[i]);
                    }
                }
            }


            DialogResult der = MessageBox.Show("Вы Выйграли!!!) \nПопробуете снова?", "Победа!", MessageBoxButtons.YesNo, MessageBoxIcon.None);
            if (der == DialogResult.Yes) { ResetImages(); sp.Play(); }
            else if (der == DialogResult.No)
            {
                Form d = Application.OpenForms[0];
                d.Visible = true;
                sp.Stop();
                this.Close();
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
                    if (der == DialogResult.Yes) { ResetImages();sp.Play(); }
                    else if (der == DialogResult.No)
                    {
                        Form d = Application.OpenForms[0];
                        d.Visible = true;
                        sp.Stop();
                        this.Close();
                    }

                    //    MessageBox.Show("Время вышло!(");
                    //ResetImages();
                }
                var ssTime = TimeSpan.FromSeconds(time);
                if (time > 60)
                    label1.Text = "01: " + (time - 60).ToString();
                else label1.Text = "00: " + (time-30).ToString();
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
                pic.Image = Properties.Resources.Обложка2;
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

        private void Form3_Load(object sender, EventArgs e)
        {
            sp.Play();
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
                sp.Stop();
                d.Visible = true;
                this.Close();
            }
            }
        int ss=1;
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

