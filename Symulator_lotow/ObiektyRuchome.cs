using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator_lotow
{

    public class ObiektyRuchome
    {
        protected int id;
        public Punkt aktualna_pozycja;
        public Trasa trasa;
        public readonly int rozmiar = 1;// rozmiar statku powietrznego bedzie potrzebny przy wykrywaniu kolizji
        public string nazwa;
        public virtual int hmin { get; }
        public virtual int hmax { get; }
        public virtual int vmin { get; }
        public virtual int vmax { get; }


        public void zmien_trase_recznie(Trasa t1)
        {
            trasa = new Trasa(t1);
        }

        public Trasa generuj_trase_losowo()
        {
            return new Trasa(5000, 500, new Punkt(500,500,5000));
            //Ta funkcja powinna wygenerowac losowa trase
            //wysokosc = rand..
        }
    }

    public class Dron : ObiektyRuchome
    {
        public override int hmin { get => 0; } // ustawic wartosci hmin,hmax,vmin,vmax
        public override int hmax { get => 10000; }
        public override int vmin { get => 0; }
        public override int vmax { get => 1000; }
    }

    public class Samolot : ObiektyRuchome
    {
        public override int hmin { get => 0; }
        public override int hmax { get => 10000; }
        public override int vmin { get => 0; }
        public override int vmax { get => 1000; }
    }

    public class Smiglowiec : ObiektyRuchome
    {
        public override int hmin { get => 0; }
        public override int hmax { get => 10000; }
        public override int vmin { get => 0; }
        public override int vmax { get => 1000; }
    }

    public class Balon : ObiektyRuchome
    {
        public override int hmin { get => 0; }
        public override int hmax { get => 10000; }
        public override int vmin { get => 0; }
        public override int vmax { get => 1000; }
    }

    public class Szybowiec : ObiektyRuchome
    {
        public override int hmin { get => 0; }
        public override int hmax { get => 10000; }
        public override int vmin { get => 0; }
        public override int vmax { get => 1000; }
    }

}
