using Labyrinth.Events;
using Labyrinth.Items;
using Labyrinth.Tiles;

namespace Labyrinth.Build
{
    public interface IBuilder
    {
        static readonly (int, int) InvalidPosition = (-1, -1);

        event EventHandler<StartEventArgs>? StartPositionFound;

        Tile[,] Build();
    }
}
