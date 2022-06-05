using System;
using System.Diagnostics;

namespace Symulator_lotow
{
	//Jedna z glownych klas programu. Odpowiadajaca za symulacje obiektow, wczytywanie obiektow z pliku i wykrywanie kolizji.
	public class Symulator
	{
		internal List<ObiektyStale> obiekty_stale = new List<ObiektyStale>();
		internal List<ObiektyRuchome> statki_powietrzne = new List<ObiektyRuchome>();
		public List<NiebezpieczneZblizenie> wykryte_zblizenia = new List<NiebezpieczneZblizenie>();
		public List<Kolizja> wykryte_kolizje = new List<Kolizja>();
		private static readonly Random rand = new Random();
		public const int MAXX = 1800, MAXY = 1000;
		public const double ODLEGLOSC_KOLIZJI = 10;
		public const double ODLEGLOSC_NIEBEZPIECZNA = 50;
		public const int ILE_RODZAJOW_SAMOLOTOW = 5;
		public const int ILE_SAMOLOTOW = 8;
		public const int MIN_LICZBA_ODCINKOW = 2;
		public const int MAX_LICZBA_ODCINKOW = 4;
		public Symulator()
		{
		}

		//Funkcja odpowiada za wczytywanie obiektow stalych z pliku. Jesli plik zawiera niepoprawne dane, to zglaszane sa odpowiednie wyjatki.
		public void SprobujWczytacPlik(string sciezka)
		{
			try
			{
				WczytajPlik(sciezka);
			}
			catch (FileNotFoundException e)
			{
				Debug.WriteLine(e + "Nie znaleziono pliku.");
				Environment.Exit(0);
			}
			catch (BadDataInFileException e)
			{
				Debug.WriteLine(e + "Nieznany kod typu samolotu w pliku.");
				Environment.Exit(0);
			}
			catch (FormatException e)
            {
				Debug.WriteLine(e + "W pewnym miejscu w pliku oczekiwano liczby, a nie napisu.");
				Environment.Exit(0);
            }
			catch (OverflowException e)
            {
				Debug.WriteLine(e + "W pewnym miejscu w pliku podano zbyt duza liczbe.");
				Environment.Exit(0);
			}
			catch (Exception)
			{
				Debug.WriteLine("Wystapil nieznany blad.");
				Environment.Exit(0);
			}
		}

		private void WczytajPlik(string sciezka)
		{
			string[] linie = File.ReadAllLines(sciezka);
			for (int i = 0; i < linie.Length; i++)
			{
				string[] dane = linie[i].Split(",");
				WczytajLinie(i, dane);
			}
		}

		private void WczytajLinie(int nr, string[] dane)
        {
			/* FORMAT DANYCH	
			W wierszu pliku dane oddzielone sa przecinkiem.
			Jesli typ to blok oznaczony jako B to linia sklada sie z 5 danych: 
				typ, wspolrzedna x srodka, wspolrzedna y srodka, dlugosc, szerokosc, wysokosc
			W pozostalych przypadkach linia zawiera 4 dane.
			W przypadku drzewa oznaczonego D lub komina oznaczonego K:
				typ, wspolrzedna x srodka, wspolrzedna y srodka, promien, wysokosc
			W przypadku wiezowca oznaczonego W dlugosc i szerokosc sa takie same (jest kwadratem):
				typ, wspolrzedna x srodka, wspolrzedna y srodka, dlugosc, wysokosc*/
			int x = Convert.ToInt32(dane[1]);
			int y = Convert.ToInt32(dane[2]);
			int z = Convert.ToInt32(dane[^1]);
			int r, a, b;
			switch (dane[0])
			{
				case "D":
					r = Convert.ToInt32(dane[3]);
					obiekty_stale.Add(new Drzewo(new Punkt(x, y, z / 2), r, z, "Drzewo " + nr.ToString()));
					break;
				case "K":
					r = Convert.ToInt32(dane[3]);
					obiekty_stale.Add(new Komin(new Punkt(x, y, z / 2), r, z, "Komin " + nr.ToString()));
					break;
				case "W":
					a = Convert.ToInt32(dane[3]);
					obiekty_stale.Add(new Wiezowiec(new Punkt(x, y, z / 2), a, z, "Wiezowiec " + nr.ToString()));
					break;
				case "B":
					a = Convert.ToInt32(dane[3]);
					b = Convert.ToInt32(dane[4]);
					obiekty_stale.Add(new Blok(new Punkt(x, y, z / 2), a, b, z, "Blok " + nr.ToString()));
					break;
				default:
					throw new BadDataInFileException("Niepoprawne dane w pliku");
			}
		}
		//Wykrywa zblizenia i kolizje. Zapisuje je do list wykryte_zblizenia i wykryte_kolizje w obiekcie klasy Symulator.
		public void WykryjZblizeniaOrazKolizje()
		{
			wykryte_zblizenia.Clear();
			wykryte_kolizje.Clear();
			WykryjBliskieObiektyRuchome();
			WykryjBliskieObiektyStale();
		}
		private void WykryjBliskieObiektyRuchome()
        {
			for (int i = 0; i < statki_powietrzne.Count; ++i)
			{
				ObiektyRuchome sp = statki_powietrzne[i];
				for (int j = i + 1; j < statki_powietrzne.Count; ++j)
				{
					ObiektyRuchome sp2 = statki_powietrzne[j];
					double odleglosc = sp.aktualna_pozycja.Odleglosc(sp2.aktualna_pozycja);
					if (odleglosc < ODLEGLOSC_KOLIZJI)
					{
						wykryte_kolizje.Add(new Kolizja(sp, sp2, odleglosc));
					}
					else if (odleglosc < ODLEGLOSC_NIEBEZPIECZNA)
					{
						wykryte_zblizenia.Add(new NiebezpieczneZblizenie(sp, sp2, odleglosc));
					}
				}
			}
		}

