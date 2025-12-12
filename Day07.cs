namespace AdventOfCode2025;

public class Day07
{
    public static void Run()
    {
        Console.WriteLine($"The answer to the first part of the puzzle is {PartOne()}");
        Console.WriteLine($"The answer to the second part of the puzzle is {PartTwo()}");
    }
    private static long PartOne()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day07Input.txt");
        StreamReader sr = new(InputPath);

        int Splits = 0;
        string CurrentLine = sr.ReadLine();
        HashSet<int> Beams = [];
        Beams.Add(CurrentLine.IndexOf('S'));
        CurrentLine = sr.ReadLine();
        while (CurrentLine != null && CurrentLine != "")
        {
            HashSet<int> SplitBeams = [];
            foreach (int Beam in Beams)
            {
                if (CurrentLine[Beam] == '^')
                {
                    SplitBeams.Add(Beam + 1);
                    SplitBeams.Add(Beam - 1);
                    Splits++;
                } else
                {
                    SplitBeams.Add(Beam);
                }
            }
            Beams = SplitBeams;
            CurrentLine = sr.ReadLine();
        }

        return Splits;
    }

    private static long PartTwo()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day07Input.txt");
        StreamReader sr = new(InputPath);

        string CurrentLine = sr.ReadLine();
        Dictionary<int, long> Beams = [];
        Beams.Add(CurrentLine.IndexOf('S'), 1);
        CurrentLine = sr.ReadLine();
        while (CurrentLine != null && CurrentLine != "")
        {
            Dictionary<int, long> SplitBeams = [];
            foreach ((int Beam, long Timelines) in Beams)
            {
                if (CurrentLine[Beam] == '^')
                {
                    if (!SplitBeams.TryAdd(Beam + 1, Timelines))
                    {
                        SplitBeams[Beam + 1] += Timelines;
                    }

                    if (!SplitBeams.TryAdd(Beam - 1, Timelines))
                    {
                        SplitBeams[Beam - 1] += Timelines;
                    }
                }
                else
                {
                    if (!SplitBeams.TryAdd(Beam, Timelines))
                    {
                        SplitBeams[Beam] += Timelines;
                    }
                }
            }

            Beams = SplitBeams;
            CurrentLine = sr.ReadLine();
        }
        return Beams.Values.Sum();
    }
}
