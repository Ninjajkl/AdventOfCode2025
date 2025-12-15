namespace AdventOfCode2025
{
    internal class Day09
    {
        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            List<(int x, int y)> corners = [];
            foreach (string line in RawInput)
            {
                int[] items = line.Split(',').Select(int.Parse).ToArray();
                corners.Add(new(items[0], items[1]));
            }

            long biggestArea = 0;

            for (int i = 0; i < corners.Count - 1; i++)
            {
                for (int j = i + 1; j < corners.Count; j++)
                {
                    (long x1, long y1) = corners[i];
                    (long x2, long y2) = corners[j];
                    long area = (Math.Abs(x1 - x2) + 1) * (Math.Abs(y1 - y2) + 1);
                    if (area > biggestArea)
                    {
                        biggestArea = area;
                    }
                }
            }

            return biggestArea.ToString();
        }

        public string Part2(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            List<(int x, int y)> corners = [];
            foreach (string line in RawInput)
            {
                int[] items = line.Split(',').Select(int.Parse).ToArray();
                corners.Add(new(items[0], items[1]));
            }

            PriorityQueue<((long x, long y), (long x, long y)), long> areas = new();
            for (int i = 0; i < corners.Count - 1; i++)
            {
                for (int j = i + 1; j < corners.Count; j++)
                {
                    (long x1, long y1) = corners[i];
                    (long x2, long y2) = corners[j];
                    long area = -(Math.Abs(x1 - x2) + 1) * (Math.Abs(y1 - y2) + 1);
                    areas.Enqueue(((x1, y1), (x2, y2)), area);
                }
            }

            while (areas.Count > 0)
            {
                ((long x, long y), (long x, long y)) bigSquare = areas.Dequeue();

                long xMin = Math.Min(bigSquare.Item1.x, bigSquare.Item2.x);
                long xMax = Math.Max(bigSquare.Item1.x, bigSquare.Item2.x);
                long yMin = Math.Min(bigSquare.Item1.y, bigSquare.Item2.y);
                long yMax = Math.Max(bigSquare.Item1.y, bigSquare.Item2.y);

                bool broken = false;

                for (int i = 0; i < corners.Count; i++)
                {
                    (long x, long y) c1 = corners[i];
                    (long x, long y) c2 = corners[(i + 1) % corners.Count];

                    if (c1.x != c2.x)
                    {
                        long cy = c1.y;
                        long cxMin = Math.Min(c1.x, c2.x);
                        long cxMax = Math.Max(c1.x, c2.x);

                        if (yMin < cy && cy < yMax && !(cxMax <= xMin || cxMin >= xMax))
                        {
                            broken = true;
                            break;
                        }
                    }
                    else
                    {
                        long cx = c1.x;
                        long cyMin = Math.Min(c1.y, c2.y);
                        long cyMax = Math.Max(c1.y, c2.y);

                        if (xMin < cx && cx < xMax && !(cyMax <= yMin || cyMin >= yMax))
                        {
                            broken = true;
                            break;
                        }
                    }
                }

                if (!broken)
                {
                    return ((xMax - xMin + 1) * (yMax - yMin + 1)).ToString();
                }
            }

            return "No Valid Areas";
        }
    }
}