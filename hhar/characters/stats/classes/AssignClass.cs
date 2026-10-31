namespace hhar.characters.stats.classes
{
        
    // A class that represents the assignment of a starting class to a player character. This class is used during character creation to set the player's starting class and stats.
    public class AssignClass
    {
        public ClassList Class { get; }
        public CharacterStats Stats { get; }

        public AssignClass(ClassList startingClass, CharacterStats stats)
        {
            Class = startingClass;
            Stats = stats.SetBaseStats();
            AssignPoints();
            Stats.CalculateModifiers();
        }

        private void AssignPoints()
        {
            switch (Class)
            {
                case ClassList.Unassigned:
                    break;
                case ClassList.Slave:
                    Stats.BaseHealth += 5;
                    Stats.BaseStamina += 5;
                    break;
                case ClassList.Apprentice:
                    Stats.BaseIntelligence += 5;
                    Stats.BaseWisdom += 5;
                    break;
                case ClassList.Deprived:
                    Stats.BaseAttackDamage += 5;
                    Stats.BaseDefense += 5;
                    break;
                case ClassList.FortuneSeeker:
                    Stats.BaseLuck += 10;
                    break;
                case ClassList.Veteran:
                    Stats.BaseCharisma += 5;
                    Stats.BaseAttackDamage += 5;
                    break;
                case ClassList.Penitent:
                    Stats.BaseWisdom += 10;
                    break;
                case ClassList.Butcher:
                    Stats.BaseDefense += 5;
                    Stats.BaseHostility += 10;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}