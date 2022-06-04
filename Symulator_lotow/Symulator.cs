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
		private static readonly Random rand = new Random();
		public const int MAXX = 1800, MAXY = 1000;
		public const double ODLEGLOSC_KOLIZJI = 10;
		public const double ODLEGLOSC_NIEBEZPIECZNA = 50;
		public const int ILE_RODZAJOW_SAMOLOTOW = 5;
		public const int ILE_SAMOLOTOW = 10;
		public Symulator()
		{
		}

		public void SprobujWczytacPlik(string sciezka)
		{
			try
			{
				WczytajPlik(sciezka);
			}
			catch (FileNotFoundException e)
			{
				Debug.WriteLine(e);
				Environment.Exit(0);
			}
			catch (BadDataInFileException e)
			{
				Debug.WriteLine(e);
				Environment.Exit(0);
			}
			catch (Exception)
			{
				Debug.WriteLine("Wystapil nieznany blad");
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

		private void WykryjBliskieObiektyRuchome()
        {
			for (int i = 0; i < statki_powietrzne.Count; ++i) //uzycie petli for zamiast foreach  aby nie dodawac 2 razy tej samej kolizji
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
					/*if (os.CzyZawieraPunkt(sp.aktualna_pozycja) == true)
					{
						wykryte_kolizje.Add(new Kolizja(sp, os, 0));
					}*/
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
		public void WykryjKolizje()
        {		
			wykryte_zblizenia.Clear();
			wykryte_kolizje.Clear();
			WykryjBliskieObiektyRuchome();
			WykryjBliskieObiektyStale();
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

		public void GenerujStatkiPowietrzne()
        {
			for(int i = 0; i < ILE_SAMOLOTOW; i++)
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
				if (os.CzyZawieraPunkt(p)) return true;
			}
			return false;
		}

		private bool CzyDolecialDoKoncaOdcinka(Punkt v, Punkt v2)
        {
			return (v.x * v2.x < 0) || (v.y * v2.y < 0); // gdy samolot dolatuje do konca odcinka skladowe jego predkosci zmieniaja sie na przeciwne
		}

		public void SymulujRuch(double krok)
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
