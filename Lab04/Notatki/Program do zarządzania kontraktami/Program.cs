using LabWSIZ;

Employee[] employees =
{
    new Employee("Jan", "Nowak"), //intership default 1000zł
    new Employee("Jan1", "Nowak1", new Position(5200m, 10)), //employee
    new Employee("Jan2", "Nowak2"),//intership default 1000zł
    new Employee("Jan3", "Nowa3", new Position(5200m, 120)) //employee
};


foreach (var item in employees)
{
    Console.WriteLine(item.ToString());
}