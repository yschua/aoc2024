using System.Text.RegularExpressions;

class Day13_1
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(args[0]);
        var total = 0.0;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] == "")
            {
                continue;
            }

            var m_a = Regex.Match(lines[i], @"Button A: X\+(\d+), Y\+(\d+)");
            var x_a = int.Parse(m_a.Groups[1].Value);
            var y_a = int.Parse(m_a.Groups[2].Value);

            var m_b = Regex.Match(lines[i + 1], @"Button B: X\+(\d+), Y\+(\d+)");
            var x_b = int.Parse(m_b.Groups[1].Value);
            var y_b = int.Parse(m_b.Groups[2].Value);

            var m_p = Regex.Match(lines[i + 2], @"Prize: X=(\d+), Y=(\d+)");
            var x_p = int.Parse(m_p.Groups[1].Value);
            var y_p = int.Parse(m_p.Groups[2].Value);

            var a = 1.0 * (x_b * y_p - x_p * y_b) / (x_b * y_a - x_a * y_b);
            var b = 1.0 * (x_p - a * x_a) / x_b;

            if (a == (int)a && b == (int)b)
            {
                total += 3 * a + b;
            }

            i = i + 2;
        }

        Console.WriteLine((int)total);
    }
}