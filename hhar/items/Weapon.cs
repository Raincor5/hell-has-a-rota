using hhar.characters.stats;
using hhar.characters;
using hhar.characters.inventory;
namespace hhar.items
{
    public class Weapon : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }
        public List<StatModifier> StatModifiers { get; set; }

        public EquipmentSlots EquipmentSlot { get; }
        private readonly Func<Player, object?, bool> _action;
        public bool Activate(Player player, object? target) => _action(player, target);

        public Weapon(string name, string description, int damage, List<StatModifier> statModifiers,
            EquipmentSlots slot = EquipmentSlots.RightHand, Rarity rarity = Rarity.Common)
        {
            if (slot is not (EquipmentSlots.LeftHand or EquipmentSlots.RightHand))
                throw new ArgumentOutOfRangeException(nameof(slot));
            EquipmentSlot = slot;
            _action = ItemActions.CreateAction(slot, statModifiers, rarity);
            Name = name;
            Description = description;
            Damage = damage;
            StatModifiers = statModifiers;
        }

        public List<StatModifier> GetStatModifiers()
        {
            return StatModifiers;
        }

        public dynamic OnUse<T>(Player player, T target)
        {
            // Implementation for using the item
            Console.WriteLine($"{Name} has been equipped.");
            if (target is CharacterStats stats)
            {
                var statsToApply =  GetStatModifiers();
                stats.AddModifiers(statsToApply);
                return this;   
            }
            return null; // Or some other appropriate return value if target is not null
        }

        public void Action(ICharacter owner, ICharacter target)
        {
            // Implementation of usage action for the weapon, e.g., attack
            Console.WriteLine($"{Name} is used to attack, dealing {Damage} damage.");
            // Here you can implement the logic to apply damage to the target character
            owner.DealDamage(target, this);
        }
    }
}