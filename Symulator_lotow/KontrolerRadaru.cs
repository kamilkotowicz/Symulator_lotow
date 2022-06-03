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


        public KontrolerRadaru(MainForm mainForm)
        {
            this.mainForm = mainForm;
            ekran = new Ekran();
            symulator = new Symulator();
            mainForm.pictureBox1.Paint += new PaintEventHandler(RysujZawartoscPictureBoxa);
        }
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
                string opis = sp.nazwa + "\nWysokosc " + sp.aktualna_pozycja.z + "\nPredkosc " + sp.trasa.PredkoscAktualnegoOdcinka();
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

        double KROK_SYMULACJI = 0.05;
        private void SymulujRuch(Object myObject, EventArgs e)
        {
            symulator.SymulujRuch(KROK_SYMULACJI);
            symulator.WykryjKolizje();//tutaj trzeba dodac jeszcze obsluge kolizji
            ZareagujNaZdarzenia(symulator.wykryte_zblizenia, symulator.wykryte_kolizje);
            mainForm.Redraw();
        }
        bool can_send_message = true;
        private void ZareagujNaKolizje(List<Kolizja> wykryte_kolizje)
        {
            for (int i = 0; i < wykryte_kolizje.Count; ++i)
            {
                Kolizja kolizja = wykryte_kolizje[i];
                if (!(((kolizja.a is ObiektyRuchome sp3) && (sp3.czy_skonczyl_lot == true)) || ((kolizja.b is ObiektyRuchome sp4) && (sp4.czy_skonczyl_lot == true))))
                {
                    if (kolizja.a is ObiektyRuchome sp)
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
                                KROK_SYMULACJI = 0.05;
                            }
                        }
                    }
                    if (kolizja.b is ObiektyRuchome sp2)
                    {
                        sp2.czy_skonczyl_lot = true;
                        sp2.trasa.odcinki.Clear();
                        if (can_send_message)
                        {
                            can_send_message = false;
                            KROK_SYMULACJI = 0;
                            DialogResult res = MessageBox.Show("BUM!", $"Kolizja. Obiekt{sp2.nazwa} zniszczony", MessageBoxButtons.OK);
                            if (res == DialogResult.OK)
                            {
                                can_send_message = true;
                                KROK_SYMULACJI = 0.05;
                            }
                        }
                    }
                }


            }
        }

        private static void ZmienTrase(ObiektyRuchome sp)
        {
            OdcinekTrasy losowy = sp.GenerujLosowyOdcinek(Symulator.MAXX, Symulator.MAXY);
            int ile_odcinkow = sp.trasa.odcinki.Count;
            if (sp.trasa.nr_aktualnego_odcinka < ile_odcinkow - 1) // jesli zostal wiecej niz 1 odcinek na trasie
            {
                sp.trasa.odcinki[sp.trasa.nr_aktualnego_odcinka] = new OdcinekTrasy(losowy);
            }
        }
        private void ZareagujNaZblizenia(List<NiebezpieczneZblizenie> wykryte_zblizenia)
        {
            if (wykryte_zblizenia.Count == 0)
            {
                KROK_SYMULACJI = 0.05;
                return;
            }
            if (wykryte_zblizenia.Count > 1)
            {
                wykryte_zblizenia.Sort((a, b) => a.odleglosc.CompareTo(b.odleglosc)); //zblizenia z mniejsza odlegloscia powinny byc obsluzone przez kontrolera jako pierwsze
            }
            foreach (NiebezpieczneZblizenie zblizenie in wykryte_zblizenia)
            {
                // jesli zaden z obiektow nie jest samolotem ktory juz skonczyl lot 
                if (!(((zblizenie.a is ObiektyRuchome sp) && (sp.czy_skonczyl_lot == true)) || ((zblizenie.b is ObiektyRuchome sp2) && (sp2.czy_skonczyl_lot == true))))
                {
                    string message = $"Zblizenie miedzy {zblizenie.a.nazwa} a {zblizenie.b.nazwa}\nOdleglosc {zblizenie.odleglosc} ";
                    if (can_send_message)
                    {
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
                            if (zblizenie.a is ObiektyRuchome ob1)
                            {
                                ZmienTrase(ob1);
                            }
                            if (zblizenie.b is ObiektyRuchome ob2)
                            {
                                ZmienTrase(ob2);
                            }
                            can_send_message = true;
                            KROK_SYMULACJI = 0.25;
                        }
                    }
                    break;
                }

            }
        }
        private void ZareagujNaZdarzenia(List<NiebezpieczneZblizenie> wykryte_zblizenia, List<Kolizja> wykryte_kolizje)
        {
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
