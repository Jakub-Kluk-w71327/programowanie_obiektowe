using System.Text.Json;
using Biblioteka.Interfaces;

namespace Biblioteka.Models
{
    internal class Book : IBook
    {
        //properties
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int Year { get; set; }
        public bool isAvailable { get; set; }

        //methods
        public void AddBook()
        {
            List<Book> books = LoadBooks();

            int Id = getLastIdBook() + 1;
            Console.Write($"Id tej książki to: {Id}\n");

            Console.Write($"Podaj Tytuł: ");
            string? Title = Console.ReadLine();

            Console.Write($"Podaj Autora: ");
            string? Author = Console.ReadLine();

            Console.Write($"Podaj rok wydania książki: ");
            int Year;
            while (!int.TryParse(Console.ReadLine(), out Year))
            {
                Console.WriteLine("Podaj poprawną wartość");
            }

            Book book = new Book(Id, Title, Author, Year, true);

            books.Add(book);

            SaveBooks(books);

            Console.WriteLine();
            Console.WriteLine("Pomyślnie dodano nową książkę");     
        }

        public void DeleteBook()
        {
            List<Book> books = LoadBooks();

            Console.Write($"Podaj id książki, którą chcesz usunąć: ");
            int deletingBook;
            while (!int.TryParse(Console.ReadLine(), out deletingBook))
            {
                Console.WriteLine("Podaj poprawne id");
            }

            Book? book = books.FirstOrDefault(b => b.Id == deletingBook);

            if (book == null)
            {
                Console.WriteLine("Błąd: nie znaleziono użytkownika o podanym identyfikatorze");
                return;
            }

            Console.Write($"Czy na pewno chcesz książkę o id {book.Id} i tytule {book.Title} (T/N): ");

            char c = char.ToLower(Console.ReadKey().KeyChar);

            if (c == 't' || c == 'y')
            {
                books.Remove(book);
                SaveBooks(books);
                Console.WriteLine();
                Console.WriteLine("Pomyślnie usunięto książkę z bazy danych");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Książka nie została prawidłowo usunięta z bazy danych. Spróbuj ponownie.");
            }
            
        }


        public void isAvailableBook()
        {
            List<Book> books = LoadBooks();

            Console.Write("Podaj ID książki: ");
            int bookId = int.Parse(Console.ReadLine()!);

            Book? book = books.FirstOrDefault(b => b.Id == bookId);

            if (book == null)
            {
                Console.WriteLine("Nie znaleziono pozycji o podanym id.");
                return;
            }

            if (book.isAvailable)
            {
                Console.WriteLine("Książka jest dostępna.");
                return;
            }
            else
            {
                Console.WriteLine("Książka jest wypożyczona.");
                return;
            }
        }
        public int getLastIdBook()
        {
            List<Book> books = LoadBooks();

            if (books == null || !books.Any())
            {
                return 0;
            }

            return books.Max(p => p.Id);
        }

        public void SaveBooks(List<Book> books)
        {
            string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("books.json", json);
        }

        public List<Book> LoadBooks()
        {
            List<Book> books = new List<Book>();

            if (File.Exists("books.json"))
            {
                string json = File.ReadAllText("books.json");
                books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
            }

            return books;
        }


        //constructors
        public Book(int Id, string? Title, string? Author, int Year, bool isAvailable)
        {
            this.Id = Id;
            this.Title = Title;
            this.Author = Author;
            this.Year = Year;
            this.isAvailable = isAvailable;
        }

        public Book()
        {
            
        }
    }
}
