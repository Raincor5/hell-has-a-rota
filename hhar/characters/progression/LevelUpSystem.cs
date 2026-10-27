using System.Reflection.PortableExecutable;
using hhar.characters.stats;
using hhar.characters.progression;
namespace hhar.characters.progression
{
    public class LevelUpSystem
    {
        public void LevlUpListener(CharacterStats stats, LevelProgression levelProgression)
        {
            while (true)
            {
                levelProgression.LevelUp();
            }
        }

        public void AllocationSelector(CharacterStats stats, LevelProgression levelProgression, string statName, int points)
        {
            stats.GetStatsToLevelUp();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
            levelProgression.AllocatePoints(stats, statName, points);
            Console.WriteLine($"Allocated {points} points to {statName}. Remaining unallocated points: {levelProgression.UnallocatedPoints}");
        }
    }
}