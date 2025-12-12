namespace AdventOfCode2025;

public class Day01
{
    public static void Run()
    {
        Console.WriteLine($"The answer to the first part of the puzzle is {PartOne()}");
        Console.WriteLine($"The answer to the second part of the puzzle is {PartTwo()}");
    }
    private static int PartOne()
    {
        int Password = 0;
        int Dial = 50;
        string? Line;


        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day01Input.txt");
        StreamReader sr = new(InputPath);

        Line = sr.ReadLine();
        while (Line != null)
        {
            int Offset = Int32.Parse(Line[1..^0]);
            if (Line[0] == 'L')
            {
                Dial -= Offset;
            }
            else
            {
                Dial += Offset;
            }

            Dial %= 100;
            if (Dial == 0)
            {
                Password += 1;
            }
            Line = sr.ReadLine();
        }

        sr.Close();

        return Password;
    }

    private static int PartTwo()
    {
        int Password = 0;
        int Dial = 50;
        string? Line;


        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day01Input.txt");
        StreamReader sr = new(InputPath);

        Line = sr.ReadLine();
        while (Line != null && Line != "")
        {
            int Offset = Int32.Parse(Line[1..^0]);
            if (Line[0] == 'L')
            {
                // turn left
                if (Dial == 0)
                {
                    Dial = 100;
                }

                Dial -= Offset;
                while (Dial < 0)
                {
                    Dial += 100;
                    Password += 1;
                }
                if (Dial == 0)
                {
                    Password += 1;
                }
            }
            else
            {
                // turn right
                Dial += Offset;
                Password += Dial / 100;
                Dial %= 100;
            }

            Line = sr.ReadLine();
        }

        sr.Close();

        return Password;
    }
}
