namespace hhar.Tests;

using hhar.characters.stats;
using hhar.characters.stats.classes;
using hhar.characters.stats.gifts;
using hhar.characters;

public class TestCharacterCreation
{
    [Theory]
    [InlineData(
        2,
        1,
        ClassList.Slave,
        GiftList.LastRation,
        25, 15, 5, 5, 0, 5, 5, 5, 5)]
    [InlineData(
        8,
        8,
        ClassList.Butcher,
        GiftList.MemoryOfHome,
        20, 10, 5, 10, 10, 5, 5, 5, 5)]
    public void Create_CreatesExpectedStats(
        int classChoice,
        int giftChoice,
        ClassList expectedClass,
        GiftList expectedGift,
        int expectedHealth,
        int expectedStamina,
        int expectedAttackDamage,
        int expectedDefense,
        int expectedHostility,
        int expectedIntelligence,
        int expectedWisdom,
        int expectedCharisma,
        int expectedLuck)
    {
        var originalInput = Console.In;
        Console.SetIn(new StringReader($"Ada\n{classChoice}\n{giftChoice}\n1\n1\n"));

        try
        {
            var createdCharacter = CharacterCreation.Create();
            var stats = createdCharacter.Player.Stats;

            Assert.Equal("Ada", createdCharacter.Player.Name);
            Assert.Equal(expectedClass, createdCharacter.StartingClass);
            Assert.Equal(expectedGift, createdCharacter.StartingGift);
            Assert.Equal(
                new List<int>
                {
                    expectedHealth,
                    expectedStamina,
                    expectedAttackDamage,
                    expectedDefense,
                    expectedHostility,
                    expectedIntelligence,
                    expectedWisdom,
                    expectedCharisma,
                    expectedLuck
                },
                stats.GetStatsAsList());
        }
        finally
        {
            Console.SetIn(originalInput);
        }
    }
}