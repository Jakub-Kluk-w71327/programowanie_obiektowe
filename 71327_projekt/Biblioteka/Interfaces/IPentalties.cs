using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Models;

namespace Biblioteka.Interfaces
{
    internal interface IPentalties
    {
        //properties
        int Id { get; set; }
        int ReaderId { get; set; }
        int BookId { get; set; }
        decimal Amount { get; set; }

        //methods
        void ShowPenalties();
        decimal CalculatePenalty(Borrowing borrowing);
        void SavePenalties(List<Penalties> penalties);
        List<Penalties> LoadPenalties();
    }
}
