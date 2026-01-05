using Google.OrTools.LinearSolver;

namespace AdventOfCode2025
{
    internal class Day10
    {
        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            int minPresses = 0;
            foreach (string machine in RawInput)
            {
                string[] mComp = machine.Split(' ');
                bool[] lightGuide = mComp[0].Trim(['[', ']']).Select(c => c == '#').ToArray();
                int[][] buttons = mComp[1..^1].Select(b => b.Trim('(', ')').Split(',').Select(l => int.Parse(l)).ToArray()).ToArray();

                int numPresses = 0;
                List<bool[]> curLights = [new bool[lightGuide.Length]];
                HashSet<bool[]> seen = new(new BoolArrayComparer());
                bool foundPresses = false;

                while (curLights.Count > 0)
                {
                    List<bool[]> nextLights = [];
                    foreach (bool[] prevlight in curLights)
                    {
                        foreach (int[] button in buttons)
                        {
                            bool[] nextLight = (bool[])prevlight.Clone();
                            foreach (int light in button)
                            {
                                nextLight[light] = !nextLight[light];
                            }
                            if (nextLight.SequenceEqual(lightGuide))
                            {
                                foundPresses = true;
                                break;
                            }
                            if (!seen.Contains(nextLight))
                            {
                                seen.Add(nextLight);
                                nextLights.Add(nextLight);
                            }
                        }
                    }
                    if (foundPresses)
                    {
                        minPresses += numPresses + 1;
                        break;
                    }
                    curLights = nextLights;
                    numPresses++;
                }
            }

            return minPresses.ToString();
        }

        public string Part2(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            int minPresses = 0;
            foreach (string machine in RawInput)
            {
                string[] mComp = machine.Split(' ');
                int[][] buttons = mComp[1..^1].Select(b => b.Trim('(', ')').Split(',').Select(l => int.Parse(l)).ToArray()).ToArray();
                int[] joltageGuide = mComp[^1].Trim(['{', '}']).Split(',').Select(j => int.Parse(j)).ToArray();

                int[] goalVector = joltageGuide;

                int[,] buttonMatrix = new int[joltageGuide.Length, buttons.Length];
                for (int i = 0; i < buttons.Length; i++)
                {
                    foreach (int idx in buttons[i])
                    {
                        buttonMatrix[idx, i] = 1;
                    }
                }

                Solver solver = Solver.CreateSolver("SCIP");

                int numButtons = buttonMatrix.GetLength(1);
                int numRows = buttonMatrix.GetLength(0);

                // Variables: X[i] >= 0, integer
                Variable[] X = new Variable[numButtons];
                for (int i = 0; i < numButtons; i++)
                {
                    X[i] = solver.MakeIntVar(0, int.MaxValue, $"X_{i}");
                }

                // Constraints: buttonMatrix * X = goalMatrix
                for (int r = 0; r < numRows; r++)
                {
                    Constraint ct = solver.MakeConstraint(goalVector[r], goalVector[r]);
                    for (int i = 0; i < numButtons; i++)
                    {
                        ct.SetCoefficient(X[i], buttonMatrix[r, i]);
                    }
                }

                // Objective: Minimize sum(X)
                Objective objective = solver.Objective();
                for (int i = 0; i < numButtons; i++)
                {
                    objective.SetCoefficient(X[i], 1);
                }

                objective.SetMinimization();

                // Solve
                Solver.ResultStatus resultStatus = solver.Solve();
                int[] solution = X.Select(x => (int)x.SolutionValue()).ToArray();
                minPresses += solution.Sum();
            }

            return minPresses.ToString();
        }

        public string Part2old(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            int minPresses = 0;
            foreach (string machine in RawInput)
            {
                string[] mComp = machine.Split(' ');
                int[][] buttons = mComp[1..^1].Select(b => b.Trim('(', ')').Split(',').Select(l => int.Parse(l)).ToArray()).ToArray();
                int[] joltageGuide = mComp[^1].Trim(['{', '}']).Split(',').Select(j => int.Parse(j)).ToArray();

                int numPresses = 0;
                List<int[]> curJoltages = [new int[joltageGuide.Length]];
                HashSet<int[]> seen = new(new IntArrayComparer());
                bool foundPresses = false;

                while (curJoltages.Count > 0)
                {
                    List<int[]> nextJoltages = [];
                    foreach (int[] prevJoltage in curJoltages)
                    {
                        foreach (int[] button in buttons)
                        {
                            int[] nextJoltage = (int[])prevJoltage.Clone();
                            Array.ForEach(button, idx => nextJoltage[idx]++);
                            if (nextJoltage.SequenceEqual(joltageGuide))
                            {
                                foundPresses = true;
                                break;
                            }
                            if (nextJoltage.Zip(joltageGuide, (a, b) => a > b).Any(x => x))
                            {
                                continue;
                            }
                            if (!seen.Contains(nextJoltage))
                            {
                                seen.Add(nextJoltage);
                                nextJoltages.Add(nextJoltage);
                            }
                        }
                    }
                    if (foundPresses)
                    {
                        minPresses += numPresses + 1;
                        break;
                    }
                    curJoltages = nextJoltages;
                    numPresses++;
                }
            }

            return minPresses.ToString();
        }

    }

    internal class BoolArrayComparer : IEqualityComparer<bool[]>
    {
        public bool Equals(bool[] x, bool[] y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(bool[] obj)
        {
            if (obj == null)
            {
                return 0;
            }

            int hash = 17;
            foreach (bool b in obj)
            {
                hash = (hash * 31) + b.GetHashCode();
            }

            return hash;
        }
    }

    internal class IntArrayComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(int[] obj)
        {
            if (obj == null)
            {
                return 0;
            }

            int hash = 17;
            foreach (int i in obj)
            {
                hash = (hash * 31) + i.GetHashCode();
            }

            return hash;
        }
    }
}