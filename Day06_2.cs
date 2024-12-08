class Day06_2
{
    record struct Vector(int X, int Y)
    {
        public static Vector None => new Vector(-1, -1);
        public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
    }

    class Grid
    {
        private List<List<char>> _grid;

        public Grid(string path)
        {
            var lines = File.ReadAllLines(path);
            _grid = lines.Select(line => line.ToList()).ToList();
        }

        public char this[Vector point]
        {
            get => _grid[point.Y][point.X];
            set => _grid[point.Y][point.X] = value;
        }

        public IEnumerable<Vector> Points => Enumerable.Range(0, _grid.Count).SelectMany(
            (y, row) => Enumerable.Range(0, _grid[y].Count).Select(x => new Vector(x, y)));

        public Vector Find(char c)
        {
            foreach (var point in Points)
            {
                if (this[point] == c)
                {
                    return point;
                }
            }
            return Vector.None;
        }

        public bool OutOfRange(Vector point)
        {
            return point.X < 0 || point.X >= _grid[0].Count || point.Y < 0 || point.Y >= _grid.Count;
        }

        public bool IsEdge(Vector point)
        {
            return point.X == 0 || point.X == _grid[point.Y].Count - 1 || point.Y == 0 || point.Y == _grid.Count - 1;
        }
    }

    static void Main(string[] args)
    {
        var grid = new Grid(args[0]);

        Vector MoveNext(Grid grid, Vector start, Vector direction)
        {
            var current = start;
            while (true)
            {
                if (grid.OutOfRange(current) || grid[current] == '#')
                {
                    return current - direction;
                }
                current += direction;
            }
        }

        void Mark(Vector start, Vector end, Vector direction)
        {
            for (Vector point = start; point != end; point += direction)
            {
                grid[point] = 'X';
            }
            grid[end] = 'X';
        }

        Vector Rotate(Vector direction)
        {
            if (direction == new Vector(0, -1)) return new(1, 0);
            if (direction == new Vector(1, 0)) return new(0, 1);
            if (direction == new Vector(0, 1)) return new(-1, 0);
            if (direction == new Vector(-1, 0)) return new(0, -1);
            return Vector.None;
        }

        {
            var start = grid.Find('^');
            var direction = new Vector(0, -1);
            var current = start;
            while (!grid.IsEdge(current))
            {
                var next = MoveNext(grid, current, direction);
                Mark(current, next, direction);
                current = next;
                direction = Rotate(direction);
            }
            grid[start] = '^';
        }

        bool CheckLoopWithObstructionAt(Vector point)
        {
            var newGrid = new Grid(args[0]);
            newGrid[point] = '#';
            var current = newGrid.Find('^');
            var direction = new Vector(0, -1);
            var history = new List<(Vector, Vector)>();

            while (!newGrid.IsEdge(current))
            {
                current = MoveNext(newGrid, current, direction);
                direction = Rotate(direction);
                if (history.Contains((current, direction)))
                {
                    return true;
                }
                history.Add((current, direction));
            }

            return false;
        }

        var count = 0;
        foreach (var point in grid.Points)
        {
            if (grid[point] == 'X' && CheckLoopWithObstructionAt(point))
            {
                count++;
            }
        }
        Console.WriteLine(count);
    }
}