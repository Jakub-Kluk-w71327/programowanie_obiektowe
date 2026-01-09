using System.Text.Json;
using Biblioteka.Interfaces;

namespace Biblioteka.Models
{
    public class Reader : IReader
    {
        //properties
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        //methods
        public void AddReader()
        {
            List<Reader> readers = LoadReaders();

            int id = getLastIdReaders() + 1;
            Console.Write($"Id tego czytelnika to: {id}\n");

            Console.Write($"Podaj Imię: ");
            string? firstName = Console.ReadLine();

            Console.Write($"Podaj Nazwisko: ");
            string? lastName = Console.ReadLine();

            Reader reader = new Reader(id, firstName, lastName);
            readers.Add(reader);

            SaveReaders(readers);

            Console.WriteLine();
            Console.WriteLine("Dodano nowego czytelnika!");
        }
        public void DeleteReader()
        {
            List<Reader> readers = LoadReaders();

            Console.Write($"Podaj id użytkownika, którego chcesz usunąć: ");
            int deletingUser;
            while (!int.TryParse(Console.ReadLine(), out deletingUser))
            {
                Console.WriteLine("Podaj poprawne id");
            }

            Reader? reader = readers.FirstOrDefault(b => b.Id == deletingUser);

            if (reader == null)
            {
                Console.WriteLine("Nie znaleziono użytkownika o podanym ID.");
                return;
            }

            Console.Write($"Czy na pewno chcesz usunąć czytelnika {reader.FirstName} {reader.LastName} (T/N): ");

            char c = char.ToLower(Console.ReadKey().KeyChar);

            if (c == 't' || c == 'y' )
            {
                readers.Remove(reader);
                SaveReaders(readers);
                Console.WriteLine();
                Console.WriteLine("Czytelnik został prawidłowo usunięty z bazy danych");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Wybrano nieprawidłową opcję. Czytelnik nie został usunięty");
            }
        }

        public int getLastIdReaders()
        {
            List<Reader> readers = LoadReaders();

            if (readers == null || !readers.Any())
            {
                return 0;
            }

            return readers.Max(p => p.Id);
        }

        public void SaveReaders(List<Reader> readers)
        {
            string json = JsonSerializer.Serialize(
                readers,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText("readers.json", json);
        }

        public List<Reader> LoadReaders()
        {
            if (!File.Exists("readers.json"))
                return new List<Reader>();

            string json = File.ReadAllText("readers.json");
            return JsonSerializer.Deserialize<List<Reader>>(json)
                  ?? new List<Reader>();
        }

        //constructors
        public Reader(int id, string? firstName, string? lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public Reader()
        {

        }
    }
}
