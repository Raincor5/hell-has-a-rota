using hhar.items;
namespace hhar.characters.inventory
{
    public interface IEquipment
    {
        public Dictionary<EquipmentSlots, EquipmentSlot> Equipment { get; set; }

        void EquipItem(IItem item, EquipmentSlots slot);
    }
}