using hhar.characters.stats;
namespace hhar.characters.progression
{
    public class LevelProgression
    {
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceToNextLevel { get; set; } = 20; // Default experience required for the first level up
        public int UnallocatedPoints { get; set; }

        public LevelProgression(int unallocatedPoints = 0)
        {
            Level = 1;
            Experience = 0;
            ExperienceToNextLevel = CalculateExperienceToNextLevel(Level);
            UnallocatedPoints = unallocatedPoints;
        }


        public void LevelUp()
        {
            while (Experience >= ExperienceToNextLevel)
            {
                Experience -= ExperienceToNextLevel; // Reset experience after leveling up
                Level++;
                if (Level % 5 == 0) // Every 5 levels, grant an unallocated point
                {
                    UnallocatedPoints += 5;
                }
                else
                {
                    UnallocatedPoints += 1;
                }
                ExperienceToNextLevel = CalculateExperienceToNextLevel(Level);
            }
        }

        private int CalculateExperienceToNextLevel(int currentLevel)
        {
            // Example formula for experience required to reach the next level
            return (int)(100 * Math.Pow(1.5, currentLevel - 1));
        }

        public void AllocatePoints(CharacterStats stats, string statName, int points)
        {
            if (UnallocatedPoints >= points)
            {
                stats.ApplySkillPoint(statName, points);
                UnallocatedPoints -= points; // Deduct the allocated points
            }
            else
            {
                throw new InvalidOperationException("Not enough points");
            }
        }
    }
}