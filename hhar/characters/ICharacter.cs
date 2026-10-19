using hhar.items;
using hhar.characters.stats;
using hhar.characters.progression;
namespace hhar.characters {
    public interface ICharacter
    {
       string Name {get; set;}
        LevelProgression LevelProgression {get; set;}
        CharacterStats Stats {get; set;}
       List<IItems> items {get; set; }
    }
}