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
        public readonly int promien;
    }

    class Blok : ObiektyStale
    {
        public readonly int dlugosc;
        public readonly int szerokosc;
    }

    class Wiezowiec : ObiektyStale
    {
        public readonly int bok;
    }

    class Komin : ObiektyStale
    {
        public readonly int promien;
    }


}
