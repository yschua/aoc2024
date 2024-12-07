class Day05_1
{
    static void Main(string[] args)
    {
        using var sr = new StreamReader(args[0]);
        var graph = new Dictionary<int, List<int>>();
        string line;
        var result = 0;

        while ((line = sr.ReadLine()) != "")
        {
            var nums = line.Split('|').Select(x => int.Parse(x)).ToList();
            var key = nums[0];
            var value = nums[1];
            if (!graph.ContainsKey(key))
            {
                graph[key] = new();
            }
            graph[key].Add(value);
        }

        bool Check(List<int> pages)
        {
            foreach (var i in Enumerable.Range(0, pages.Count - 1))
            {
                var (prev, next) = (pages[i], pages[i + 1]);
                if (!graph.ContainsKey(prev) || !graph[prev].Contains(next))
                {
                    return false;
                }
            }
            return true;
        }

        while ((line = sr.ReadLine()) != null)
        {
            var pages = line.Split(',').Select(x => int.Parse(x)).ToList();
            if (Check(pages))
            {
                result += pages[pages.Count / 2];
            }
        }

        Console.WriteLine(result);
    }
}