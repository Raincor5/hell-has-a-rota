namespace hhar.characters.stats
{
    public class CharacterStats
    {
        public int BaseHealth { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int BaseStamina { get; set; }
        public int MaxStamina { get; set; }
        public int CurrentStamina { get; set; }
        public int BaseAttackDamage { get; set; }
        public int AttackDamage { get; set; }

        public int BaseHostility { get; set; }
        public int Hostility { get; set; }
        public int BaseDefense { get; set; }
        public int Defense { get; set; }

        public int BaseIntelligence { get; set; }
        public int Intelligence { get; set; }

        public int BaseWisdom { get; set; }
        public int Wisdom { get; set; }

        public int BaseCharisma { get; set; }
        public int Charisma { get; set; }

        public int BaseLuck { get; set; }
        public int Luck { get; set; }
        public int StatCap { get; set; } = 99;

        private List<StatModifier> _modifiers = [];

        // Using LINQ to calculate the total modifiers for each stat
        public void CalculateModifiers()
        {
            AttackDamage = BaseAttackDamage + _modifiers.Where(m => m.StatName == "AttackDamage").Sum(m => m.Modifier);
            Defense = BaseDefense + _modifiers.Where(m => m.StatName == "Defense").Sum(m => m.Modifier);
            MaxHealth = BaseHealth + _modifiers.Where(m => m.StatName == "MaxHealth").Sum(m => m.Modifier);
            MaxStamina = BaseStamina + _modifiers.Where(m => m.StatName == "MaxStamina").Sum(m => m.Modifier);
            CurrentHealth = Math.Min(CurrentHealth, MaxHealth);
            CurrentStamina = Math.Min(CurrentStamina, MaxStamina);
            Hostility = _modifiers.Where(m => m.StatName == "Hostility").Sum(m => m.Modifier);
            Intelligence = BaseIntelligence + _modifiers.Where(m => m.StatName == "Intelligence").Sum(m => m.Modifier);
            Wisdom = BaseWisdom + _modifiers.Where(m => m.StatName == "Wisdom").Sum(m => m.Modifier);
            Charisma = BaseCharisma + _modifiers.Where(m => m.StatName == "Charisma").Sum(m => m.Modifier);
            Luck = BaseLuck + _modifiers.Where(m => m.StatName == "Luck").Sum(m => m.Modifier);

            // Ensure that the stats do not exceed the cap
            AttackDamage = Math.Min(AttackDamage, StatCap);
            Defense = Math.Min(Defense, StatCap);
            MaxHealth = Math.Min(MaxHealth, StatCap);
            MaxStamina = Math.Min(MaxStamina, StatCap);
            Hostility = Math.Min(Hostility, StatCap);
            Intelligence = Math.Min(Intelligence, StatCap);
            Wisdom = Math.Min(Wisdom, StatCap);
            Charisma = Math.Min(Charisma, StatCap);
            Luck = Math.Min(Luck, StatCap);
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            CalculateModifiers();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
            CalculateModifiers();
        }

        public void ApplySkillPoint(string statName, int points)
        {
            switch (statName.ToLower())
            {
                case "health":
                    BaseHealth = Math.Min(BaseHealth + points, StatCap);
                    break;
                case "stamina":
                    BaseStamina = Math.Min(BaseStamina + points, StatCap);
                    break;
                case "attackdamage":
                    BaseAttackDamage = Math.Min(BaseAttackDamage + points, StatCap);
                    break;
                case "hostility":
                    BaseHostility = Math.Min(BaseHostility + points, StatCap);
                    break;
                case "defense":
                    BaseDefense = Math.Min(BaseDefense + points, StatCap);
                    break;
                case "intelligence":
                    BaseIntelligence = Math.Min(BaseIntelligence + points, StatCap);
                    break;
                case "wisdom":
                    BaseWisdom = Math.Min(BaseWisdom + points, StatCap);
                    break;
                case "charisma":
                    BaseCharisma = Math.Min(BaseCharisma + points, StatCap);
                    break;
                case "luck":
                    BaseLuck = Math.Min(BaseLuck + points, StatCap);
                    break;
                default:
                    throw new ArgumentException($"Stat {statName} does not exist.");
            }
            CalculateModifiers();
        }

        public string GetStatSummary()
        {
            return $"Health: {CurrentHealth}/{MaxHealth}, Stamina: {CurrentStamina}/{MaxStamina}, Attack Damage: {AttackDamage}, Defense: {Defense}, Hostility: {Hostility}, Intelligence: {Intelligence}, Wisdom: {Wisdom}, Charisma: {Charisma}, Luck: {Luck}";
        }

        public string GetBaseStatSummary()
        {
            return $"Base Health: {BaseHealth}, Base Stamina: {BaseStamina}, Base Attack Damage: {BaseAttackDamage}, Base Defense: {BaseDefense}, Base Hostility: {BaseHostility}, Base Intelligence: {BaseIntelligence}, Base Wisdom: {BaseWisdom}, Base Charisma: {BaseCharisma}, Base Luck: {BaseLuck}";
        }

        public string GetStatsToLevelUp()
        {
            return $"Health: {BaseHealth}, \nStamina: {BaseStamina}, \nAttack Damage: {BaseAttackDamage}, \nDefense: {BaseDefense}, \nIntelligence: {BaseIntelligence}, \nWisdom: {BaseWisdom}, \nCharisma: {BaseCharisma}, \nLuck: {BaseLuck}";
        }
    }
}