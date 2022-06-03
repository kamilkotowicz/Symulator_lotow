namespace Symulator_lotow
{
    public class Trasa
    {
        public List<OdcinekTrasy> odcinki = new List<OdcinekTrasy>();
        public int nr_aktualnego_odcinka = 0;
        public Punkt KoniecAktualnegoOdcinka()
        {
            int ile_odcinkow = odcinki.Count;
            if (nr_aktualnego_odcinka < ile_odcinkow)
            {
                return odcinki[nr_aktualnego_odcinka].koniec_odcinka;
            }
            if (ile_odcinkow > 0)
            {
                return odcinki[ile_odcinkow - 1].koniec_odcinka;
            }
            return new Punkt(0, 0, 0); // tutaj chyba powinien byc wyjatek
        }
        public double PredkoscAktualnegoOdcinka()
        {
            int ile_odcinkow = odcinki.Count;
            if(nr_aktualnego_odcinka < ile_odcinkow)
            {
                return odcinki[nr_aktualnego_odcinka].predkosc;
            }
            return 0;
            
        }
    }

    public class OdcinekTrasy
    {
        public double predkosc;
        public Punkt koniec_odcinka;

        public OdcinekTrasy(double predkosc, Punkt punkt_docelowy)
        {
            this.predkosc = predkosc;
            this.koniec_odcinka = new Punkt(punkt_docelowy.x, punkt_docelowy.y, punkt_docelowy.z);
        }
        public OdcinekTrasy(OdcinekTrasy odc)
        {
            this.predkosc=odc.predkosc;
            this.koniec_odcinka=new Punkt(odc.koniec_odcinka);
        }
    }
}
