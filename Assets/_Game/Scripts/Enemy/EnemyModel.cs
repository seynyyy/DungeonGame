using _Game.Scripts.Infrastructure;

namespace _Game.Scripts.Enemy
{
    public class EnemyModel : EntityModel
    {
        public EnemyModel(EnemyView view, string name, int maxHp, int hp, int baseAtk, float attackRange, float baseMS,
            float baseCritRate, float baseCritDmg) : base(view, name, maxHp, hp, baseAtk, attackRange, baseMS,
            baseCritRate, baseCritDmg)
        {
        }
    }
}