namespace AdventOfCode2025
{
    internal class Day08
    {
        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);
            List<Coord> junctionBoxes = ParseJunctions(RawInput);
            PriorityQueue<(Coord, Coord), long> shortestPaths = BuildShortestPaths(junctionBoxes);
            int numConns = RawInput.Length <= 20 ? 10 : 1000;
            List<Circuit> circuits = [];
            for (int i = 0; i < numConns; i++)
            {
                shortestPaths.TryDequeue(out (Coord, Coord) Coords, out long dist);
                Coord c1 = Coords.Item1;
                Coord c2 = Coords.Item2;
                Circuit c1Circuit = null;
                Circuit c2Circuit = null;
                foreach (Circuit c in circuits)
                {
                    if (c.junctions.Contains(c1))
                    {
                        c1Circuit = c;
                    }

                    if (c.junctions.Contains(c2))
                    {
                        c2Circuit = c;
                    }
                }
                if (c1Circuit is null)
                {
                    if (c2Circuit is null)
                    {
                        Circuit newCircuit = new(c1, c2, dist);
                        circuits.Add(newCircuit);
                    }
                    else
                    {
                        c2Circuit.junctions.Add(c1);
                        c2Circuit.circuitLength += dist;
                    }
                }
                else if (c2Circuit is null)
                {
                    c1Circuit.junctions.Add(c2);
                    c1Circuit.circuitLength += dist;
                }
                else
                {
                    if (c1Circuit == c2Circuit)
                    {
                        continue;
                    }

                    MergeCircuits(c1Circuit, c2Circuit, dist, circuits);
                }
            }
            List<int> circuitLengths = circuits.Select(c => c.junctions.Count).ToList();
            circuitLengths.Sort((a, b) => b.CompareTo(a));
            return (circuitLengths[0] * circuitLengths[1] * circuitLengths[2]).ToString();
        }

        public string Part2(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);
            List<Coord> junctionBoxes = ParseJunctions(RawInput);
            PriorityQueue<(Coord, Coord), long> shortestPaths = BuildShortestPaths(junctionBoxes);
            List<Circuit> circuits = [];
            Coord c1 = null;
            Coord c2 = null;
            while (circuits.Count == 0 || circuits[0].junctions.Count < junctionBoxes.Count)
            {
                shortestPaths.TryDequeue(out (Coord, Coord) Coords, out long dist);
                c1 = Coords.Item1;
                c2 = Coords.Item2;
                Circuit c1Circuit = null;
                Circuit c2Circuit = null;
                foreach (Circuit c in circuits)
                {
                    if (c.junctions.Contains(c1))
                    {
                        c1Circuit = c;
                    }

                    if (c.junctions.Contains(c2))
                    {
                        c2Circuit = c;
                    }
                }
                if (c1Circuit is null)
                {
                    if (c2Circuit is null)
                    {
                        Circuit newCircuit = new(c1, c2, dist);
                        circuits.Add(newCircuit);
                    }
                    else
                    {
                        c2Circuit.junctions.Add(c1);
                        c2Circuit.circuitLength += dist;
                    }
                }
                else if (c2Circuit is null)
                {
                    c1Circuit.junctions.Add(c2);
                    c1Circuit.circuitLength += dist;
                }
                else
                {
                    if (c1Circuit == c2Circuit)
                    {
                        continue;
                    }

                    MergeCircuits(c1Circuit, c2Circuit, dist, circuits);
                }
            }
            return (c1.x * c2.x).ToString();
        }
        private static List<Coord> ParseJunctions(string[] rawInput)
        {
            return rawInput.Select(line =>
            {
                int[] items = line.Split(',').Select(int.Parse).ToArray();
                return new Coord(items[0], items[1], items[2]);
            }).ToList();
        }

        private static PriorityQueue<(Coord, Coord), long> BuildShortestPaths(List<Coord> junctionBoxes)
        {
            PriorityQueue<(Coord, Coord), long> shortestPaths = new();
            for (int i = 0; i < junctionBoxes.Count - 1; i++)
            {
                for (int j = i + 1; j < junctionBoxes.Count; j++)
                {
                    Coord junctOne = junctionBoxes[i];
                    Coord junctTwo = junctionBoxes[j];
                    long straightDist = junctOne.Distance(junctTwo);
                    shortestPaths.Enqueue((junctOne, junctTwo), straightDist);
                }
            }
            return shortestPaths;
        }

        private static void MergeCircuits(Circuit c1Circuit, Circuit c2Circuit, long dist, List<Circuit> circuits)
        {
            c1Circuit.junctions.UnionWith(c2Circuit.junctions);
            c1Circuit.circuitLength += c2Circuit.circuitLength + dist;
            circuits.Remove(c2Circuit);
        }

    }

    internal class Circuit
    {
        public HashSet<Coord> junctions;
        public long circuitLength = 0;

        public Circuit(Coord j1, Coord j2, long dist)
        {
            circuitLength = dist;
            junctions = new HashSet<Coord>([j1, j2]);
        }
    }

    internal class Coord
    {
        public int x;
        public int y;
        public int z;

        public Coord(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public long Distance(Coord otherCoord)
        {
            return (long)Math.Sqrt(Math.Pow(x - otherCoord.x, 2) + Math.Pow(y - otherCoord.y, 2) + Math.Pow(z - otherCoord.z, 2));
        }
    }
}