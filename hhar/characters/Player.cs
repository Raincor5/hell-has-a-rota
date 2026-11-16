using hhar.items;
using hhar.characters.stats;
using hhar.characters.progression;
using hhar.characters.inventory;
namespace hhar.characters {
    public class Player : ICharacter
    {
        public string Name { get; set; }
        public LevelProgression LevelProgression { get; set; }
        public CharacterStats Stats { get; set; }
        public IInventory Inventory { get; set; }

        public IEquipment Equipment { get; set; }

        public Player(string name)
        {
            Name = name;
            LevelProgression = new LevelProgression(5);
            Stats = new CharacterStats();
            Inventory = new PlayerInventory();
            Equipment = new PlayerEquipment();
        }

        public void TakeDamage(int damage)
        {
            Stats.CurrentHealth -= damage;
            Console.WriteLine($"{Name} took {damage} damage. Remaining health: {Stats.CurrentHealth}");
        }

        public void DealDamage(ICharacter target, IItem Weapon)
        {
            var damage = ((Weapon)Weapon).Damage;
            target.TakeDamage(damage);
        }
    }
}