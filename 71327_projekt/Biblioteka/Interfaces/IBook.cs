using Biblioteka.Models;

namespace Biblioteka.Interfaces
{
    internal interface IBook
    {
        //properties
        int Id { get; set; }
        string? Title { get; set; }
        string? Author { get; set; }
        int Year { get; set; }
        bool isAvailable { get; set; }

        //methods
        void AddBook();
        void DeleteBook();
        void isAvailableBook();
        void SaveBooks(List<Book> books);
        List<Book> LoadBooks();
    }
}
