using hhar.items;
namespace hhar.characters.inventory
{
    public class PlayerEquipment : IEquipment
    {
        
        public Dictionary<EquipmentSlots, EquipmentSlot> Equipment { get; set; }
        public PlayerEquipment()
        {
            IEnumerable<EquipmentSlots> slots = Enum.GetValues(typeof(EquipmentSlots)).Cast<EquipmentSlots>();
            Equipment = new Dictionary<EquipmentSlots, EquipmentSlot>();
            foreach (var slot in slots)
            {
                Equipment[slot] = new EquipmentSlot();
            }
        }

        public void EquipItem(IItem item, EquipmentSlots slot)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(slot));
            // Implementation for equipping an item to a specific slot
            if (Equipment.ContainsKey(slot) && Equipment[slot].isEmpty())
            {
                Equipment[slot].AddItem(item);
            }
            else
            {
                throw new ArgumentException($"Invalid equipment slot: {slot}");
            }
        }

        public List<IItem?> GetEquippedItems()
        {
            var equippedItems = new List<IItem?>();
            foreach (var slot in Equipment.Values)
            {
                equippedItems.Add(slot.Item);
            }
            return equippedItems;
        }
    }
}