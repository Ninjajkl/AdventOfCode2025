namespace AdventOfCode2025
{
    internal class Day05
    {
        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            Dictionary<long, long> freshTakes = [];

            int numCertifiedFresh = 0;

            bool isIngredients = false;
            foreach (string rawInput in RawInput)
            {
                if (isIngredients)
                {
                    long ingredientID = long.Parse(rawInput);

                    foreach (long key in freshTakes.Keys)
                    {
                        if (key <= ingredientID && freshTakes[key] >= ingredientID)
                        {
                            numCertifiedFresh++;
                            break;
                        }
                    }
                }
                else if (rawInput.Length == 0)
                {
                    isIngredients = true;
                }
                else
                {
                    string[] takeEnds = rawInput.Split('-');
                    long begin = long.Parse(takeEnds[0]);
                    long end = long.Parse(takeEnds[1]);
                    if (freshTakes.ContainsKey(begin))
                    {
                        if (freshTakes[begin] < end)
                        {
                            freshTakes[begin] = end;
                        }
                    }
                    else
                    {
                        freshTakes.Add(begin, end);
                    }
                }
            }

            return numCertifiedFresh.ToString();
        }

        public string Part2(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            Dictionary<long, long> freshTakes = [];

            long numCertifiedFresh = 0;

            foreach (string rawInput in RawInput)
            {
                if (rawInput.Length == 0)
                {
                    break;
                }
                else
                {
                    string[] takeEnds = rawInput.Split('-');
                    long begin = long.Parse(takeEnds[0]);
                    long end = long.Parse(takeEnds[1]);

                    bool contained = false;
                    foreach (long key in freshTakes.Keys)
                    {
                        long val = freshTakes[key];

                        if (key > begin && val < end)
                        {
                            freshTakes.Remove(key);
                            continue;
                        }

                        if (key <= begin && val >= begin)
                        {
                            if (val <= end)
                            {
                                begin = key;
                            }
                            else
                            {
                                contained = true;
                                break;
                            }
                        }
                        else if (key <= end && val >= end)
                        {
                            end = val;
                            freshTakes.Remove(key);
                        }
                    }
                    if (!contained)
                    {
                        freshTakes[begin] = end;
                    }
                }
            }

            foreach (KeyValuePair<long, long> entry in freshTakes.OrderBy(e => e.Key))
            {
                numCertifiedFresh += entry.Value - entry.Key + 1;
            }

            return numCertifiedFresh.ToString();
        }
    }
}