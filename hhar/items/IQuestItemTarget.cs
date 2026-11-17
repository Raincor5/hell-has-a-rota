using hhar.characters;

namespace hhar.items;

public interface IQuestItemTarget
{
    bool TryUseItem(Player player, ItemIds itemId);
}