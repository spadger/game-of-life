namespace GameOfLife;

public class GameState
{
    public static GameState Initialise(int width, int height, IEnumerable<Coordinate> initialState)
    {
        var flatState = new bool[width * height];

        foreach (var coordinate in initialState)
        {
            var flatPos = ToFlatPosition(width, coordinate.X, coordinate.Y);
            flatState[flatPos] = true;
        }

        return new GameState(0, width, height, flatState);
    }


    private uint Generation { get; }
    private readonly int _width;
    private readonly int _height;
    private readonly bool[] _currentState;

    public GameState(uint generation, int width, int height, bool[] currentState)
    {
        Generation = generation;
        _width = width;
        _height = height;
        _currentState = currentState;
    }

    public GameState Next()
    {
        var newState = _currentState.Select((x, i) =>
        {
            var position = FromFlatPosition(i);
            return NewState(x, position);
        });

        return new GameState(Generation + 1, _width, _height, newState.ToArray());
    }

    private bool NewState(bool currentlyAlive, Coordinate coordinate)
    {
        var livingNeighbourCount = CountAlive(coordinate.X, coordinate.Y);
        if (!currentlyAlive) return livingNeighbourCount == 3;

        return livingNeighbourCount == 2 || livingNeighbourCount == 3;
    }

    private int CountAlive(int x, int y)
    {
        var results = new[]
        {
            IsAlive(x - 1, y - 1),
            IsAlive(x, y - 1),
            IsAlive(x + 1, y - 1),
            IsAlive(x - 1, y),
            IsAlive(x + 1, y),
            IsAlive(x - 1, y + 1),
            IsAlive(x, y + 1),
            IsAlive(x + 1, y + 1)
        };

        return results.Count(it => it);
    }

    private bool IsAlive(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _width || y >= _height)
        {
            return false;
        }

        var arrayPos = ToFlatPosition(_width, x, y);
        return _currentState[arrayPos];
    }

    private static int ToFlatPosition(int totalWidth, int x, int y) => (totalWidth * y) + x;

    private Coordinate FromFlatPosition(int position)
    {
        var x = position % _width;
        var y = (position - x) / _width;

        return new Coordinate(x, y);
    }

    public IEnumerable<Cell> GetGrid()
    {
        return _currentState.Select((x, i) =>
        {
            var position = FromFlatPosition(i);
            return new Cell(position.X, position.Y, x);
        });
    }
}

public record Cell(int X, int Y, bool Alive) : Coordinate(X, Y);