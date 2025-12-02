namespace AdventOfCode2024
{
    internal class Day01
    {
        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            int curNum = 50;
            int numZero = 0;
            foreach (string line in RawInput)
            {
                int dir = line[0] == 'L' ? -1 : 1;
                int rotation = int.Parse(line[1..]);
                curNum = (curNum + (dir * rotation) + 100) % 100;
                if (curNum == 0)
                {
                    numZero++;
                }
            }

            return numZero.ToString();
        }

        public string Part2(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            int curNum = 50;
            int numZero = 0;
            foreach (string line in RawInput)
            {
                int dir = line[0] == 'L' ? -1 : 1;
                int rotation = int.Parse(line[1..]);
                int newVal = curNum + (dir * rotation);

                if (newVal <= 0 || newVal >= 100)
                {
                    numZero += Math.Abs(newVal) / 100;

                    if (curNum > 0 && newVal <= 0)
                    {
                        numZero++;
                    }
                }

                curNum = ((newVal % 100) + 100) % 100;
            }

            return numZero.ToString();
        }
    }
}