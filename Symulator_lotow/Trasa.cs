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
            return new Punkt(0, 0, 0);
        }
    }

}
