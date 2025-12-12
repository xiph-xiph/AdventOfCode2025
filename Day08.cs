namespace AdventOfCode2025;
public class Day08
{
    public static void Run()
    {
        Console.WriteLine($"The answer to the first part of the puzzle is {PartOne()}");
        Console.WriteLine($"The answer to the second part of the puzzle is {PartTwo()}");
    }
    private static long PartOne()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day08Input.txt");
        StreamReader sr = new(InputPath);
        List<int[]> Jboxes = [];
        string CurrentLine = sr.ReadLine();
        while (CurrentLine != null && CurrentLine != "")
        {
            int[] Jbox = [.. CurrentLine.Split(',').Select(Int32.Parse)];
            Jboxes.Add(Jbox);
            CurrentLine = sr.ReadLine();
        }



        List<(double, int, int)> Distances = [];

        for (int x = 0; x < Jboxes.Count; x++)
        {
            for (int y = x + 1; y < Jboxes.Count; y++)
            {                
                Distances.Add((CalculateDistance(Jboxes[x], Jboxes[y]), x, y));
            }
        }

        Distances.Sort((a, b) => a.Item1.CompareTo(b.Item1));

        List<HashSet<int>> Circuits = [];


        for (int i = 0; i < 1000; i++)
        {
            int Box1 = Distances[i].Item2;
            int Box2 = Distances[i].Item3;

            List<HashSet<int>> ContainingCircuits = [];
            foreach (HashSet<int> Circuit in Circuits)
            {
                if (Circuit.Contains(Box1) || Circuit.Contains(Box2)) {
                    Circuit.Add(Box1);
                    Circuit.Add(Box2);
                    ContainingCircuits.Add(Circuit);
                }
            }
            if (ContainingCircuits.Count == 2)
            {
                ContainingCircuits[0].UnionWith(ContainingCircuits[1]);
                Circuits.Remove(ContainingCircuits[1]);
            } else if (ContainingCircuits.Count == 0)
            {
                HashSet<int> NewCircuit = [Box1, Box2];
                Circuits.Add(NewCircuit);
            }
        }

        Circuits.Sort((a, b) => b.Count.CompareTo(a.Count));

        return Circuits[0].Count * Circuits[1].Count * Circuits[2].Count;
    }

    private static long PartTwo()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day08Input.txt");
        StreamReader sr = new(InputPath);
        List<int[]> Jboxes = [];
        string CurrentLine = sr.ReadLine();
        while (CurrentLine != null && CurrentLine != "")
        {
            int[] Jbox = [.. CurrentLine.Split(',').Select(Int32.Parse)];
            Jboxes.Add(Jbox);
            CurrentLine = sr.ReadLine();
        }



        List<(double, int, int)> Distances = [];

        for (int x = 0; x < Jboxes.Count; x++)
        {
            for (int y = x + 1; y < Jboxes.Count; y++)
            {
                Distances.Add((CalculateDistance(Jboxes[x], Jboxes[y]), x, y));
            }
        }

        Distances.Sort((a, b) => a.Item1.CompareTo(b.Item1));

        List<HashSet<int>> Circuits = [];

        int Box1 = 0;
        int Box2 = 0;

        int i = 0;
        int MaxIterations = Distances.Count;

        while (i < MaxIterations)
        {
            Box1 = Distances[i].Item2;
            Box2 = Distances[i].Item3;

            List<HashSet<int>> ContainingCircuits = [];
            foreach (HashSet<int> Circuit in Circuits)
            {
                if (Circuit.Contains(Box1) || Circuit.Contains(Box2))
                {
                    Circuit.Add(Box1);
                    Circuit.Add(Box2);
                    ContainingCircuits.Add(Circuit);
                }
            }

            if (ContainingCircuits.Count == 2)
            {
                ContainingCircuits[0].UnionWith(ContainingCircuits[1]);
                Circuits.Remove(ContainingCircuits[1]);
            }
            else if (ContainingCircuits.Count == 0)
            {
                HashSet<int> NewCircuit = [Box1, Box2];
                Circuits.Add(NewCircuit);
            }

            i++;

            if (Circuits.Count == 1 && i > 1000) break;
        }

        return Jboxes[Box1][0] * Jboxes[Box2][0];
    }

    private static double CalculateDistance(int[] a, int[] b)
    {
        double dx = b[0] - a[0];
        double dy = b[1] - a[1];
        double dz = b[2] - a[2];

        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}
