namespace AdventOfCode2025
{
    internal class Day02
    {
        public string Part1(string inputName)
        {
            string rawLine = File.ReadLines(inputName).First();
            long total = 0;
            foreach (string pair in rawLine.Split(','))
            {
                string[] parts = pair.Split('-');
                long beginRange = long.Parse(parts[0]);
                long endRange = long.Parse(parts[1]);
                for (long i = beginRange; i <= endRange; i++)
                {
                    string numStr = i.ToString();
                    if (numStr.Length % 2 != 0)
                    {
                        continue;
                    }
                    int halfLen = numStr.Length / 2;
                    string firstHalf = numStr[..halfLen];
                    string secondHalf = numStr[halfLen..];
                    if (firstHalf.Equals(secondHalf))
                    {
                        total += i;
                    }
                }
            }

            return total.ToString();
        }

        public string Part2(string inputName)
        {
            string rawLine = File.ReadLines(inputName).First();
            HashSet<long> validNumbers = [];

            foreach (string pair in rawLine.Split(','))
            {
                string[] parts = pair.Split('-');
                long beginRange = long.Parse(parts[0]);
                long endRange = long.Parse(parts[1]);

                for (int digits = 1; digits <= endRange.ToString().Length; digits++)
                {
                    for (int patternLen = 1; patternLen <= digits / 2; patternLen++)
                    {
                        if (digits % patternLen != 0)
                        {
                            continue;
                        }

                        long minPattern = (long)Math.Pow(10, patternLen - 1);
                        long maxPattern = (long)Math.Pow(10, patternLen) - 1;

                        for (long pattern = minPattern; pattern <= maxPattern; pattern++)
                        {
                            string patternStr = pattern.ToString();
                            string repeated = string.Concat(Enumerable.Repeat(patternStr, digits / patternLen));
                            long number = long.Parse(repeated);

                            if (number >= beginRange && number <= endRange)
                            {
                                validNumbers.Add(number);
                            }
                        }
                    }
                }
            }

            return validNumbers.Sum().ToString();
        }

        public string Part2Old(string inputName)
        {
            string rawLine = File.ReadLines(inputName).First();
            long total = 0;
            foreach (string pair in rawLine.Split(','))
            {
                string[] parts = pair.Split('-');
                long beginRange = long.Parse(parts[0]);
                long endRange = long.Parse(parts[1]);
                for (long i = beginRange; i <= endRange; i++)
                {
                    string numStr = i.ToString();
                    bool isFound = false;
                    for (int j = 1; j <= numStr.Length / 2; j++)
                    {
                        if (numStr.Length % j != 0) { continue; }

                        string[] pieces = numStr.Chunk(j).Select(chunk => new string(chunk)).ToArray();
                        for (int k = 0; k < pieces.Length - 1; k++)
                        {
                            if (!pieces[k].Equals(pieces[k+1]))
                            {
                                break;
                            }
                            else if (k == pieces.Length - 2)
                            {
                                total += i;
                                isFound = true;
                            }
                        }
                        if (isFound)
                        {
                            break;
                        }
                    }
                }
            }

            return total.ToString();
        }
    }
}