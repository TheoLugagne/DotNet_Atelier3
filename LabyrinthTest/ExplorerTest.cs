using Labyrinth.Build;
using Labyrinth.Crawl;
using Labyrinth.Events;
using Labyrinth.Tiles;

namespace LabyrinthTest.Crawl;

[TestFixture]
public class ExplorerTest
{
    [Test]
    public void GetOutStopsAfterMaximumNumberOfMoves()
    {
        var labyrinth = new Labyrinth.Labyrinth(new AsciiParser("""
            +++++
            +   +
            +  x+
            +   +
            +++++
            """));
        var crawler = labyrinth.NewCrawler();
        var explorer = new Explorer(crawler, new FixedRandom(0));

        explorer.GetOut(1);

        using var all = Assert.EnterMultipleScope();
        Assert.That(crawler.X, Is.EqualTo(3));
        Assert.That(crawler.Y, Is.EqualTo(1));
        Assert.That(crawler.FacingTile, Is.TypeOf<Wall>());
    }

    [Test]
    public void GetOutStopsWhenFacingOutside()
    {
        var labyrinth = new Labyrinth.Labyrinth(new AsciiParser("""
            +x+
            +++
            +++
            """));
        var crawler = labyrinth.NewCrawler();
        var explorer = new Explorer(crawler, new FixedRandom(0));

        explorer.GetOut(10);

        using var all = Assert.EnterMultipleScope();
        Assert.That(crawler.X, Is.EqualTo(1));
        Assert.That(crawler.Y, Is.EqualTo(0));
        Assert.That(crawler.FacingTile, Is.TypeOf<Outside>());
    }

    [Test]
    public void GetOutRaisesMovementEventsWithCurrentPositionAndDirection()
    {
        var labyrinth = new Labyrinth.Labyrinth(new AsciiParser("""
            +++++
            +x  +
            +++++
            """));
        var crawler = labyrinth.NewCrawler();
        CrawlingEventArgs? positionChanged = null;
        CrawlingEventArgs? directionChanged = null;
        var positionChangeCount = 0;
        var directionChangeCount = 0;

        crawler.PositionChanged += (_, args) =>
        {
            positionChanged = args;
            positionChangeCount++;
        };
        crawler.DirectionChanged += (_, args) =>
        {
            directionChanged = args;
            directionChangeCount++;
        };

        new Explorer(crawler, new FixedRandom(0)).GetOut(1);

        using var all = Assert.EnterMultipleScope();
        Assert.That(positionChangeCount, Is.EqualTo(1));
        Assert.That(positionChanged, Is.Not.Null);
        Assert.That(positionChanged!.X, Is.EqualTo(2));
        Assert.That(positionChanged.Y, Is.EqualTo(1));
        Assert.That(positionChanged.Direction, Is.EqualTo(Direction.East));
        Assert.That(directionChangeCount, Is.EqualTo(1));
        Assert.That(directionChanged, Is.Not.Null);
        Assert.That(directionChanged!.X, Is.EqualTo(1));
        Assert.That(directionChanged.Y, Is.EqualTo(1));
        Assert.That(directionChanged.Direction, Is.EqualTo(Direction.East));
    }

    private sealed class FixedRandom(int value) : Random
    {
        public override int Next(int maxValue) => value;
    }
}
