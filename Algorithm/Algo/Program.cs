using System;
using System.Text;

class Program
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder();
        int input = int.Parse(Console.ReadLine());
        for (int i = 0; i < input; i++)
        {
            using (StringReader reader = new StringReader(Console.ReadLine()))
            {
                string[] parts = reader.ReadLine().Split(' ');
                int num_1 = int.Parse(parts[0]);
                int num_2 = int.Parse(parts[1]);
                sb.Append((num_1 + num_2) + "\n");
            }
        }
        Console.WriteLine(sb);
    }
}