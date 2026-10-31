using hhar.items;
namespace hhar.characters.inventory
{
    public interface IEquipment
    {
        IItem? LeftHand { get; set; }
        IItem? RightHand { get; set; }
        IItem? QuickSlot1 { get; set; }
        IItem? QuickSlot2 { get; set; }
        IItem? QuickSlot3 { get; set; }
        IItem? QuickSlot4 { get; set; }
        IItem? Accessory1 { get; set; }
        IItem? Accessory2 { get; set; }
    }
}