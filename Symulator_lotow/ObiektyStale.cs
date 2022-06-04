using System;

namespace Symulator_lotow
{

    abstract class ObiektyStale : SymulowanyObiekt
    {
        public double wysokosc;
        public Punkt pozycja_srodka;
        public ObiektyStale(Punkt srodek, double wysokosc, string nazwa)
        {
            this.wysokosc = wysokosc;
            this.pozycja_srodka = srodek;
            this.nazwa = nazwa;
        }
        public override abstract bool CzyZawieraPunkt(Punkt p);
        public abstract double OdlegloscDoSamolotu(Punkt p);

        public static double NajblizszyPunkt(double x, double minx, double maxx)
        {
            if (x < minx) return minx;
            if (x > maxx) return maxx;
            return x;
        }
    }

    class Drzewo : ObiektyStale
    {
        public readonly double promien;
        public Drzewo(Punkt srodek,double promien, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {
            this.promien = promien;
        }
        public override bool CzyZawieraPunkt(Punkt p)
        {
            Punkt srodek = new Punkt(pozycja_srodka);
            srodek.z = p.z;
            if (p.Odleglosc(srodek) <= promien && p.z <= wysokosc) return true;
            return false;
        }

        public override double OdlegloscDoSamolotu(Punkt p)
        {
            Punkt srodek = new Punkt(pozycja_srodka);
            srodek.z = Math.Min(p.z, wysokosc);
            return p.Odleglosc(srodek) - promien;
        }
        
    }
    class Komin : ObiektyStale
    {
        public readonly double promien;
        public Komin(Punkt srodek, double promien, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {
            this.promien = promien;
        }
        public override bool CzyZawieraPunkt(Punkt p)
        {
            Punkt srodek = new Punkt(pozycja_srodka);
            srodek.z = p.z;
            if (p.Odleglosc(srodek) <= promien && p.z <= wysokosc) return true;
            return false;
        }
        public override double OdlegloscDoSamolotu(Punkt p)
        {
            Punkt srodek = new Punkt(pozycja_srodka);
            srodek.z = Math.Min(p.z, wysokosc);
            return p.Odleglosc(srodek) - promien;
        }

    }

    class Blok : ObiektyStale
    {
        public readonly double dlugosc;
        public readonly double szerokosc;
        public Blok(Punkt srodek, double dlugosc, double szerokosc, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {
            this.dlugosc = dlugosc;
            this.szerokosc = szerokosc;
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

        public override double OdlegloscDoSamolotu(Punkt p)
        {
            Punkt najblizszy = new Punkt(pozycja_srodka);
            najblizszy.x = NajblizszyPunkt(p.x, najblizszy.x - dlugosc, najblizszy.x + dlugosc);
            najblizszy.y = NajblizszyPunkt(p.y, najblizszy.y - szerokosc, najblizszy.y + szerokosc);
            najblizszy.z = Math.Min(p.z, wysokosc);
            return p.Odleglosc(najblizszy);
        }
    }

    class Wiezowiec : ObiektyStale
    {
        public readonly double bok;
        public Wiezowiec(Punkt srodek, double bok, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {
            this.bok = bok;
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
        public override double OdlegloscDoSamolotu(Punkt p)
        {
            Punkt najblizszy = new Punkt(pozycja_srodka);
            najblizszy.x = NajblizszyPunkt(p.x, najblizszy.x - bok, najblizszy.x + bok);
            najblizszy.y = NajblizszyPunkt(p.y, najblizszy.y - bok, najblizszy.y + bok);
            najblizszy.z = Math.Min(p.z, wysokosc);
            return p.Odleglosc(najblizszy);
        }
    }



}
