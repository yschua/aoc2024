class Day02_2
{
    static void Main(string[] args)
    {
        bool CheckInner(List<int> levels)
        {
            bool increasing = true;
            bool decreasing = true;
            for (var i = 0; i < levels.Count - 1; i++)
            {
                var delta = levels[i + 1] - levels[i];
                increasing = increasing && delta > 0;
                decreasing = decreasing && delta < 0;
                if (!increasing && !decreasing)
                {
                    return false;
                }
                if (Math.Abs(delta) > 3)
                {
                    return false;
                }
            }
            return true;
        }

        bool CheckOuter(List<int> levels)
        {
            if (CheckInner(levels))
            {
                return true;
            }
            for (var i = 0; i < levels.Count; i++)
            {
                var copy = levels.ToList();
                copy.RemoveAt(i);
                if (CheckInner(copy))
                {
                    return true;
                }
            }
            return false;
        }

        var lines = File.ReadAllLines(args[0]);
        var count = 0;
        foreach (var line in lines)
        {
            var levels = line.Split(' ').Select(x => int.Parse(x)).ToList();
            if (CheckOuter(levels))
            {
                count++;
            }
        }
        Console.WriteLine(count);
    }
}