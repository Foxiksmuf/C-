using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dwudzielny
{
    abstract class Graf<T>
    {
        public List<T> V; //lista wszystkich wierzchołków grafu
        public abstract List<T> sasiedzi(T v); //obliczenie listy sąsiadów wierzchołka v
        public abstract void zaznacz(T v); //zaznaczenie wierzchołka v jako odwiedzony 
        public abstract bool zaznaczony(T v); //sprawdzenie, czy wierzchołek v jest zaznaczony
        public abstract void register(T v); // zarejestrowanie wierzchołka v do procesu wyświetlania v
        // względnie zapisania v do jakiejś specjalnej kolekcji
        public abstract T select(); // wybranie jakiegoś wierzchołka grafu 
        public abstract void usun_zaznaczenia(); //usuwanie zaznaczeń wierzchołków grafu 
        public abstract void wyslij_komunikat(string s);
        public abstract bool equal(T v, T w);
        public void DFS(T v)
        {
            foreach (T w in sasiedzi(v))
            {
                if (!zaznaczony(w))
                {
                    register(w);
                    zaznacz(w);
                    DFS(w);
                }
            }
        }
        public void spojne_skladowe()
        {
            usun_zaznaczenia();
            foreach (T v in V)
            {
                if (!zaznaczony(v))
                {
                    wyslij_komunikat("Kolejna spojna skladowa");
                    register(v);
                    zaznacz(v);
                    DFS(v);
                }
            }
            wyslij_komunikat("Koniec obliczania spójnych składowych");
        }
        public void obejscie_DFS()
        {//dla grafu spojnego
            wyslij_komunikat("Obejście DFS");
            usun_zaznaczenia();
            T v = select();
            register(v);
            zaznacz(v);
            DFS(v);
            wyslij_komunikat("Koniec obejscia DFS");
        }
    }

    //-----------------------------------------------------------

    abstract class SGraf<T> : Graf<T>
    {//implementacja na tablicy list sąsiedztwa i tablicy zaznaczeń
        //nadal abstrahujemy od typu wierzchołków grafu
        public int index(T v)
        {//przyporządkowanie wierzchołkowi v jego indeksu na liście V wierzchołków
            for (int i = 0; i < V.Count(); i++)
                if (equal(v, V[i])) return i + 1;
            return -1;
        }
        public List<T>[] S;
        public bool[] marked;
        public Data_Source<T> DS;
        public override List<T> sasiedzi(T v)
        {
            return S[index(v)];
        }
        public override void zaznacz(T v)
        {
            marked[index(v)] = true;
        }
        public override bool zaznaczony(T v)
        {
            return marked[index(v)];
        }
        public override T select()
        {
            return V[0];
        }
        public override void usun_zaznaczenia()
        {
            for (int i = 1; i < V.Count(); i++) marked[i] = false;
        }
        public override void wyslij_komunikat(string s)
        {
            Console.WriteLine();
            Console.WriteLine(s);
        }
        public void wczytaj_graf()
        {
            V = new List<T>();
            wyslij_komunikat("Wprowadzanie wierzchołków");
            T v;
            while (!DS.end_of_vertexes())
            {
                //V.Add(DS.get_vertex());
                v = DS.get_vertex();
                if (!DS.end_of_vertexes()) V.Add(v);
            }
            int n = V.Count();
            S = new List<T>[n + 1];
            for (int i = 1; i <= n; i++) S[i] = new List<T>();
            marked = new bool[n + 1];
            for (int i = 1; i <= n; i++) marked[i] = false;
            wyslij_komunikat("Wprowadzanie krawędzi");
            Edge<T> e;
            T a, b;
            while (!DS.end_of_edges())
            {
                e = DS.get_edge();
                if (!DS.end_of_edges())
                {
                    a = e.first; b = e.second;
                    S[index(a)].Add(b); S[index(b)].Add(a);
                }
            }
        }
    }

    class Edge<X>
    {
        public X first;
        public X second;
        public Edge(X a, X b) { first = a; second = b; }
    }

    interface Data_Source<X>
    {
        bool end_of_vertexes();
        bool end_of_edges();
        X get_vertex();
        Edge<X> get_edge();
    }

    //-----------------------------------------------------------

    enum Kolor { bialy, czarny, neutralny }

    abstract class BGraf<T> : SGraf<T>
    {
        public bool przerwanie;
        public Kolor[] K;
        public Kolor dualny(Kolor k)
        {
            if (k == Kolor.bialy) return Kolor.czarny;
            if (k == Kolor.czarny) return Kolor.bialy;
            if (k == Kolor.neutralny) return Kolor.neutralny;
            return Kolor.neutralny;
        }
        public void BDFS(T v)
        {
            if (!przerwanie)
            {
                foreach (T w in sasiedzi(v))
                {
                    if (!przerwanie) //zabezpieczenie przed wykonywaniem dlaszych akcji pętli foreach,
                                     //gdy dla wcześniejszego w było już przerwanie
                    {
                        if (K[index(w)] == Kolor.neutralny) { K[index(w)] = dualny(K[index(v)]); BDFS(w); }
                        else
                        {
                            if (K[index(w)] == K[index(v)])//konflikt kolorów
                            {
                                wyslij_komunikat("Graf nie jest dwudzielny");
                                wyslij_komunikat("Konflikt kolorów na wierzchołkach ");
                                register(v); register(w);
                                przerwanie = true;
                            }
                        }
                    }
                }
            }
        }
        public void bipartite_test()
        {
            int n = V.Count();
            K = new Kolor[n + 1];
            for (int i = 1; i <= n; i++) K[i] = Kolor.neutralny;
            T v = select();
            przerwanie = false;
            K[index(v)] = Kolor.bialy;
            BDFS(v);
            if (!przerwanie)
            {
                wyslij_komunikat("Graf jest dwudzielny");
                List<T> LB = new List<T>();
                List<T> LC = new List<T>();
                foreach (T w in V)
                {
                    if (K[index(w)] == Kolor.bialy) LB.Add(w);
                    else LC.Add(w);
                }
                wyslij_komunikat("Wierzchołki białe");
                foreach (T w in LB) register(w);
                wyslij_komunikat("Wierzchołki czarne");
                foreach (T w in LC) register(w);
            }
        }
    }

    //------------------------------------------------------------

    class IntBGraf : BGraf<int>
    {
        public override bool equal(int a, int b) { return a == b; }
        public override void register(int v)
        {
            Console.Write(v + " ");
        }
        public IntBGraf(Data_Source<int> IDS)
        {
            DS = IDS;
            wczytaj_graf();
        }
    }

    class IntDS : Data_Source<int>
    {
        int a;
        int x;
        int y;
        public bool end_of_vertexes()
        {
            return a == 0;
        }
        public bool end_of_edges()
        {
            return x == 0;
        }
        public int get_vertex()
        {
            a = Convert.ToInt16(Console.ReadLine());
            return a;
        }
        public Edge<int> get_edge()
        {
            x = Convert.ToInt16(Console.ReadLine());
            y = Convert.ToInt16(Console.ReadLine());
            return new Edge<int>(x, y);
        }

        public IntDS()
        {
            //Console.WriteLine("Pierwszy wierzchołek");
            //a = Convert.ToInt16(Console.ReadLine());
            a = 1;
            x = 1;
            y = 1;
        }
    }

    public record Punkt(int x, int y);

    class PunktGraf : SGraf<Punkt>
    {
        public List<Punkt> Punkty { get; private set; }

        public override bool equal(Punkt v, Punkt w) => throw new NotImplementedException();

        public override void register(Punkt v)
        {
            Punkty ??= new List<Punkt>();
            Punkty.Add(v);
        }

        public double srednica()
        {
            if (Punkty == null || Punkty.Count < 2)
                return 0;

            double max_odleglosc = 0;

            for (int i = 0; i < Punkty.Count - 1; i++)
            {
                for (int j = i + 1; j < Punkty.Count; j++)
                {
                    int deltaX = Punkty[i].x - Punkty[j].x;
                    int deltaY = Punkty[i].y - Punkty[j].y;
                    double odleglosc = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                    if (odleglosc > max_odleglosc)
                    {
                        max_odleglosc = odleglosc;
                    }
                }
            }

            return max_odleglosc;
        }

        class Program
        {
            static void Main(string[] args)
            {
                Punkt p1 = new Punkt(0, 0);
                Punkt p2 = new Punkt(3, 4);
                Punkt p3 = new Punkt(7, 1);

                PunktGraf punktGraf = new PunktGraf();
                punktGraf.register(p1);
                punktGraf.register(p2);
                punktGraf.register(p3);

                double srednica = punktGraf.srednica();
                Console.WriteLine("Srednica: " + srednica);

                Console.WriteLine("Koniec");
                Console.ReadLine();
            }
        }
    }
}