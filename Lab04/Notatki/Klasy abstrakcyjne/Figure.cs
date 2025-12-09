namespace ConsoleApp1
{
    // Abstract class
    abstract class Figure
    {
        // Abstract method (does not have a body)
        public abstract double area();
        public abstract double circumference();

        // Regular method
        public void view()
        {
            Console.WriteLine("Figura: ");
        }
    }
}
