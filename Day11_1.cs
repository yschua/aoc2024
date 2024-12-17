class Day11_1
{
    static void Main(string[] args)
    {
        var stones = File.ReadAllLines(args[0])[0].Split(' ').ToList();

        List<string> Blink(List<string> stones)
        {
            var blinked = new List<string>();
            foreach (var stone in stones)
            {
                if (stone == "0")
                {
                    blinked.Add("1");
                }
                else if (stone.Length % 2 == 0)
                {
                    var mid = stone.Length / 2;
                    var left = stone.Substring(0, mid);
                    var right = long.Parse(stone.Substring(mid, mid)).ToString();
                    blinked.Add(left);
                    blinked.Add(right);
                }
                else
                {
                    blinked.Add($"{long.Parse(stone) * 2024}");
                }
            }
            return blinked;
        }

        foreach (var _ in Enumerable.Range(0, 25))
        {
            stones = Blink(stones);
        }

        Console.WriteLine(stones.Count);
    }
}