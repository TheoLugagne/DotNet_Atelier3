using Labyrinth.Build;
using Labyrinth.Crawl;
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

    private sealed class FixedRandom(int value) : Random
    {
        public override int Next(int maxValue) => value;
    }
}
