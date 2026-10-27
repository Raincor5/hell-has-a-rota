using hhar.items;
using hhar.characters.stats;
using hhar.characters.progression;
using hhar.characters.inventory;
namespace hhar.characters {
    public interface ICharacter
    {
       string Name {get; set;}
        LevelProgression LevelProgression {get; set;}
        CharacterStats Stats {get; set;}
       IInventory Inventory {get; set;}
    }
}