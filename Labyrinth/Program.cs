using Labyrinth.Build;
using Labyrinth.Crawl;
using Labyrinth.Tiles;

var labyrinth = new Labyrinth.Labyrinth(new AsciiParser("""
    +--+--------+
    |  /        |
    |  +--+--+  |
    |     |k    |
    +--+  |  +--+
       |k  x    |
    +  +-------/|
    |           |
    +-----------+
    """));

Console.Write(labyrinth);

var crawler = labyrinth.NewCrawler();
var previousX = crawler.X;
var previousY = crawler.Y;

crawler.PositionChanged += (_, args) =>
{
    Console.SetCursorPosition(previousX, previousY);
    Console.Write(GetTileSymbol(labyrinth.GetTile(previousX, previousY)));
    previousX = args.X;
    previousY = args.Y;
    DrawCrawler(args.X, args.Y, args.Direction);
};
crawler.DirectionChanged += (_, args) => DrawCrawler(args.X, args.Y, args.Direction);

DrawCrawler(crawler.X, crawler.Y, crawler.Direction);
new Explorer(crawler).GetOut(100);
Console.SetCursorPosition(0, labyrinth.Height);

static void DrawCrawler(int x, int y, Direction direction)
{
    var symbol = (direction.DeltaX, direction.DeltaY) switch
    {
        (0, -1) => '^',
        (1, 0) => '>',
        (0, 1) => 'v',
        (-1, 0) => '<',
        _ => throw new ArgumentOutOfRangeException(nameof(direction), "Unknown direction.")
    };

    Console.SetCursorPosition(x, y);
    Console.Write(symbol);
}

static char GetTileSymbol(Tile tile) => tile switch
{
    Room => ' ',
    Wall => '#',
    Door => '/',
    _ => throw new NotSupportedException("Unknown tile type")
};