using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Imaging;


namespace IgricaMica
{
    public partial class Form2 : Form
    {
        int t = 0;
        //Niz Pointa u kojem se nalaze koordinate svih tacaka
        PointF[] tacke;
        //duzina i sirina ploce za igranje
        public float minFirst;
        //matrica figura sastavljena od 0 1 i -1.Bijele figure su na mjestima 1,crne na -1, a ostala polja su prazna
        int[,] matrica = new int[3, 3];
        string trenutni_klik;
        string trenutni_igrac = "bijeli";
        string uzimanje_postavljanje = "uzimanje";
        string dio_igre = "rasporedjivanje";
        Form1 forma;

        //matrica kvadrata koji nam sluze za postavljanje figura pomocu metode Contains
        RectangleF[,] mice = new RectangleF[3, 3];
        //slike dodate u resource projekta
        Image slika_crni = Properties.Resources.wood_button_png_3;
        Image slika_bijeli = Properties.Resources.wood_button_png_32;
        public Form2(Form1 fm1)
        {
            InitializeComponent();
            this.forma = fm1;
            button1.BackgroundImage = Properties.Resources.download.GetThumbnailImage(button1.Width, button1.Height, null, new IntPtr());

        }
        int brojac = 0;


        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.Icon = Properties.Resources.nine_men_s_morris_morabaraba_tic_tac_toe_three_men_s_morris_game_png_favpng_rFSzs9kCVAyq7GE6vicyQvgnv;

            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.DarkOliveGreen, 0, 0, panel1.Width, panel1.Height);
            //Sirina i visina ploce za igru
            minFirst = (int)(0.9f * minValue());
            //Bojenje pozadine
            g.FillRectangle(Brushes.BurlyWood, (panel1.ClientRectangle.Width - minFirst) / 2f, (panel1.ClientRectangle.Height - minFirst) / 2f, minFirst, minFirst);
            //Dodavanje olovke kojom se crta
            Pen olovka = new Pen(Color.Black, 5f);

            g.DrawRectangle(olovka, (panel1.ClientRectangle.Width - minFirst) / 2f, (panel1.ClientRectangle.Height - minFirst) / 2f, minFirst, minFirst);

            g.DrawLine(olovka, (int)((panel1.ClientRectangle.Width - minFirst) / 2f), (int)((panel1.ClientRectangle.Height - minFirst) / 2f), (int)(minFirst + (panel1.ClientRectangle.Width - minFirst) / 2f), (minFirst + (panel1.ClientRectangle.Height - minFirst) / 2f));
            g.DrawLine(olovka, minFirst + (panel1.ClientRectangle.Width - minFirst) / 2f, (panel1.ClientRectangle.Height - minFirst) / 2f, (panel1.ClientRectangle.Width - minFirst) / 2f, minFirst + (panel1.ClientRectangle.Height - minFirst) / 2f);
            g.DrawLine(olovka, panel1.Width / 2.0f, (panel1.ClientRectangle.Height - minFirst) / 2f, panel1.Width / 2.0f, minFirst + (panel1.ClientRectangle.Height - minFirst) / 2f);
            g.DrawLine(olovka, panel1.Width / 2.0f, (panel1.ClientRectangle.Height - minFirst) / 2f, panel1.Width / 2.0f, minFirst + (panel1.ClientRectangle.Height - minFirst) / 2f);
            g.DrawLine(olovka, (panel1.ClientRectangle.Width - minFirst) / 2f, panel1.Height / 2.0f, minFirst + (panel1.ClientRectangle.Width - minFirst) / 2f, panel1.Height / 2.0f);

            //Niu tacaka sa koordinatama
            tacke = new PointF[] { new PointF((int)((panel1.Width - minFirst) / 2f), (int)((panel1.ClientRectangle.Height - minFirst) / 2f)), new PointF(panel1.Width / 2.0f, (panel1.ClientRectangle.Height - minFirst) / 2f), new PointF(minFirst + (panel1.ClientRectangle.Width - minFirst) / 2f, (panel1.ClientRectangle.Height - minFirst) / 2f), new PointF((panel1.ClientRectangle.Width - minFirst) / 2f, panel1.Height / 2.0f), new PointF(panel1.Width / 2, panel1.Height / 2), new PointF(minFirst + (panel1.ClientRectangle.Width - minFirst) / 2f, panel1.Height / 2.0f), new PointF((panel1.ClientRectangle.Width - minFirst) / 2f, minFirst + (panel1.ClientRectangle.Height - minFirst) / 2f), new PointF(panel1.Width / 2.0f, minFirst + (panel1.ClientRectangle.Height - minFirst) / 2f), new PointF((panel1.ClientRectangle.Width - minFirst) / 2f + minFirst, minFirst + (panel1.ClientRectangle.Height - minFirst) / 2f) };
            //Ovdje se prolazi kroz matricu i na mjestima -1 crtaju bijele, a na mjestima 1 crne figure
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
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

