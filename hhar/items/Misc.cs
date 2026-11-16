using hhar.characters.stats;
using hhar.characters.inventory;
using hhar.characters;
namespace hhar.items
{
    public class Misc : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<StatModifier> StatModifiers { get; set; }

        public EquipmentSlots EquipmentSlot => EquipmentSlots.NotEquipment;

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

        public dynamic OnUse<T>(Player player, T target)
        {
            // Implementation for using the item
            Console.WriteLine($"{Name} has been used.");
            if (target is CharacterStats stats)
            {
                var statsToApply =  GetStatModifiers();
                stats.AddModifiers(statsToApply);
                return this;   
            }
            return null; // Or some other appropriate return value if target is not null
        }
    }
}