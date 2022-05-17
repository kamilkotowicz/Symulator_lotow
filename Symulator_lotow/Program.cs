namespace Symulator_lotow
{
    using System.Diagnostics;
    using System.Windows.Forms;
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());

        }

        //Symulator sym;
        //Ekran ek;
        public static void WczytajMape()
        {
            // wczytywanie mapy z pliku
        }
        private static void SymulujRuch(Object myObject, EventArgs e)
        {
            //Funkcja powinna zmieniac wspolrzedne na ekranie wszystkich obiektow poruszajacych sie
        }
        public static void UruchomSymulacje()
        {
            WczytajMape();
            // co jakis okres czasu: aktualizuj pozycje w symuatorze i przesun obiekty na ekranie
            Timer t1 = new Timer();
            t1.Tick += new EventHandler(SymulujRuch);//funkcja wywolujaca sie co jakis czas
            t1.Interval = 5000;
            t1.Start();
        }
    }
}