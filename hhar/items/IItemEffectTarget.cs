using hhar.characters;

namespace hhar.items
{
    public interface IItemEffectTarget
    {
        bool TryApply(Player player, int power);
    }
}