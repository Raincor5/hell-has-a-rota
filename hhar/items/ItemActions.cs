using hhar.characters;
using hhar.characters.inventory;
using hhar.characters.stats;

namespace hhar.items
{
    public class ItemActions
    {
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
                "Ask Steve. He might know what to do with this item.",
                "Even the creators of this item don't know what it does."
            };

        private delegate bool Strategy(Player player, object? target, int power);

        private static readonly Dictionary<(EquipmentSlots Slot, string Stat), Strategy[]> Strategies = new()
        {
            [(EquipmentSlots.LeftHand, "MaxStamina")] = [Restore],
            [(EquipmentSlots.LeftHand, "MaxHealth")] = [Heal],
            [(EquipmentSlots.LeftHand, "Defense")] = [BlockNext],
            [(EquipmentSlots.RightHand, "MaxStamina")] = [Drain],
            [(EquipmentSlots.RightHand, "MaxHealth")] = [LifeSteal],
            [(EquipmentSlots.RightHand, "AttackDamage")] = [Strike],
            [(EquipmentSlots.QuickSlot, "MaxStamina")] = [Restore],
            [(EquipmentSlots.QuickSlot, "MaxHealth")] = [Heal],
            [(EquipmentSlots.NotEquipment, "Intelligence")] = [Solve],
            [(EquipmentSlots.NotEquipment, "Wisdom")] = [Apply]
        };

        public static Func<Player, object?, bool> CreateAction(EquipmentSlots slot,
            IEnumerable<StatModifier> modifiers, Rarity rarity, Random? random = null)
        {
            var primary = modifiers.GroupBy(m => m.StatName)
                .Select(group => new { Stat = group.Key, Total = group.Sum(m => m.Modifier) })
                .Where(entry => entry.Total > 0)
                .OrderByDescending(entry => entry.Total)
                .ThenBy(entry => entry.Stat, StringComparer.Ordinal)
                .FirstOrDefault();

            if (primary is null || !Strategies.TryGetValue((slot, primary.Stat), out var pool))
                return NoAction;

            var strategy = pool[(random ?? Random.Shared).Next(pool.Length)];
            return Bind(strategy, rarity);
        }

        public static Func<Player, object?, bool> CreateRestoreStamina(Rarity rarity) =>
            Bind(Restore, rarity);

        private static Func<Player, object?, bool> Bind(Strategy strategy, Rarity rarity)
        {
            int power = rarity switch
            {
                Rarity.Common => 3,
                Rarity.Uncommon => 10,
                Rarity.Rare => 15,
                Rarity.Epic => 20,
                Rarity.Legendary => 40,
                _ => throw new ArgumentOutOfRangeException(nameof(rarity))
            };
            return (player, target) => strategy(player, target, power);
        }

        private static bool NoAction(Player player, object? target)
        {
            Console.WriteLine(_noActionDescriptions[Random.Shared.Next(_noActionDescriptions.Count)]);
            return false;
        }

        private static bool Restore(Player player, object? target, int power)
        {
            var stats = player.Stats;
            int restored = Math.Min(power, stats.MaxStamina - stats.CurrentStamina);
            if (restored <= 0) return false;
            stats.CurrentStamina += restored;
            Console.WriteLine($"Stamina restored by {restored} points.");
            return true;
        }

        private static bool Heal(Player player, object? target, int power)
        {
            var stats = player.Stats;
            int restored = Math.Min(power, stats.MaxHealth - stats.CurrentHealth);
            if (stats.CurrentHealth <= 0 || restored <= 0) return false;
            stats.CurrentHealth += restored;
            return true;
        }

        private static bool BlockNext(Player player, object? target, int power) =>
            player.Stats.PrepareBlock(power);

        private static bool Drain(Player player, object? target, int power)
        {
            if (target is not ICharacter enemy || ReferenceEquals(player, enemy)) return false;
            int drained = Math.Min(enemy.Stats.CurrentStamina, power);
            if (enemy.Stats.CurrentHealth <= 0 || drained <= 0) return false;
            enemy.Stats.CurrentStamina -= drained;
            return true;
        }

        private static bool Strike(Player player, object? target, int power)
        {
            if (target is not ICharacter enemy || ReferenceEquals(player, enemy)
                || enemy.Stats.CurrentHealth <= 0) return false;
            enemy.TakeDamage(Math.Min(power, enemy.Stats.CurrentHealth));
            return true;
        }

        private static bool LifeSteal(Player player, object? target, int power)
        {
            if (target is not ICharacter enemy) return false;
            int before = enemy.Stats.CurrentHealth;
            if (!Strike(player, enemy, power)) return false;
            Heal(player, null, before - enemy.Stats.CurrentHealth);
            return true;
        }

        private static bool Solve(Player player, object? target, int power) =>
            target is IPuzzleTarget puzzle && puzzle.TrySolve(player, power);

        private static bool Apply(Player player, object? target, int power) =>
            target is IItemEffectTarget receiver && receiver.TryApply(player, power);
    }
}
