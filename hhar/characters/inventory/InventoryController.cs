using hhar.characters;
using hhar.items;

namespace hhar.characters.inventory
{
    public class InventoryController
    {
        public Player Player { get; }
        public IInventory Inventory => Player.Inventory;

        public InventoryController(Player player)
        {
            Player = player;
        }

        public void AddItem(IItem item)
        {
            Inventory.AddItem(item);
        }

        public void AddItem(IItem item, int slot)
        {
            Inventory.AddItem(item, slot);
        }

        public void RemoveItem(IItem item)
        {
            Inventory.RemoveItem(item);
        }

        public void EquipItem(int inventorySlot)
        {
            var slot = GetInventorySlot(inventorySlot);
            if (slot.IsEmpty)
            {
                throw new InvalidOperationException($"Inventory slot is empty: {inventorySlot}");
            }

            var item = slot.Item!;
            if (item.EquipmentSlot == EquipmentSlots.NotEquipment)
            {
                throw new InvalidOperationException($"{item.Name} cannot be equipped.");
            }

            Player.Equipment.EquipItem(item, item.EquipmentSlot);
            Player.Stats.AddModifiers(item.GetStatModifiers());
            slot.Clear();
        }

        public void UnequipItem(EquipmentSlots equipmentSlot)
        {
            var slot = GetEquipmentSlot(equipmentSlot);
            if (slot.IsEmpty)
            {
                throw new InvalidOperationException($"Equipment slot is empty: {equipmentSlot}");
            }

            var item = slot.Item!;
            Inventory.AddItem(item);
            Player.Stats.RemoveModifiers(item.GetStatModifiers());
            slot.Clear();
        }

        public void UseItem(int slot, object? target = null)
        {
            var inventorySlot = GetInventorySlot(slot);
            if (inventorySlot.IsEmpty)
            {
                throw new InvalidOperationException($"Inventory slot is empty: {slot}");
            }

            if (inventorySlot.Item!.EquipmentSlot != EquipmentSlots.NotEquipment)
            {
                EquipItem(slot);
                return;
            }

            inventorySlot.UseItem(Player, target);
        }

        public bool ActivateEquipment(EquipmentSlots equipmentSlot, object? target = null)
        {
            return GetEquipmentSlot(equipmentSlot).Activate(Player, target);
        }

        public string DisplayInventory()
        {
            return string.Join(
                Environment.NewLine,
                Inventory.Items.OrderBy(entry => entry.Key).Select(entry =>
                    entry.Value.IsEmpty
                        ? $"Slot {entry.Key}: Empty"
                        : $"Slot {entry.Key}: {entry.Value.Item!.Name} x{entry.Value.Quantity}"));
        }

        public string DisplayEquipment()
        {
            return string.Join(
                Environment.NewLine,
                Player.Equipment.Equipment
                    .Where(entry => entry.Key != EquipmentSlots.NotEquipment)
                    .OrderBy(entry => entry.Key)
                    .Select(entry =>
                        entry.Value.IsEmpty
                            ? $"{entry.Key}: Empty"
                            : $"{entry.Key}: {entry.Value.Item!.Name} x{entry.Value.Quantity}"));
        }

        public void Run()
        {
            Run(() => Console.ReadKey(intercept: true), Console.Out);
        }

        public void Run(Func<ConsoleKeyInfo> readKey, TextWriter output)
        {
            ArgumentNullException.ThrowIfNull(readKey);
            ArgumentNullException.ThrowIfNull(output);

            var selectedInventorySlot = Inventory.Items.Keys.OrderBy(slot => slot).FirstOrDefault();
            var selectedEquipmentSlot = EquipmentSlots.LeftHand;
            DisplayControls(output);

            while (true)
            {
                var key = readKey();
                if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Q)
                {
                    return;
                }

                try
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.I:
                            output.WriteLine(DisplayInventory());
                            break;
                        case ConsoleKey.E:
                            output.WriteLine(DisplayEquipment());
                            break;
                        case ConsoleKey.LeftArrow:
                            selectedInventorySlot = SelectInventorySlot(selectedInventorySlot, -1);
                            output.WriteLine($"Selected inventory slot: {selectedInventorySlot}");
                            break;
                        case ConsoleKey.RightArrow:
                            selectedInventorySlot = SelectInventorySlot(selectedInventorySlot, 1);
                            output.WriteLine($"Selected inventory slot: {selectedInventorySlot}");
                            break;
                        case ConsoleKey.UpArrow:
                            selectedEquipmentSlot = SelectEquipmentSlot(selectedEquipmentSlot, -1);
                            output.WriteLine($"Selected equipment slot: {selectedEquipmentSlot}");
                            break;
                        case ConsoleKey.DownArrow:
                            selectedEquipmentSlot = SelectEquipmentSlot(selectedEquipmentSlot, 1);
                            output.WriteLine($"Selected equipment slot: {selectedEquipmentSlot}");
                            break;
                        case ConsoleKey.Enter:
                        case ConsoleKey.U:
                            UseItem(selectedInventorySlot);
                            output.WriteLine("Selected item used or equipped.");
                            break;
                        case ConsoleKey.G:
                            EquipItem(selectedInventorySlot);
                            output.WriteLine("Selected item equipped.");
                            break;
                        case ConsoleKey.X:
                            UnequipItem(selectedEquipmentSlot);
                            output.WriteLine("Selected item unequipped.");
                            break;
                        case ConsoleKey.A:
                            output.WriteLine(ActivateEquipment(selectedEquipmentSlot)
                                ? "Selected equipment activated."
                                : "Selected equipment could not be activated.");
                            break;
                        case ConsoleKey.H:
                            DisplayControls(output);
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    output.WriteLine($"Error: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    output.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void DisplayControls(TextWriter output)
        {
            output.WriteLine("I: inventory | E: equipment | arrows: select | Enter/U: use");
            output.WriteLine("G: equip | X: unequip | A: activate | H: help | Q/Esc: close");
        }

        private InventorySlot GetInventorySlot(int slot)
        {
            if (!Inventory.Items.TryGetValue(slot, out var inventorySlot))
            {
                throw new ArgumentOutOfRangeException(nameof(slot), slot, "The inventory slot does not exist.");
            }

            return inventorySlot;
        }

        private EquipmentSlot GetEquipmentSlot(EquipmentSlots slot)
        {
            if (slot == EquipmentSlots.NotEquipment ||
                !Player.Equipment.Equipment.TryGetValue(slot, out var equipmentSlot))
            {
                throw new ArgumentOutOfRangeException(nameof(slot), slot, "The equipment slot does not exist.");
            }

            return equipmentSlot;
        }

        private int SelectInventorySlot(int currentSlot, int direction)
        {
            var slots = Inventory.Items.Keys.OrderBy(slot => slot).ToArray();
            if (slots.Length == 0)
            {
                throw new InvalidOperationException("The inventory has no slots.");
            }

            var currentIndex = Array.IndexOf(slots, currentSlot);
            if (currentIndex < 0)
            {
                currentIndex = 0;
            }

            var nextIndex = (currentIndex + direction + slots.Length) % slots.Length;
            return slots[nextIndex];
        }

        private static EquipmentSlots SelectEquipmentSlot(EquipmentSlots currentSlot, int direction)
        {
            var slots = Enum.GetValues<EquipmentSlots>()
                .Where(slot => slot != EquipmentSlots.NotEquipment)
                .ToArray();
            var currentIndex = Array.IndexOf(slots, currentSlot);
            var nextIndex = (currentIndex + direction + slots.Length) % slots.Length;

            return slots[nextIndex];
        }
    }
}
