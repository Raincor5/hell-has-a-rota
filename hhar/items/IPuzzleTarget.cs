using hhar.characters;

namespace hhar.items
{
    public interface IPuzzleTarget
    {
        bool TrySolve(Player player, int power);
    }
}