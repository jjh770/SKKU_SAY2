using System;
using System.Text;

class Program
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder();
        int input = int.Parse(Console.ReadLine());

        LinkedList<int> deque = new LinkedList<int>();
        int command = 0;
        int inputNum = 0;

        for (int i = 0; i < input; i++)
        {
            string[] inputCommand = Console.ReadLine().Split(" ");
            command = int.Parse(inputCommand[0]);
            if (command == 1 || command == 2)
                inputNum = int.Parse(inputCommand[1]);
            switch (command)
            {
                case 1:
                    deque.AddFirst(inputNum);
                    break;
                case 2:
                    deque.AddLast(inputNum);
                    break;
                case 3:
                    if (deque.Count != 0)
                    {
                        sb.Append($"{deque.First.Value}\n");
                        deque.RemoveFirst();
                    }
                    else
                    {
                        sb.Append("-1\n");
                    }
                    break;
                case 4:
                    if (deque.Count != 0)
                    {
                        sb.Append($"{deque.Last.Value}\n");
                        deque.RemoveLast();
                    }
                    else
                    {
                        sb.Append("-1\n");
                    }
                    break;
                case 5:
                    sb.Append($"{deque.Count}\n");
                    break;
                case 6:
                    sb.Append(deque.Count != 0 ? "0\n" : "1\n");
                    break;
                case 7:
                    sb.Append(deque.Count != 0 ? $"{deque.First.Value}\n" : "-1\n");
                    break;
                case 8:
                    sb.Append(deque.Count != 0 ? $"{deque.Last.Value}\n" : "-1\n");
                    break;
            }
        }
        Console.Write(sb);
    }
}
