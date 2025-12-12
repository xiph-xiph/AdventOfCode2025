namespace AdventOfCode2025;

public class Day10
{
    public static void Run()
    {
        Console.WriteLine($"The answer to the first part of the puzzle is {PartOne()}");
        Console.WriteLine($"The answer to the second part of the puzzle is {PartTwo()}");
    }
    private static int PartOne()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day10Input.txt");
        StreamReader sr = new(InputPath);
        List<Machine> Machines = [];
        string CurrentLine = sr.ReadLine();
        while (CurrentLine != null && CurrentLine != "")
        {
            string[] MachineRaw = CurrentLine.Split(' ');

            int LightsGoal = 0;
            int i = 0;
            foreach (char Light in MachineRaw[0])
            {
                if (Light == '#')
                {
                    LightsGoal |= 1 << i;
                    i++;
                }
                else if (Light == '.')
                {
                    i++;
                }
            }

            int LightsAmount = i;

            List<int[]> Buttons = [];
            List<int> ButtonBitmasks = [];
            foreach (string ButtonRaw in MachineRaw.Skip(1).SkipLast(1))
            {
                string[] ButtonWirings = ButtonRaw[1..^1].Split(',');
                Buttons.Add(ButtonWirings.Select(Int32.Parse).ToArray());
                int Button = 0;
                foreach (string ButtonWiring in ButtonWirings)
                {
                    Button |= 1 << Int32.Parse(ButtonWiring);
                }
                ButtonBitmasks.Add(Button);
            }

            int[] JoltageRequirements = MachineRaw[^1][1..^1]
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();

            Machine Machine = new(
                LightsGoal,
                LightsAmount,
                [.. Buttons],
                [.. ButtonBitmasks],
                JoltageRequirements
            );
            Machines.Add(Machine);
            CurrentLine = sr.ReadLine();
        }

        int TotalPresses = 0;

        foreach (Machine Machine in Machines)
        {
            int Start = 0;
            int[] Distance = new int[1 << Machine.LightsAmount];
            bool[] Visited = new bool[1 << Machine.LightsAmount];
            Distance[Start] = 0;
            Visited[Start] = true;
            Queue<int> BFSQueue = [];
            BFSQueue.Enqueue(Start);

            while (BFSQueue.Count > 0)
            {
                int State = BFSQueue.Dequeue();
                if (State == Machine.LightsGoal)
                {
                    TotalPresses += Distance[State];
                    break;
                }

                foreach (int Button in Machine.ButtonBitmasks)
                {
                    int NewState = State ^ Button;
                    if (!Visited[NewState])
                    {
                        BFSQueue.Enqueue(NewState);
                        Visited[NewState] = true;
                        Distance[NewState] = Distance[State] + 1;
                    }
                }
                
            }
            
        }

        return TotalPresses;
    }

    private static int PartTwo()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day10Input.txt");
        StreamReader sr = new(InputPath);
        List<Machine> Machines = [];
        string CurrentLine = sr.ReadLine();
        while (CurrentLine != null && CurrentLine != "")
        {
            string[] MachineRaw = CurrentLine.Split(' ');

            int LightsGoal = 0;
            int i = 0;
            foreach (char Light in MachineRaw[0])
            {
                if (Light == '#')
                {
                    LightsGoal |= 1 << i;
                    i++;
                }
                else if (Light == '.')
                {
                    i++;
                }
            }

            int LightsAmount = i;

            List<int[]> Buttons = [];
            List<int> ButtonBitmasks = [];
            foreach (string ButtonRaw in MachineRaw.Skip(1).SkipLast(1))
            {
                string[] ButtonWirings = ButtonRaw[1..^1].Split(',');
                Buttons.Add(ButtonWirings.Select(Int32.Parse).ToArray());
                int Button = 0;
                foreach (string ButtonWiring in ButtonWirings)
                {
                    Button |= 1 << Int32.Parse(ButtonWiring);
                }
                ButtonBitmasks.Add(Button);
            }

            int[] JoltageRequirements = MachineRaw[^1][1..^1]
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();

            Machine Machine = new(
                LightsGoal,
                LightsAmount,
                [.. Buttons],
                [.. ButtonBitmasks],
                JoltageRequirements
            );
            Machines.Add(Machine);
            CurrentLine = sr.ReadLine();
        }

        int TotalPresses = 0;


    }
}

public record Machine(
    int LightsGoal,
    int LightsAmount,
    int[][] Buttons,
    int[] ButtonBitmasks,
    int[] JoltageRequirements
);