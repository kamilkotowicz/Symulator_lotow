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
    public static int licznik = 0;
    public void RysujZawartoscPictureBoxa(object sender, PaintEventArgs e)
    {


        Graphics g = e.Graphics;
        RysujStale(g);
        ++licznik;
        if (licznik % 2 == 1)
        {           
            RysujRuchome(g);
        }




        void RysujStale(Graphics g)
        {
            ekran.RysujDrzewo(new Point(100, 300), 8, g);
            ekran.RysujNapis("Drzewo", Brushes.Red, new Point(100, 300), g);
            ekran.RysujBlok(new Point(200, 400), new Point(40, 120), g);
            ekran.RysujNapis("Blok", Brushes.Red, new Point(200, 400), g);
            ekran.RysujWiezowiec(new Point(400, 200), 80, g);
            ekran.RysujNapis("Wiezowiec", Brushes.Red, new Point(400, 200), g);
            ekran.RysujKomin(new Point(500, 100), 14, g);
            ekran.RysujNapis("Komin", Brushes.Blue, new Point(500, 100), g);
        }

        void RysujRuchome(Graphics g)
        {
            ekran.RysujSamolot(new Point(300, 100), g);
            ekran.RysujNapis("Samolot", Brushes.Red, new Point(300, 100), g);
            ekran.RysujDron(new Point(700, 100), g);
            ekran.RysujNapis("Dron", Brushes.Red, new Point(700, 100), g);
            ekran.RysujSmiglowiec(new Point(700, 200), g);
            ekran.RysujNapis("Śmigłowiec", Brushes.Red, new Point(700, 200), g);
            ekran.RysujBalon(new Point(700, 300), g);
            ekran.RysujNapis("Balon", Brushes.Red, new Point(700, 300), g);
            ekran.RysujSzybowiec(new Point(800, 300), g);
            ekran.RysujNapis("Szybowiec", Brushes.Red, new Point(800, 300), g);
        }

    }
    public void WczytajMape()
    {
        string sciezka = "nazwa_pliku.txt"; //zmieniec nazwe pliku na wlasciwa
        symulator.wczytaj_z_pliku(sciezka);

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
        System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        t1.Tick += new EventHandler(SymulujRuch);
        t1.Interval = 200;
        t1.Start();
    }

}
