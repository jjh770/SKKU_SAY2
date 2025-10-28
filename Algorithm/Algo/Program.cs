using System;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine().Trim();
        Console.WriteLine(input == "" ? 0 : input.Split().Length);
    }
}


//long start = int.Parse(Console.ReadLine());
//long end = int.Parse(Console.ReadLine());

//Console.WriteLine(end * (end + 1) / 2 - start * (start + 1) / 2);