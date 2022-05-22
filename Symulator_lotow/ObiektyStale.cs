using System;

namespace Symulator_lotow
{

    class Obiekty_stale
    {
        protected int id;
        protected int wysokosc;
        protected Punkt pozycja_srodka;
        public string nazwa;

    }

    class Drzewo : Obiekty_stale
    {
        private readonly int promien;
    }

    class Blok : Obiekty_stale
    {
        private readonly int dlugosc;
        private readonly int szerokosc;
    }

    class Wieżowiec : Obiekty_stale
    {
        private readonly int bok;
    }

    class Komin : Obiekty_stale
    {
        private readonly int promien;
    }


}
