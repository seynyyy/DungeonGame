using System;
using _Game.Scripts.Infrastructure._Game.Scripts.Infrastructure;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Infrastructure
{
    public abstract class EntityModel
    {
        public string Name { get; private set; }
        public int MaxHp { get; private set; }
        public int Hp { get; private set; } //health point
        public int BaseAtk { get; private set; } //attack
        public float AttackRange { get; private set; } //attack range
        public float BaseMS { get; private set; } //move speed
        public float BaseCritRate { get; private set; } //critical hit rate
        public float BaseCritDmg { get; private set; } //critical hit damage multiplier

        private readonly ActionContainer<Action<int, int>> _hpContainer = new();

        public ActionContainer<Action<int, int>> GetHpContainer(object caller)
        {
            Debug.Log($"{this}, {caller}");
            return _hpContainer;
        }

        public EntityView View { get; private set; }

        protected EntityModel(EntityView view, string name, int maxHp, int hp, int baseAtk, float attackRange, float baseMS,
            float baseCritRate, float baseCritDmg)
        {
            View = view;
            Name = name;
            MaxHp = maxHp;
            Hp = hp;
            BaseAtk = baseAtk;
            AttackRange = attackRange;
            BaseMS = baseMS;
            BaseCritRate = baseCritRate;
            BaseCritDmg = baseCritDmg;
        }

        public void TakeDamage(int damage)
        {
            Hp = Math.Max(0, Hp - damage);
            _hpContainer.Action?.Invoke(Hp, MaxHp);
        }

        public void TakeHeal(int heal)
        {
            Hp = Math.Min(MaxHp, Hp + heal);
            _hpContainer.Action?.Invoke(Hp, MaxHp);
        }

        public int CalculateDamage(EntityModel target)      
        {
            var damage = BaseAtk;

            if (Random.Range(0f, 1f) > BaseCritRate)
            {
                damage = (int)(damage * BaseCritDmg);
            }

            return damage;
        }
    }
}