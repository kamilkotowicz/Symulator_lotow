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
            Random generator = new Random(0);
            int wysokosc = generator.Next(0, 11000);
            int predkosc = generator.Next(5, 1000);
            int x = generator.Next(0, 1000);
            int y = generator.Next(0, 800);
            return new Trasa(wysokosc, predkosc, new Punkt(x,y,wysokosc));
        }

    }

    public class Dron : ObiektyRuchome
    {
        public override int hmin { get => 0; } 
        public override int hmax { get => 100; }
        public override int vmin { get => 5; }
        public override int vmax { get => 100; }
    }

    public class Samolot : ObiektyRuchome
    {
        public override int hmin { get => 5000; }
        public override int hmax { get => 11000; }
        public override int vmin { get => 200; }
        public override int vmax { get => 1000; }
    }

    public class Smiglowiec : ObiektyRuchome
    {
        public override int hmin { get => 50; }
        public override int hmax { get => 7000; }
        public override int vmin { get => 10; }
        public override int vmax { get => 300; }
    }

    public class Balon : ObiektyRuchome
    {
        public override int hmin { get => 50; }
        public override int hmax { get => 2000; }
        public override int vmin { get => 20; }
        public override int vmax { get => 40; }
    }

    public class Szybowiec : ObiektyRuchome
    {
        public override int hmin { get => 200; }
        public override int hmax { get => 600; }
        public override int vmin { get => 100; }
        public override int vmax { get => 300; }
    }

}
