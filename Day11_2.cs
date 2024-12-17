class Day11_2
{
    record struct Key(string Stone, int Blinks);

    static void Main(string[] args)
    {
        var stones = File.ReadAllLines(args[0])[0].Split(' ').ToList();
        var dp = new Dictionary<Key, long>();

        long Blink(Key key)
        {
            if (dp.ContainsKey(key))
            {
                return dp[key];
            }

            var stone = key.Stone;
            var blinks = key.Blinks;
            long count = 0;

            if (blinks == 1)
            {
                count = (stone.Length % 2 == 0) ? 2 : 1;
            }
            else if (stone == "0")
            {
                count = Blink(new("1", blinks - 1));
            }
            else if (stone.Length % 2 == 0)
            {
                var mid = stone.Length / 2;
                var left = stone.Substring(0, mid);
                var right = long.Parse(stone.Substring(mid, mid)).ToString();
                count = Blink(new (left, blinks - 1)) + Blink(new(right, blinks - 1));
            }
            else
            {
                var multipled = $"{long.Parse(stone) * 2024}";
                count = Blink(new(multipled, blinks - 1));
            }

            dp[key] = count;
            return count;
        }

        long count = 0;
        foreach (var stone in stones)
        {
            count += Blink(new(stone, 75));
        }
        Console.WriteLine(count);
    }
}