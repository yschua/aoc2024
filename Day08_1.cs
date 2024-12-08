class Day08_1
{
    record struct Vector(int X, int Y)
    {
        public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector operator *(Vector a, Vector b) => new(a.X * b.X, a.Y * b.Y);
    }

    class Grid
    {
        private List<List<char>> _grid;

        public Grid(string path)
        {
            var lines = File.ReadAllLines(path);
            _grid = lines.Select(line => line.ToList()).ToList();
        }

        public char this[Vector vec]
        {
            get => _grid[vec.Y][vec.X];
            set => _grid[vec.Y][vec.X] = value;
        }

        public IEnumerable<Vector> Range => Enumerable.Range(0, _grid.Count).SelectMany(
            (y, row) => Enumerable.Range(0, _grid[y].Count).Select(x => new Vector(x, y)));

        public Vector Find(char c)
        {
            foreach (var vec in Range)
            {
                if (this[vec] == c)
                {
                    return vec;
                }
            }
            throw new InvalidOperationException();
        }

        public bool OutOfRange(Vector vec) => vec.X < 0 || vec.X >= _grid[0].Count || vec.Y < 0 || vec.Y >= _grid.Count;

        public bool IsEdge(Vector vec) => vec.X == 0 || vec.X == _grid[vec.Y].Count - 1 || vec.Y == 0 || vec.Y == _grid.Count - 1;
        
        public void Print()
        {
            Console.Write(" ");
            Console.WriteLine(string.Join("", Enumerable.Range(0, _grid[0].Count).Select(col => col % 10)));
            foreach (var row in Enumerable.Range(0, _grid.Count))
            {
                Console.Write(row % 10);
                Console.WriteLine(string.Join("", _grid[row]));
            }
        }
    }

    static void Main(string[] args)
    {
        var grid = new Grid(args[0]);
        var dict = new Dictionary<char, List<Vector>>();

        foreach (var vec in grid.Range)
        {
            var key = grid[vec];
            if (key == '.')
            {
                continue;
            }
            if (!dict.ContainsKey(key))
            {
                dict[key] = new();
            }
            dict[key].Add(vec);
        }

        foreach (var (key, vecs) in dict)
        {
            foreach (var a in vecs)
            {
                foreach (var b in vecs)
                {
                    if (a == b)
                    {
                        continue;
                    }
                    var antinode = (a - b) * new Vector(-1, -1) + b;
                    if (!grid.OutOfRange(antinode))
                    {
                        grid[antinode] = '#';
                    }
                }
            }
        }

        grid.Print();
        var count = 0;
        foreach (var vec in grid.Range)
        {
            if (grid[vec] == '#')
            {
                count++;
            }
        }
        Console.WriteLine(count);
    }
}