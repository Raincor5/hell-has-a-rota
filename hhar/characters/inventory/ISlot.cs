using hhar.items;
namespace hhar.characters.inventory
{
    public interface ISlot
    {
        bool IsEmpty { get; }
        IItem? Item { get; }
        int Quantity { get; }

        void AddItem(IItem item, int quantity = 1); // Add more items of the same type to the slot
        void RemoveItem(int quantity = 1); // Remove a certain quantity of items from the slot
        void Clear(); // Clear the slot, removing all items

        bool isEmpty(); // Check if the slot is empty
    }
}