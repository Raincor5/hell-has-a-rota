namespace hhar.Tests;

using hhar.characters.inventory;
using hhar.characters;
using hhar.characters.stats;
using hhar.items;

public class TestInventory
{
    [Fact]
    public void AddItem_PlacesItemInSelectedSlot()
    {
        var inventory = new PlayerInventory();
        var item = new Misc("Torch", "A handheld light source", []);

        inventory.AddItem(item, 3);

        Assert.False(inventory.Items[3].IsEmpty);
        Assert.Same(item, inventory.Items[3].Item);
    }

    [Fact]
    public void AddItem_WithoutSlot_UsesFirstEmptySlot()
    {
        var inventory = new PlayerInventory();
        var item = new Misc("Torch", "A handheld light source", []);

        inventory.AddItem(item);

        Assert.Same(item, inventory.Items[0].Item);
    }

    [Fact]
    public void RemoveItem_ClearsItemFromItsSlot()
    {
        var inventory = new PlayerInventory();
        var item = new Misc("Torch", "A handheld light source", []);
        inventory.AddItem(item, 3);

        inventory.RemoveItem(item);

        Assert.True(inventory.Items[3].IsEmpty);
    }

    [Fact]
    public void EquipAndUnequipItem_AppliesAndRemovesItsModifiers()
    {
        var player = new Player("Test");
        player.Stats.SetBaseStats();
        var weapon = new Weapon(
            "Sword",
            "A sword",
            5,
            [new StatModifier("Sharp", "AttackDamage", 3, "Sword")]);
        var controller = new InventoryController(player);

        controller.AddItem(weapon, 3);
        controller.EquipItem(3);

        Assert.Equal(8, player.Stats.AttackDamage);
        Assert.Same(weapon, player.Equipment.Equipment[EquipmentSlots.RightHand].Item);

        controller.UnequipItem(EquipmentSlots.RightHand);

        Assert.Equal(5, player.Stats.AttackDamage);
        Assert.True(player.Equipment.Equipment[EquipmentSlots.RightHand].IsEmpty);
    }
}