		private void WykryjBliskieObiektyStale()
        {
			foreach (ObiektyRuchome sp in statki_powietrzne)
			{
				foreach (ObiektyStale os in obiekty_stale)
				{
					double odleglosc = os.OdlegloscDoSamolotu(sp.aktualna_pozycja);
					if(odleglosc < ODLEGLOSC_KOLIZJI)
                    {
						wykryte_kolizje.Add(new Kolizja(sp, os, odleglosc));
					}
					else if(odleglosc < ODLEGLOSC_NIEBEZPIECZNA)
                    {
						wykryte_zblizenia.Add(new NiebezpieczneZblizenie(sp, os, odleglosc));
					}
				}
			}
		}
		/*
		Generuje losowe statki powietrzne. Kazdy typ samolotu ma równe szanse na wylosowanie sie.
		Poczatkowa pozycja jest losowa, ale nie moze sie zaczynac od zajetego juz punktu (przez inny samolot lub budynek).
		Jest ustalana losowa trasa, skladajaca sie z kilku prostych odcinkow.
		*/
		public void GenerujStatkiPowietrzne()
		{
			for (int i = 0; i < ILE_SAMOLOTOW; i++)
			{
				ObiektyRuchome nowy_statek = StatekLosowegoTypu(i);
				do
				{
					nowy_statek.UstawNaLosowaPozycje(MAXX, MAXY);
				}
				while (CzyZajete(nowy_statek.aktualna_pozycja));
				do
				{
					nowy_statek.UstawTraseLosowo(MAXX, MAXY, MIN_LICZBA_ODCINKOW, MAX_LICZBA_ODCINKOW);
				}
				while (CzyZajete(nowy_statek.trasa.KoniecAktualnegoOdcinka()));
				nowy_statek.aktualna_pozycja.z = nowy_statek.trasa.KoniecAktualnegoOdcinka().z;
				statki_powietrzne.Add(nowy_statek);
			}
		}
		private static ObiektyRuchome StatekLosowegoTypu(int id)
        {
			int rodzaj = rand.Next(0, ILE_RODZAJOW_SAMOLOTOW);
            ObiektyRuchome statek = rodzaj switch
            {
                0 => new Dron("Dron " + id.ToString()),
                1 => new Samolot("Samolot " + id.ToString()),
                2 => new Smiglowiec("Smiglowiec " + id.ToString()),
                3 => new Balon("Balon " + id.ToString()),
                _ => new Szybowiec("Szybowiec " + id.ToString()),
            };
            return statek;
		}

		private bool CzyZajete(Punkt p) // Sprawdza czy w danym punkcie znajduje sie jakis samolot lub budynek
		{
			foreach (ObiektyRuchome sp in statki_powietrzne)
			{
				Punkt akt_poz = sp.aktualna_pozycja;
				if (p.Odleglosc(akt_poz) <= ODLEGLOSC_KOLIZJI)
				{
					return true;
				}
			}
			foreach (ObiektyStale os in obiekty_stale)
			{
				if (os.CzyZawieraPunkt(p)) return true;
			}
			return false;
		}

		private bool CzyDolecialDoKoncaOdcinka(Punkt v, Punkt v2)
        {
			return (v.x * v2.x < 0) || (v.y * v2.y < 0); // gdy samolot dolatuje do konca odcinka skladowe jego predkosci zmieniaja sie na przeciwne
		}

		public void SymulujRuch(double krok) //Symulacja ruchu, czyli obliczenie nowych wspolrzednych wszystkich obiektow ruchomych
		{
			for (int i=0;i<statki_powietrzne.Count;++i)
			{
				ObiektyRuchome sp = statki_powietrzne[i];
				if (sp.czy_skonczyl_lot == true) continue;
				Punkt v_stara = sp.SkladowePredkosci();
				sp.WykonajRuch(krok);
				Punkt v_nowa = sp.SkladowePredkosci();
                if (CzyDolecialDoKoncaOdcinka(v_stara, v_nowa))
                {
					sp.PrzejdzDoNastepnegoOdcinka();
				}
			}
		}
	}
}
