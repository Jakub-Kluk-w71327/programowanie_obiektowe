using Biblioteka.Models;

namespace Biblioteka.Interfaces
{
    internal interface IReader
    {
        //properties
        int Id { get; set; }
        string? FirstName { get; set; }
        string? LastName { get; set; }

        //methods
        void AddReader();
        void DeleteReader();
        void SaveReaders(List<Reader> readers);
        List<Reader> LoadReaders();
    }
}
