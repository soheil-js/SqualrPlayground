namespace MemoryTarget
{
    internal class Program
    {
        private static int _health = 100;

        static unsafe void Main(string[] args)
        {
            Console.Title = "Memory Target";

            fixed (int* pointer = &_health)
            {
                Console.WriteLine($"PID: {Environment.ProcessId}");
                Console.WriteLine($"Health address: 0x{(nuint)pointer:X}");
                Console.WriteLine($"Health: {_health}");
                Console.WriteLine();
                Console.WriteLine("Keep this program running...");

                while (true)
                {
                    Console.WriteLine($"Health = {_health}");
                    Console.ReadKey();
                    --_health;
                }
            }
        }
    }
}
