using hhar.items;
using hhar.characters.stats;
using hhar.characters.progression;
using hhar.characters.inventory;
namespace hhar.characters {
    public class Player : ICharacter
    {
        public string Name { get; set; }
        public LevelProgression LevelProgression { get; set; }
        public CharacterStats Stats { get; set; }
        public IInventory Inventory { get; set; }

        public Player(string name)
        {
            Name = name;
            LevelProgression = new LevelProgression(5);
            Stats = new CharacterStats();
            Inventory = new PlayerInventory();
        }
    }
}