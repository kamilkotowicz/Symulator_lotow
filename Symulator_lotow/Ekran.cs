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
            SolidBrush brush = new SolidBrush(kolor);
            Rectangle rect = new Rectangle(pos.X, pos.Y, size.X, size.Y);
            g.FillRectangle(brush, rect);
        }

        private void RysujKwadrat(Point pos, int bok, Color kolor, Graphics g)
        {
            RysujProstokat(new Point(pos.X, pos.Y), new Point(bok, bok), kolor, g);
        }

        private void RysujKolo(Point pos, int r, Color kolor, Graphics g)
        {
            SolidBrush brush = new SolidBrush(kolor);
            g.FillEllipse(brush, pos.X, pos.Y, 2 * r, 2 * r);
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
            SolidBrush brush = new SolidBrush(kolor);
            g.FillPolygon(brush, wierzcholki);
        }
        private void RysujTrojkat(Point p1, Point p2, Point p3, Color kolor, Graphics g)
        {
            Point[] wierzcholki = { p1, p2, p3 };
            RysujWielokat(wierzcholki, kolor, g);
        }
        public void RysujLamana(Point[] points, Color kolor, Graphics g)
        {
            Pen pen = new Pen(kolor, 1);//1 to grubosc linii
            g.DrawLines(pen, points);
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
            RysujObiektLatajacy(pos_srodka, Color.Black, 4, g);//rozmiar 4
        }
        public void RysujSamolot(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Yellow, 12, g); //rozmiar 12
        }

        public void RysujSmiglowiec(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Orange, 10, g); //rozmiar 10
        }

        public void RysujBalon(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Violet, 6, g);//rozmiar 6
        }

        public void RysujSzybowiec(Point pos_srodka, Graphics g)
        {
            RysujObiektLatajacy(pos_srodka, Color.Red, 8, g); //rozmiar 8
        }

        public void RysujNiebezpieczenstwo(Point pos, Graphics g)
        {
            RysujNapis("BUM!", Brushes.Red, pos, g);
        }
        public void RysujObiekt(Object o, Graphics g)
        {
            int x=0, y=0, r;
            if(o is ObiektyStale os)
            {
                x = Convert.ToInt32(os.pozycja_srodka.x);
                y = Convert.ToInt32(os.pozycja_srodka.y);
            }
            if (o is ObiektyRuchome or)
            {
                x = Convert.ToInt32(or.aktualna_pozycja.x);
                y = Convert.ToInt32(or.aktualna_pozycja.y);
            }
            if (o is Drzewo d)
            {
                r = Convert.ToInt32(d.promien);
                RysujDrzewo(new Point(x, y), r, g);
            }
            if(o is Komin k)
            {
                r = Convert.ToInt32(k.promien);
                RysujKomin(new Point(x, y), r, g);
            }
            if(o is Blok bl)
            {
                int pol_a = Convert.ToInt32(bl.dlugosc/ 2.0);
                int pol_b = Convert.ToInt32(bl.szerokosc/2.0);
                RysujBlok(new Point(x - pol_a, y - pol_b), new Point(pol_a * 2, pol_b * 2), g);
            }
            if (o is Wiezowiec w)
            {
                int pol_a = Convert.ToInt32(w.bok/ 2.0);
                RysujWiezowiec(new Point(x - pol_a, y - pol_a), pol_a * 2, g);
            }
        }
        private void WyswietlMape(List<ObiektyStale> obiekty_stale, List<ObiektyRuchome> statki_powietrzne, Graphics g)
        {
            foreach (ObiektyStale os in obiekty_stale)
            {
                RysujObiekt(os, g);
            }
            foreach (ObiektyRuchome sp in statki_powietrzne)
            {
                RysujObiekt(sp, g);
            }
        }
    }
}
