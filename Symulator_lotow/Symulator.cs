using System;
using System.Diagnostics;

namespace Symulator_lotow
{
	public class Symulator
	{
		internal List<ObiektyStale> obiekty_stale = new List<ObiektyStale>();
		internal List<ObiektyRuchome> statki_powietrzne = new List<ObiektyRuchome>();
		public List<NiebezpieczneZblizenie> wykryte_zblizenia = new List<NiebezpieczneZblizenie>();
		public List<Kolizja> wykryte_kolizje = new List<Kolizja>();
		private const int MAXX = 1000, MAXY = 1000;
		public Symulator()
		{
		}
		public void WczytajPlik(string sciezka)
        {
			obiekty_stale.Clear();
			try
			{
				string[] linie = File.ReadAllLines(sciezka);
				string[] dane;
				int id = 0;
				foreach (string s in linie)
				{
					dane = linie[id].Split(",");
					int ile_danych = dane.Length;
					string typ = dane[0];
					int x = Convert.ToInt32(dane[1]);
					int y = Convert.ToInt32(dane[2]);
					int z = Convert.ToInt32(dane[ile_danych - 1]);
					int r, a, b;
					if (dane[0] == "D")
					{
						r = Convert.ToInt32(dane[3]);
						obiekty_stale.Add(new Drzewo(new Punkt(x, y, z / 2), r, z, "Drzewo " + id.ToString()));
					}
					else if (dane[0] == "K")
					{
						r = Convert.ToInt32(dane[3]);
						obiekty_stale.Add(new Komin(new Punkt(x, y, z / 2), r, z, "Komin " + id.ToString()));
					}
					else if (dane[0] == "W") { 
						a = Convert.ToInt32(dane[3]);
						obiekty_stale.Add(new Wiezowiec(new Punkt(x, y, z / 2), a, z, "Wiezowiec " + id.ToString()));
					}
					else if (dane[0] == "B")
					{
						a = Convert.ToInt32(dane[3]);
						b = Convert.ToInt32(dane[4]);
						obiekty_stale.Add(new Blok(new Punkt(x, y, z / 2), a, b, z, "Blok " + id.ToString()));
					}					
					++id;
				}
			}
			catch (FileNotFoundException e)
			{
				Debug.WriteLine(e+": Nie znaleziono pliku!!");
			}
		}

		public void WykryjKolizje()
        {
			const double ODLEGLOSC_KOLIZJI = 25;
			const double ODLEGLOSC_NIEBEZPIECZNA = 100;
			wykryte_zblizenia.Clear();
			wykryte_kolizje.Clear();
			for (int i = 0; i < statki_powietrzne.Count; ++i) //uzycie petli for zamiast foreach w celu wydajnosciowym - aby nie dodawac 2 razy tej samej kolizji
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

			for (int i = 0; i < statki_powietrzne.Count; ++i)
			{
				ObiektyRuchome sp = statki_powietrzne[i];
				foreach (ObiektyStale os in obiekty_stale)
				{
					if (os.CzyZawieraPunkt(sp.aktualna_pozycja) == true)
					{
						wykryte_kolizje.Add(new Kolizja(sp, os, 0));
					}
				}
			}
		}

		private ObiektyRuchome StatekLosowegoTypu(int id)
        {
			const int ILE_RODZAJOW = 5;
			Random rand = new Random();
			int rodzaj = rand.Next(0, ILE_RODZAJOW);
			ObiektyRuchome statek;
			switch (rodzaj)
			{
				case 0:
					statek = new Dron("Dron " + id.ToString());
					break;
				case 1:
					statek = new Samolot("Samolot " + id.ToString());
					break;
				case 2:
					statek = new Smiglowiec("Smiglowiec " + id.ToString());
					break;
				case 3:
					statek = new Balon("Balon " + id.ToString());
					break;
				default:
					statek = new Szybowiec("Szybowiec " + id.ToString());
					break;
			}
			return statek;
		}

		public void GenerujStatkiPowietrzne()
        {
			Random rand = new Random();
			const int ILE_STATKOW = 10;
			for(int i= 0; i < ILE_STATKOW; i++)
            {
				ObiektyRuchome nowy_statek = StatekLosowegoTypu(i);
				do
				{
					nowy_statek.UstawNaLosowaPozycje(MAXX, MAXY);
				}
				while (CzyZajete(nowy_statek.aktualna_pozycja));
				do
				{
					nowy_statek.UstawTraseLosowo(MAXX, MAXY);
				}
				while (CzyZajete(nowy_statek.trasa.KoniecAktualnegoOdcinka()));
				nowy_statek.aktualna_pozycja.z = nowy_statek.trasa.KoniecAktualnegoOdcinka().z;
				statki_powietrzne.Add(nowy_statek);
			}
        }
		public void SymulujRuch(double krok)
		{
			foreach (ObiektyRuchome sp in statki_powietrzne)
			{
				Punkt v = sp.skladowe_predkosci();
				sp.aktualna_pozycja.x += krok * v.x;
				sp.aktualna_pozycja.y += krok * v.y;
				Punkt v2 = sp.skladowe_predkosci();
                if ((v.x * v2.x < 0) || (v.y * v2.y < 0)) // gdy samolot doleci do konca odcinka zmienilby swoja predkosc na przeciwna
                {
					sp.aktualna_pozycja = new Punkt(sp.trasa.KoniecAktualnegoOdcinka());
					++sp.trasa.nr_aktualnego_odcinka;
					sp.aktualna_pozycja.z = sp.trasa.KoniecAktualnegoOdcinka().z;
					if(sp.trasa.nr_aktualnego_odcinka < sp.trasa.odcinki.Count())
                    {
						sp.aktualna_pozycja.z = sp.trasa.KoniecAktualnegoOdcinka().z;
					}
                    else
                    {
						sp.aktualna_pozycja.z = 0;
						sp.czy_skonczyl_lot = true;
                    }
				}
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
				if(os.CzyZawieraPunkt(p)) return true;
			}
			return false;
		}


	}
}
