using Spectre.Console;

const int width = 50;
const int height = 50;

var initialState = new[]
 {
     new Coordinate(45, 1),
     new Coordinate(43, 2),
     new Coordinate(44, 2),
     new Coordinate(44, 3),
     new Coordinate(45, 3),
     
     
     
     new Coordinate(25, 10),
     new Coordinate(26, 10),
     new Coordinate(24, 11),
     new Coordinate(27, 11),
     new Coordinate(24, 12),
     new Coordinate(27, 12),
     new Coordinate(25, 13),
     new Coordinate(26, 13),
     
    
    
    new Coordinate(13, 30),
    new Coordinate(11,31),
    new Coordinate(13, 31),
    new Coordinate(12, 32),
    new Coordinate(13, 32),

    new Coordinate(22, 32),
    new Coordinate(20,33),
    new Coordinate(21, 33),
    new Coordinate(21, 34),
    new Coordinate(22, 34),
 };

var engine = new Engine(width, height, initialState);

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
        Thread.Sleep(100);
    }
});




public record Coordinate(int X, int Y);