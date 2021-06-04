using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IgricaMica
{
    public partial class Form3 : Form
    {
       //Sve tri mice su uradjene po istom sablonu
        
        public float minFirst;
        public float minSecound;
        PointF[] tacke;
        int[,] matrica = new int[4, 4];
        string trenutni_klik;
        string trenutni_igrac = "bijeli";
        string uzimanje_postavljanje = "uzimanje";
        string dio_igre = "rasporedjivanje";
        Form1 forma;
        RectangleF[,] mice = new RectangleF[4, 4];
        Image slika_crni = Properties.Resources.wood_button_png_3;
        Image slika_bijeli = Properties.Resources.wood_button_png_32;
        string uklanjanje = " ";
        int t = 0;
        public Form3(Form1 fm1)
        {
            InitializeComponent();
            this.forma = fm1;
            button1.BackgroundImage = Properties.Resources.download.GetThumbnailImage(button1.Width, button1.Height, null, new IntPtr());
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.Icon = Properties.Resources.nine_men_s_morris_morabaraba_tic_tac_toe_three_men_s_morris_game_png_favpng_rFSzs9kCVAyq7GE6vicyQvgnv;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.DarkOliveGreen, 0, 0, panel1.Width, panel1.Height);
            minFirst = 0.8f * minValue();
             minSecound = 0.4f * minValue();
            g.FillRectangle(Brushes.BurlyWood, (panel1.ClientRectangle.Width - minFirst) / 2f, (panel1.ClientRectangle.Height - minFirst) / 2f, minFirst, minFirst);

            Pen olovka = new Pen(Color.Black, 5f);

           
           
            g.DrawRectangle(olovka, (panel1.ClientRectangle.Width - minFirst) / 2f, (panel1.ClientRectangle.Height - minFirst) / 2f, minFirst, minFirst);
            g.DrawRectangle(olovka, (panel1.ClientRectangle.Width - minSecound) / 2f, (panel1.ClientRectangle.Height - minSecound) / 2f, minSecound, minSecound);

            g.DrawLine(olovka, panel1.Width / 2.0f, (panel1.ClientRectangle.Height - minFirst) / 2f, panel1.Width / 2.0f, (panel1.Height-minFirst)/2+(minFirst-minSecound)/2);
            g.DrawLine(olovka, (panel1.ClientRectangle.Width - minFirst) / 2f, panel1.Height / 2.0f,(panel1.Width-minFirst)/2.0f+(minFirst-minSecound)/2.0f, panel1.Height / 2.0f);
            g.DrawLine(olovka, (panel1.ClientRectangle.Width - minFirst) / 2f + minFirst, panel1.Height / 2.0f, (panel1.ClientRectangle.Width - minFirst) / 2f+(minFirst-minSecound)/2.0f+minSecound, panel1.Height / 2.0f);
            g.DrawLine(olovka, panel1.Width / 2.0f, (panel1.Height - minFirst) / 2.0f + minFirst, panel1.Width / 2.0f, (panel1.ClientRectangle.Height - minFirst) / 2f + (minFirst - minSecound) / 2.0f + minSecound);

            tacke = new PointF[] { new PointF((panel1.Width - minFirst) / 2, (panel1.Height - minFirst) / 2), new PointF(panel1.Width / 2, (panel1.Height - minFirst) / 2), new PointF((panel1.Width - minFirst) / 2 + minFirst, (panel1.Height - minFirst) / 2), new PointF((panel1.Width - minSecound) / 2, (panel1.Height - minSecound) / 2), new PointF(panel1.Width / 2, (panel1.Height - minSecound) / 2), new PointF(minSecound + (panel1.Width - minSecound) / 2, (panel1.Height - minSecound) / 2), new PointF((panel1.Width - minFirst) / 2, panel1.Height / 2), new PointF((panel1.Width - minSecound) / 2, panel1.Height / 2), new PointF((panel1.Width - minSecound) / 2 + minSecound, panel1.Height / 2), new PointF((panel1.Width - minFirst) / 2 + minFirst, panel1.Height / 2), new PointF((panel1.Width - minSecound) / 2, (panel1.Height - minSecound) / 2 + minSecound), new PointF(panel1.Width / 2, (panel1.Height - minSecound) / 2 + minSecound), new PointF((panel1.Width - minSecound) / 2 + minSecound, (panel1.Height - minSecound) / 2 + minSecound), new PointF((panel1.Width - minFirst) / 2, (panel1.Height - minFirst) / 2 + minFirst), new PointF(panel1.Width / 2, (panel1.Height - minFirst) / 2 + minFirst), new PointF((panel1.Width - minFirst) / 2 + minFirst, (panel1.Height - minFirst) / 2 + minFirst) };

            
            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    mice[i, j] = new RectangleF(tacke[k].X - 25, tacke[k].Y - 25, 50, 50);
                    k++;
                    if (matrica[i, j] == 1)
                    {
                        g.DrawImage(slika_crni, mice[i, j]);
                    }
                    else if (matrica[i, j] == -1)
                    {
                        g.DrawImage(slika_bijeli, mice[i, j]);
                    }


                }
            }


            

            panel1.Invalidate();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });
        }
        public float minValue()
        {
            return Math.Min(panel1.ClientRectangle.Height, panel1.ClientRectangle.Width);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int brojac = 0;

            Pen olovka = new Pen(Color.White, 2f);
            Graphics g = panel1.CreateGraphics();

            tacke = new PointF[] { new PointF((panel1.Width - minFirst) / 2, (panel1.Height - minFirst) / 2), new PointF(panel1.Width / 2, (panel1.Height - minFirst) / 2), new PointF((panel1.Width - minFirst) / 2 + minFirst, (panel1.Height - minFirst) / 2), new PointF((panel1.Width - minSecound) / 2, (panel1.Height - minSecound) / 2), new PointF(panel1.Width / 2, (panel1.Height - minSecound) / 2), new PointF(minSecound + (panel1.Width - minSecound) / 2, (panel1.Height - minSecound) / 2), new PointF((panel1.Width - minFirst) / 2, panel1.Height / 2), new PointF((panel1.Width - minSecound) / 2, panel1.Height / 2), new PointF((panel1.Width - minSecound) / 2 + minSecound, panel1.Height / 2), new PointF((panel1.Width - minFirst) / 2 + minFirst, panel1.Height / 2), new PointF((panel1.Width - minSecound) / 2, (panel1.Height - minSecound) / 2 + minSecound), new PointF(panel1.Width / 2, (panel1.Height - minSecound) / 2 + minSecound), new PointF((panel1.Width - minSecound) / 2 + minSecound, (panel1.Height - minSecound) / 2 + minSecound), new PointF((panel1.Width - minFirst) / 2, (panel1.Height - minFirst) / 2 + minFirst), new PointF(panel1.Width / 2, (panel1.Height - minFirst) / 2 + minFirst), new PointF((panel1.Width - minFirst) / 2 + minFirst, (panel1.Height - minFirst) / 2 + minFirst) };
           
            
           
            
            
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (mice[i, j].Contains(e.Location))
                        {
                            if (trenutni_klik[0] == 'c' && trenutni_igrac == "crni" && matrica[i, j] == 0)
                            {
                                matrica[i, j] = 1;
                                trenutni_igrac = "bijeli";
                                foreach (Control item in panel2.Controls)
                                {
                                    if (item is PictureBox)
                                    {

                                        if (item.AccessibleName == trenutni_klik)
                                        {
                                            brojac++;
                                            item.Dispose();
                                        }

                                    }
                                }
                                t++;
                            }
                            else if (trenutni_klik[0] == 'b' && trenutni_igrac == "bijeli" && matrica[i, j] == 0)
                            {
                                matrica[i, j] = -1;
                                trenutni_igrac = "crni";
                                foreach (Control item in panel2.Controls)
                                {
                                    if (item is PictureBox)
                                    {

                                        if (item.AccessibleName == trenutni_klik)
                                        {
                                            brojac++;
                                            item.Dispose();
                                        }

                                    }
                                }
                                t++;
                            }


                        


                    }
                }
            }
           

            if (brojac <= 1)
            {
                provjeri_pobjednika();
                

            }
            
            if (t > 11)
            {
                
                dio_igre = "pomjeranje";
            }

            if (dio_igre == "pomjeranje")
            {
                pomjeranje(e.Location);
                provjeri_pobjednika();

            }

            

           
            


        }


        private void klik_na_micu(object sender, EventArgs e)
        {

            PictureBox slika = (PictureBox)sender;

            trenutni_klik = slika.AccessibleName;

        }

        void provjeri_pobjednika()
        {
             
            label1.Location = new Point(10, 5);
           
            if (matrica[0, 0] == matrica[0, 1] && matrica[0, 0] == matrica[0, 2] )
            {
                if (matrica[0, 0] == -1)
                 label1.Text = "Pobjednik je " + forma.textBox1.Text;              

                else if (matrica[0, 0] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }
            if (matrica[0, 3] == matrica[1, 0] && matrica[0, 3] == matrica[1, 1])
            {
                if (matrica[0, 3] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[0, 3] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }
            if (matrica[2, 2] == matrica[2, 3] && matrica[2, 2] == matrica[3, 0])
            {
                if (matrica[2, 2] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[2, 2] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }


            if (matrica[3, 1] == matrica[3, 2] && matrica[3, 1] == matrica[3, 3])
            {
                if (matrica[3, 1] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[3, 1] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }
            if (matrica[0, 0] == matrica[1, 2] && matrica[0, 0] == matrica[3, 1])
            {
                if (matrica[0, 0] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[0, 0] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }
            if (matrica[0, 3] == matrica[1, 3] && matrica[0, 3] == matrica[2, 2])
            {
                if (matrica[0, 3] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[0, 3] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }

            if (matrica[1, 1] == matrica[2, 0] && matrica[1, 1] == matrica[3, 0])
            {
                if (matrica[1, 1] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[1, 1] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }

            if (matrica[0, 2] == matrica[2, 1] && matrica[0, 2] == matrica[3, 3])
            {
                if (matrica[0, 2] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[0, 2] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }
           
           
            

        }

        void pomjeranje(Point lokacija_klika)
        {

            if (t == 12)
            {
                trenutni_igrac = "bijeli";
            }
            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {
                    if (mice[i, j].Contains(lokacija_klika))
                    {
                        if (trenutni_igrac == "bijeli")
                        {
                            if (uzimanje_postavljanje == "uzimanje" && matrica[i, j] == -1)
                            {
                                matrica[i, j] = 0;
                                uzimanje_postavljanje = "postavljanje";
                            }
                            else if (uzimanje_postavljanje == "postavljanje" && matrica[i, j] == 0)
                            {
                                matrica[i, j] = -1;
                                uzimanje_postavljanje = "uzimanje";
                                trenutni_igrac = "crni";
                            }
                        }
                        if (trenutni_igrac == "crni")
                        {
                            if (uzimanje_postavljanje == "uzimanje" && matrica[i, j] == 1)
                            {
                                matrica[i, j] = 0;
                                uzimanje_postavljanje = "postavljanje";
                            }
                            else if (uzimanje_postavljanje == "postavljanje" && matrica[i, j] == 0)
                            {
                                matrica[i, j] = 1;
                                uzimanje_postavljanje = "uzimanje";
                                trenutni_igrac = "bijeli";
                            }
                        }
                    }
                }
            }
            panel1.Invalidate();
            t++;



            trenutni_klik = "w";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = Form1.ActiveForm;

            using (var bmp = new Bitmap(frm.Width, frm.Height))
            {
                frm.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                bmp.Save("..//..//Pictures//scrinsot" + Properties.Settings.Default.broj_skrinsota++ + ".jpeg", ImageFormat.Jpeg);
                Properties.Settings.Default.Save();
            }
        }

        //Metoda kojoj bi se figura uklanjala protivniku
        //void ukloni_figuru(Point lokacija_klika)
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        for (int j = 0; j < 4; j++)
        //        {
        //            if (mice[i, j].Contains(lokacija_klika))
        //            {
                        
        //                matrica[i, j] = 0;
                        
        //            }
        //        }
        //    }

            

        

    }
}
