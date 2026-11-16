using hhar.characters.stats;
using hhar.characters.inventory;
using hhar.characters;

namespace hhar.items
{
    public class Potion : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<StatModifier> StatModifiers { get; set; }
        private Func<Player, object?, bool> _onUse;
        public EquipmentSlots EquipmentSlot => EquipmentSlots.QuickSlot;

        public Rarity Rarity { get; set; }

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
            return _onUse(player, target); // Or some other appropriate return value if target is not null (usage: OnUse(this, player))
        }
    }
}