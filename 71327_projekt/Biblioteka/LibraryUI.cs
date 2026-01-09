using Biblioteka.Models;

namespace Biblioteka
{
    public class LibraryUI
    {
        Book bookHandler = new Book();
        Reader readerHandler = new Reader();
        Borrowing borrowingHandler = new Borrowing();
        Penalties penaltiesHandler = new Penalties();

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== BIBLIOTEKA =====");
                Console.WriteLine();
                Console.WriteLine("1. Wypożycz książkę ");
                Console.WriteLine("2. Zwróć książkę ");
                Console.WriteLine("3. Sprawdź, czy książka jest dostępna");
                Console.WriteLine("4. Aktualnie wypożyczone książki przez czytelnika");
                Console.WriteLine("5. Dodaj nowego czytelnika ");
                Console.WriteLine("6. Usuń czytelnika ");
                Console.WriteLine("7. Dodaj książkę ");
                Console.WriteLine("8. Usuń książkę ");
                Console.WriteLine("9. Kary ");
                Console.WriteLine("0. Wyjście");
                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                string? option = Console.ReadLine();

                try
                {
                    switch (option)
                    {

                        case "1": borrowingHandler.BorrowBook(); break;
                        case "2": borrowingHandler.ReturnBook(); break;
                        case "3": bookHandler.isAvailableBook(); break;
                        case "4": borrowingHandler.ShowBorrowings(); break;
                        case "5": readerHandler.AddReader(); break;
                        case "6": readerHandler.DeleteReader(); break;
                        case "7": bookHandler.AddBook(); break;
                        case "8": bookHandler.DeleteBook(); break;
                        case "9": penaltiesHandler.ShowPenalties(); break;

                        case "0": return;
                        default: Console.WriteLine("Nieznana opcja!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }

                Console.WriteLine("\nWciśnij ENTER aby kontynuować...");
                Console.ReadLine();
            }
        }
    }
}
