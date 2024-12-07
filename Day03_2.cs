using System.Text.RegularExpressions;

class Day03_2
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        var memoryStr = string.Join("", lines);
        var result = 0;
        var enable = true;

        string ParseFunction(int startIdx)
        {
            var funcStrs = new[] { "mul(", "do()", "don't()" };
            foreach (var funcStr in funcStrs)
            {
                var substrLength = Math.Min(funcStr.Length, memoryStr.Length - startIdx);
                var substr = memoryStr.Substring(startIdx, substrLength);
                if (substr == funcStr)
                {
                    return substr;
                }
            }
            return "";
        }
        
        for (var i = 0; i < memoryStr.Length; i++)
        {
            var func = ParseFunction(i);
            if (func == "")
            {
                continue;
            }
            else if (func == "do()")
            {
                enable = true;
                i += func.Length - 1;
                continue;
            }
            else if (func == "don't()")
            {
                enable = false;
                i += func.Length - 1;
                continue;
            }
            if (!enable)
            {
                continue;
            }
            var startIdx = i + func.Length;
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