using System;

namespace _Game.Scripts.Infrastructure
{
    public abstract class EntityModel
    {
        public int MaxHp { get; private set; }
        public int Hp { get; private set; } //health point
        public int BaseAtk { get; private set; } //attack
        public float BaseMS { get; private set; } //move speed
        public float BaseCritRate { get; private set; } //critical hit rate
        public float BaseCritDmg { get; private set; } //critical hit damage multiplier

        public Action<int> OnHpChanged;

        public EntityView View { get; private set; }

        protected EntityModel(EntityView view, int maxHp, int hp, int baseAtk, float baseMS, float baseCritRate, float baseCritDmg)
        {
            View = view;
            MaxHp = maxHp;
            Hp = hp;
            BaseAtk = baseAtk;
            BaseMS = baseMS;
            BaseCritRate = baseCritRate;
            BaseCritDmg = baseCritDmg;
        }

        public void TakeDamage(int damage)
        {
            Hp = Math.Max(0, Hp - damage);
            OnHpChanged?.Invoke(Hp);
        }

        public void TakeHeal(int heal)
        {
            Hp = Math.Max(MaxHp, Hp + heal);
            OnHpChanged?.Invoke(Hp);
        }
    }
}
