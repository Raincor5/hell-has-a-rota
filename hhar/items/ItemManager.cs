using hhar.characters.stats;
namespace hhar.items
{
    public class ItemManager
    {
        private enum ItemType
        {
            Potion,
            Consumable,
            LeftHand,
            RightHand,
            Accessory,
            Quest
        }

        private sealed record ItemDefinitions(string Name, string Description, ItemType Type, string? Stat = null, int Damage = 0);

        private readonly Random _random;
        private readonly ItemBuffs _itemBuffs = new();

        private static readonly Dictionary<ItemIds, ItemDefinitions> Definitions = new()
        {
            // Potions
            { ItemIds.HealthPotion, new ItemDefinitions("Health Potion", "Restores health.", ItemType.Potion, "MaxHealth") },
            { ItemIds.BrainConcoction, new ItemDefinitions("Brain Concoction", "Increases intelligence.", ItemType.Potion, "Intelligence") },
            { ItemIds.StaminaPotion, new ItemDefinitions("Stamina Potion", "Restores stamina.", ItemType.Potion, "MaxStamina") },
            { ItemIds.Bluecow, new ItemDefinitions("Bluecow", "Why do people drink Mobster Energy? Restores stamina.", ItemType.Potion, "MaxStamina") },
            { ItemIds.MobsterEnergy, new ItemDefinitions("Mobster Energy", "Why do people drink Bluecow? Restores stamina.", ItemType.Potion, "MaxStamina") },
            { ItemIds.VendingMachineCoffee, new ItemDefinitions("Vending Machine Coffee", "Why do people drink this? Increases stamina.", ItemType.Potion, "MaxStamina") },
            // Consumables
            { ItemIds.FiveMinuteBreak, new ItemDefinitions("Five Minute Break Allowance", "The currency. Restores stamina.", ItemType.Consumable, "MaxStamina") },
            { ItemIds.LastCigarette, new ItemDefinitions("Last Cigarette", "Misery is calling me. Restores stamina.", ItemType.Consumable, "MaxStamina") },
            { ItemIds.LuckyFagsBlue, new ItemDefinitions("Lucky Fags Blue", "Do not forget to flip one. Just in case.", ItemType.Consumable, "Luck") },
            { ItemIds.LuckyFagsRed, new ItemDefinitions("Lucky Fags Red", "Nothing helps anymore. Just pray now.", ItemType.Consumable, "Luck") },
            { ItemIds.BreakRoomBiscuit, new ItemDefinitions("Break Room Biscuit", "A simple snack. Restores a small amount of health.", ItemType.Consumable, "MaxHealth") },
            { ItemIds.ShiftSwapFavour, new ItemDefinitions("Shift Swap Favour", "A token of appreciation. They like me now?!", ItemType.Consumable, "Charisma") },
            // Left Hand
            { ItemIds.ClipboardOfAuthority, new ItemDefinitions("Clipboard of Authority", "Look busy. Write nothing. Like a true boss.", ItemType.LeftHand, Damage: 1) },
            { ItemIds.CrackedWorkPhone, new ItemDefinitions("Cracked Work Phone", "A relic of the past. Still functional.", ItemType.LeftHand, Damage: 1) },
            { ItemIds.StaffHandbook, new ItemDefinitions("Staff Handbook", "The rules are updated at my command now!", ItemType.LeftHand, Damage: 2) },
            // Right Hand
            { ItemIds.BorrowedMarker, new ItemDefinitions("Borrowed Marker", "Permanent. Unlike my contract.", ItemType.RightHand, Damage: 3) },
            { ItemIds.PricingGun, new ItemDefinitions("Pricing Gun", "Everything has a price. And it's not always money.", ItemType.RightHand, Damage: 4) },
            { ItemIds.ManagersKeys, new ItemDefinitions("Manager's Keys", "The keys to the kingdom. Or at least the store.", ItemType.RightHand, Damage: 5) },
            // Accessories
            { ItemIds.ActingManagerBadge, new ItemDefinitions("Acting Manager Badge", "I am the manager now. For now.", ItemType.Accessory) },
            { ItemIds.EmployeeOfTheMonthPin, new ItemDefinitions("Employee of the Month Pin", "My reward is another shift.", ItemType.Accessory) },
            { ItemIds.SingleEarbud, new ItemDefinitions("Single Earbud", "I can hear you. But I don't care.", ItemType.Accessory) },
            { ItemIds.TraineeBadge, new ItemDefinitions("Trainee Badge", "I am learning. But I am also dangerous.", ItemType.Accessory) },
            // Quest Items
            { ItemIds.ApprovedLeaveForm, new ItemDefinitions("Approved Leave Form", "I can finally go home. Or can I?", ItemType.Quest) },
            { ItemIds.OriginalRota, new ItemDefinitions("Original Rota", "The original schedule. See, I was off.", ItemType.Quest) },
            { ItemIds.WrittenWarning, new ItemDefinitions("Written Warning", "I am in trouble!", ItemType.Quest) },
            { ItemIds.ResignationLetter, new ItemDefinitions("Resignation Letter", "I quit. Or do I?", ItemType.Quest) },
            { ItemIds.ClockOutReceipt, new ItemDefinitions("Clock Out Receipt", "I can leave now. But I might not want to.", ItemType.Quest) },
            { ItemIds.DuplicateLockerKey, new ItemDefinitions("Duplicate Locker Key", "I can access the locker now. But what is inside?", ItemType.Quest) },
            { ItemIds.MissingRadioBattery, new ItemDefinitions("Missing Radio Battery", "I shall never return it. That music sucks.", ItemType.Quest) },
            { ItemIds.UnsignedLeavingCard, new ItemDefinitions("Unsigned Leaving Card", "Everyone meant to sign it. Nobody did.", ItemType.Quest) },
            { ItemIds.ShiftHandoverNote, new ItemDefinitions("Shift Handover Note", "Everything is apparently fine.", ItemType.Quest) },
            { ItemIds.UnionRepNumber, new ItemDefinitions("Union Rep Number", "I can call the union now. But will they help me?", ItemType.Quest) },
            { ItemIds.UnclaimedPayslip, new ItemDefinitions("Unclaimed Payslip", "A name. A number. A discrepancy.", ItemType.Quest) },
            { ItemIds.ExitInterviewTape, new ItemDefinitions("Exit Interview Tape", "The new blackmail strategy.", ItemType.Quest) }
        };
    }
}