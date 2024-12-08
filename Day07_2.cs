class Day07_2
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        long total = 0;

        foreach (var line in lines)
        {
            var testValue = long.Parse(line.Split(':')[0]);
            var numbers = line.Split(':')[1].Split(' ').Skip(1).Select(x => long.Parse(x)).ToList();
            var bitWidth = numbers.Count - 1;

            IEnumerable<List<int>> GetNextCounter()
            {
                var numOperators = 3;
                var counter = Enumerable.Repeat(0, bitWidth).ToList();

                while (true)
                {
                    yield return counter;

                    if (counter.All(x => x == numOperators - 1))
                    {
                        break;
                    }

                    bool incrementNext = true;
                    for (int i = 0; incrementNext; i++)
                    {
                        counter[i]++;
                        incrementNext = counter[i] == numOperators;
                        if (incrementNext)
                        {
                            counter[i] = 0;
                        }
                    }
                }
            }

            bool Test()
            {
                foreach (var counter in GetNextCounter())
                {
                    long result = numbers[0];
                    foreach (var bit in Enumerable.Range(0, bitWidth))
                    {
                        var next = numbers[bit + 1];
                        result = counter[bit] switch
                        {
                            0 => result + next,
                            1 => result * next,
                            2 => long.Parse($"{result}{next}"),
                        };
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