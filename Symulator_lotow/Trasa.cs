namespace Symulator_lotow
{
    public class Trasa
    {
        private int wysokosc;
        private int predkosc;
        public Punkt punkt_docelowy;
        private Punkt kierunek;

        public Trasa(int wysokosc, int predkosc, Punkt punkt_docelowy)
        {
            this.wysokosc = wysokosc;
            this.predkosc = predkosc;
            this.punkt_docelowy = new Punkt(punkt_docelowy.x, punkt_docelowy.y, punkt_docelowy.z);
            //kierunek powinien byc obliczony
        }

        public Trasa(Trasa t)
        {
            wysokosc = t.wysokosc;
            predkosc = t.predkosc;
            punkt_docelowy = t.punkt_docelowy;
            kierunek = t.kierunek;
        }
        public Punkt skladowe_predkosci()
        {
            //Funkcja powinna zwracac skladowe predkosci w kierunkacj x,y,z
            //vx = (x.punkt_docelowy - x.aktualna_pozycja) * predkosc / Math.sqrt((x.punkt_docelowy - x.aktualna_pozycja) * (x.punkt_docelowy - x.aktualna_pozycja) + (y.punkt_docelowy - y.aktualna_pozycja) * (y.punkt_docelowy - y.aktualna_pozycja));
            //vy = (y.punkt_docelowy - y.aktualna_pozycja) * predkosc / Math.sqrt((x.punkt_docelowy - x.aktualna_pozycja) * (x.punkt_docelowy - x.aktualna_pozycja) + (y.punkt_docelowy - y.aktualna_pozycja) * (y.punkt_docelowy - y.aktualna_pozycja));
            return new Punkt(0, 0, 0);
        }
    }

}
