using Symulator_lotow;
using System.Windows.Forms;
public class KontrolerRadaru
{
    private MainForm mainForm;
    private Ekran ekran;
    private Symulator symulator;

    //public MainForm MainForm { get; }

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
        // wczytywanie mapy z pliku
    }
    private void SymulujRuch(Object myObject, EventArgs e)
    {
        //Zmien pozycje obiektow w symulatorze
        symulator.SymulujRuch();
        //Narysuj ponownie wszystkie obiekty
        mainForm.Redraw();

    }
    public void UruchomSymulacje()
    {
        WczytajMape();
        // co jakis okres czasu: aktualizuj pozycje w symuatorze i przesun obiekty na ekranie
        System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        t1.Tick += new EventHandler(SymulujRuch);//funkcja wywolujaca sie co jakis czas
        t1.Interval = 200;
        t1.Start();
    }
}
