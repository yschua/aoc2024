class Day04_2
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        var grid = lines.Select(line => line.ToCharArray()).ToArray();
        var count = 0;

        char Get(int x, int y)
        {
            if (x < 0 || y < 0 || x >= grid[0].Length || y >= grid.Length)
            {
                return '\0';
            }
            return grid[y][x];
        }

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[0].Length; x++)
            {
                var center = Get(x, y);
                var topLeft = Get(x - 1, y - 1);
                var topRight = Get(x + 1, y - 1);
                var botLeft = Get(x - 1, y + 1);
                var botRight = Get(x + 1, y + 1);
                var diag1 = $"{topLeft}{center}{botRight}";
                var diag2 = $"{botLeft}{center}{topRight}";
                if (diag1 is "SAM" or "MAS" && diag2 is "SAM" or "MAS")
                {
                    count++;
                }
            }
        }

        Console.WriteLine(count);
    }
}