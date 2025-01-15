using System;

namespace Ulamek
{
    class Ulamek
    {
        public int l;
        public int m;

        public void pokaz()
        {
            Console.WriteLine(l + "/" + m);
        }

        public Ulamek(int a, int b)
        {
            if (b != 0)
            {
                l = a;
                m = b;
            }
            else
            {
                l = 0;
                m = 1;
            }
        }
    }

    class Arytmetyka_Ulamkow
    {
        public Ulamek dod(Ulamek u1, Ulamek u2)
        {
            int a = u1.l * u2.m + u2.l * u1.m;
            int b = u1.m * u2.m;
            Ulamek u = new Ulamek(a, b);
            return u;
        }

        public Ulamek odj(Ulamek u1, Ulamek u2)
        {
            int a = u1.l * u2.m - u2.l * u1.m;
            int b = u1.m * u2.m;
            Ulamek u = new Ulamek(a, b);
            return u;
        }

        public Ulamek mnz(Ulamek u1, Ulamek u2)
        {
            int a = u1.l * u2.l;
            int b = u1.m * u2.m;
            Ulamek u = new Ulamek(a, b);
            return u;
        }

        public Ulamek dzl(Ulamek u1, Ulamek u2)
        {
            int a = u1.l * u2.m;
            int b = u1.m * u2.l;
            Ulamek u = new Ulamek(a, b);
            return u;
        }

        public Ulamek skr(Ulamek u)
        {
            int a = nwd(Math.Abs(u.l), Math.Abs(u.m));
            int licz = u.l / a;
            int mian = u.m / a;
            return new Ulamek(licz, mian);
        }

        private int nwd(int a, int b)
        {
            int n = a;
            int m = b;
            while (n != m)
            {
                if (n > m)
                {
                    n = n - m;
                }
                else
                {
                    m = m - n;
                }
            }
            return n;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Arytmetyka_Ulamkow A = new Arytmetyka_Ulamkow();

           
            Ulamek u1 = new Ulamek(2, 3);
            Ulamek u2 = new Ulamek(3, 2);
            Ulamek u3 = new Ulamek(4, 6);
            Ulamek u4 = new Ulamek(1, 2);
            Ulamek u5 = new Ulamek(2, 8);
            Ulamek u6 = new Ulamek(1, 2);
            Ulamek u7 = new Ulamek(3, 5);

            
            Ulamek wynik = A.dzl(
                                A.dod(
                                    A.mnz(
                                        A.dod(
                                            A.dod(A.dod(A.dod(A.dod(A.dod(u1, u2), u3), u4), u5), u6),
                                            A.odj(u3, u4)
                                        ),
                                        u5
                                    ),
                                    u2
                                ),
                                A.dod(u6, u7)
                            );

      
            Console.WriteLine("Wynik: ");
            wynik.pokaz();

            Console.WriteLine("Wynik po skróceniu:");
            A.skr(wynik).pokaz();

            Console.WriteLine("Koniec");
            Console.ReadLine();
        }
    }
}