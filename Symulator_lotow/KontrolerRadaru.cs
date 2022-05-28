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
            if (os is Drzewo d)
            {
                Point pos = new Point((int)d.pozycja_srodka.x, (int)d.pozycja_srodka.y);
                ekran.RysujDrzewo(pos, (int)d.promien, g);
                ekran.RysujNapis(d.nazwa, Brushes.Black, pos, g);
            }
            else if (os is Komin k)
            {
                Point pos = new Point((int)k.pozycja_srodka.x, (int)k.pozycja_srodka.y);
                ekran.RysujKomin(pos, (int)k.promien, g);
                ekran.RysujNapis(k.nazwa, Brushes.Black, pos, g);
            }
            else if (os is Blok b)
            {
                Point pos = new Point((int)b.pozycja_srodka.x, (int)b.pozycja_srodka.y);
                Point rozm = new Point((int)b.dlugosc, (int)b.szerokosc);
                ekran.RysujBlok(pos, rozm, g);
                ekran.RysujNapis(b.nazwa, Brushes.Black, pos, g);
            }
            else if (os is Wiezowiec w)
            {
                Point pos = new Point((int)w.pozycja_srodka.x, (int)w.pozycja_srodka.y);
                ekran.RysujWiezowiec(pos, (int)w.bok, g);
                ekran.RysujNapis(w.nazwa, Brushes.Black, pos, g);
            }
        }
    }
    void RysujRuchome(Graphics g)
    {
        foreach (ObiektyRuchome sp in symulator.statki_powietrzne)
        {
            Point pos = new Point((int)sp.aktualna_pozycja.x, (int)sp.aktualna_pozycja.y);
            string opis = sp.nazwa + "\nWysokosc " + sp.aktualna_pozycja.z + "\nPredkosc " + sp.trasa.predkosc;
            ekran.RysujNapis(opis, Brushes.Black,pos, g);
            Point[] points =new Point[2];
            points[0] = pos;
            points[1] = new Point((int)sp.trasa.punkt_docelowy.x, (int)sp.trasa.punkt_docelowy.y);
            ekran.RysujLamana(points, Color.Black, g);
            if (sp is Dron) ekran.RysujDron(pos, g);
            else if (sp is Samolot) ekran.RysujSamolot(pos, g);
            else if (sp is Balon) ekran.RysujBalon(pos, g);
            else if(sp is Smiglowiec) ekran.RysujSmiglowiec(pos, g);
            else if(sp is Szybowiec) ekran.RysujSzybowiec(pos, g);             
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
