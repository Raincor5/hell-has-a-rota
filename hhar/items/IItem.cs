using hhar.characters.stats;
using hhar.characters.inventory;
using hhar.characters;
namespace hhar.items {
    public interface IItem {
        string Name { get; set; }
        string Description { get; set; }
        List<StatModifier> StatModifiers { get; set; }

        public List<StatModifier> GetStatModifiers();
        public dynamic OnUse<T>(Player player, T target);
        bool Activate(Player player, object? target) => false;
        public EquipmentSlots EquipmentSlot { get; }
    }
}
