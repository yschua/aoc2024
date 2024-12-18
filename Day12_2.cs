class Day12_2
{
    record struct Vector(int X, int Y)
    {
        public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector operator *(Vector a, Vector b) => new(a.X * b.X, a.Y * b.Y);
        public static Vector Up => new Vector(0, 1);
        public static Vector Down => new Vector(0, -1);
        public static Vector Left => new Vector(-1, 0);
        public static Vector Right => new Vector(1, 0);
        public List<Vector> Neighbours => new List<Vector>
        {
            this + Up, this + Down, this + Left, this + Right
        };
    }

    class Grid<T> where T : new()
    {
        private List<List<T>> _grid = new();
        private HashSet<Vector> _visited = new();

        public Grid(int row, int col)
        {
            _grid = Enumerable.Repeat(Enumerable.Repeat(new T(), col).ToList(), row).ToList();
        }

        public static Grid<char> Create(string path)
        {
            var lines = File.ReadAllLines(path);
            var grid = new Grid<char>(0, 0);
            grid._grid = lines.Select(line => line.ToList()).ToList();
            return grid;
        }

        public T this[Vector vec]
        {
            get => _grid[vec.Y][vec.X];
            set => _grid[vec.Y][vec.X] = value;
        }

        public void Visit(Vector vec) => _visited.Add(vec);

        public bool HasVisit(Vector vec) => _visited.Contains(vec);

        public int Rows => _grid.Count;

        public int Columns => _grid[0].Count;

        public IEnumerable<Vector> Vectors => Enumerable.Range(0, Rows).SelectMany(
            (y, row) => Enumerable.Range(0, Columns).Select(x => new Vector(x, y)));

        public Vector Find(T value) => Vectors.First(vec => this[vec].Equals(value));

        public bool OutBoundary(Vector vec) => vec.X < 0 || vec.X >= Columns || vec.Y < 0 || vec.Y >= Rows;

        public bool OnBoundary(Vector vec) => vec.X == 0 || vec.X == Columns - 1 || vec.Y == 0 || vec.Y == Rows - 1;

        public void Print()
        {
            Console.Write(" ");
            Console.WriteLine(string.Join("", Enumerable.Range(0, Columns).Select(col => col % 10)));
            foreach (var row in Enumerable.Range(0, Rows))
            {
                Console.Write(row % 10);
                Console.WriteLine(string.Join("", _grid[row]));
            }
        }
    }

    static void Main(string[] args)
    {
        var map = Grid<char>.Create(args[0]);

        void Search(Vector vec, char type, List<Vector> plots)
        {
            if (map.HasVisit(vec))
            {
                return;
            }
            map.Visit(vec);
            plots.Add(vec);
            foreach (var neighbour in vec.Neighbours)
            {
                if (!map.OutBoundary(neighbour) && map[neighbour] == type)
                {
                    Search(neighbour, type, plots);
                }
            }
        }

        bool FormsPerimeter(Vector a, Vector b)
        {
            return map.OutBoundary(a) || map.OutBoundary(b) || map[a] != map[b];
        }

        int CountSides(List<Vector> plots)
        {
            int sides = 0;

            void ProcessSlice(List<int> slice)
            {
                for (int i = 0; i < slice.Count; i++)
                {
                    if (i + 1 == slice.Count || Math.Abs(slice[i] - slice[i + 1]) > 1)
                    {
                        sides++;
                    }
                }
            }

            foreach (var row in Enumerable.Range(0, map.Rows))
            {
                foreach (var offset in new[] {Vector.Up, Vector.Down})
                {
                    ProcessSlice(plots.Where(vec => vec.Y == row && FormsPerimeter(vec, vec + offset))
                        .Select(vec => vec.X).Order().ToList());
                }
            }

            foreach (var col in Enumerable.Range(0, map.Columns))
            {
                foreach (var offset in new[] { Vector.Left, Vector.Right })
                {
                    ProcessSlice(plots.Where(vec => vec.X == col && FormsPerimeter(vec, vec + offset))
                        .Select(vec => vec.Y).Order().ToList());
                }
            }

            return sides;
        }

        var total = 0;
        foreach (var vec in map.Vectors)
        {
            var plots = new List<Vector>();
            Search(vec, map[vec], plots);
            total += plots.Count * CountSides(plots);
        }
        Console.WriteLine(total);
    }
}