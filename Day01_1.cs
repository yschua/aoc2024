using System.Text.RegularExpressions;

class Day01_1
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        List<int> llist = new();
        List<int> rlist = new();
        foreach (var line in lines)
        {
            var m = Regex.Match(line, @"(\d+)\s+(\d+)");
            llist.Add(int.Parse(m.Groups[1].Value));
            rlist.Add(int.Parse(m.Groups[2].Value));
        }
        llist.Sort();
        rlist.Sort();
        var distance = 0;
        foreach (var (l, r) in llist.Zip(rlist, (l, r) => (l, r)))
        {
            distance += Math.Abs(l - r);
        }
        Console.WriteLine(distance);
    }
}