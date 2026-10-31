using hhar.items;
namespace hhar.characters.inventory
{
    public class PlayerEquipment : IEquipment
    {
        public IItem? LeftHand { get; set; }
        public IItem? RightHand { get; set; }
        public IItem? QuickSlot1 { get; set; }
        public IItem? QuickSlot2 { get; set; }
        public IItem? QuickSlot3 { get; set; }
        public IItem? QuickSlot4 { get; set; }
        public IItem? Accessory1 { get; set; }
        public IItem? Accessory2 { get; set; }

        public PlayerEquipment()
        {
            LeftHand = null;
            RightHand = null;
            QuickSlot1 = null;
            QuickSlot2 = null;
            QuickSlot3 = null;
            QuickSlot4 = null;
            Accessory1 = null;
            Accessory2 = null;
        }


        private void EquipItem(IItem? item, string slotName)
        {
            if (item == null)
            {
                Console.WriteLine($"Cannot equip null item to {slotName}.");
                return;
            }

            switch (slotName)
            {
                case "LeftHand":
                    LeftHand = item;
                    break;
                case "RightHand":
                    RightHand = item;
                    break;
                case "QuickSlot1":
                    QuickSlot1 = item;
                    break;
                case "QuickSlot2":
                    QuickSlot2 = item;
                    break;
                case "QuickSlot3":
                    QuickSlot3 = item;
                    break;
                case "QuickSlot4":
                    QuickSlot4 = item;
                    break;
                case "Accessory1":
                    Accessory1 = item;
                    break;
                case "Accessory2":
                    Accessory2 = item;
                    break;
                default:
                    Console.WriteLine($"Invalid slot name: {slotName}");
                    return;
            }

            Console.WriteLine($"Equipped {item.Name} to {slotName}.");
        }
    }
}