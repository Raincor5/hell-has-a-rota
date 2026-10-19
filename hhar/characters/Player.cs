using hhar.items;
using hhar.characters.stats;
using hhar.characters.progression;

namespace hhar.characters {
    public class Player : ICharacter
    {
        public string Name { get; set; }
        public LevelProgression LevelProgression { get; set; }
        public CharacterStats Stats { get; set; }
        public List<IItems> items { get; set; } = new List<IItems>();

        public Player(string name)
        {
            Name = name;
            LevelProgression = new LevelProgression(5);
            Stats = new CharacterStats();
        }
    }
}