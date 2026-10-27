using hhar.items;
namespace hhar.characters.inventory
{
    public class PlayerInventory : IInventory
    {
        public List<IItem> Items { get; set; }

        public PlayerInventory()
        {
            Items = new List<IItem>();
        }

        public void AddItem(IItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            Items.Remove(item);
        }
    }
}