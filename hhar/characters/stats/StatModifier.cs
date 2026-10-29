namespace hhar.characters.stats {
    public sealed class StatModifier(string BuffName, string StatName, int Modifier, string Source): IStatModifier {
        public string BuffName { get; set; } = BuffName;
        public string StatName { get; set; } = StatName;
        public int Modifier { get; set; } = Modifier;
        public string Source { get; set; } = Source;
    }
}