using hhar.items;

namespace hhar.characters.inventory
{
    public class InventorySlot : ISlot
    {
        public bool IsEmpty => Item is null;
        public IItem? Item { get; private set; }
        public int Quantity => _quantity;

        private int _quantity;

        public void AddItem(IItem item, int quantity = 1)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (Item is null || Item.Equals(item))
            {
                Item = item;
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

        public InventorySlot(IItem? item = null, int quantity = 0)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            Item = item;
            _quantity = quantity;
        }

        public void UseItem()
        {
            if (Item is null)
            {
                throw new InvalidOperationException("No item to use in this slot.");
            }
            
            Item.OnUse();
            RemoveItem(1);
        }
    }
}
