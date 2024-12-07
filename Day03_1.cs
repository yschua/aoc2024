using System.Text.RegularExpressions;

class Day03_1
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        var memoryStr = string.Join("", lines);
        var strToMatch = "mul(";
        var result = 0;
        
        for (var i = 0; i < memoryStr.Length - strToMatch.Length + 1; i++)
        {
            var substr = memoryStr.Substring(i, strToMatch.Length);
            if (substr != strToMatch)
            {
                continue;
            }
            var startIdx = i + substr.Length;
            var endIdx = memoryStr.IndexOf(')', startIdx);
            if (endIdx == -1)
            {
                continue;
            }
            var parameters = memoryStr.Substring(startIdx, endIdx - startIdx);
            var m = Regex.Match(parameters, @"^(\d{1,3}),(\d{1,3})$");
            if (m.Success)
            {
                result += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
            }
        }
        Console.WriteLine(result);
    }
}