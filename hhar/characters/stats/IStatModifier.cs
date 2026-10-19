namespace hhar.characters.stats {
    public interface IStatModifier {
        string StatName { get; set; }
        int Modifier { get; set; }

        string Source { get; set; }
    }
}