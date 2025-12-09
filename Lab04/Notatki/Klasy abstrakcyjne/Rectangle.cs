namespace ConsoleApp1
{
    class Rectangle : Figure
    {
        double a = 5, b = 2;
        public override double area()
        {
            return a * b;
        }
        public override double circumference()
        {
            return (2 * a) + (2 * b);
        }
        public void view()
        {
            Console.WriteLine("Prostokat, pole: " + area() + ", obwod: " + circumference());
        }
    }
}