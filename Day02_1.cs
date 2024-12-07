class Day02_1
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        var safe = 0;
        foreach (var line in lines)
        {
            var levels = line.Split(' ').Select(x => int.Parse(x)).ToList();
            bool Check()
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
            if (Check())
            {
                safe++;
            }
        }
        Console.WriteLine(safe);
    }
}