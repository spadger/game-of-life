using GameOfLife;
using Spectre.Console;

const int width = 50;
const int height = 50;

var engine = new Engine(width, height, InitialState.Get());

var canvas = new Canvas(width, height);
AnsiConsole.Live(canvas).Start(ctx =>
{
    foreach (var state in engine)
    {
        var grid = state.GetGrid();
        foreach (var cell in grid)
        {
            canvas.SetPixel(cell.X, cell.Y, cell.Alive ? Color.Yellow2 : Color.DarkViolet);
        }

        ctx.Refresh();
        Thread.Sleep(10);
    }
});

public record Coordinate(int X, int Y);