        //Funkcija u kojoj trazimo minimalni od visine i sirine
        public float minValue()
        {
            return Math.Min(panel1.ClientRectangle.Height, panel1.ClientRectangle.Width);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int brojac = 0;
            
            Pen olovka = new Pen(Color.White, 2f);
            Graphics g = panel1.CreateGraphics();
          
            
            tacke = new PointF[] { new PointF((int)((panel1.Width - minFirst) / 2f), (int)((panel1.ClientRectangle.Height - minFirst) / 2f)), new PointF(panel1.Width / 2.0f, (panel1.ClientRectangle.Height - minFirst) / 2f), new PointF(minFirst + (panel1.ClientRectangle.Width - minFirst) / 2f, (panel1.ClientRectangle.Height - minFirst) / 2f), new PointF((panel1.ClientRectangle.Width - minFirst) / 2f, panel1.Height / 2.0f), new PointF(panel1.Width / 2, panel1.Height / 2), new PointF(minFirst + (panel1.ClientRectangle.Width - minFirst) / 2f, panel1.Height / 2.0f), new PointF((panel1.ClientRectangle.Width - minFirst) / 2f, minFirst + (panel1.ClientRectangle.Height - minFirst) / 2f), new PointF(panel1.Width / 2.0f, minFirst + (panel1.ClientRectangle.Height - minFirst) / 2f), new PointF((panel1.ClientRectangle.Width - minFirst) / 2f + minFirst, minFirst + (panel1.ClientRectangle.Height - minFirst) / 2f) };
            
            //Prolazak kroz matricu kvadrata i provjera da li je kliknuto u neki kvadrat
            //Ako jeste dodajemo sve to u matricu sa 0,1 i -1.Zavisi od igraca koji je na redu da igra
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (mice[i, j].Contains(e.Location))
                    {
                        if (trenutni_klik[0] == 'c' && trenutni_igrac == "crni" && matrica[i,j]==0)
                        {
                            matrica[i, j] = 1;
                            trenutni_igrac = "bijeli";
                            //Pomocu foeach petlje prolazi se kroz sve picturebox-ove na panelu 
                            //Potrebno je jer zelimo da ih uklonimo sa panela cim odu na plocu za igranje

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
                //Funkcija koja provjerava da li je iko od igraca postavio tri figure u red
                provjeri_pobjednika();

            }
            if (t>5)
            {
                
                dio_igre = "pomjeranje";
            }

            if (dio_igre == "pomjeranje")
            {
                //funkcija koja sluzi za pomjeranje figura.Proslijedjujemo joj lokaciju na koju je kliknuto misem
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
            //Postavljanje lokacije labele
            label1.Location = new Point(10, 5);
            
            if (matrica[0, 0] == matrica[0, 1] && matrica[0, 0] == matrica[0, 2])
            {
                if (matrica[0, 0] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                    
                
                else if (matrica[0, 0] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;                
            }
             if (matrica[1, 0] == matrica[1, 1] && matrica[1 , 1] == matrica[1,2])
            {
                if (matrica[1, 0] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[1, 0] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }
             if (matrica[2, 0] == matrica[2, 1] && matrica[2, 0] == matrica[2, 2])
            {
                if (matrica[2, 0] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[2, 0] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }


               if (matrica[0, 0] == matrica[1,0] && matrica[0, 0] == matrica[2,0])
            {
                if (matrica[0, 0] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[0, 0] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }
             if (matrica[0, 1] == matrica[1, 1] && matrica[0, 1] == matrica[2, 1])
            {
                if (matrica[0, 1] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[0, 1] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }
             if (matrica[0, 2] == matrica[1, 2] && matrica[0, 2] == matrica[2, 2])
            {
                if (matrica[0, 2] == -1)
                    label1.Text = "Pobjednik je " + forma.textBox1.Text;
                else if (matrica[0, 2] == 1)
                    label1.Text = "Pobjednik je " + forma.textBox2.Text;
            }

             if (matrica[0, 0] == matrica[1, 1] && matrica[0, 0] == matrica[2, 2])
             {
                 if (matrica[0, 0] == -1)
                     label1.Text = "Pobjednik je " + forma.textBox1.Text;
                 else if (matrica[0, 0] == 1)
                     label1.Text = "Pobjednik je " + forma.textBox2.Text;
             }

             if (matrica[0, 2] == matrica[1, 1] && matrica[0, 2] == matrica[2, 0])
             {
                 if (matrica[0, 2] == -1)
                     label1.Text = "Pobjednik je " + forma.textBox1.Text;
                 else if (matrica[0, 2] == 1)
                     label1.Text = "Pobjednik je " + forma.textBox2.Text;
             }

            

        }

        void pomjeranje(Point lokacija_klika)
        {
            
            if(t==6)
            {
                trenutni_igrac = "bijeli";
            }
            for (int i = 0; i < 3; i++)
            {
            
                for (int j = 0; j < 3; j++)
                {
                    if (mice[i, j].Contains(lokacija_klika))
                    {
                        if(trenutni_igrac == "bijeli" )
                        {
                            if (uzimanje_postavljanje == "uzimanje" && matrica[i, j] == -1)
                            {
                                matrica[i, j] = 0;
                                uzimanje_postavljanje = "postavljanje";
                            }
                            else  if(uzimanje_postavljanje == "postavljanje" && matrica[i,j]==0)
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
            


                 trenutni_klik="w";
           
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
    }
}
