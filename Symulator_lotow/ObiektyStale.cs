using System;

namespace Symulator_lotow
{

    class ObiektyStale
    {
        protected int id;
        public double wysokosc;
        public Punkt pozycja_srodka;
        public string nazwa;
        public ObiektyStale(Punkt srodek, double wysokosc, string nazwa)
        {
            this.wysokosc = wysokosc;
            this.pozycja_srodka = srodek;
            this.nazwa = nazwa;
        }
        public virtual bool CzyZawieraPunkt(Punkt p) {
            return true;
        }
    }

    class Drzewo : ObiektyStale
    {
        public int promien = 0;
        public Drzewo(Punkt srodek, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {

        }
        public override bool CzyZawieraPunkt(Punkt p)
        {
            if (p.Odleglosc(pozycja_srodka) <= promien) return true;
            return false;
        }
        
    }
    class Komin : ObiektyStale
    {
        public int promien = 0;
        public Komin(Punkt srodek, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {

        }
        public override bool CzyZawieraPunkt(Punkt p)
        {
            if (p.Odleglosc(pozycja_srodka) <= promien) return true;
            return false;
        }
        
    }

    class Blok : ObiektyStale
    {
        public int dlugosc = 0;
        public int szerokosc = 0;
        public Blok(Punkt srodek, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {

        }
        public override bool CzyZawieraPunkt(Punkt p)
        {
            double minx = pozycja_srodka.x - dlugosc;
            double maxx = pozycja_srodka.x + dlugosc;
            double miny = pozycja_srodka.y - szerokosc;
            double maxy = pozycja_srodka.y + szerokosc;
            if (minx <= p.x && miny <= p.y && p.x <= maxx && p.y <= maxy && p.z <= wysokosc) return true;   
            return false;
        }
    }

    class Wiezowiec : ObiektyStale
    {
        public int bok = 0;
        public Wiezowiec(Punkt srodek, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {

        }
        public override bool CzyZawieraPunkt(Punkt p)
        {
            double minx = pozycja_srodka.x - bok;
            double maxx = pozycja_srodka.x + bok;
            double miny = pozycja_srodka.y - bok;
            double maxy = pozycja_srodka.y + bok;
            if (minx <= p.x && miny <= p.y && p.x <= maxx && p.y <= maxy && p.z <= wysokosc) return true;
            return false;     
        }
        
    }



}
