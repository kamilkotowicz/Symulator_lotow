using System;
namespace Symulator_lotow
{
	public class Symulator
	{
		private List<ObiektyStale> obiekty_stale = new List<ObiektyStale>();
		private List<ObiektyRuchome> statki_powietrzne = new List<ObiektyRuchome>();
		private int maxx = 1000, maxy = 1000, maxz = 10000;
		public Symulator()
		{
		}
		public void wczytaj_z_pliku(string text)
        {
			//string text = System.IO.File.ReadAllText(@"G:\PO\Projekt\Mapa.txt");



			//Funkcja powinna wczytywac z pliku do list obiekty_stale
			//Jesli plik nie istnieje powinien byc zwrocony wyjatek.
		}

		public void WykryjKolizje()
        {
			//Funkcja musi jakosc przekazac informacje o kolizjach do kontrolera lotu
			//należy każdy statek powietrzny porównać z każdym statkiem powietrznym i obiektem stałym
			//wykryj kolizje jeśli odległość jest mniejsza niż 100
			//1. Porównujemy wysokość (składowa z)
			foreach (ObiektyRuchome sp in statki_powietrzne)
			{
				foreach (ObiektyRuchome sp2 in statki_powietrzne)
				{
					if (sp2 != sp)
					{
						if (Math.Abs(sp2.aktualna_pozycja.z - sp.aktualna_pozycja.z) < 100)
                        {
							//wykryto kolizje - wysokosc miedzy obiektami mniejsza od 100
                        }
                        else
                        {
							//2. Sprawdzamy czy na płaszczyźnie odległość jest mniejsza od 100 (skladowe x,y)
							if(Math.Sqrt(Math.Abs(sp.aktualna_pozycja.x- sp2.aktualna_pozycja.x)* Math.Abs(sp.aktualna_pozycja.x - sp2.aktualna_pozycja.x) + Math.Abs(sp.aktualna_pozycja.y - sp2.aktualna_pozycja.y) * Math.Abs(sp.aktualna_pozycja.y - sp2.aktualna_pozycja.y)) <100)
                            {
								//wykryto kolizje
                            }
                        }
					}
				}
			}



		}

		public void GenerujStatkiPowietrzne()
        {
			Random rand = new Random();
			const int ILE_STATKOW = 10;
			const int ILE_RODZAJOW = 5;
			for(int i= 0; i < ILE_STATKOW; i++)
            {
				ObiektyRuchome nowy_statek;
				int rodzaj = rand.Next(0, ILE_RODZAJOW);
                switch (rodzaj)
                {
					case 0:
						nowy_statek = new Dron();
						break;
					case 1:
						nowy_statek = new Samolot();
						break;
					case 2:
						nowy_statek = new Smiglowiec();
						break;
					case 3:
						nowy_statek = new Balon();
						break;
					default:
						nowy_statek = new Szybowiec();
						break;
                }
				do
				{
					int losowy_x = rand.Next(0, maxx);
					int losowy_y = rand.Next(0, maxy);
					int losowy_z = rand.Next(nowy_statek.hmin, nowy_statek.hmax);
					nowy_statek.aktualna_pozycja = new Punkt(losowy_x, losowy_y, losowy_z);
				}
				while (CzyZajete(nowy_statek.aktualna_pozycja));

				do
				{
					nowy_statek.trasa = nowy_statek.generuj_trase_losowo();
				}
				while (CzyZajete(nowy_statek.trasa.punkt_docelowy));
				
			}
        }
		public void SymulujRuch(double krok) // trzeba przetestowac jaka wartosc krok bedzie sensowna
		{
			foreach (ObiektyRuchome sp in statki_powietrzne)
			{
				Punkt v = sp.skladowe_predkosci();
				sp.aktualna_pozycja.x += krok * v.x;
				sp.aktualna_pozycja.y += krok * v.y;
			}
		}

		private bool CzyZajete(Punkt p)
		{
			foreach (ObiektyRuchome sp in statki_powietrzne)
            {
				Punkt akt_poz = sp.aktualna_pozycja;
                if (p.Odleglosc(akt_poz) <= sp.rozmiar)
                {
					return true;
                }
            }
			foreach (ObiektyStale os in obiekty_stale)
			{
				if(os is Drzewo d)
                {
                    if (p.Odleglosc(os.pozycja_srodka) <= d.promien)
                    {
						return true;
                    }
				}
				if(os is Komin k)
                {
                    if (p.Odleglosc(os.pozycja_srodka) <= k.promien)
                    {
						return true;
                    }
                }
				if(os is Blok b)
                {
					double minx = os.pozycja_srodka.x - b.dlugosc;
					double maxx = os.pozycja_srodka.x + b.dlugosc;
					double miny = os.pozycja_srodka.y - b.szerokosc;
					double maxy = os.pozycja_srodka.y + b.szerokosc;
					if(minx<= p.x && miny<= p.y && p.x<=maxx && p.y<=maxy && p.z <= os.wysokosc)
                    {
						return true;
                    }
                }
				if(os is Wiezowiec w)
                {
					double minx = os.pozycja_srodka.x - w.bok;
					double maxx = os.pozycja_srodka.x + w.bok;
					double miny = os.pozycja_srodka.y - w.bok;
					double maxy = os.pozycja_srodka.y + w.bok;
					if (minx <= p.x && miny <= p.y && p.x <= maxx && p.y <= maxy && p.z <= os.wysokosc)
					{
						return true;
					}
				}
			}
			return false;
		}


	}
}
