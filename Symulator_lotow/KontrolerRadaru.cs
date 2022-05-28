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
        /*ekran.RysujSamolot(new Point(300, 100), g);
        ekran.RysujNapis("Samolot", Brushes.Red, new Point(300, 100), g);
        ekran.RysujDron(new Point(700, 100), g);
        ekran.RysujNapis("Dron", Brushes.Red, new Point(700, 100), g);
        ekran.RysujSmiglowiec(new Point(700, 200), g);
        ekran.RysujNapis("Śmigłowiec", Brushes.Red, new Point(700, 200), g);
        ekran.RysujBalon(new Point(700, 300), g);
        ekran.RysujNapis("Balon", Brushes.Red, new Point(700, 300), g);
        ekran.RysujSzybowiec(new Point(800, 300), g);
        ekran.RysujNapis("Szybowiec", Brushes.Red, new Point(800, 300), g);*/
        foreach (ObiektyRuchome sp in symulator.statki_powietrzne)
        {
            Point pos = new Point((int)sp.aktualna_pozycja.x, (int)sp.aktualna_pozycja.y);
            ekran.RysujNapis(sp.nazwa, Brushes.Black,pos, g);
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
        double krok = 1;//trzeba pzetestowac jaka tu dac wartosc
        symulator.SymulujRuch(krok);
        symulator.WykryjKolizje();//tutaj trzeba dodac jeszcze obsluge kolizji
        mainForm.Redraw();
    }
    public void UruchomSymulacje()
    {
        WczytajMape();
        //symulator.GenerujStatkiPowietrzne(); na razie jest jakis blad
        System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        t1.Tick += new EventHandler(SymulujRuch);
        t1.Interval = 200;
        t1.Start();
    }

}
