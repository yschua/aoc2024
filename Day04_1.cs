class Day04_1
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        var grid = lines.Select(line => line.ToCharArray()).ToArray();
        var count = 0;

        bool Match(int xStart, int yStart, int xOffset, int yOffset)
        {
            var str = "";
            foreach (var i in Enumerable.Range(0, 4))
            {
                var x = xStart + xOffset * i;
                var y = yStart + yOffset * i;
                if (x < 0 || y < 0 || x >= grid[0].Length || y >= grid.Length)
                {
                    return false;
                }
                str += grid[y][x];
            }
            return str is "XMAS" or "SAMX";
        }

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[0].Length; x++)
            {
                var offsets = new[] { (1, 0), (0, 1), (1, 1), (-1, 1) };
                foreach (var offset in offsets)
                {
                    if (Match(x, y, offset.Item1, offset.Item2))
                    {
                        count++;
                    }
                }
            }
        }

        Console.WriteLine(count);
    }
}