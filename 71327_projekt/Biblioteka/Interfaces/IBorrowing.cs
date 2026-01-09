using Biblioteka.Models;

namespace Biblioteka.Interfaces
{
    internal interface IBorrowing
    {
        //properties
        int Id { get; set; }
        int BookId { get; set; }
        int ReaderId { get; set; }
        DateTime BorrowDate { get; set; }
        DateTime DueDate { get; set; }
        DateTime? ReturnDate { get; set; }

        //methods
        void BorrowBook();
        void ReturnBook();
        void ShowBorrowings();
        void SaveBorrowings(List<Borrowing> borrowings);
        List<Borrowing> LoadBorrowings();
    }
}
