using Labyrinth.Items;
using Labyrinth.Tiles;

namespace Labyrinth.Crawl
{
    /// <summary>
    /// Labyrinth crawler interface.
    /// </summary>
    public interface ICrawler
    {
        /// <summary>
        /// Gets the current X position.
        /// </summary>
        int X { get; }

        /// <summary>
        /// Gets the current Y position.
        /// </summary>
        int Y { get; }

        /// <summary>
        /// Gets the current direction.
        /// </summary>
        Direction Direction { get; }

        /// <summary>
        /// Gets the tile in front of the crawler.
        /// </summary>
        Tile FacingTile { get; }

        /// <summary>
        /// Pass the tile in front of the crawler and move into it.
        /// </summary>
        /// <returns>An inventory of the collectable items in the place reached.</returns>
        Inventory Walk();

        /// <summary>
        /// Turn 90 degrees to the left.
        /// </summary>
        void TurnLeft();

        /// <summary>
        /// Turn 90 degrees to the right.
        /// </summary>
        void TurnRight();

        /// <summary>
        /// Turn 180 degrees.
        /// </summary>
        void TurnAround();
    }
}