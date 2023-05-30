using TaxationServices.IO.Interfaces;

namespace TaxationServices.IO
{
    public class ConsoleReader : IReader
    {
        public string? ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
