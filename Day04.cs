using System;
using System.Collections.Generic;

namespace AdventOfCode2025;

public class Day04
{
    public static void Run()
    {
        Console.WriteLine($"The answer to the first part of the puzzle is {PartOne()}");
        Console.WriteLine($"The answer to the second part of the puzzle is {PartTwo()}");
    }
    private static int PartOne()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day04Input.txt");
        StreamReader sr = new(InputPath);
        List<string> Lines = [];
        string? CurrentLine = sr.ReadLine();
        while (CurrentLine != null && CurrentLine != "")
        {
            Lines.Add(CurrentLine);
            CurrentLine = sr.ReadLine();
        }
        sr.Close();

        int AccessibleRolls = 0;

        for  (int LineIndex = 0; LineIndex < Lines.Count; LineIndex++)
        {
            string Line = Lines[LineIndex];
            for (int CharIndex = 0; CharIndex < Line.Length; CharIndex++)
            {
                char Char = Line[CharIndex];
                if (Char == '@')
                {
                    int SurroundingRolls = 0;

                    // check top 3
                    string PreviousLine = SafeListIndex(Lines, LineIndex - 1);
                    if (PreviousLine != null)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            if (SafeStringIndex(PreviousLine, CharIndex + i) == '@')
                            {
                                SurroundingRolls++;
                                if (SurroundingRolls > 3)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (SurroundingRolls > 3)
                    {
                        continue;
                    }

                    // check left
                    if (SafeStringIndex(Line, CharIndex - 1) == '@')
                    {
                        SurroundingRolls++;
                        if (SurroundingRolls > 3)
                        {
                            continue;
                        }
                    }

                    // check right
                    if (SafeStringIndex(Line, CharIndex + 1) == '@')
                    {
                        SurroundingRolls++;
                        if (SurroundingRolls > 3)
                        {
                            continue;
                        }
                    }

                    // check bottom 3
                    string NextLine = SafeListIndex(Lines, LineIndex + 1);
                    if (NextLine != null)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            if (SafeStringIndex(NextLine, CharIndex + i) == '@')
                            {
                                SurroundingRolls++;
                                if (SurroundingRolls > 3)
                                {
                                    break;
                                }
                            }
                        }
                    }


                    if (SurroundingRolls < 4)
                    {
                        AccessibleRolls++;
                    }
                }
            }
        }

        return AccessibleRolls;
    }

    private static long PartTwo()
    {
        string InputPath = Path.Combine(AppContext.BaseDirectory, "./Input/Day04Input.txt");
        StreamReader sr = new(InputPath);
        List<string> Lines = [];
        string? CurrentLine = sr.ReadLine();
        while (CurrentLine != null && CurrentLine != "")
        {
            Lines.Add(CurrentLine);
            CurrentLine = sr.ReadLine();
        }
        sr.Close();

        int RemovedRolls = 0;
        bool RemovedRollsThisIteration = true;

        while (RemovedRollsThisIteration)
        {
            RemovedRollsThisIteration = false;
            for (int LineIndex = 0; LineIndex < Lines.Count; LineIndex++)
            {
                string Line = Lines[LineIndex];
                for (int CharIndex = 0; CharIndex < Line.Length; CharIndex++)
                {
                    char Char = Line[CharIndex];
                    if (Char == '@')
                    {
                        int SurroundingRolls = 0;

                        // check top 3
                        string PreviousLine = SafeListIndex(Lines, LineIndex - 1);
                        if (PreviousLine != null)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                if (SafeStringIndex(PreviousLine, CharIndex + i) == '@')
                                {
                                    SurroundingRolls++;
                                    if (SurroundingRolls > 3)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (SurroundingRolls > 3)
                        {
                            continue;
                        }

                        // check left
                        if (SafeStringIndex(Line, CharIndex - 1) == '@')
                        {
                            SurroundingRolls++;
                            if (SurroundingRolls > 3)
                            {
                                continue;
                            }
                        }

                        // check right
                        if (SafeStringIndex(Line, CharIndex + 1) == '@')
                        {
                            SurroundingRolls++;
                            if (SurroundingRolls > 3)
                            {
                                continue;
                            }
                        }

                        // check bottom 3
                        string NextLine = SafeListIndex(Lines, LineIndex + 1);
                        if (NextLine != null)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                if (SafeStringIndex(NextLine, CharIndex + i) == '@')
                                {
                                    SurroundingRolls++;
                                    if (SurroundingRolls > 3)
                                    {
                                        break;
                                    }
                                }
                            }
                        }


                        if (SurroundingRolls < 4)
                        {
                            // replace removed roll with '.'
                            char[] CharArray = Lines[LineIndex].ToCharArray();
                            CharArray[CharIndex] = '.';
                            Lines[LineIndex] = new string(CharArray);

                            RemovedRollsThisIteration = true;
                            RemovedRolls++;
                        }
                    }
                }
            }
        }

        return RemovedRolls;
    }

    private static T? SafeListIndex<T>(List<T> List, int Index)
    {
        if (Index < 0 || Index >= List.Count)
            return default;
        return List[Index];
    }
    private static char SafeStringIndex(string String, int Index)
    {
        if (Index < 0 || Index >= String.Length)
            return default;
        return String[Index];
    }
}
