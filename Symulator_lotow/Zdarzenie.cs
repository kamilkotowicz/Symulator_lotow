namespace Symulator_lotow
{
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
        public Kolizja(SymulowanyObiekt a, SymulowanyObiekt b) : base(a, b) { }
    }

    public class NiebezpieczneZblizenie : Zdarzenie
    {
        public NiebezpieczneZblizenie(SymulowanyObiekt a, SymulowanyObiekt b) : base(a, b) { }
    }
}