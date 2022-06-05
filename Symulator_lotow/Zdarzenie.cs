namespace Symulator_lotow
{
    //Przechowuje informacje o obiektach bioracych udzial w kolizji lub zblizeniu
    public class Zdarzenie
    {
        public SymulowanyObiekt a, b;
        public Zdarzenie(SymulowanyObiekt a, SymulowanyObiekt b)
        {
            this.a = a;
            this.b = b;
        }
    }
    public class Kolizja : Zdarzenie
    {
        public double odleglosc;
        public Kolizja(SymulowanyObiekt a, SymulowanyObiekt b, double odleglosc) : base(a, b) { 
            this.odleglosc = odleglosc;
        }
    }

    public class NiebezpieczneZblizenie : Zdarzenie
    {
        public double odleglosc;
        public NiebezpieczneZblizenie(SymulowanyObiekt a, SymulowanyObiekt b, double odleglosc) : base(a, b) {
            this.odleglosc = odleglosc;
        }
    }
}