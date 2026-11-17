using hhar.characters;
using hhar.characters.inventory;
using hhar.characters.stats;

namespace hhar.items;

public sealed class Accessory : IItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<StatModifier> StatModifiers { get; set; }

    public EquipmentSlots EquipmentSlot => EquipmentSlots.Accessory;
    public Rarity Rarity { get; }

    public Accessory(string name, string description, List<StatModifier> modifiers, Rarity rarity)
    {
        Name = name;
        Description = description;
        StatModifiers = modifiers;
        Rarity = rarity;
    }

    public List<StatModifier> GetStatModifiers()
    {
        return StatModifiers;
    }

    public bool Activate(Player player, object? target) => false;

    public dynamic OnUse<T>(Player player, T target)
    {
        if (target is CharacterStats stats)
        {
            stats.AddModifiers(StatModifiers);
            return this;
        }

        return false;
    }
}