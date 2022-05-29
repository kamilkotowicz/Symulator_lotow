using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator_lotow
{

    public class ObiektyRuchome : SymulowanyObiekt
    {
        public Punkt aktualna_pozycja;
        public Trasa trasa;
        public bool czy_skonczyl_lot = false;
        public readonly int rozmiar = 1;// rozmiar statku powietrznego bedzie potrzebny przy wykrywaniu kolizji
        //public string nazwa;
        public virtual int hmin { get; }
        public virtual int hmax { get; }
        public virtual int vmin { get; }
        public virtual int vmax { get; }
        public override bool CzyZawieraPunkt(Punkt p)
        {
            if (p.Odleglosc(aktualna_pozycja) <= rozmiar)
            {
                return true;
            }
            return false;
        }

        public ObiektyRuchome(string nazwa)
        {
            this.nazwa = nazwa;
        }
        private Punkt LosujPozycje(int maxx, int maxy)
        {
            Random rand = new Random();
            int x = rand.Next(0, maxx);
            int y = rand.Next(0, maxy);
            int z = rand.Next(hmin, hmax);
            return new Punkt(x, y, z);
        }
        public void UstawNaLosowaPozycje(int maxx, int maxy)
        {
            aktualna_pozycja = LosujPozycje(maxx, maxy);
        }

        public void zmien_trase_recznie(Trasa t, double predkosc, Punkt nowy_koniec_odcinka) //wykonujemy ta funkcje jesli "wykryto kolizje"
        {        
            t.odcinki[t.nr_aktualnego_odcinka] = new OdcinekTrasy(predkosc, nowy_koniec_odcinka);
        }

        public void UstawTraseLosowo(int maxx,int maxy)
        {
            Random rand = new Random();
            int LICZBA_ODCINKOW = rand.Next(2, 4);
            trasa = new Trasa();
            for(int i=0;i<LICZBA_ODCINKOW; i++)
            {
                int predkosc = rand.Next(vmin, vmax);
                Punkt koniec_odcinka = LosujPozycje(maxx, maxy);
                OdcinekTrasy odc = new OdcinekTrasy(predkosc, koniec_odcinka);
                trasa.odcinki.Add(odc);
            }
        }
        public Punkt skladowe_predkosci()
        {
            double licznik_x = (trasa.KoniecAktualnegoOdcinka().x - aktualna_pozycja.x) * trasa.PredkoscAktualnegoOdcinka();
            double licznik_y = (trasa.KoniecAktualnegoOdcinka().y - aktualna_pozycja.y) * trasa.PredkoscAktualnegoOdcinka();
            double mianownik = aktualna_pozycja.Odleglosc(trasa.KoniecAktualnegoOdcinka());
            const double EPSILON = 1e-6;
            if(mianownik < EPSILON) //dzielenie przez zero, wystepuje gdy samolot jest juz w punkcie docelowym
            {
                return new Punkt(0, 0, 0);
            }
            double vx = licznik_x / mianownik;
            double vy= licznik_y / mianownik;
            return new Punkt(vx, vy, 0);
        }
    }

    public class Dron : ObiektyRuchome
    {
        public Dron(string nazwa) : base(nazwa)
        {
        }
        public override int hmin { get => 20; } 
        public override int hmax { get => 100; }
        public override int vmin { get => 20; }
        public override int vmax { get => 100; }
    }

    public class Samolot : ObiektyRuchome
    {
        public Samolot(string nazwa) : base(nazwa)
        {
        }
        public override int hmin { get => 5000; }
        public override int hmax { get => 11000; }
        public override int vmin { get => 200; }
        public override int vmax { get => 1000; }
    }

    public class Smiglowiec : ObiektyRuchome
    {
        public Smiglowiec(string nazwa) : base(nazwa)
        {
        }
        public override int hmin { get => 50; }
        public override int hmax { get => 700; }
        public override int vmin { get => 60; }
        public override int vmax { get => 300; }
    }

    public class Balon : ObiektyRuchome
    {
        public Balon(string nazwa) : base(nazwa)
        {
        }
        public override int hmin { get => 50; }
        public override int hmax { get => 200; }
        public override int vmin { get => 20; }
        public override int vmax { get => 40; }
    }

    public class Szybowiec : ObiektyRuchome
    {
        public Szybowiec(string nazwa) : base(nazwa)
        {
        }
        public override int hmin { get => 200; }
        public override int hmax { get => 600; }
        public override int vmin { get => 100; }
        public override int vmax { get => 300; }
    }

}
