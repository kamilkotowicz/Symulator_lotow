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
    }

    class Drzewo : ObiektyStale
    {
        public Drzewo(Punkt srodek, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {

        }
        public readonly int promien = 0;
    }

    class Blok : ObiektyStale
    {
        public Blok(Punkt srodek, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {

        }
        public readonly int dlugosc = 0;
        public readonly int szerokosc = 0 ;
    }

    class Wiezowiec : ObiektyStale
    {
        public Wiezowiec(Punkt srodek, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {

        }
        public readonly int bok = 0;
    }

    class Komin : ObiektyStale
    {
        public Komin(Punkt srodek, double wysokosc, string nazwa) : base(srodek, wysokosc, nazwa)
        {

        }
        public readonly int promien = 0;
    }


}
