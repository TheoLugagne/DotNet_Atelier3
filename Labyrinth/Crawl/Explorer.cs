using Labyrinth.Tiles;

namespace Labyrinth.Crawl
{
    /// <summary>
    /// Explores a labyrinth by choosing random directions.
    /// </summary>
    public class Explorer(ICrawler crawler, Random? random = null)
    {
        private readonly Random _random = random ?? Random.Shared;

        public void GetOut(int n)
        {
            for (var move = 0; move < n; move++)
            {
                if (crawler.FacingTile is Outside)
                {
                    return;
                }

                var turns = _random.Next(4);
                for (var turn = 0; turn < turns; turn++)
                {
                    crawler.TurnRight();
                }

                var canWalk = false;
                for (var direction = 0; direction < 4; direction++)
                {
                    if (crawler.FacingTile is Outside)
                    {
                        return;
                    }

                    if (crawler.FacingTile.IsTraversable)
                    {
                        canWalk = true;
                        break;
                    }

                    crawler.TurnRight();
                }

                if (!canWalk)
                {
                    return;
                }

                crawler.Walk();
                if (crawler.FacingTile is Outside)
                {
                    return;
                }
            }
        }
    }
}
