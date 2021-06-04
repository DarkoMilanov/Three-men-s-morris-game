using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IgricaMica
{
    public partial class Form1 : Form
    {

        bool slika1 = false;
        bool slika2 = false;
        bool slika3 = false;
        

      
        
        public Form1()
        {
            InitializeComponent();

            
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.Icon = Properties.Resources.nine_men_s_morris_morabaraba_tic_tac_toe_three_men_s_morris_game_png_favpng_rFSzs9kCVAyq7GE6vicyQvgnv;
            string igrac1 = textBox1.Text;
            string igrac2 = textBox2.Text;
            Image slika_1=Properties.Resources._1200px_Three_Men_s_Morris_variant_board_svg;
            Image slika_2 = Properties.Resources._1200px_Six_Men_s_Morris_svg;
            Image slika_3 = Properties.Resources._1200px_Twelve_Men_s_Morris_board_svg;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            Graphics g = e.Graphics;
            int duzina = ClientRectangle.Width; ;
            int visina = ClientRectangle.Height;

            if (slika1 == false && slika2==false && slika3==false)
            {
                g.FillRectangle(Brushes.DarkOliveGreen, 0, 0, ClientRectangle.Width, ClientRectangle.Height);
                String dobrodoslica = "Dobro došli";
                String igra = "Izaberite igru";
                Font font = new Font("Monotype Corsiva", 18, FontStyle.Bold);
                g.DrawString(dobrodoslica, font, Brushes.Yellow, (ClientRectangle.Width - g.MeasureString(dobrodoslica, font).Width) / 2, 0);
                g.DrawString(igra, font, Brushes.Yellow, (ClientRectangle.Width - g.MeasureString(igra, font).Width) / 2, 50);

            }
            else if(slika1==true)
            {
                panel1.BackgroundImage = slika_1.GetThumbnailImage(panel1.Width,panel1.Height-30,null,new IntPtr());
                this.BackColor = Color.DarkOliveGreen;
            }
            else if (slika2 == true)
            {
                panel1.BackgroundImage = slika_2.GetThumbnailImage(panel1.Width, panel1.Height-50, null, new IntPtr());
                this.BackColor = Color.DarkOliveGreen;
            }
            else if (slika3 == true)
            {
                panel1.BackgroundImage = slika_3.GetThumbnailImage(panel1.Width, panel1.Height-50, null, new IntPtr());
                this.BackColor = Color.DarkOliveGreen;
            }
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });

            Invalidate();
           
        }

        private void button1_MouseEnter_1(object sender, EventArgs e)
        {
            slika1 = true;
           
        }
        private void button1_MouseLeave_2(object sender, EventArgs e)
        {
            slika1 = false;

        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            slika2 = true;

        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            slika2 = false;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            slika3 = true;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            slika3 = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 forma2 = new Form2(this);
            forma2.Show();
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 forma3 = new Form3(this);
            forma3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 forma4 = new Form4(this);
            forma4.Show();
        }







    }
}
