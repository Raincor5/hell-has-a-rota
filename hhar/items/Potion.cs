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
        private Func<Player, object?, bool>? _onUse;

        public Potion() { }

        public Potion(string name, string description, List<StatModifier> modifiers, Rarity rarity)
        {
            Name = name;
            Description = description;
            StatModifiers = modifiers;
            Rarity = rarity;
            _onUse = ItemActions.CreateAction(EquipmentSlot, modifiers, rarity);
        }

        public bool Activate(Player player, object? target)
        {
            _onUse ??= ItemActions.CreateAction(EquipmentSlot, StatModifiers, Rarity);
            return _onUse(player, target);
        }
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
            return Activate(player, target);
        }
    }
}