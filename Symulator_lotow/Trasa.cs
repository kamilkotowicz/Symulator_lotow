using System.Diagnostics;

namespace Symulator_lotow
{
    //Kazdy obiekt ruchomy posiada trase. Trasa sklada sie z listy odcinkow o stalym kierunku, wysokosci i predkosci.
    public class Trasa
    {
        private List<OdcinekTrasy> odcinki = new List<OdcinekTrasy>();
        public int nr_aktualnego_odcinka = 0;

        public List<OdcinekTrasy> Odcinki { get => odcinki;  }

        private OdcinekTrasy AktualnyOdcinek()
        {
            if (nr_aktualnego_odcinka < odcinki.Count)
            {
                return odcinki[nr_aktualnego_odcinka];
            }
            throw new BrakOdcinkaException("Numer aktualnego odcinka jest nieprawidlowy lub trasa nie zawiera ani jednego odcinka");
        }
        public Punkt KoniecAktualnegoOdcinka()
        {
            try
            {
                OdcinekTrasy odc = AktualnyOdcinek();
                return odc.koniec_odcinka;
            }
            catch(BrakOdcinkaException e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
        public double PredkoscAktualnegoOdcinka()
        {
            try
            {
                OdcinekTrasy odc = AktualnyOdcinek();
                return odc.predkosc;
            }
            catch (BrakOdcinkaException e)
            {
                Debug.WriteLine(e);
                return 0;
            }         
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
