using hhar.characters.stats;
namespace hhar.items {
    public interface IItem {
        string Name { get; set; }
        string Description { get; set; }
        List<StatModifier> StatModifiers { get; set; }
    }
}
