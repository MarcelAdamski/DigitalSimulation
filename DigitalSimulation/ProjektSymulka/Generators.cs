using System;

namespace ProjektSymulka
{
    class Generators
    {
        const int Q = 127773; //const from invocom
        const int R = 2836; //const from invocom
        const int Range = 2147483647; //2^31-1
        public static int[] Seeds = new int[10];

        public static double Uniform(ref int x) // from 0 to 1
        {
            int h = (x / Q);
            x = 16807 * (x - Q * h) - R * h;
            if (x < 0) x += Range;
            double value = x / ((double)Range);
            return value;
        }

        public static double Exponential(double lambda, ref int x)
        {
            lambda = 1 / lambda;
            double value = -Math.Log(Uniform(ref x)) / lambda;
            return value;
        }

        public static double Normal(ref int x, ref int x1,double mean, double var)
        {
            double s;

            if (Uniform(ref x) > 0.50) s = 1.0; //sign - or +
            else s = -1.0;

            double r = Uniform(ref x1);
            return s * ((Math.Sqrt(var)) * Math.Sqrt((Math.PI) / 8)) * Math.Log((1 + r) / (1 - r)) + mean;
        }
    }
}
