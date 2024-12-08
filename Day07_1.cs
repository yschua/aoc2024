class Day07_1
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        long total = 0;

        foreach (var line in lines)
        {
            var testValue = long.Parse(line.Split(':')[0]);
            var numbers = line.Split(':')[1].Split(' ').Skip(1).Select(x => long.Parse(x)).ToList();

            bool Test()
            {
                var bitWidth = numbers.Count - 1;
                foreach (var counter in Enumerable.Range(0, 1 << bitWidth))
                {
                    long result = numbers[0];
                    foreach (var bit in Enumerable.Range(0, bitWidth))
                    {
                        if (((counter >> bit) & 1) == 0)
                        {
                            result += numbers[bit + 1];
                        }
                        else
                        {
                            result *= numbers[bit + 1];
                        }
                    }
                    if (result == testValue)
                    {
                        return true;
                    }
                }
                return false;
            }

            if (Test())
            {
                total += testValue;
            }
        }

        Console.WriteLine(total);
    }
}