namespace AdventOfCode2025
{
    internal class Day03
    {
        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            int highestJoltage = 0;
            foreach (string bank in RawInput)
            {
                int highestBankJoltage = 0;
                int highestFirstDigit = 0;

                for (int i = 0; i < bank.Length; i++)
                {
                    int joltageDigit = bank[i] - 48;

                    int newJoltage = (highestFirstDigit * 10) + joltageDigit;
                    if (newJoltage > highestBankJoltage)
                    {
                        highestBankJoltage = newJoltage;
                    }

                    if (joltageDigit > highestFirstDigit)
                    {
                        highestFirstDigit = joltageDigit;
                    }
                }
                highestJoltage += highestBankJoltage;
            }

            return highestJoltage.ToString();
        }

        public string Part2(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            long highestJoltage = 0;
            foreach (string bank in RawInput)
            {
                long highestBankJoltage = 0;
                int startingIndex = 0;
                for (int i = 11; i >= 0; i--)
                {
                    string possibleDigits = bank[startingIndex..^i];
                    int highestDigit = -1;
                    int highestIndex = -1;
                    for (int j = 0; j < possibleDigits.Length; j++)
                    {
                        if (possibleDigits[j] - 48 > highestDigit)
                        {
                            highestDigit = possibleDigits[j] - 48;
                            highestIndex = j;
                        }
                    }
                    highestBankJoltage += highestDigit * (long)Math.Pow(10, i);
                    startingIndex += highestIndex + 1;
                }

                highestJoltage += highestBankJoltage;
            }

            return highestJoltage.ToString();
        }
    }
}