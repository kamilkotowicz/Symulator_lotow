using System;
namespace Symulator_lotow
{
	public class Symulator
	{
		private List<Obiekty_stale> obiekty_stale = new List<Obiekty_stale>();
		private List<Statek_powietrzny> statki_powietrzne = new List<Statek_powietrzny>();
		public Symulator()
		{
		}
		public void wczytaj_z_pliku(string sciezka)
        {
			//Funkcja powinna wczytywac z pliku do list obiekty_stale i statki_powietrzne
			//Jesli plik nie istnieje powinien byc zwrocony wyjatek.
		}
		public void SymulujRuch()
        {
			// Symuluje ruch obiektow w symulatorze
			double krok = 10; // trzeba przetestowac jaka wartosc bedzie sensowna
			foreach (Statek_powietrzny sp in statki_powietrzne)
			{
				sp.aktualna_pozycja.x += krok * sp.trasa.skladowe_predkosci().x;
				sp.aktualna_pozycja.y += krok * sp.trasa.skladowe_predkosci().y;
			}
		}


	}
}
