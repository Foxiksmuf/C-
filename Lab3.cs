using System;
using System.Collections.Generic;
using System.Linq;

class Samochód
{
    public int Rocznik { get; set; }
    public string Producent { get; set; }
    public bool Przegląd { get; set; }
    public string Nr { get; set; }

    public Samochód() { }

    public Samochód(int rocznik, string producent, bool przegląd, string nr)
    {
        Rocznik = rocznik;
        Producent = producent;
        Przegląd = przegląd;
        Nr = nr;
    }

    public virtual void WypiszDane()
    {
        Console.WriteLine($"Nr: {Nr}, Rocznik: {Rocznik}, Producent: {Producent}, Przegląd: {(Przegląd ? "Tak" : "Nie")}");
    }
}

class Osobowy : Samochód
{
    public int LiczbaDrzwi { get; set; }

    public Osobowy() { }

    public Osobowy(int rocznik, string producent, bool przegląd, string nr, int liczbaDrzwi)
        : base(rocznik, producent, przegląd, nr)
    {
        LiczbaDrzwi = liczbaDrzwi;
    }

    public override void WypiszDane()
    {
        base.WypiszDane();
        Console.WriteLine($"Liczba drzwi: {LiczbaDrzwi}");
    }
}

class Ciężarówka : Samochód
{
    public int Ładowność { get; set; }

    public Ciężarówka() { }

    public Ciężarówka(int rocznik, string producent, bool przegląd, string nr, int ładowność)
        : base(rocznik, producent, przegląd, nr)
    {
        Ładowność = ładowność;
    }

    public override void WypiszDane()
    {
        base.WypiszDane();
        Console.WriteLine($"Ładowność: {Ładowność}");
    }
}

class EwidencjaS
{
    private List<Samochód> pojazdy = new List<Samochód>();

    public void Wstaw(Samochód s)
    {
        pojazdy.Add(s);
    }

    public void Info()
    {
        foreach (var pojazd in pojazdy)
        {
            pojazd.WypiszDane();
        }
    }

    public void Pokaz(int n)
    {
        var ciężarówki = pojazdy.OfType<Ciężarówka>().Where(c => c.Przegląd && c.Ładowność >= n);
        if (ciężarówki.Any())
        {
            Console.WriteLine($"Ciężarówki z ważnym przeglądem i ładownością co najmniej {n}:");
            foreach (var ciężarówka in ciężarówki)
            {
                ciężarówka.WypiszDane();
            }
        }
        else
        {
            Console.WriteLine("Brak ciężarówek spełniających kryteria.");
        }
    }

    public void Znajdź(int ładunek)
    {
        var ciężarówki = pojazdy.OfType<Ciężarówka>().Where(c => c.Przegląd && c.Ładowność > ładunek);
        if (ciężarówki.Any())
        {
            Console.WriteLine($"Ciężarówki z przeglądem i ładownością przekraczającą {ładunek}:");
            foreach (var ciężarówka in ciężarówki)
            {
                ciężarówka.WypiszDane();
            }
        }
        else
        {
            Console.WriteLine($"Brak ciężarówek spełniających kryteria.");
        }
    }

    public void ZnajdźMin(int ładunek)
    {
        var ciężarówki = pojazdy.OfType<Ciężarówka>().Where(c => c.Przegląd && c.Ładowność > ładunek).OrderBy(c => c.Ładowność);
        if (ciężarówki.Any())
        {
            int suma = 0;
            foreach (var ciężarówka in ciężarówki)
            {
                suma += ciężarówka.Ładowność;
                if (suma >= ładunek)
                {
                    Console.WriteLine($"Minimalna suma ładowności ciężarówek przekraczających {ładunek}: {suma}");
                    Console.WriteLine($"Ciężarówki o przekraczającej ładowności ({ładunek}):");
                    ciężarówka.WypiszDane();
                    return;
                }
            }
            Console.WriteLine("Nie znaleziono ciężarówek spełniających kryteria.");
        }
        else
        {
            Console.WriteLine("Brak ciężarówek spełniających kryteria.");
        }
    }
}

interface OrdQueue<T>
{
    void Enqueue(T a); 
    void Dequeue(); 
    bool Empty();
    void MakeEmpty(); 
}

class OQueue<T> : OrdQueue<T>
{
    public List<T> Q;

    public OQueue() { Q = new List<T>(); }

    public void Enqueue(T a) { Q.Add(a); }

    public void Dequeue()
    {
        if (Q.Count > 0)
            Q.RemoveAt(0);
    }

    public bool Empty() { return Q.Count == 0; }

    public void MakeEmpty() { Q.Clear(); }
}

class Program
{
    static void Main(string[] args)
    {
        EwidencjaS ewidencja = new EwidencjaS();
        ewidencja.Wstaw(new Osobowy(2020, "Toyota", true, "ABC123", 4000));
        ewidencja.Wstaw(new Ciężarówka(2018, "Volvo", true, "XYZ456", 6000));
        ewidencja.Wstaw(new Ciężarówka(2019, "MAN", false, "DEF789", 7000));

        Console.WriteLine("Wszystkie pojazdy:");
        ewidencja.Info();

        Console.WriteLine("\nCiężarówki z ważnym przeglądem i ładownością co najmniej 6000:");
        ewidencja.Pokaz(6000);

        Console.WriteLine("\nCiężarówki z przeglądem i ładownością przekraczającą 5000:");
        ewidencja.Znajdź(5000);

        Console.WriteLine("\nMinimalna suma ładowności ciężarówek przekraczających 6000:");
        ewidencja.ZnajdźMin(6000);

        OQueue<int> kolejka = new OQueue<int>();
        kolejka.Enqueue(1);
        kolejka.Enqueue(2);
        kolejka.Enqueue(3);

        while (!kolejka.Empty())
        {
            Console.WriteLine(kolejka.Q[0]);
            kolejka.Dequeue();
        }
    }
}