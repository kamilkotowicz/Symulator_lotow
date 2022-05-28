namespace Symulator_lotow
{
    public class Trasa
    {
        public double wysokosc;
        public double predkosc;
        public Punkt punkt_docelowy;
        private Punkt kierunek;

        public Trasa(double wysokosc, double predkosc, Punkt punkt_docelowy)
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
    }

}
