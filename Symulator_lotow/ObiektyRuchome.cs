using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator_lotow
{

    public class ObiektyRuchome
    {
        public Punkt aktualna_pozycja;
        public Trasa trasa;
        public readonly int rozmiar = 1;// rozmiar statku powietrznego bedzie potrzebny przy wykrywaniu kolizji
        public string nazwa;
        public virtual int hmin { get; }
        public virtual int hmax { get; }
        public virtual int vmin { get; }
        public virtual int vmax { get; }

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

        public void zmien_trase_recznie(Trasa t1)
        {
            //jesli "wykryto kolizje"
            int nowawysokosc = int.Parse(Console.ReadLine());
            trasa = new Trasa(nowawysokosc, trasa.predkosc, trasa.punkt_docelowy);
        }

        public Trasa UstawTraseLosowo(int maxx,int maxy)
        {
            Random rand = new Random();
            int predkosc = rand.Next(vmin, vmax);
            Punkt pozycja_celu = LosujPozycje(maxx, maxy);
            return new Trasa(pozycja_celu.z, predkosc, pozycja_celu);
        }
        public Punkt skladowe_predkosci()
        {
            //Funkcja powinna zwracac skladowe predkosci w kierunkacj x,y,z
            double vx = (trasa.punkt_docelowy.x - aktualna_pozycja.x) * trasa.predkosc / Math.Sqrt((trasa.punkt_docelowy.x - aktualna_pozycja.x) * (trasa.punkt_docelowy.x - aktualna_pozycja.x) + (trasa.punkt_docelowy.y - aktualna_pozycja.y) * (trasa.punkt_docelowy.y - aktualna_pozycja.y));
            double vy = (trasa.punkt_docelowy.y - aktualna_pozycja.y) * trasa.predkosc / Math.Sqrt((trasa.punkt_docelowy.x - aktualna_pozycja.x) * (trasa.punkt_docelowy.x - aktualna_pozycja.x) + (trasa.punkt_docelowy.y - aktualna_pozycja.y) * (trasa.punkt_docelowy.y - aktualna_pozycja.y));
            return new Punkt(vx, vy, 0);
        }

    }

    public class Dron : ObiektyRuchome
    {
        public Dron(string nazwa) : base(nazwa)
        {
        }
        public override int hmin { get => 0; } 
        public override int hmax { get => 100; }
        public override int vmin { get => 5; }
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
        public override int hmax { get => 7000; }
        public override int vmin { get => 10; }
        public override int vmax { get => 300; }
    }

    public class Balon : ObiektyRuchome
    {
        public Balon(string nazwa) : base(nazwa)
        {
        }
        public override int hmin { get => 50; }
        public override int hmax { get => 2000; }
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
