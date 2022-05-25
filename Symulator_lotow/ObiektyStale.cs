using System;

namespace Symulator_lotow
{

    class ObiektyStale
    {
        protected int id;
        public int wysokosc;
        public Punkt pozycja_srodka;
        public string nazwa;

    }

    class Drzewo : ObiektyStale
    {
        public readonly int promien = 0;
    }

    class Blok : ObiektyStale
    {
        public readonly int dlugosc = 0;
        public readonly int szerokosc = 0 ;
    }

    class Wiezowiec : ObiektyStale
    {
        public readonly int bok = 0;
    }

    class Komin : ObiektyStale
    {
        public readonly int promien = 0;
    }


}
