namespace AdventOfCode2025;

public class Day09
{
    public static void Run()
    {
        Console.WriteLine($"The answer to the first part of the puzzle is {PartOne()}");
        Console.WriteLine($"The answer to the second part of the puzzle is {PartTwo()}");
    }
    private static long PartOne()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day09Input.txt");
        StreamReader sr = new(InputPath);
        List<int[]> RedTiles = [];
        string CurrentLine = sr.ReadLine();
        while (CurrentLine != null && CurrentLine != "")
        {
            int[] RedTile = [.. CurrentLine.Split(',').Select(Int32.Parse)];
            RedTiles.Add(RedTile);
            CurrentLine = sr.ReadLine();
        }

        List<(long, int, int)> Areas = [];

        for (int x = 0; x < RedTiles.Count; x++)
        {
            for (int y = x + 1; y < RedTiles.Count; y++)
            {
                Areas.Add((CalculateArea(RedTiles[x], RedTiles[y]), x, y));
            }
        }

        Areas.Sort((a, b) => b.Item1.CompareTo(a.Item1));


        return Areas[0].Item1;
    }

    private static long PartTwo()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day09Input.txt");
        StreamReader sr = new(InputPath);
        List<int[]> RedTiles = [];
        string CurrentLine = sr.ReadLine();
        while (CurrentLine != null && CurrentLine != "")
        {
            int[] RedTile = [.. CurrentLine.Split(',').Select(Int32.Parse)];
            RedTiles.Add(RedTile);
            CurrentLine = sr.ReadLine();
        }

        List<(int[], int[])> AllGreenTileLines = [];
        for (int i = 0; i < RedTiles.Count; i++)
        {
            int[] current = RedTiles[i];
            int[] next = RedTiles[(i + 1) % RedTiles.Count];
            AllGreenTileLines.Add((current, next));
        }

        List<(int[], int[])> HorizontalGreenTileLines = [];
        List<(int[], int[])> VerticalGreenTileLines = [];
        foreach (var Line in AllGreenTileLines)
        {
            if (Line.Item1[0] == Line.Item2[0])
            {
                if (Line.Item1[1] < Line.Item2[1])
                {
                    VerticalGreenTileLines.Add(Line);
                } else
                {
                    VerticalGreenTileLines.Add((Line.Item2, Line.Item1));
                }
            } else
            {
                if (Line.Item1[0] < Line.Item2[0])
                {
                    HorizontalGreenTileLines.Add(Line);
                }
                else
                {
                    HorizontalGreenTileLines.Add((Line.Item2, Line.Item1));
                }
            }
        }



        List<(long, int, int)> Areas = [];

        for (int x = 0; x < RedTiles.Count; x++)
        {
            for (int y = x + 1; y < RedTiles.Count; y++)
            {
                Areas.Add((CalculateArea(RedTiles[x], RedTiles[y]), x, y));
            }
        }

        Areas.Sort((a, b) => b.Item1.CompareTo(a.Item1));

        (long, int, int) LargestArea = (0, 0, 0);
        foreach (var Area in Areas)
        {
            int MinY = Math.Min(RedTiles[Area.Item2][1], RedTiles[Area.Item3][1]);
            int MaxY = Math.Max(RedTiles[Area.Item2][1], RedTiles[Area.Item3][1]);
            int MinX = Math.Min(RedTiles[Area.Item2][0], RedTiles[Area.Item3][0]);
            int MaxX = Math.Max(RedTiles[Area.Item2][0], RedTiles[Area.Item3][0]);

            bool Intersects = false;
            
            foreach (var Line in VerticalGreenTileLines)
            {
                int LineX = Line.Item1[0];
                int LineMinY = Math.Min(Line.Item1[1], Line.Item2[1]);
                int LineMaxY = Math.Max(Line.Item1[1], Line.Item2[1]);

                if (LineMinY < MaxY && LineMaxY > MinY && LineX > MinX && LineX < MaxX)
                {
                    Intersects = true;
                    break;
                }
            }

            if (Intersects) continue;
           
            foreach (var Line in HorizontalGreenTileLines)
            {
                int LineMinX = Math.Min(Line.Item1[0], Line.Item2[0]);
                int LineMaxX = Math.Max(Line.Item1[0], Line.Item2[0]);
                int LineY = Line.Item1[1];

                if (LineMinX < MaxX && LineMaxX > MinX && LineY > MinY && LineY < MaxY)
                {
                    Intersects = true;
                    break;
                }
            }

            if (!Intersects)
            {
                LargestArea = Area;
                break;
            }
        }

        return LargestArea.Item1;
    }

    private static long CalculateArea(int[] a, int[] b)
    {
        return (long)(Math.Abs(b[0] - a[0]) + 1) * (Math.Abs(b[1] - a[1]) + 1);
    }
}
