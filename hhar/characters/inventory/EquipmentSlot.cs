using hhar.items;

namespace hhar.characters.inventory
{
    public class EquipmentSlot : ISlot
    {
        public bool IsEmpty => Item is null;
        public IItem? Item { get; private set; }
        public int Quantity => _quantity;

        private int _quantity = 0;

        public void AddItem(IItem item, int quantity = 1)
        {
            if (Item == null || Item.Equals(item))
            {
                Item = item;
                _quantity += quantity;
            }
            else
            {
                // Handle the case where a different item is being added to the slot
                throw new InvalidOperationException("Cannot add a different item to the equipment slot.");
            }
        }

        public void RemoveItem(int quantity = 1)
        {
            // Implementation for removing items from the equipment slot
            if (Quantity >= quantity)
            {
                _quantity -= quantity;
                if (Quantity <= 0)
                {
                    Item = null; // Clear the item if quantity reaches zero
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
            if (Item == null || Quantity <= 0)
            {
                return true;
            }
            return false;
        }

        public EquipmentSlot(IItem? item = null, int quantity = 0)
        {
            Item = item;
            _quantity = quantity;
        }
    }
}