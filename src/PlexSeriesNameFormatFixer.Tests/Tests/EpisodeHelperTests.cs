using NUnit.Framework;
using NUnit.Framework.Legacy;
using PlexSeriesNameFormatFixer.DataObjects;
using PlexSeriesNameFormatFixer.Helpers;

[TestFixture]
public class EpisodeParserTests
{
    public EpisodeHelper episodeHelper;

    [SetUp]
    public void SetUp()
    {
        episodeHelper = new EpisodeHelper();
    }

    [TestCase("S01-02", 1, 2)]
    [TestCase("S1-2", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_DashSeparatedFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("S01.E02", 1, 2)]
    [TestCase("S01_E02", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_DotOrUnderscoreSeparatedFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("Season 1 Episode 2", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_LongFormFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("S1E2", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_CompactFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("S1 - E02", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_SpacedDashEFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("S1 - 02", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_SpacedDashFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("S01 E02", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_SpaceSeparatedFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("E02.S01", 1, 2)]
    [TestCase("E02_S01", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_EpisodeFirstDotOrUnderscoreFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("Episode 2 Season 1", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_EpisodeFirstLongFormFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("E2S1", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_EpisodeFirstCompactFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("E02 - S1", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_EpisodeFirstSpacedDashFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("02 - S1", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_EpisodeFirstSimpleDashFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    [TestCase("E02 S01", 1, 2)]
    public void GetNormalizedEpisode_Should_Parse_EpisodeFirstSpaceSeparatedFormat(string input, int expectedSeason, int expectedEpisode)
    {
        TestEpisodeParsing(input, expectedSeason, expectedEpisode);
    }

    private void TestEpisodeParsing(string input, int expectedSeason, int expectedEpisode)
    {
        // Act
        EpisodeInfo result = episodeHelper.GetNormalizedEpisode(input);

        // Assert
        Assert.That(result.Season, Is.EqualTo(expectedSeason));
        Assert.That(result.Episode, Is.EqualTo(expectedEpisode));
        Assert.That(result.Name, Is.EqualTo($"S{result.Season:D2}E{result.Episode:D2}"));
    }
}