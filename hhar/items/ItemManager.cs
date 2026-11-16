using hhar.characters.stats;
namespace hhar.items
{
    public class ItemManager
    {
        private readonly Dictionary<int, IItem> _items;

        public ItemManager()
        {
            _items = new Dictionary<int, IItem>();
        }
        // Register all the items from the enum class setting their custom behaviours
        public void RegisterItems()
        {
            // Register all the items from the enum class setting their custom behaviours
            foreach (ItemIds itemName in Enum.GetValues(typeof(ItemIds)))
            {
                IItem item = itemName switch
                {
                    // TODO: Items go brrr here
                };

                _items.Add((int)itemName, item);
            }
        }
    }
}