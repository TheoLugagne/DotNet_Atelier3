using Labyrinth.Items;
using Labyrinth.Tiles;

namespace Labyrinth.Crawl
{
    /// <summary>
    /// Crawler moving through the labyrinth.
    /// </summary>
    public class Crawler(Labyrinth labyrinth, int startX, int startY, Tile facingTile) : ICrawler
    {
        private Labyrinth Labyrinth { get; } = labyrinth;
        public int X { get; private set; } = startX;

        public int Y { get; private set; } = startY;

        public Direction Direction {get; set; } = Direction.North;

        public Tile FacingTile { get; set; } = facingTile;
        
        public MyInventory Inventory { get; } = new MyInventory();

        public Inventory Walk()
        {
            var inventory = FacingTile.Pass();
            (X, Y) = GetFacingTileCoordinates();
            UpdateFacingTile();
            return inventory;
        }
        
        public void TurnLeft()
        {
            Direction.TurnLeft();
            UpdateFacingTile();
        }

        public void TurnRight()
        {
            Direction.TurnRight();
            UpdateFacingTile();
        }
        
        public void TurnAround()
        {
            Direction.TurnRight();
            Direction.TurnRight();
            UpdateFacingTile();
        }
        
        private void UpdateFacingTile()
        {
            var (x, y) = GetFacingTileCoordinates();
            FacingTile = Labyrinth.GetTile(x, y);
        }
        
        private (int, int) GetFacingTileCoordinates() => (X + Direction.DeltaX, Y + Direction.DeltaY);
        
        public void pickUp(Inventory inventory)
        {
            Inventory.MoveItemFrom(inventory);
        }
    }
}