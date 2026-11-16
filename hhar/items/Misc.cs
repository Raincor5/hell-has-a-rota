using hhar.characters.stats;
using hhar.characters.inventory;
using hhar.characters;
namespace hhar.items
{
    public class Misc : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }

        private Func<Player, object?, bool> _onUse;
        public List<StatModifier> StatModifiers { get; set; }

        public EquipmentSlots EquipmentSlot => EquipmentSlots.NotEquipment;

        public Misc(string name, string description, List<StatModifier> statModifiers, Func<Player, object?, bool> useAction = null)
        {
            Name = name;
            Description = description;
            StatModifiers = statModifiers;
            _onUse = useAction;
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
            return _onUse(player, target);
        }
    }
}