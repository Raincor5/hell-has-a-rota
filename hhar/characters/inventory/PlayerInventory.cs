using hhar.items;
namespace hhar.characters.inventory
{
    public class PlayerInventory : IInventory
    {
        public Dictionary<int, InventorySlot> Items { get; set; }
        public IEquipment Equipment { get; set; }

        public PlayerInventory(int slotCount = 20)
        {
            if (slotCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slotCount));
            }

            Items = new Dictionary<int, InventorySlot>();
            for (var slot = 0; slot < slotCount; slot++)
            {
                Items[slot] = new InventorySlot();
            }

            Equipment = new PlayerEquipment();
        }

        public void AddItem(IItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var slot = Items.FirstOrDefault(entry => entry.Value.IsEmpty);
            if (slot.Equals(default(KeyValuePair<int, InventorySlot>)))
            {
                throw new InvalidOperationException("The inventory is full.");
            }

            AddItem(item, slot.Key);
        }

        public void AddItem(IItem item, int slot)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (!Items.TryGetValue(slot, out var inventorySlot))
            {
                throw new ArgumentException($"Invalid inventory slot: {slot}", nameof(slot));
            }

            if (!inventorySlot.IsEmpty)
            {
                throw new ArgumentException($"Inventory slot is occupied: {slot}", nameof(slot));
            }

            inventorySlot.AddItem(item);
        }

        public void RemoveItem(IItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var slot = Items.Values.FirstOrDefault(entry => entry.Item?.Equals(item) == true);
            slot?.Clear();
        }
    }
}