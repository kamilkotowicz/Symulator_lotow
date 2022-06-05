using System;
namespace Symulator_lotow
{
    public class Ekran
    {
        public Ekran()
        {
        }
        private Font fnt = new Font("Arial", 10);
        private void RysujProstokat(Point pos, Point size, Color kolor, Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(kolor))
            {
                Rectangle rect = new Rectangle(pos.X, pos.Y, size.X, size.Y);
                g.FillRectangle(brush, rect);
            }
        }

        private void RysujKwadrat(Point pos, int bok, Color kolor, Graphics g)
        {
            RysujProstokat(new Point(pos.X, pos.Y), new Point(bok, bok), kolor, g);
        }

        private void RysujKolo(Point pos, int r, Color kolor, Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(kolor))
            {
                g.FillEllipse(brush, pos.X, pos.Y, 2 * r, 2 * r);
            }
        }

        public void RysujNapis(string napis, Brush kolor, Point pos, Graphics g)
        {
            g.DrawString(napis, fnt, kolor, pos);
        }

        public void RysujDrzewo(Point pos, int r, Graphics g) // promien <4,12>
        {
            RysujKolo(pos, r, Color.Green, g);
        }

        public void RysujKomin(Point pos, int r, Graphics g) //promien <8,20>
        {
            RysujKolo(pos, r, Color.Brown, g);
        }

        public void RysujBlok(Point pos, Point size, Graphics g)// boki <40,120>
        {
            RysujProstokat(pos, size, Color.Gray, g);
        }

        public void RysujWiezowiec(Point pos, int bok, Graphics g) //bok <40,120>
        {
            RysujKwadrat(pos, bok, Color.Blue, g);
        }


        private void RysujWielokat(Point[] wierzcholki, Color kolor, Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(kolor))
            {
                g.FillPolygon(brush, wierzcholki);
            }         
        }
        private void RysujTrojkat(Point p1, Point p2, Point p3, Color kolor, Graphics g)
        {
            Point[] wierzcholki = { p1, p2, p3 };
            RysujWielokat(wierzcholki, kolor, g);
        }
        public void RysujLamana(Point[] points, Color kolor, Graphics g)
        {
            using (Pen pen = new Pen(kolor, 1))//1 to grubosc linii
            {
                g.DrawLines(pen, points);
            }       
        }

        public void RysujObiektLatajacy(Point pos_srodka, Color kolor, int rozmiar, Graphics g)
        {
            Point p1 = new Point(pos_srodka.X, pos_srodka.Y - rozmiar);
            Point p2 = new Point(pos_srodka.X - rozmiar, pos_srodka.Y + rozmiar);
            Point p3 = new Point(pos_srodka.X + rozmiar, pos_srodka.Y + rozmiar);
            RysujTrojkat(p1, p2, p3, kolor, g);
        }
        public void RysujDron(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Black, 4, g);
        }
        public void RysujSamolot(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Yellow, 12, g);
        }

        public void RysujSmiglowiec(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Orange, 10, g);
        }

        public void RysujBalon(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Violet, 6, g);
        }

        public void RysujSzybowiec(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Red, 8, g);
        }
    }
}
