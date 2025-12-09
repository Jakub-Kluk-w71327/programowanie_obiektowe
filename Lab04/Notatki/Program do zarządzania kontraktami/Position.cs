
using System.Globalization;

namespace LabWSIZ
{
    internal class Position: IContract
    {
        public decimal MonthlyRate { get; set; }
        public decimal Overtime { get; set; }

        public Position(decimal monthlyRate, decimal overtime)
        {
            MonthlyRate = monthlyRate;
            Overtime = overtime;
        }

        public decimal Salary()
        {
            return MonthlyRate + Overtime * (MonthlyRate / 60m);
        }

        public override string? ToString()
        {
            return $"Umowa w której pensja wynosi: " +
                $"{MonthlyRate.ToString("C", new CultureInfo("pl-PL"))}" +
                $" Liczba Nadgodzin wynosi: {Overtime}";
        }
    }
}
