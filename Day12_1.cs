class Day12_1
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

    record Region(int Area, int Perimeter)
    {
        public int Area { get; set; }
        public int Perimeter { get; set; }
    }

    static void Main(string[] args)
    {
        var map = Grid<char>.Create(args[0]);

        void Search(Vector vec, char type, Region region)
        {
            if (map.HasVisit(vec))
            {
                return;
            }
            map.Visit(vec);
            region.Area++;
            foreach (var neighbour in vec.Neighbours)
            {
                if (map.OutBoundary(neighbour) || map[neighbour] != type)
                {
                    region.Perimeter++;
                }
                if (!map.OutBoundary(neighbour) && map[neighbour] == type)
                {
                    Search(neighbour, type, region);
                }
            }
        }

        var total = 0;
        foreach (var vec in map.Vectors)
        {
            var region = new Region(0, 0);
            Search(vec, map[vec], region);
            total += region.Area * region.Perimeter;
        }
        Console.WriteLine(total);
    }
}