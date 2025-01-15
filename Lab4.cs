using System;
using System.Collections.Generic;

class Graf
{
    private int n;
    private List<int>[] S;
    private bool[] marked;

    public Graf(int vertices)
    {
        n = vertices;
        S = new List<int>[n + 1];
        marked = new bool[n + 1];
        for (int i = 1; i <= n; i++)
        {
            S[i] = new List<int>();
        }
    }

    public void DodajKrawedz(int v, int w)
    {
        S[v].Add(w);
        S[w].Add(v); 
    }

    public List<int> Sasiadow(int v)
    {
        return S[v];
    }

    public bool Zaznaczony(int v)
    {
        return marked[v];
    }

    public void Zaznacz(int v)
    {
        marked[v] = true;
    }

    public void DFS(int v)
    {
        if (Zaznaczony(v))
            return;

        Zaznacz(v);
        Console.WriteLine(v); 

        foreach (int w in Sasiadow(v))
        {
            if (!Zaznaczony(w))
            {
                DFS(w);
            }
        }
    }

    public void UsunZaznaczenia()
    {
        Array.Clear(marked, 1, n);
    }

    public bool Zad6()
    {
  
        int liczbaSpójnychSkładowych = 0;
        for (int i = 1; i <= n; i++)
        {
            if (!Zaznaczony(i))
            {
                liczbaSpójnychSkładowych++;
                if (liczbaSpójnychSkładowych > 1)
                {
                    Console.WriteLine("Graf ma więcej niż jedną spójną składową.");
                    return false;
                }
                DFS(i);
            }
        }

        UsunZaznaczenia(); 

       
        for (int i = 1; i <= n; i++)
        {
            if (S[i].Count % 2 != 0)
            {
                Console.WriteLine($"Stopień wierzchołka {i} nie jest parzysty.");
                return false;
            }
        }

        Console.WriteLine("Graf ma tylko jedną spójną składową i wszystkie wierzchołki mają parzyste stopnie.");
        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Graf graf = new Graf(5);
        graf.DodajKrawedz(1, 2);
        graf.DodajKrawedz(1, 5);
        graf.DodajKrawedz(2, 3);
        graf.DodajKrawedz(2, 4);
        graf.DodajKrawedz(3, 4);

        bool czyGrafOk = graf.Zad6();

        if (czyGrafOk)
        {
            Console.WriteLine("Graf spełnia warunki zadania.");
        }
        else
        {
            Console.WriteLine("Graf nie spełnia warunków zadania.");
        }
    }
}