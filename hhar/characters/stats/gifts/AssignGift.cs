namespace hhar.characters.stats.gifts
{
    public class AssignGift
    {
        public GiftList Gift { get; set; }
        public CharacterStats Stats { get; }
        public AssignGift(GiftList startingGift, CharacterStats stats)
        {
            Gift = startingGift;
            Stats = stats.SetBaseStats();
            ApplyGift(Stats, Gift);
            Stats.CalculateModifiers();
        }



        private void ApplyGift(CharacterStats stats, GiftList gift)
        {
            StatModifier[] modifiers = gift switch
            {
                GiftList.LastRation =>
                [
                    new("Well Fed", "MaxHealth", 5, "Last Ration"),
                    new("Reserve Strength", "MaxStamina", 2, "Last Ration")
                ],

                GiftList.UnpaidGartitude =>
                [
                    new("Motivated", "AttackDamage", 3, "Unpaid Gratitude"),
                    new("Driven", "MaxStamina", 2, "Unpaid Gratitude")
                ],
                
                GiftList.DyingEmber =>
                [
                    new("Burning Spirit", "AttackDamage", 5, "Dying Ember"),
                    new("Flickering Flame", "MaxHealth", -2, "Dying Ember")
                ],


                GiftList.FalseReference =>
                [
                    new("Deceptive", "Charisma", 4, "False Reference"),
                    new("Cunning", "Intelligence", 3, "False Reference")
                ],

                GiftList.LastBreak =>
                [
                    new("Resilient", "Defense", 3, "Last Break"),
                    new("Unyielding", "MaxHealth", 2, "Last Break")
                ],

                GiftList.InheritedHardship =>
                [
                    new("Hardened", "Defense", 3, "Inherited Hardship"),
                    new("Enduring", "MaxHealth", 3, "Inherited Hardship")
                ],

                GiftList.ForbiddenRecipe =>
                [
                    new("Culinary Genius", "Intelligence", 5, "Forbidden Recipe"),
                    new("Secret Knowledge", "Wisdom", 3, "Forbidden Recipe")
                ],

                GiftList.MemoryOfHome =>
                [
                    new("Nostalgic", "Charisma", 3, "Memory of Home"),
                    new("Comforted", "MaxHealth", 2, "Memory of Home")
                ],

                _ => throw new ArgumentOutOfRangeException(nameof(gift))
            };

            foreach (StatModifier modifier in modifiers)
            {
                stats.AddModifier(modifier);
            }
        }
    }
    
}