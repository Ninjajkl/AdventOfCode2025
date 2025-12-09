namespace AdventOfCode2025
{
    internal class Day07
    {
        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            HashSet<int> beamIndexes = [];
            beamIndexes.Add(RawInput[0].IndexOf('S'));
            int numSplit = 0;

            for (int i = 1; i < RawInput.Length; i++)
            {
                HashSet<int> workingBeams = [.. beamIndexes];
                foreach (int beamIndex in beamIndexes)
                {
                    if (RawInput[i][beamIndex] == '^')
                    {
                        workingBeams.Add(beamIndex-1);
                        workingBeams.Add(beamIndex+1);
                        workingBeams.Remove(beamIndex);
                        numSplit++;
                    }
                }
                beamIndexes = workingBeams;
            }

            return numSplit.ToString();
        }

        public string Part2(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            Dictionary<int, long> beamIndexes = [];
            beamIndexes.Add(RawInput[0].IndexOf('S'), 1);

            for (int i = 1; i < RawInput.Length; i++)
            {
                Dictionary<int, long> workingBeams = new(beamIndexes);
                foreach (KeyValuePair<int, long> beamInfo in beamIndexes)
                {
                    int index = beamInfo.Key;
                    long numBeams = beamInfo.Value;
                    if (RawInput[i][index] == '^')
                    {
                        long leftBeamPrevVal = workingBeams.ContainsKey(index-1) ? workingBeams[index-1] : 0;
                        workingBeams[index-1] = leftBeamPrevVal + numBeams;
                        long rightBeamPrevVal = workingBeams.ContainsKey(index+1) ? workingBeams[index+1] : 0;
                        workingBeams[index+1] = rightBeamPrevVal + numBeams;
                        workingBeams.Remove(index);
                    }
                }
                beamIndexes = workingBeams;
            }

            return beamIndexes.Values.Sum().ToString();
        }
    }
}