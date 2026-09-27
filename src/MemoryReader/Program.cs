using Squalr.Engine.Memory;
using Squalr.Engine.OS;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

const ulong healthAddress = 0x7FFBD306B058;

Console.Title = "MemoryReader";

Process? process = Process.GetProcessesByName("MemoryTarget").FirstOrDefault();

if (process is null)
{
    Console.WriteLine("MemoryTarget is not running.");
    return;
}

Console.WriteLine($"Found process:");
Console.WriteLine($"Name: {process.ProcessName}");
Console.WriteLine($"PID : {process.Id}");

Processes.Default.OpenedProcess = process;

Console.WriteLine();
Console.WriteLine("Reading memory...");

while (true)
{
    int health = Reader.Default.Read<int>(healthAddress, out bool success);

    if (success)
    {
        Console.WriteLine($"Health = {health}");
    }
    else
    {
        Console.WriteLine("Error!");
    }

    Console.ReadKey();
}

