using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Symulator_lotow
{
    
    
    public partial class MainForm : Form
    {
        private Ekran ekran = new Ekran();
        public MainForm()
        {
            InitializeComponent();
        }
        private float skala_piksele_metry = 1F; //nie wiem czy to bedzie potrzebne
        public PictureBox pictureBox1 = new PictureBox();
        private void Form1_Load(object sender, EventArgs e)// funkcja musi byc polaczona ze zdarzeniem load
        {
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BackColor = Color.White;
            
            this.Controls.Add(pictureBox1);
        }

        public int GetScreenHeight()
        {
            return pictureBox1.ClientSize.Height;
        }

        public int GetScreenWidth()
        {
            return pictureBox1.ClientSize.Width;
        }

        public void Redraw()
        {
            this.pictureBox1.Invalidate();
        }


        /*private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Debug.WriteLine(pictureBox1.ClientSize.Width);
            //Debug.WriteLine(pictureBox1.ClientSize.Height);
            Graphics g = e.Graphics;
            Point[] points =
{
                 new Point(10,  10),
                 new Point(10, 100),
                 new Point(200,  50),
                 new Point(250, 300)
             };
            RysujLamana(points, Color.Black, g);
            RysujNapis("Trasa samolotu", Brushes.Black, new Point(10, 10), g);

            RysujDrzewo(new Point(100, 300), 8, g);
            RysujNapis("Drzewo", Brushes.Red, new Point(100, 300), g);
            RysujBlok(new Point(200, 400), new Point(40, 120), g);
            RysujNapis("Blok", Brushes.Red, new Point(200, 400), g);
            RysujWiezowiec(new Point(400, 200), 80, g);
            RysujNapis("Wiezowiec", Brushes.Red, new Point(400, 200), g);
            RysujKomin(new Point(500, 100), 14, g);
            RysujNapis("Komin", Brushes.Blue, new Point(500, 100), g);

            RysujSamolot(new Point(300, 100), g);
            RysujNapis("Samolot", Brushes.Red, new Point(300, 100), g);
            RysujDron(new Point(700, 100), g);
            RysujNapis("Dron", Brushes.Red, new Point(700, 100), g);
            RysujSmiglowiec(new Point(700, 200), g);
            RysujNapis("Śmigłowiec", Brushes.Red, new Point(700, 200), g);
            RysujBalon(new Point(700, 300), g);
            RysujNapis("Balon", Brushes.Red, new Point(700, 300), g);
            RysujSzybowiec(new Point(800, 300), g);
            RysujNapis("Szybowiec", Brushes.Red, new Point(800, 300), g);

            RysujNiebezpieczenstwo(new Point(500, 500), g);
        }*/
        /*private void RysujWielokat(Point[] wierzcholki, Color kolor, Graphics g)
        {
            SolidBrush brush = new SolidBrush(kolor);
            g.FillPolygon(brush, wierzcholki);
        }
        private void RysujTrojkat(Point p1, Point p2, Point p3, Color kolor, Graphics g)
        {
            Point[] wierzcholki = { p1, p2, p3 };
            RysujWielokat(wierzcholki, kolor, g);
        }
        private void RysujLamana(Point[] points, Color kolor, Graphics g)
        {
            Pen pen = new Pen(kolor, 3);//3 to grubosc linii
            g.DrawLines(pen, points);
        }

        public void RysujObiektLatajacy(Point pos_srodka, Color kolor,int rozmiar, Graphics g)
        {
            Point p1 = new Point(pos_srodka.X, pos_srodka.Y - rozmiar);
            Point p2 = new Point(pos_srodka.X - rozmiar, pos_srodka.Y + rozmiar);
            Point p3 = new Point(pos_srodka.X + rozmiar, pos_srodka.Y + rozmiar);
            RysujTrojkat(p1, p2, p3, kolor, g);
        }
        public void RysujDron(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Black,4, g);//rozmiar 4
        }
        public void RysujSamolot(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Yellow,12, g); //rozmiar 12
        }

        public void RysujSmiglowiec(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Orange,10, g); //rozmiar 10
        }

        public void RysujBalon(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Violet,6, g);//rozmiar 6
        }

        public void RysujSzybowiec(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Red,8, g); //rozmiar 8
        }

        public void RysujNiebezpieczenstwo(Point pos,Graphics g)
        {
            RysujNapis("BUM!", Brushes.Red, pos, g);
        }*/

        /*public void RysujObiekt(Object o, Graphics g)
        {
            int x, y, r, a, b;
            if (o is ObiektyStale || o is StatkiPowietrzne)
            {
                x = Convert.ToInt32(o.getX() * skala_piksele_metry);
                y = Convert.ToInt32(o.getY() * skala_piksele_metry);
            }
            if (o is Drzewo)
            {
                r = Convert.ToInt32(o.getR() * skala_piksele_metry);
                RysujDrzewo(new Point(x, y), r, g);
            }
            if(o is Komin)
            {
                r = Convert.ToInt32(o.getR() * skala_piksele_metry);
                RysujKomin(new Point(x, y), r, g);
            }
            if(o is Blok)
            {
                pol_a = Convert.ToInt32(o.getA() * skala_piksele_metry/2.0);
                pol_b = Convert.ToInt32(o.getB() * skala_piksele_metry/2.0);
                RysujBlok(new Point(x - pol_a, y - pol_b), new Point(pol_a * 2, pol_b * 2), g);
            }
            if (o is Wiezowiec)
            {
                pol_a = Convert.ToInt32(o.getA() * skala_piksele_metry / 2.0);
                RysujWiezowiec(new Point(x - pol_a, y - pol_a), pol_a * 2, g);
            }
        }*/
        /*public void WyswietlMape(List<ObiektyStale> obiekty_stale, List<StatkiPowietrzne> statki_powietrzne, Graphics g)
        {
            foreach (ObiektyStale os in obiekty_stale)
            {
                RysujObiekt(os, g);
            }
            foreach(StatkiPowietrzne sp in statki_powietrzne)
            {
                RysujObiekt(sp, g);
            }
        }*/


    }

}
