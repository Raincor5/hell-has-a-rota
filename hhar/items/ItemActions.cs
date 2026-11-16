using hhar.characters.inventory;
using hhar.characters;
namespace hhar.items
{
    public class ItemActions
    {
        // Each item type has a defined action function. For example, a misc item can be used to unlock a door, a potion is always applied on the player, etc.
        // The family of actions are defined using the simillar rules and theme for each of the item types.
        // Left-hand items have defensive actions, right-hand items have offensive actions, quick-slot items have utility actions, and accessory items have passive actions.
        private readonly Dictionary<EquipmentSlots, List<Func<Player, object?, bool>>> _itemActions; //Defines a dictionary that maps each equipment slot (or basically item type) to a list of functions that can be performed.
        private static readonly List<string> _noActionDescriptions = new List<string>
            {
                "This item has no action.",
                "No action.",
                "Maybe your manager knows what to do with this item.",
                "This item has no effect.",
                "Nothing happens when you try to use this item.",
                "This item is just for show.",
                "This item is purely decorative.",
                "This item is a mystery. Its purpose is unknown.",
                "This item is a puzzle. Its function is unclear.",
                "Ask Steve. He might know what to do with this item.",
                "Even the creators of this item don't know what it does."
            };

        private static readonly Dictionary<Rarity, int> _rarityValuesStaminaRestoration = new Dictionary<Rarity, int>
        {
            { Rarity.Common, 3 },
            { Rarity.Uncommon, 10 },
            { Rarity.Rare, 15 },
            { Rarity.Epic, 20 },
            { Rarity.Legendary, 40 }
        };

        public ItemActions()
        {
            _itemActions = new Dictionary<EquipmentSlots, List<Func<Player, object?, bool>>>
            {
                { EquipmentSlots.LeftHand, new List<Func<Player, object?, bool>>() }, // Defensive actions
                { EquipmentSlots.RightHand, new List<Func<Player, object?, bool>>() }, // Offensive actions
                { EquipmentSlots.QuickSlot, new List<Func<Player, object?, bool>>() }, // Utility actions
                { EquipmentSlots.Accessory, new List<Func<Player, object?, bool>>() } // Passive actions
            };
        }

        private static bool NoAction(Player player, object? target)
        {
            // This is a placeholder function that does nothing when an item is used without a defined action.
            // It's used as a default action for items that don't have a specific use case.
            Random rand = new Random();
            int index = rand.Next(0, _noActionDescriptions.Count); // Generates a random number between
            Console.WriteLine(_noActionDescriptions[index]);
            return false; // Always returns false, indicating that no action was performed.
        }

        // Left-hand item actions (defensive) : Strategy + Factory
        private static bool RestoreStamina(Player player, object? target, int amount)
        {
            var stats = player.Stats;
            int restored = Math.Min(amount, stats.MaxStamina - stats.CurrentStamina);

            if (restored <= 0)
                return false;

            stats.CurrentStamina += restored;
            Console.WriteLine($"Stamina restored by {restored} points.");
            return true;
        }
        public static Func<Player, object?, bool> CreateRestoreStamina(Rarity rarity)
        {
            int amount = _rarityValuesStaminaRestoration[rarity];
            return (player, target) => RestoreStamina(player, target, amount);
        }
    }
}