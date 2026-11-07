using Labyrinth.Items;
using Labyrinth.Tiles;
using Labyrinth.Events;

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

        public event EventHandler<CrawlingEventArgs>? PositionChanged;

        public event EventHandler<CrawlingEventArgs>? DirectionChanged;
        
        public MyInventory Inventory { get; } = new MyInventory();

        public Inventory Walk()
        {
            var inventory = FacingTile.Pass();
            (X, Y) = GetFacingTileCoordinates();
            UpdateFacingTile();
            PositionChanged?.Invoke(this, new CrawlingEventArgs(X, Y, Direction));
            return inventory;
        }
        
        public void TurnLeft()
        {
            Direction.TurnLeft();
            UpdateFacingTile();
            DirectionChanged?.Invoke(this, new CrawlingEventArgs(X, Y, Direction));
        }

        public void TurnRight()
        {
            Direction.TurnRight();
            UpdateFacingTile();
            DirectionChanged?.Invoke(this, new CrawlingEventArgs(X, Y, Direction));
        }
        
        public void TurnAround()
        {
            Direction.TurnRight();
            Direction.TurnRight();
            UpdateFacingTile();
            DirectionChanged?.Invoke(this, new CrawlingEventArgs(X, Y, Direction));
        }
        
        private void UpdateFacingTile()
        {
            var (x, y) = GetFacingTileCoordinates();
            FacingTile = Labyrinth.GetTile(x, y);
        }
        
        private (int, int) GetFacingTileCoordinates() => (X + Direction.DeltaX, Y + Direction.DeltaY);
        
        public void PickUp(Inventory inventory)
        {
            Inventory.MoveItemFrom(inventory);
        }
    }
}