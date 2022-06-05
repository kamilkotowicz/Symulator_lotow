namespace Symulator_lotow
{
    public class Punkt
    {
        public double x, y, z;

        public Punkt(double xx, double yy, double zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }
        public Punkt(Punkt p)
        {
            this.x = p.x;
            this.y = p.y;
            this.z = p.z;
        }
        public double Odleglosc(Punkt p) //odleglosc miedzy dwoma punktami
        {
            double dx=x-p.x;
            double dy=y-p.y;
            double dz=z-p.z;
            return Math.Sqrt(dx*dx+dy*dy+dz*dz);
        }
    }

}
