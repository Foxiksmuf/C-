using System;
using System.Collections.Generic;

namespace GrafProsty
{
    class Graf
    {
        public int n;
        public bool[] marked;
        public List<int>[] S;

        public List<int> sasiedzi(int k) { return S[k]; }
        public void zaznacz(int k) { marked[k] = true; }
        public void usun_zaznaczenia()
        {
            for (int i = 1; i <= n; i++) marked[i] = false;
        }
        public bool zaznaczony(int k) { return marked[k]; }
        public void DFS(int k)
        {
            zaznacz(k); 

            List<int> L = S[k];
            foreach (int w in L)
            {
                if (!zaznaczony(w))
                {
                    DFS(w); 
                }
            }
        }
        public void wczytaj_graf()
        {
            Console.Write("Podaj liczbe wierzcholkow grafu n=");
            n = Convert.ToInt32(Console.ReadLine());
            marked = new bool[n + 1];
            S = new List<int>[n + 1];
            for (int i = 1; i <= n; i++) S[i] = new List<int>();

            Console.WriteLine("Podaj krawędzie grafu, koniec zaznacz jako 0 0");

            int a = Convert.ToInt32(Console.ReadLine());
            int b = Convert.ToInt32(Console.ReadLine());

            while (a != 0 && b != 0)
            {
                S[a].Add(b);
                S[b].Add(a);
                a = Convert.ToInt32(Console.ReadLine());
                b = Convert.ToInt32(Console.ReadLine());
            }
        }

        public int SprawdzZrodlo()
        {
            bool[] czyMaKrawedz = new bool[n + 1];

           
            for (int i = 1; i <= n; i++)
            {
                foreach (int sasiad in S[i])
                {
                    czyMaKrawedz[sasiad] = true;
                }
            }

            for (int i = 1; i <= n; i++)
            {
                if (!czyMaKrawedz[i])
                {
                    return i; 
                }
            }

            return -1; 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Graf G = new Graf();
            G.wczytaj_graf();

            int zrodlo = G.SprawdzZrodlo();
            if (zrodlo != -1)
            {
                Console.WriteLine("Znaleziono źródło wierzchołek: " + zrodlo);
            }
            else
            {
                Console.WriteLine("Brak źródła.");
            }

            Console.ReadLine();
        }
    }
}