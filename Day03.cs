using System.IO;

namespace Days;

public class Day03
{
    public void Run()
    {
        Console.WriteLine($"The answer to the first part of the puzzle is {PartOne()}");
        Console.WriteLine($"The answer to the second part of the puzzle is {PartTwo()}");
    }
    private int PartOne()
    {
        int TotalJoltage = 0;

        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day03Input.txt");
        StreamReader sr = new(InputPath);
        string? Bank;
        Bank = sr.ReadLine();
        while (Bank != null & Bank != "")
        {
            int HighestBattery = 0;
            int HighestIndex = 0;
            // skip the last battery
            for (int i = 0; i < Bank.Length - 1; i++)
            {
                int Battery = Int32.Parse(Bank[i].ToString());
                if (Battery > HighestBattery)
                {
                    HighestBattery = Battery;
                    HighestIndex = i;
                }
            }

            // loop for other highest
            int SecondBattery = 0;
            for (int i = HighestIndex + 1; i < Bank.Length; i++)
            {
                int Battery = Int32.Parse(Bank[i].ToString());
                if (Battery > SecondBattery)
                {
                    SecondBattery = Battery;
                }
            }

            string JoltageString = HighestBattery.ToString() + SecondBattery.ToString();
            int Joltage = Int32.Parse(JoltageString);
            TotalJoltage += Joltage;
            Console.WriteLine(Joltage);

            Bank = sr.ReadLine();
        }
        sr.Close();

        return TotalJoltage;
    }

    private long PartTwo()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day03Input.txt");
        StreamReader sr = new(InputPath);
        string? Input = sr.ReadLine();
        sr.Close();

        return 0;
    }
}
