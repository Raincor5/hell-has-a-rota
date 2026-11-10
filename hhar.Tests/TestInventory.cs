namespace hhar.Tests;

using hhar.characters.inventory;
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
}
