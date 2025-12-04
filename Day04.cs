namespace AdventOfCode2025
{
    internal class Day04
    {
        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            int numMovable = 0;
            for (int r = 0; r < RawInput.Length; r++)
            {
                for (int c = 0; c < RawInput[r].Length; c++)
                {
                    if (RawInput[r][c] == '.')
                    {
                        continue;
                    }
                    int numAdj = 0;
                    for (int dr = -1; dr <= 1; dr++)
                    {
                        for (int cr = -1; cr <= 1; cr++)
                        {
                            int adjustedRow = r + dr;
                            int adjustedCol = c + cr;
                            if ((dr == 0 && cr == 0) ||adjustedRow < 0 || adjustedRow >= RawInput.Length || adjustedCol < 0 || adjustedCol >= RawInput[0].Length)
                            {
                                continue;
                            }
                            if (RawInput[adjustedRow][adjustedCol] == '@')
                            {
                                numAdj++;
                            }
                        }
                    }
                    if (numAdj < 4)
                    {
                        numMovable++;
                    }
                }
            }

            return numMovable.ToString();
        }

        public string Part2(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);
            int rows = RawInput.Length;
            int cols = RawInput[0].Length;
            char[,] referenceGrid = new char[rows, cols];
            Array.ForEach(RawInput.Select((line, i) => (line, i)).ToArray(), tuple =>
            {
                for (int j = 0; j < cols; j++)
                {
                    referenceGrid[tuple.i, j] = tuple.line[j];
                }
            });

            char[,] modifyingMap = DeepCopyGrid(referenceGrid);

            int numRemoved = 0;
            int lastMoved = -1;
            while (lastMoved != 0)
            {
                lastMoved = 0;
                referenceGrid = DeepCopyGrid(modifyingMap);
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < rows; c++)
                    {
                        if (referenceGrid[r, c] == '.')
                        {
                            continue;
                        }
                        int numAdj = 0;
                        for (int dr = -1; dr <= 1; dr++)
                        {
                            for (int cr = -1; cr <= 1; cr++)
                            {
                                int adjustedRow = r + dr;
                                int adjustedCol = c + cr;
                                if ((dr == 0 && cr == 0) ||adjustedRow < 0 || adjustedRow >= rows || adjustedCol < 0 || adjustedCol >= cols)
                                {
                                    continue;
                                }
                                if (referenceGrid[adjustedRow, adjustedCol] == '@')
                                {
                                    numAdj++;
                                }
                            }
                        }
                        if (numAdj < 4)
                        {
                            numRemoved++;
                            lastMoved++;
                            modifyingMap[r, c] = '.';
                        }
                    }
                }
            }

            return numRemoved.ToString();
        }

        private static char[,] DeepCopyGrid(char[,] source)
        {
            int rows = source.GetLength(0);
            int cols = source.GetLength(1);
            char[,] copy = new char[rows, cols];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    copy[r, c] = source[r, c];
                }
            }

            return copy;
        }
    }
}