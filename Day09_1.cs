class Day09_1
{
    static void Main(string[] args)
    {
        var line = File.ReadAllLines(args[0])[0];
        var blocks = new List<int>();
        foreach (var i in Enumerable.Range(0, line.Length))
        {
            var value = (i % 2 == 0) ? i / 2 : -1;
            var count = line[i] - '0';
            blocks.AddRange(Enumerable.Repeat(value, count));
        }

        int FindFreeRight(int idx)
        {
            while (blocks[idx] != -1)
            {
                idx++;
            }
            return idx;
        }

        int FindFileLeft(int idx)
        {
            while (blocks[idx] == -1)
            {
                idx--;
            }
            return idx;
        }

        var leftIdx = 0;
        var rightIdx = blocks.Count - 1;
        while (true)
        {
            leftIdx = FindFreeRight(leftIdx);
            rightIdx = FindFileLeft(rightIdx);
            if (leftIdx >= rightIdx)
            {
                break;
            }
            blocks[leftIdx] = blocks[rightIdx];
            blocks[rightIdx] = -1;
            leftIdx++;
            rightIdx--;
        }

        long checksum = 0;
        foreach (var i in Enumerable.Range(0, blocks.Count))
        {
            if (blocks[i] != -1)
            {
                checksum += i * blocks[i];
            }
        }
        Console.WriteLine(checksum);
    }
}