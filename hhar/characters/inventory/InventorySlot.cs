using hhar.items;
using hhar.characters.stats;
namespace hhar.characters.inventory
{
    public class InventorySlot : ISlot
    {
        public bool IsEmpty => Item is null;
        public IItem? Item { get; private set; }
        public int Quantity => _quantity;

        private int _quantity;

        public InventorySlot(IItem? item = null, int quantity = 0)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            Item = item;
            _quantity = quantity;
        }

        public void AddItem(IItem item, int quantity = 1)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            if (Item is null || Item.Equals(item))
            {
                Item ??= item;
                _quantity += quantity;
            }
            else
            {
                throw new InvalidOperationException("Cannot add a different item to the inventory slot.");
            }
        }

        public void RemoveItem(int quantity = 1)
        {
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            if (Quantity >= quantity)
            {
                _quantity -= quantity;
                if (_quantity == 0)
                {
                    Item = null;
                }
            }
        }

        public void Clear()
        {
            Item = null;
            _quantity = 0;
        }

        public bool isEmpty()
        {
            return Item is null || Quantity <= 0;
        }

        public void UseItem(Player player) => UseItem(player, player.Stats);

        public void UseItem(Player player, object? target)
        {
            if (Item is null)
            {
                throw new InvalidOperationException("No item to use in this slot.");
            }
            
            if (Item.EquipmentSlot != EquipmentSlots.NotEquipment)
            {
                try
                {
                    player.Equipment.EquipItem(Item, Item.EquipmentSlot);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error equipping item: {ex.Message}");
                    return;
                }
                if (Item is not Potion)
                    Item.OnUse(player, player.Stats);
                RemoveItem(1);
                return;
            }

            object? result = Item.OnUse<object?>(player, target);
            if (result is null or false) return;
            RemoveItem(1);
        }
    }
}
