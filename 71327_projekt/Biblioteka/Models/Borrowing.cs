using System.Reflection.PortableExecutable;
using System.Text.Json;
using Biblioteka.Interfaces;

namespace Biblioteka.Models
{
    internal class Borrowing : IBorrowing
    {
        //properties
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        //methods
        public void BorrowBook()
        {
            Book bookHandler = new Book();
            List<Book> books = bookHandler.LoadBooks();

            List<Borrowing> borrowings = LoadBorrowings();

            Console.Write("Podaj ID książki: ");
            int bookId = int.Parse(Console.ReadLine()!);

            Book? book = books.FirstOrDefault(b => b.Id == bookId);

            if (book == null)
            {
                Console.WriteLine("Nie znaleziono książki o podanym id");
                return;
            }

            if (book.isAvailable)
            {
                Console.Write("Podaj ID czytelnika: ");
                int readerId = int.Parse(Console.ReadLine()!);

                int id = GetLastBorrowingId() + 1;

                Borrowing borrowing = new Borrowing
                {
                    Id = id,
                    BookId = bookId,
                    ReaderId = readerId,
                    BorrowDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(14),
                    ReturnDate = null
                };

                borrowings.Add(borrowing);
                SaveBorrowings(borrowings);

                book.isAvailable = false;
                bookHandler.SaveBooks(books);

                Console.WriteLine("Książka została wypożyczona.");
            }
            else
            {
                Console.WriteLine("Książka jest wypożyczona przez innego czytelnika.");
            }
            
        }
        public void ReturnBook()
        {
            Book bookHandler = new Book();
            List<Book> books = bookHandler.LoadBooks();

            List<Borrowing> borrowings = LoadBorrowings();
                   
            Console.Write("Podaj ID książki do zwrotu: ");
            int bookId = int.Parse(Console.ReadLine()!);

            Borrowing? borrowing = borrowings.FirstOrDefault(b => b.BookId == bookId && b.ReturnDate == null);

            if (borrowing == null)
            {
                Console.WriteLine("Ta książka nie jest aktualnie wypożyczona.");
                return;
            }

            borrowing.ReturnDate = DateTime.Now;

            Penalties penaltiesHandler = new Penalties();
            decimal penalty = penaltiesHandler.CalculatePenalty(borrowing);

            Book? book = books.FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                book.isAvailable = true;
            }

            SaveBorrowings(borrowings);
            bookHandler.SaveBooks(books);

            if (penalty > 0)
            {
                List<Penalties> penalties = penaltiesHandler.LoadPenalties();

                penalties.Add(new Penalties
                {
                    Id = penaltiesHandler.GetLastPenaltiyId() + 1,
                    ReaderId = borrowing.ReaderId,
                    BookId = bookId,
                    Amount = penalty,
                });

                penaltiesHandler.SavePenalties(penalties);

                Console.WriteLine($"Kara za opóźnienie: {penalty} zł");
            }
            else
            {
                Console.WriteLine("Książka oddana w terminie.");
            }
            
        }

        public void ShowBorrowings()
        {
            List<Borrowing> borrowings = LoadBorrowings();

            Reader readersHandler = new Reader();
            List<Reader> readers = readersHandler.LoadReaders();

            Console.Write("Podaj ID czytelnika: ");
            int readerId = int.Parse(Console.ReadLine()!);


            if(readers != null)
            {
                if (borrowings == null || borrowings.Count == 0) //system jest pusty.
                {
                    Console.WriteLine("Brak wypożyczeń w systemie.");
                    return;
                }

                Reader? reader = readers.FirstOrDefault(r => r.Id == readerId);

                if (reader == null)
                {
                    Console.WriteLine("Nie znaleziono czytelnika o podanym id");
                    return;
                }
                int counter = 0;

                foreach (var b in borrowings)
                {
                    if (b.ReaderId == reader.Id && b.ReturnDate == null)
                    {
                        counter++;
                    }
                }

                Console.Write($"Czytelnik {reader.FirstName} {reader.LastName} ma wypożyczone następujące książki: \n");


                foreach (var b in borrowings)
                {
                    if (b.ReaderId == reader.Id && b.ReturnDate == null)
                    {
                        Console.WriteLine(
                            $"Książka ID: {b.BookId} | " +
                            $"Od: {b.BorrowDate:yyyy-MM-dd} | Do: {b.DueDate:yyyy-MM-dd}"
                        );
                    }
                }
            }   
        }
        private int GetLastBorrowingId()
        {
            List<Borrowing> borrowings = LoadBorrowings();

            LoadBorrowings();
            if (borrowings == null || !borrowings.Any())
            {
                return 0;
            }

            return borrowings.Max(p => p.Id);
        }

        public void SaveBorrowings(List<Borrowing> borrowings)
        {
            string json = JsonSerializer.Serialize(borrowings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("borrowings.json", json);
        }

        public List<Borrowing> LoadBorrowings()
        {
            List<Borrowing> borrowings = new List<Borrowing>();

            if (File.Exists("borrowings.json"))
            {
                string json = File.ReadAllText("borrowings.json");
                borrowings = JsonSerializer.Deserialize<List<Borrowing>>(json) ?? new List<Borrowing>();
            }

            return borrowings;
           
        }

        //constructors
        public Borrowing(int id, int bookId, int readerId, DateTime borrowDate, DateTime dueDate, DateTime? returnDate)
        {
            Id = id;
            BookId = bookId;
            ReaderId = readerId;
            BorrowDate = borrowDate;
            DueDate = dueDate;
            ReturnDate = returnDate;
        }

        public Borrowing()
        {

        }
    }
}
