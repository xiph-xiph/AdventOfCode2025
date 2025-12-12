namespace AdventOfCode2025;

public class Day05
{
    public static void Run()
    {
        Console.WriteLine($"The answer to the first part of the puzzle is {PartOne()}");
        Console.WriteLine($"The answer to the second part of the puzzle is {PartTwo()}");
    }
    private static int PartOne()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day05Input.txt");
        StreamReader sr = new(InputPath);
        List<(long, long)> Ranges = [];
        string? CurrentLine = sr.ReadLine();
        // save all until the blank line as ranges
        while (CurrentLine != "")
        {
            string[] CurrentLineSplit = CurrentLine.Split('-');
            (string MinValueString, string MaxValueString) = (CurrentLineSplit[0], CurrentLineSplit[1]);
            long MinValue = Int64.Parse(MinValueString);
            long MaxValue = Int64.Parse(MaxValueString);
            Ranges.Add((MinValue, MaxValue));
            CurrentLine = sr.ReadLine();
            
        }

        List<long> IDs = [];
        CurrentLine = sr.ReadLine();
        // save the rest as ids
        while (CurrentLine != null && CurrentLine != "")
        {
            long ID = Int64.Parse(CurrentLine);
            IDs.Add(ID);
            CurrentLine = sr.ReadLine();
        }

        sr.Close();

        int FreshIDs = 0;

        foreach (long ID in IDs)
        {
            foreach ((long Min, long Max) in Ranges)
            {
                if (ID >= Min && ID <= Max)
                {
                    FreshIDs++;
                    break;
                }
            }
        }

        
        return FreshIDs;
    }

    private static long PartTwo()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day05Input.txt");
        StreamReader sr = new(InputPath);
        List<(long, long)> Ranges = [];
        string? CurrentLine = sr.ReadLine();
        // save all until the blank line as ranges
        while (CurrentLine != "")
        {
            string[] CurrentLineSplit = CurrentLine.Split('-');
            (string MinValueString, string MaxValueString) = (CurrentLineSplit[0], CurrentLineSplit[1]);
            long MinValue = Int64.Parse(MinValueString);
            long MaxValue = Int64.Parse(MaxValueString);
            Ranges.Add((MinValue, MaxValue));
            CurrentLine = sr.ReadLine();

        }
        // rest is not needed anymore

        sr.Close();

        // merge ranges so there is no overlap

        // sort ranges by min value
        Ranges.Sort((a, b) => a.Item1.CompareTo(b.Item1));

        List<(long, long)> MergedRanges = [];
        MergedRanges.Add(Ranges[0]);

        foreach ((long, long) Range in Ranges.Skip(1))
        {
            (long, long) TopRange = MergedRanges[MergedRanges.Count - 1];

            // check for overlap
            if (Range.Item1 <= TopRange.Item2)
            {
                // merge
                MergedRanges[^1] = (MergedRanges[^1].Item1, Math.Max(Range.Item2, TopRange.Item2));
            } else
            {
                // no overlap
                MergedRanges.Add(Range);
            }
        }

        long FreshIDs = 0;

        foreach ((long Min, long Max) in MergedRanges)
        {
            FreshIDs += Max - Min + 1;
        }

        return FreshIDs;
    }
}
