using System.Reflection.PortableExecutable;
using System.Text.Json;
using Biblioteka.Interfaces;

namespace Biblioteka.Models
{
    internal class Penalties : IPentalties
    {
        //fields
        public int Id { get; set; }
        public int ReaderId { get; set; }
        public int BookId { get; set; }
        public decimal Amount { get; set; }

        //methods
        public void ShowPenalties()
        {
            List<Penalties> penalties = LoadPenalties();

            Reader readersHandler = new Reader();
            List<Reader> readers = readersHandler.LoadReaders();

            Console.Write("Podaj ID czytelnika: ");
            int readerId = int.Parse(Console.ReadLine()!);

            if (readers != null)
            {
                Reader? reader = readers.FirstOrDefault(r => r.Id == readerId);

                if (reader == null)
                {
                    Console.WriteLine("Nie znaleziono czytelnika o podanym ID");
                    return;
                }

                Console.WriteLine($"Kary czytelnika {reader.FirstName} {reader.LastName}:");

                decimal total = 0;

                foreach (var p in penalties)
                {
                    if (p.ReaderId == reader.Id)
                    {
                        Console.WriteLine(
                            $"Id książki: {p.BookId} | Kwota: {p.Amount} zł"
                        );
                        total += p.Amount;
                    }
                }
                Console.WriteLine($"Łączna kwota do zapłaty: {total} zł");
            }
        }

        public decimal CalculatePenalty(Borrowing borrowing)
        {
            if (borrowing.ReturnDate == null)
                return 0;

            if (borrowing.ReturnDate <= borrowing.DueDate)
                return 0;

            int daysLate = (borrowing.ReturnDate.Value - borrowing.DueDate).Days;
            return daysLate * 1.0m;
        }

        public void SavePenalties(List<Penalties> penalties)
        {
            string json = JsonSerializer.Serialize(penalties, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("penalties.json", json);
        }
        public List<Penalties> LoadPenalties()
        {
            List<Penalties> penalties = new List<Penalties>();

            if (File.Exists("penalties.json"))
            {
                string json = File.ReadAllText("penalties.json");
                penalties = JsonSerializer.Deserialize<List<Penalties>>(json) ?? new List<Penalties>();
            }

            return penalties;
        }
        public int GetLastPenaltiyId()
        {
            List<Penalties> penalties = LoadPenalties();

            if (penalties == null || !penalties.Any())
            {
                return 0;
            }

            return penalties.Max(p => p.Id);
        }

        //constructor
        public Penalties()
        {

        }
        public Penalties(int id, int readerId, int bookId, decimal amount)
        {
            Id = id;
            ReaderId = readerId;
            BookId = bookId;
            Amount = amount;
        }
    }
}
