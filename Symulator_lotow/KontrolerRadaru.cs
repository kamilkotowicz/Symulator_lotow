using Symulator_lotow;
using System.Diagnostics;
using System.Windows.Forms;

namespace Symulator_lotow
{
    public class KontrolerRadaru
    {
        private readonly MainForm mainForm;
        private readonly Ekran ekran;
        private readonly Symulator symulator;
        private double KROK_SYMULACJI = 0.05;

        public KontrolerRadaru(MainForm mainForm)
        {
            this.mainForm = mainForm;
            ekran = new Ekran();
            symulator = new Symulator();
            mainForm.pictureBox1.Paint += new PaintEventHandler(RysujZawartoscPictureBoxa);
        }
        private void RysujStale(Graphics g)
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

        private void RysujTrase(ObiektyRuchome sp, Graphics g, Point pos)
        {
            int ile_odcinkow = sp.trasa.odcinki.Count;
            int nr_odcinka = sp.trasa.nr_aktualnego_odcinka;
            if (nr_odcinka < ile_odcinkow)
            {
                Point[] points = new Point[ile_odcinkow - nr_odcinka + 1];
                points[0] = pos;
                for (int i = nr_odcinka; i < ile_odcinkow; i++)
                {
                    Punkt koniec_odcinka = sp.trasa.odcinki[i].koniec_odcinka;
                    points[i + 1 - nr_odcinka] = new Point((int)koniec_odcinka.x, (int)koniec_odcinka.y);
                }
                ekran.RysujLamana(points, Color.Black, g);
            }
        }
        private void RysujRuchome(Graphics g)
        {
            foreach (ObiektyRuchome sp in symulator.statki_powietrzne)
            {
                Point pos = new Point((int)sp.aktualna_pozycja.x, (int)sp.aktualna_pozycja.y);
                string opis = sp.nazwa + "\nWysokosc " + sp.aktualna_pozycja.z + "\nPredkosc " + sp.trasa.PredkoscAktualnegoOdcinka();
                RysujTrase(sp,g, pos);
                if (sp is Dron) ekran.RysujDron(pos, g);
                else if (sp is Samolot) ekran.RysujSamolot(pos, g);
                else if (sp is Balon) ekran.RysujBalon(pos, g);
                else if (sp is Smiglowiec) ekran.RysujSmiglowiec(pos, g);
                else if (sp is Szybowiec) ekran.RysujSzybowiec(pos, g);
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
            symulator.SprobujWczytacPlik(sciezka);
        }      
        private void SymulujRuch(Object myObject, EventArgs e)
        {
            symulator.SymulujRuch(KROK_SYMULACJI);
            symulator.WykryjKolizje();
            ZareagujNaZdarzenia(symulator.wykryte_zblizenia, symulator.wykryte_kolizje);
            mainForm.Redraw();
        }
        bool can_send_message = true;

        private bool SamolotKtoryWyladowal(SymulowanyObiekt so)
        {
            return (so is ObiektyRuchome sp && sp.czy_skonczyl_lot == true);
        }
        private void MessageKolizja(ObiektyRuchome sp)
        {
            sp.czy_skonczyl_lot = true;
            sp.trasa.odcinki.Clear();
            if (can_send_message)
            {
                can_send_message = false;
                KROK_SYMULACJI = 0;
                DialogResult res = MessageBox.Show("BUM!", $"Kolizja. Obiekt{sp.nazwa} zniszczony", MessageBoxButtons.OK);
                if (res == DialogResult.OK)
                {
                    can_send_message = true;
                    KROK_SYMULACJI = 0.25;
                }
            }
        }
        private void MessageZblizenie(NiebezpieczneZblizenie zdarzenie)
        {
            string message = $"Zblizenie miedzy {zdarzenie.a.nazwa} a {zdarzenie.b.nazwa}\nOdleglosc {zdarzenie.odleglosc} ";
            can_send_message = false;
            KROK_SYMULACJI = 0;
            DialogResult res = MessageBox.Show(message, "Czy chcesz zmienic trase?", MessageBoxButtons.YesNo);
            if (res == DialogResult.No)
            {
                can_send_message = true;
                KROK_SYMULACJI = 0.25; // zwiekszam szybkosc symulacji, aby pozbyc sie wielokrotnie wyskakujacych okienek do tego samego zblizenia
            }
            else if (res == DialogResult.Yes)
            {
                if (zdarzenie.a is ObiektyRuchome ob1)
                {
                    ob1.ZmienTrase();
                }
                if (zdarzenie.b is ObiektyRuchome ob2)
                {
                    ob2.ZmienTrase();
                }
                can_send_message = true;
                KROK_SYMULACJI = 0.25;
            }
        }
        private void ZareagujNaKolizje(List<Kolizja> wykryte_kolizje)
        {
            for (int i = 0; i < wykryte_kolizje.Count; ++i)
            {
                Kolizja kolizja = wykryte_kolizje[i];
                if(!(SamolotKtoryWyladowal(kolizja.a)||SamolotKtoryWyladowal(kolizja.b)))
                {
                    if (kolizja.a is ObiektyRuchome sp)
                    {
                        MessageKolizja(sp);
                    }           
                    if (kolizja.b is ObiektyRuchome sp2)
                    {
                        MessageKolizja(sp2);
                    }
                }
            }
        }

        private void ZareagujNaZblizenia(List<NiebezpieczneZblizenie> wykryte_zblizenia)
        {
            if (wykryte_zblizenia.Count > 1)
            {
                wykryte_zblizenia.Sort((a, b) => a.odleglosc.CompareTo(b.odleglosc)); //zblizenia z mniejsza odlegloscia powinny byc obsluzone przez kontrolera lotu jako pierwsze
            }
            foreach (NiebezpieczneZblizenie zblizenie in wykryte_zblizenia)
            {
                if (!(SamolotKtoryWyladowal(zblizenie.a) || SamolotKtoryWyladowal(zblizenie.b)))
                {
                    if (can_send_message)
                    {
                        MessageZblizenie(zblizenie);
                    }
                    break;
                }
            }
        }
        private void ZareagujNaZdarzenia(List<NiebezpieczneZblizenie> wykryte_zblizenia, List<Kolizja> wykryte_kolizje)
        {
            if (wykryte_zblizenia.Count + wykryte_kolizje.Count == 0)
            {
                KROK_SYMULACJI = 0.05;
                return;
            }
            ZareagujNaKolizje(wykryte_kolizje);
            ZareagujNaZblizenia(wykryte_zblizenia);
        }

        public void UruchomSymulacje()
        {
            WczytajMape();
            symulator.GenerujStatkiPowietrzne();
            System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
            t1.Tick += new EventHandler(SymulujRuch);
            t1.Interval = 500;
            t1.Start();
        }

    }
}
