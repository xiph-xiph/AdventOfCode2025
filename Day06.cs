namespace AdventOfCode2025;

public class Day06
{
    public static void Run()
    {
        Console.WriteLine($"The answer to the first part of the puzzle is {PartOne()}");
        Console.WriteLine($"The answer to the second part of the puzzle is {PartTwo()}");
    }
    private static long PartOne()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day06Input.txt");
        StreamReader sr = new(InputPath);
        List<long[]> Lines = [];
        for (int i = 0; i < 4; i++)
        {
            string[] RawNewLine = sr.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            long[] NewLine = new long[RawNewLine.Length];
            for (int j = 0; j < RawNewLine.Length; j++)
            {
                NewLine[j] = Int64.Parse(RawNewLine[j]);
            }
            Lines.Add(NewLine);
        }
        string[] Operators = sr.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        sr.Close();

        long GrandTotal = 0;

        for (int i = 0; i < Operators.Length; i++)
        {
            if (Operators[i] == "+")
            {
                GrandTotal += Lines[0][i] + Lines[1][i] + Lines[2][i] + Lines[3][i];
            } else
            {
                GrandTotal += Lines[0][i] * Lines[1][i] * Lines[2][i] * Lines[3][i];
            }
        }

        return GrandTotal;
    }

    private static long PartTwo()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day06Input.txt");
        StreamReader sr = new(InputPath);
        List<string> Lines = [];

        for (int i = 0; i < 4; i++)
        {
            Lines.Add(sr.ReadLine());
        }

        string Operators = sr.ReadLine();

        long GrandTotal = 0;
        List<long> Numbers = [];
        for (int i = 1; i <= Operators.Length; i++)
        {
            string RawNumber = "";
            foreach (string Line in Lines)
            {
                RawNumber += Line[^i];
            }
            if (!RawNumber.IsWhiteSpace())
            {
                Numbers.Add(Int64.Parse(RawNumber));
            }

            if (Operators[^i] == '+')
            {
                GrandTotal += Numbers.Sum();
                Numbers.Clear();
            }
            else if (Operators[^i] == '*')
            {
                GrandTotal += Numbers.Aggregate((a, b) => a * b);
                Numbers.Clear();
            }
        }

        return GrandTotal;
    }
}
