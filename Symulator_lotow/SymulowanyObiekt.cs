namespace Symulator_lotow
{
    //Wspolna klasa dla obiektow stalych i ruchomych
    public abstract class SymulowanyObiekt
    {
        public abstract bool CzyZawieraPunkt(Punkt p);

        public string? nazwa;
    }
}