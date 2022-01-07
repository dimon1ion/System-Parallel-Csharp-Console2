using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Work2_System_Parallel_Csharp_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                Console.WriteLine("Hello");
                Console.Write("Enter num => ");
                int numFact = Int32.Parse(Console.ReadLine());
                long result = 1;
                Parallel.For(1, numFact + 1, (num) =>
                {
                    result *= num;
                });
                Console.WriteLine($"Result => {result}");
            }

            {
                Console.WriteLine("Next");
                Console.Write("Enter num => ");
                int numFact = Int32.Parse(Console.ReadLine());
                int result = 1;
                int digits = 0;
                int sum = 0;
                Parallel.For(1, numFact + 1, (num) =>
                {
                    result *= num;
                });
                Parallel.For(1, result.ToString().Length + 1, (i) =>
                {
                    int div = 1;
                    for (int j = 0; j < i; j++)
                    {
                        div *= 10;
                    }
                    sum += result % div / (div / 10);
                    digits++;
                });
                Console.WriteLine($"Results:" +
                    $"\n Factorial => {result}" +
                    $"\n Numbers => {digits}" +
                    $"\n Sum => {sum}");
            }

            {
                Console.WriteLine("Next");
                Console.Write("Enter start num => ");
                int startNum = Int32.Parse(Console.ReadLine());
                Console.Write("Enter stop num => ");
                int stopNum = Int32.Parse(Console.ReadLine());
                string result = String.Empty;
                Parallel.For(startNum, stopNum + 1, (i) =>
                {
                    string currectNum = string.Empty;
                    for (int j = 1; j <= 10; j++)
                    {
                        currectNum += $"{i} * {j} = {i * j}\n";
                    }
                    lock (result)
                    {
                        result += currectNum;
                    }
                });
                using (FileStream fs = new FileStream("hello.txt", FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(result);
                        Console.WriteLine("Check hello.txt");
                    }
                }
            }

            {
                Console.WriteLine("Next");
                List<int> listNumbers = new List<int>();
                string file = File.ReadAllText("hello.txt");
                bool digit = false;
                int num = 0;
                foreach (char symb in file)
                {
                    if (Char.IsDigit(symb))
                    {
                        digit = true;
                        num = num * 10 + Int32.Parse(symb.ToString());
                    }
                    else if (digit)
                    {
                        listNumbers.Add(num);
                        num = 0;
                        digit = false;
                    }
                }
                Parallel.ForEach(listNumbers, (i) =>
                {
                    long fact = 1;
                    Parallel.For(1, i + 1, (num) =>
                    {
                        fact *= num;
                    });
                    if (fact < 0)
                    {
                        fact = 0;
                    }
                    Console.Write($"{fact}, ");
                });
                Console.WriteLine();
                Console.WriteLine($"Max => {listNumbers.Max()}");
                Console.WriteLine($"Min => {listNumbers.Min()}");
                Console.WriteLine($"Sum => {listNumbers.Sum()}");
            }
        }
    }
}
