using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator_lotow
{
    public class Punkt
    {
        public int x, y, z;//powinno byc prywatne tylko dodac funkcje get x,y,z

        public Punkt(int xx, int yy, int zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }
    }

    public class Trasa
    {
        private int wysokosc;
        private int predkosc;
        private Punkt punkt_docelowy;
        private Punkt kierunek;

        public Trasa(int wysokosc, int predkosc, Punkt kierunek)
        {
            this.wysokosc = wysokosc;
            this.predkosc = predkosc;
            this.kierunek = new Punkt(kierunek.x, kierunek.y, kierunek.z);
        }

        public Trasa(Trasa t)
        {
            wysokosc = t.wysokosc;
            predkosc = t.predkosc;
            punkt_docelowy = t.punkt_docelowy;
            kierunek = t.kierunek;
        }
        /*private Punkt skladowe_predkosci()
        {

        }*/
    }

    public class Statek_powietrzny
    {
        protected int id;
        protected Punkt aktualna_pozycja;
        protected Trasa trasa;
        protected readonly int rozmiar;

        public void zmien_trase_recznie(Trasa t1)
        {
            trasa = new Trasa(t1);
        }

        /*public Trasa generuj_trase_losowo()
        {
            //wysokosc = rand..
        }*/
    }

    public class Dron : Statek_powietrzny
    {
        public const int hmin=0;
        public const int hmax=0;
        public const int vmin=0;
        public const int vmax=0;
    }

    public class Samolot : Statek_powietrzny
    {
        public const int hmin=0;
        public const int hmax=0;
        public const int vmin=0;
        public const int vmax=0;
    }

    public class Smiglowiec : Statek_powietrzny
    {
        public const int hmin=0;
        public const int hmax=0;
        public const int vmin=0;
        public const int vmax=0;
    }

    public class Balon : Statek_powietrzny
    {
        public const int hmin=0;
        public const int hmax=0;
        public const int vmin=0;
        public const int vmax=0;
    }

    public class Szybowiec : Statek_powietrzny
    {
        public const int hmin=0;
        public const int hmax=0;
        public const int vmin=0;
        public const int vmax=0;
    }

}
