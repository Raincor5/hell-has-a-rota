using hhar.items;
namespace hhar.characters.inventory
{
    public interface IInventory
    {
        Dictionary<int, InventorySlot> Items { get; set; }
        void AddItem(IItem item);
        void AddItem(IItem item, int slot);
        void RemoveItem(IItem item);
    }
}