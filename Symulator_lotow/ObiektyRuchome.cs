﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator_lotow
{

    public class ObiektyRuchome : SymulowanyObiekt
    {
        public Punkt aktualna_pozycja = new Punkt(0, 0, 0);
        public Trasa trasa = new();
        public bool czy_skonczyl_lot = false;
        public virtual int Hmin { get; }
        public virtual int Hmax { get; }
        public virtual int Vmin { get; }
        public virtual int Vmax { get; }
        public override bool CzyZawieraPunkt(Punkt p)
        {
            if (p.Odleglosc(aktualna_pozycja) <= 0)
            {
                return true;
            }
            return false;
        }

        public ObiektyRuchome(string nazwa)
        {
            this.nazwa = nazwa;
        }
        public Punkt LosujPozycje(int maxx, int maxy)
        {
            Random rand = new Random();
            int x = rand.Next(0, maxx);
            int y = rand.Next(0, maxy);
            int z = rand.Next(Hmin, Hmax);
            return new Punkt(x, y, z);
        }
        public void UstawNaLosowaPozycje(int maxx, int maxy)
        {
            aktualna_pozycja = LosujPozycje(maxx, maxy);
        }

        public OdcinekTrasy GenerujLosowyOdcinek(int maxx,int maxy)
        {
            Random rand = new Random();
            int predkosc = rand.Next(Vmin, Vmax);
            Punkt koniec_odcinka = LosujPozycje(maxx, maxy);
            return new OdcinekTrasy(predkosc, koniec_odcinka);
        }

        public void UstawTraseLosowo(int maxx,int maxy,int odcinki_min, int odcinki_max)
        {
            Random rand = new Random();
            int LICZBA_ODCINKOW = rand.Next(odcinki_min, odcinki_max);
            trasa = new Trasa();
            for(int i=0;i<LICZBA_ODCINKOW; i++)
            {
                OdcinekTrasy odc = GenerujLosowyOdcinek(maxx, maxy);
                trasa.odcinki.Add(odc);
            }
        }
        public Punkt SkladowePredkosci()
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
        public void WykonajRuch(double krok)
        {
            aktualna_pozycja.x += krok * SkladowePredkosci().x;
            aktualna_pozycja.y += krok * SkladowePredkosci().y;
        }

        public void PrzejdzDoNastepnegoOdcinka()
        {
            aktualna_pozycja = new Punkt(trasa.KoniecAktualnegoOdcinka());
            ++trasa.nr_aktualnego_odcinka;
            if (trasa.nr_aktualnego_odcinka < trasa.odcinki.Count)
            {
                aktualna_pozycja.z = trasa.KoniecAktualnegoOdcinka().z;
            }
            else
            {
                aktualna_pozycja.z = 0;
                czy_skonczyl_lot = true;
            }
        }
        public void ZmienTrase()
        {
            OdcinekTrasy losowy = GenerujLosowyOdcinek(Symulator.MAXX, Symulator.MAXY);
            if (trasa.nr_aktualnego_odcinka < trasa.odcinki.Count - 1) // jesli zostal wiecej niz 1 odcinek na trasie
            {
                trasa.odcinki[trasa.nr_aktualnego_odcinka] = new OdcinekTrasy(losowy);
            }
        }
    }

    public class Dron : ObiektyRuchome
    {
        public Dron(string nazwa) : base(nazwa)
        {
        }
        public override int Hmin { get => 20; } 
        public override int Hmax { get => 100; }
        public override int Vmin { get => 20; }
        public override int Vmax { get => 100; }
    }

    public class Samolot : ObiektyRuchome
    {
        public Samolot(string nazwa) : base(nazwa)
        {
        }
        public override int Hmin { get => 5000; }
        public override int Hmax { get => 11000; }
        public override int Vmin { get => 200; }
        public override int Vmax { get => 1000; }
    }

    public class Smiglowiec : ObiektyRuchome
    {
        public Smiglowiec(string nazwa) : base(nazwa)
        {
        }
        public override int Hmin { get => 50; }
        public override int Hmax { get => 700; }
        public override int Vmin { get => 60; }
        public override int Vmax { get => 300; }
    }

    public class Balon : ObiektyRuchome
    {
        public Balon(string nazwa) : base(nazwa)
        {
        }
        public override int Hmin { get => 50; }
        public override int Hmax { get => 200; }
        public override int Vmin { get => 20; }
        public override int Vmax { get => 40; }
    }

    public class Szybowiec : ObiektyRuchome
    {
        public Szybowiec(string nazwa) : base(nazwa)
        {
        }
        public override int Hmin { get => 200; }
        public override int Hmax { get => 600; }
        public override int Vmin { get => 100; }
        public override int Vmax { get => 300; }
    }

}
