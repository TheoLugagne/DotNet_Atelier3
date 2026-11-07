using Labyrinth.Crawl;

namespace Labyrinth.Events;

public class CrawlingEventArgs(int x, int y, Direction direction) : EventArgs
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public Direction Direction { get; } = (Direction)direction.Clone();
}