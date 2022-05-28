using Symulator_lotow;
using System.Windows.Forms;
public class KontrolerRadaru
{
    private MainForm mainForm;
    private Ekran ekran;
    private Symulator symulator;


    public KontrolerRadaru(MainForm mainForm)
	{
        this.mainForm = mainForm;
        ekran = new Ekran();
        symulator = new Symulator();
        mainForm.pictureBox1.Paint += new PaintEventHandler(RysujZawartoscPictureBoxa);
    }
    public static int licznik = 0; //chyba nie jest potrzebne ale na razie zostawie
    public void RysujStale(Graphics g)
    {
        foreach (ObiektyStale os in symulator.obiekty_stale)
        {
            string opis = os.nazwa + "\nWysokosc " + os.wysokosc;
            Point pos = new Point((int)os.pozycja_srodka.x, (int)os.pozycja_srodka.y);
            if (os is Drzewo d)
            {
                ekran.RysujDrzewo(pos, (int)d.promien, g);
            }
            else if (os is Komin k)
            {
                ekran.RysujKomin(pos, (int)k.promien, g);
            }
            else if (os is Blok b)
            {
                Point rozm = new Point((int)b.dlugosc, (int)b.szerokosc);
                ekran.RysujBlok(pos, rozm, g);
            }
            else if (os is Wiezowiec w)
            {
                ekran.RysujWiezowiec(pos, (int)w.bok, g);
            }
            ekran.RysujNapis(opis, Brushes.Black, pos, g);
        }
    }
    void RysujRuchome(Graphics g)
    {
        foreach (ObiektyRuchome sp in symulator.statki_powietrzne)
        {
            Point pos = new Point((int)sp.aktualna_pozycja.x, (int)sp.aktualna_pozycja.y);
            string opis = sp.nazwa + "\nWysokosc " + sp.aktualna_pozycja.z + "\nPredkosc " + sp.trasa.predkosc;
            Point[] points =new Point[2];
            points[0] = pos;
            points[1] = new Point((int)sp.trasa.punkt_docelowy.x, (int)sp.trasa.punkt_docelowy.y);
            ekran.RysujLamana(points, Color.Black, g);
            if (sp is Dron) ekran.RysujDron(pos, g);
            else if (sp is Samolot) ekran.RysujSamolot(pos, g);
            else if (sp is Balon) ekran.RysujBalon(pos, g);
            else if(sp is Smiglowiec) ekran.RysujSmiglowiec(pos, g);
            else if(sp is Szybowiec) ekran.RysujSzybowiec(pos, g);
            ekran.RysujNapis(opis, Brushes.Black, pos, g);
        }
    }
    public void RysujZawartoscPictureBoxa(object sender, PaintEventArgs e)
    {


        Graphics g = e.Graphics;
        RysujStale(g);       
        RysujRuchome(g);

    }
    public void WczytajMape()
    {
        string sciezka = Path.Combine(Environment.CurrentDirectory.ToString(), @"..\..\..\..\Mapa.txt");
        symulator.WczytajPlik(sciezka);

    }
    private void SymulujRuch(Object myObject, EventArgs e)
    {
        double krok = 0.01;//trzeba pzetestowac jaka tu dac wartosc
        symulator.SymulujRuch(krok);
        symulator.WykryjKolizje();//tutaj trzeba dodac jeszcze obsluge kolizji
        mainForm.Redraw();
    }
    public void UruchomSymulacje()
    {
        WczytajMape();
        symulator.GenerujStatkiPowietrzne();
        System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        t1.Tick += new EventHandler(SymulujRuch);
        t1.Interval = 100;
        t1.Start();
    }

}
