using System.Collections;

public class Engine : IEnumerable<GameState>
{
    private int Width { get; }
    private int Height { get; }


    private GameState _state;

    public Engine(int width, int height, IEnumerable<Coordinate> initialState)
    {
        Width = width;
        Height = height;

        _state = GameState.Initialise(width, height, initialState);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<GameState> GetEnumerator()
    {
        while (true)
        {
           
            yield return _state;
            _state = _state.Next();
        }
    }
}