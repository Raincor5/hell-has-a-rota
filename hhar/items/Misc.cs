using hhar.characters.stats;
namespace hhar.items
{
    public class Misc : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<StatModifier> StatModifiers { get; set; }

        public Misc(string name, string description, List<StatModifier> statModifiers)
        {
            Name = name;
            Description = description;
            StatModifiers = statModifiers;
        }
    
        public List<StatModifier> GetStatModifiers()
        {
            return StatModifiers;
        }
    }
}