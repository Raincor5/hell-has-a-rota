using hhar.items;
namespace hhar.characters.inventory
{
    public interface IInventory
    {
        List<IItem> Items {get; set;}
        void AddItem(IItem item);
        void RemoveItem(IItem item);
    }
}