namespace Days;

public class Day02
{
    public void Run()
    {
        Console.WriteLine($"The answer to the first part of the puzzle is {PartOne()}");
        Console.WriteLine($"The answer to the second part of the puzzle is {PartTwo()}");
    }
    private long PartOne()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day02Input.txt");
        StreamReader sr = new(InputPath);
        string? Input = sr.ReadLine();
        sr.Close();

        string[] RawIDRanges = Input.Split(',');
        string[][] IDRanges = RawIDRanges.Select(ids => ids.Split('-')).ToArray();

        List<long> InvalidIDs = new List<long>();
        foreach (string[] IDRange in IDRanges)
        {
            long FirstID = Int64.Parse(IDRange[0]);
            long LastID = Int64.Parse(IDRange[1]);
            for (long ID = FirstID; ID <= LastID; ID++)
            {
                // check if ID is invalid
                string IDString = ID.ToString();

                // id must be valid if ID length is odd
                if (IDString.Length % 2 == 1)
                {
                    continue;
                }

            // split string in half
            int Middle = IDString.Length / 2;
                string FirstHalf = IDString.Substring(0, Middle);
                string SecondHalf = IDString.Substring(Middle);
                if (FirstHalf == SecondHalf)
                {
                    InvalidIDs.Add(ID);
                }
            }
        }

        return InvalidIDs.Sum();
    }

    private long PartTwo()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day02Input.txt");
        StreamReader sr = new(InputPath);
        string? Input = sr.ReadLine();
        sr.Close();

        string[] RawIDRanges = Input.Split(',');
        string[][] IDRanges = RawIDRanges.Select(ids => ids.Split('-')).ToArray();

        List<long> InvalidIDs = new List<long>();
        foreach (string[] IDRange in IDRanges)
        {
            long FirstID = Int64.Parse(IDRange[0]);
            long LastID = Int64.Parse(IDRange[1]);
            for (long ID = FirstID; ID <= LastID; ID++)
            {
                // check if ID is invalid
                string IDString = ID.ToString();

                // for each divisor of length, check whether or not it is made of equal parts
                foreach (int Divisor in GetDivisors(IDString.Length))
                {
                    string[] Parts = SplitIntoParts(IDString, Divisor);
                    bool AllEqual = true;
                    string FirstPart = Parts[0];
                    foreach (string Part in Parts)
                    {
                        if (Part != FirstPart)
                        {
                            AllEqual = false;
                            break;
                        }
                    }
                    if (AllEqual)
                    {
                        InvalidIDs.Add(ID);
                        break;
                    }
                }
            }
        }

        return InvalidIDs.Sum();
    }

    private List<int> GetDivisors(int Input)
    {
        List<int> Divisors = new List<int>();
        // start at 2, since we want all divisors higher than 1
        for (int i = 2; i <= Input; i++)
        {
            if (Input % i == 0)
            {
                Divisors.Add(i);
            }
        }
        return Divisors;
    }

    // assumes that length of String is divisible by AmountOfParts
    private string[] SplitIntoParts(string Input, int AmountOfParts)
    {
        int PartLength = Input.Length / AmountOfParts;

        string[] PartsArray = new string[AmountOfParts];
        for (int i = 0; i < AmountOfParts; i++)
        {
            PartsArray[i] = Input.Substring(i * PartLength, PartLength);
        }
        return PartsArray;
    }
}
