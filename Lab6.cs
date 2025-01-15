using System;
public class MyTime
{
    private int hour;
    private int minute;
    private int second;
    public MyTime()
    {
        // Konstruktor domyślny, inicjalizuje do 00:00:00
        hour = 0;
        minute = 0;
        second = 0;
    }
    public MyTime(int hour, int minute, int second)
    {
        // Konstruktor z parametrami, sprawdza poprawność danych
        SetTime(hour, minute, second);
    }
    public int GetHour()
    {
        return hour;
    }
    public int GetMinute()
    {
        return minute;
    }
    public int GetSecond()
    {
        return second;
    }
    public void SetTime(int hour, int minute, int second)
    {
        // Ustawianie czasu z poprawnością
        SetHour(hour);
        SetMinute(minute);
        SetSecond(second);
    }
    public void SetHour(int hour)
    {
        // Sprawdzanie i ustawianie godziny
        if (hour >= 0 && hour <= 23)
        {
            this.hour = hour;
        }
        else
        {
            Console.WriteLine("Błędna wartość godziny.");
        }
    }
    public void SetMinute(int minute)
    {
        // Sprawdzanie i ustawianie minut
        if (minute >= 0 && minute <= 59)
        {
            this.minute = minute;
        }
        else
        {
            Console.WriteLine("Błędna wartość minut.");
        }
    }
    public void SetSecond(int second)
    {
        // Sprawdzanie i ustawianie sekund
        if (second >= 0 && second <= 59)
        {
            this.second = second;
        }
        else
        {
            Console.WriteLine("Błędna wartość sekund.");
        }
    }
    public override string ToString()
    {
        // Formatowanie do "HH:MM:SS" z zerami wiodącymi
        return $"{hour:D2}:{minute:D2}:{second:D2}";
    }
    public MyTime NextSecond()
    {
        // Przejście do następnej sekundy
        second++;
        if (second == 60)
        {
            second = 0;
            NextMinute();
        }
        return this;
    }
    public MyTime NextMinute()
    {
        // Przejście do następnej minuty
        minute++;
        if (minute == 60)
        {
            minute = 0;
            NextHour();
        }
        return this;
    }
    public MyTime NextHour()
    {
        // Przejście do następnej godziny
        hour++;
        if (hour == 24)
        {
            hour = 0;
        }
        return this;
    }
    public MyTime PreviousSecond()
    {
        // Przejście do poprzedniej sekundy
        second--;
        if (second == -1)
        {
            second = 59;
            PreviousMinute();
        }
        return this;
    }
    public MyTime PreviousMinute()
    {
        // Przejście do poprzedniej minuty
        minute--;
        if (minute == -1)
        {
            minute = 59;
            PreviousHour();
        }
        return this;
    }
    public MyTime PreviousHour()
    {
        // Przejście do poprzedniej godziny
        hour--;
        if (hour == -1)
        {
            hour = 23;
        }
        return this;
    }
}
class TestMyTime
{
    static void Main()
    {
        // Testowanie wszystkich metod klasy MyTime
        MyTime time = new MyTime(12, 30, 45);
        Console.WriteLine("Initial Time: " + time);
        time.NextSecond();
        Console.WriteLine("Next Second: " + time);
        time.NextMinute();
        Console.WriteLine("Next Minute: " + time);
        time.NextHour();
        Console.WriteLine("Next Hour: " + time);
        time.PreviousSecond();
        Console.WriteLine("Previous Second: " + time);
        time.PreviousMinute();
        Console.WriteLine("Previous Minute: " + time);
        time.PreviousHour();
        Console.WriteLine("Previous Hour: " + time);
    }
}