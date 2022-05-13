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
        public MainForm()
        {
            InitializeComponent();
        }

        // This example creates a PictureBox control on the form and draws to it.
        // This example assumes that the Form_Load event handler method is
        // connected to the Load event of the form.

        private PictureBox pictureBox1 = new PictureBox();
        // Cache font instead of recreating font objects each time we paint.
        private Font fnt = new Font("Arial", 10);
        private void Form1_Load(object sender, System.EventArgs e)
        {
            // Dock the PictureBox to the form and set its background to white.
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BackColor = Color.White;
            // Connect the Paint event of the PictureBox to the event handler method.
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);

            // Add the PictureBox control to the Form.
            this.Controls.Add(pictureBox1);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int maxX = pictureBox1.ClientSize.Width;
            int maxY = pictureBox1.ClientSize.Height;
            Debug.WriteLine(maxX);
            Debug.WriteLine(maxY);
            Graphics g = e.Graphics;
            /*
            RysujProstokat(new Point(100, 100), new Point(30, 10), Color.Blue, g);
            RysujKwadrat(new Point(200, 200), 20, Color.Red, g);
            RysujKolo(new Point(300, 300), 20, Color.Yellow, g);
            RysujTrojkat(new Point(400, 400), new Point(400, 420), new Point(420, 400), Color.Green, g);
            
            */
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
            
        }

        private void RysujProstokat(Point pos,Point size, Color kolor, Graphics g)
        {
            SolidBrush brush = new SolidBrush(kolor);
            Rectangle rect = new Rectangle(pos.X, pos.Y, size.X, size.Y);
            g.FillRectangle(brush, rect);
        }

        private void RysujKwadrat(Point pos,int bok, Color kolor, Graphics g)
        {
            RysujProstokat(new Point(pos.X,pos.Y), new Point(bok,bok), kolor, g);
        }

        private void RysujKolo(Point pos,int r,Color kolor, Graphics g)
        {
            SolidBrush brush = new SolidBrush(kolor);
            g.FillEllipse(brush, pos.X, pos.Y, 2*r, 2*r);
            //Pen blackPen = new Pen(Color.Black, 3);
            //Rectangle rect = new Rectangle(pos.X, pos.Y, r, r);
            //g.DrawRectangle(blackPen, rect);
        }

        private void RysujWielokat(Point[] wierzcholki, Color kolor, Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.Blue);
            g.FillPolygon(brush, wierzcholki);
        }

        private void RysujTrojkat(Point p1,Point p2,Point p3,Color kolor, Graphics g)
        {
            Point[] wierzcholki = { p1, p2, p3 };
            RysujWielokat(wierzcholki, kolor, g);
        }
        private void RysujLamana(Point[] points,Color kolor, Graphics g)
        {
            Pen pen = new Pen(kolor, 3);//3 to grubosc linii
            g.DrawLines(pen, points);
        }

        private void RysujNapis(string napis,Brush kolor,Point pos, Graphics g)
        {
            g.DrawString(napis, fnt, kolor, pos);
        }

        public void RysujDrzewo(Point pos, int r, Graphics g) // promien <4,12>
        {
            RysujKolo(pos, r, Color.Green, g);
        }

        public void RysujKomin(Point pos,int r,Graphics g) //promien <8,20>
        {
            RysujKolo(pos, r, Color.Brown, g);
        }

        public void RysujBlok(Point pos,Point size,Graphics g)// boki <40,120>
        {
            RysujProstokat(pos, size, Color.Gray, g);
        }

        public void RysujWiezowiec(Point pos,int bok, Graphics g) //bok <40,120>
        {
            RysujKwadrat(pos, bok, Color.Blue, g);
        }

        //Dron czarny
        //Samolot zolty
        //Smiglowiec pomaranczowy
        //Balon fioletowy
        //Szybowiec czerwony

    }

}
