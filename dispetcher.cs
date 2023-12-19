using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            DisplayProcessList();

            Console.WriteLine("\nChoose a process (press Enter for details, Esc to exit):");

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                DisplayProcessDetails();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                break;
            }
        }
    }

    static void DisplayProcessList()
    {
        Console.WriteLine("Process List:");
        Process[] processes = Process.GetProcesses();

        foreach (Process process in processes)
        {
            try
            {
                Console.WriteLine($"[{process.Id}] {process.ProcessName} - Memory Usage: {process.WorkingSet64 / 1024} KB");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{process.Id}] {process.ProcessName} - Error: {ex.Message}");
            }
        }
    }

    static void DisplayProcessDetails()
    {
        Console.WriteLine("Enter the process ID to view details (press Backspace to go back):");
        string input = Console.ReadLine();

        if (input == "\b")
            return;

        if (int.TryParse(input, out int processId))
        {
            Process selectedProcess = Process.GetProcesses().FirstOrDefault(p => p.Id == processId);

            if (selectedProcess != null)
            {
                Console.Clear();
                Console.WriteLine($"Process Details for [{selectedProcess.Id}] {selectedProcess.ProcessName}:");
                Console.WriteLine($"Memory Usage: {selectedProcess.WorkingSet64 / 1024} KB");
                Console.WriteLine($"Physical Memory Usage: {selectedProcess.PrivateMemorySize64 / 1024} KB");

                Console.WriteLine("\nActions:");
                Console.WriteLine("D - Terminate Process");
                Console.WriteLine("Backspace - Go back");

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.D)
                {
                    try
                    {
                        selectedProcess.Kill();
                        Console.WriteLine($"Process [{selectedProcess.Id}] {selectedProcess.ProcessName} terminated.");
                        Console.ReadKey(); // Wait for user input
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error terminating process: {ex.Message}");
                        Console.ReadKey(); // Wait for user input
                    }
                }
            }
            else
            {
                Console.WriteLine($"Process with ID {processId} not found.");
                Console.ReadKey(); // Wait for user input
            }
        }

        Console.Clear();
    }
}

