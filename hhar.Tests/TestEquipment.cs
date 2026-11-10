namespace hhar.Tests;

using hhar.items;
using hhar.characters.inventory;

public class TestEquipment
{
    [Fact]
    public void EquipItem_PlacesItemInSelectedSlot()
    {
        var equipment = new PlayerEquipment();
        var item = new Misc("Torch", "A handheld light source", []);

        equipment.EquipItem(item, EquipmentSlots.LeftHand);

        Assert.False(equipment.Equipment[EquipmentSlots.LeftHand].IsEmpty);
        Assert.Same(item, equipment.Equipment[EquipmentSlots.LeftHand].Item);
        Assert.Contains(item, equipment.GetEquippedItems());
    }

    [Fact]
    public void GetEquippedItems_ReturnsAllEquippedItems()
    {
        var equipment = new PlayerEquipment();
        var item1 = new Misc("Torch", "A handheld light source", []);
        var item2 = new Misc("Shield", "A defensive item", []);

        equipment.EquipItem(item1, EquipmentSlots.LeftHand);
        equipment.EquipItem(item2, EquipmentSlots.RightHand);

        var equippedItems = equipment.GetEquippedItems();

        Assert.Contains(item1, equippedItems);
        Assert.Contains(item2, equippedItems);
    }

    [Fact]
    public void EquipItem_ThrowsException_WhenSlotIsOccupied()
    {
        var equipment = new PlayerEquipment();
        var item1 = new Misc("Torch", "A handheld light source", []);
        var item2 = new Misc("Shield", "A defensive item", []);

        equipment.EquipItem(item1, EquipmentSlots.LeftHand);

        Assert.Throws<ArgumentException>(() => equipment.EquipItem(item2, EquipmentSlots.LeftHand));
    }

    [Fact]
    public void EquipItem_ThrowsException_WhenItemIsNull()
    {
        var equipment = new PlayerEquipment();

        Assert.Throws<ArgumentNullException>(() => equipment.EquipItem(null!, EquipmentSlots.LeftHand));
    }

    [Fact]
    public void EquipItem_ThrowsException_WhenSlotIsInvalid()
    {
        var equipment = new PlayerEquipment();
        var item = new Misc("Torch", "A handheld light source", []);

        Assert.Throws<ArgumentException>(() =>
            equipment.EquipItem(item, (EquipmentSlots)999));
    }
}
