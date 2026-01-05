namespace AdventOfCode2025
{
    internal class Day12
    {
        //I hated this day

        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            string s = string.Join("\n", RawInput);
            string[] lgroups = s.Split(["\n\n"], StringSplitOptions.None);

            List<string> presents = lgroups.Take(lgroups.Length - 1).ToList();
            List<int> density = presents.Select(present => present.Count(c => c == '#')).ToList();

            string[] regions = lgroups.Last().Split('\n', StringSplitOptions.RemoveEmptyEntries);

            int numPossible = 0;

            foreach (string region in regions)
            {
                string[] parts = region.Split(": ");
                int[] xy = parts[0].Split('x').Select(int.Parse).ToArray();
                int x = xy[0], y = xy[1];
                int[] counts = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                int min_space = counts.Zip(density, (a, b) => a * b).Sum();
                int total_presents = counts.Sum();

                if (min_space <= x * y && total_presents <= x / 3 * (y / 3))
                {
                    numPossible++;
                }
            }

            return numPossible.ToString();
        }
    }
}