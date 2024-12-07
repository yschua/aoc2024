using System.Text.RegularExpressions;

class Day01_2
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        List<int> llist = new();
        Dictionary<int, int> dict = new();
        List<int> rlist = new();
        foreach (var line in lines)
        {
            var m = Regex.Match(line, @"(\d+)\s+(\d+)");
            llist.Add(int.Parse(m.Groups[1].Value));
            var r = int.Parse(m.Groups[2].Value);
            if (dict.ContainsKey(r))
            {
                dict[r]++;
            }
            else
            {
                dict.Add(r, 1);
            }
        }
        var score = 0;
        foreach (var l in llist)
        {
            if (dict.ContainsKey(l))
            {
                score += l * dict[l];
            }
        }
        Console.WriteLine(score);
    }
}