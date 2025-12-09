using ConsoleApp1;

// ========= Main ===========================
double objetosc = 0;
Pudelko p1 = new Pudelko();
Pudelko p2 = new Pudelko();
Pudelko p3 = new Pudelko();

// specyfikacja 1
p1.PobierzDlugosc(3.5);
p1.PobierzSzerokosc(4.0);
p1.PobierzWysokosc(5.5);

// specyfikacja 2
p2.PobierzDlugosc(2.5);
p2.PobierzSzerokosc(5.0);
p2.PobierzWysokosc(4.5);

// specyfikacja 3
p3.PobierzDlugosc(12.5);
p3.PobierzSzerokosc(15.0);
p3.PobierzWysokosc(14.5);

// Wyswietlenie danych wewnatrz kolejnych obiektow
Console.WriteLine("Pudelko 1: {0}", p1.ToString());
Console.WriteLine("Pudelko 2: {0}", p2.ToString());
Console.WriteLine("Pudelko 3: {0}", p3.ToString());

// objetosc 1
objetosc = p1.ObliczObjetosc();
Console.WriteLine("Objetosc 1: {0}", objetosc);

// objetosc 2
objetosc = p2.ObliczObjetosc();
Console.WriteLine("Objetosc 2: {0}", objetosc);

// Dodanie 2 obiektów
p3 = p1 + p2;

// objetosc 3
objetosc = p3.ObliczObjetosc();
Console.WriteLine("Objetosc 3: {0}", objetosc);

// porównanie obiektów
if (p1 == p2)
    Console.WriteLine("Pudełka p1 oraz p2 są identyczne");
if (p1 != p2)
    Console.WriteLine("Pudełka p1 oraz p2 są różne");

Console.ReadKey();
