# Symulator_lotow

Projekt stworzony w 2022 roku w ramach przedmiotu obieralnego na studiach "Programowanie obiektowe".


<h2>Treść zadania</h2>

Program do "kontroli lotu". Dysponujemy radarem, na którym wyświetlana jest mapa (wczytana z pliku) z obiektami nieporuszającymi się, zaś na radarze możemy umieścić różne rodzaje statków powietrznych reprezentowane przez odpowiednie znaki graficzne. Każdy statek powietrzny ma swoją trasę (złożoną z odcinków o stałych parametrach lotu: wysokość, kierunek, prędkość). Program powinien cyklicznie sprawdzać i sygnalizować niebezpieczne zbliżenia oraz kolizje między statkami. Program powinien umożliwiać modyfikowanie tras lotów statków powietrznych.
Minimalny zakres funkcjonalności:

a)	wczytywanie i wyświetlanie mapy (może być w formie tektowej) wraz z obiektami (naziemnymi i ruchomymi),

b)	Losowe generowanie statków powietrznych oraz ich tras,

c)	symulacja ruchu statków powietrznych

d)	wydawanie poleceń zmiany trasy,

e)	wykrywanie i sygnalizowanie niebezpiecznych zbliżeń i kolizji między obiektami.

<h2>Sposób rozwiązania</h2>
<ul>
<li>Do wizualizacji została wykorzystana biblioteka <b>System.Drawing</b>.</li>
<li>Statki powietrzne są trójkątami, drzewa i kominy są kołami, bloki są prostokątami, a wieżowce - kwadratami.</li>
<li> Różne rodzaje statków powierznych są oznaczane różnymi kolorami. Każdy rodzaj statku powietrznego ma swój własny zakres wysokości i prędkości, na którym odbywa swoją trasę.
<li> Statki powietrzne oraz ich trasy sa generowane losowo, natomiast obiekty stałe są wczytywane z pliku. </li>
<li> W przypadku zbliżenia między dwoma statkami powietrznymi lub między statkiem powietrznym, a obiektem stałym wyświetla się okienko z odpowiednim komunikatem, które pozwala zmienić trasę na losową.
</ul>
