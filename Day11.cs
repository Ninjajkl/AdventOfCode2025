namespace AdventOfCode2025
{
    internal class Day11
    {
        public string Part1(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            Dictionary<string, List<string>> devicesOutputs = [];
            foreach (string deviceConns in RawInput)
            {
                string device = deviceConns[..3];
                List<string> outputs = deviceConns[5..].Split(' ').ToList();
                devicesOutputs.Add(device, outputs);
            }

            Dictionary<string, long> devicePaths = [];

            long numPaths = RecursiveSearch("you", devicesOutputs, devicePaths);

            return numPaths.ToString();
        }

        public string Part2(string inputName)
        {
            string[] RawInput = File.ReadAllLines(inputName);

            Dictionary<string, List<string>> devicesOutputs = [];
            foreach (string deviceConns in RawInput)
            {
                string device = deviceConns[..3];
                List<string> outputs = deviceConns[5..].Split(' ').ToList();
                devicesOutputs.Add(device, outputs);
            }

            Dictionary<string, long[]> devicePaths = [];

            long numPaths = RecursiveSearch2("svr", devicesOutputs, devicePaths)[3];

            return numPaths.ToString();
        }

        public long RecursiveSearch(string device, Dictionary<string, List<string>> devicesOutputs, Dictionary<string, long> devicePaths)
        {
            List<string> deviceOutputs = devicesOutputs[device];
            long pathsToEnd = 0;
            foreach (string outputDevice in deviceOutputs)
            {
                if (devicePaths.TryGetValue(outputDevice, out long numPaths))
                {
                    pathsToEnd += numPaths;
                }
                else if (outputDevice == "out")
                {
                    pathsToEnd += 1;
                }
                else
                {
                    pathsToEnd += RecursiveSearch(outputDevice, devicesOutputs, devicePaths);
                }
            }

            devicePaths[device] = pathsToEnd;

            return pathsToEnd;
        }

        public long[] RecursiveSearch2(string device, Dictionary<string, List<string>> devicesOutputs, Dictionary<string, long[]> devicePaths)
        {
            List<string> deviceOutputs = devicesOutputs[device];
            // None, dac, fft, Both
            long[] pathsToEnd = [0, 0, 0, 0];
            foreach (string outputDevice in deviceOutputs)
            {
                if (devicePaths.TryGetValue(outputDevice, out long[] numPaths))
                {
                    pathsToEnd = pathsToEnd.Zip(numPaths, (a, b) => a + b).ToArray();
                }
                else if (outputDevice == "out")
                {
                    pathsToEnd[0] += 1;
                }
                else
                {
                    long[] p = RecursiveSearch2(outputDevice, devicesOutputs, devicePaths);
                    pathsToEnd = pathsToEnd.Zip(p, (a, b) => a + b).ToArray();
                }
            }

            if (device == "dac")
            {
                pathsToEnd[1] += pathsToEnd[0];
                pathsToEnd[0] = 0;
                pathsToEnd[3] += pathsToEnd[2];
                pathsToEnd[2] = 0;
            }
            else if (device == "fft")
            {
                pathsToEnd[2] += pathsToEnd[0];
                pathsToEnd[0] = 0;
                pathsToEnd[3] += pathsToEnd[1];
                pathsToEnd[1] = 0;
            }

            devicePaths[device] = pathsToEnd;

            return pathsToEnd;
        }
    }
}