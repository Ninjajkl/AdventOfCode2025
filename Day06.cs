namespace AdventOfCode2025
{
    internal class Day06
    {
        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            string[][] lines = RawInput
                .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .ToArray();

            long sum = 0;
            for (int i = 0; i < lines[0].Length; i++)
            {
                bool mult = lines[^1][i] == "*";
                long indivAnswer = int.Parse(lines[0][i]);
                for (int j = 1; j < lines.Length - 1; j++)
                {
                    if (mult)
                    {
                        indivAnswer *= int.Parse(lines[j][i]);
                    }
                    else
                    {
                        indivAnswer += int.Parse(lines[j][i]);
                    }
                }
                sum += indivAnswer;
            }

            return sum.ToString();
        }

        public string Part2(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            string[] transposed = Enumerable.Range(0, RawInput[0].Length)
                .Select(col => new string(RawInput.Select(row => row[col]).ToArray()))
                .ToArray();

            bool mult = false;
            long sum = 0;
            long indivAnswer = 0;

            for (int i = 0; i < transposed.Length; i++)
            {
                int rowLen = transposed[i].Trim().Length;
                if (rowLen != 0)
                {
                    char oper = transposed[i][^1];

                    if (oper == '*')
                    {
                        mult = true;
                        indivAnswer = 1;
                    }
                    else if (oper == '+')
                    {
                        mult = false;
                    }

                    int num = int.Parse(transposed[i][..^1]);
                    if (mult)
                    {
                        indivAnswer *= num;
                    }
                    else
                    {
                        indivAnswer += num;
                    }
                }
                if (rowLen == 0 || i == transposed.Length - 1)
                {
                    sum += indivAnswer;
                    indivAnswer = 0;
                }
            }
            return sum.ToString();
        }
    }
}