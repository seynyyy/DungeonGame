using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Infrastructure
{
    public abstract class EntityController : MonoBehaviour
    {
        private readonly ActionContainer<Action<int, int>> _hpContainer = new();

        private NavMeshAgent _agent;
        public string Name { get; private set; }
        public int MaxHp { get; private set; }
        public int Hp { get; private set; } //health point
        public int BaseAtk { get; private set; } //attack
        public float AttackRange { get; private set; } //attack range
        public float BaseMS { get; private set; } //move speed
        public float BaseCritRate { get; private set; } //critical hit rate
        public float BaseCritDmg { get; private set; } //critical hit damage multiplier

        public EntityHealthBar healthBar { get; private set; }
        public EntityDamagePopUp damagePopUp { get; private set; }

        public ActionContainer<Action<int, int>> GetHpContainer()
        {
            return _hpContainer;
        }


        public void TakeDamage(int damage, bool isCritical)
        {
            Hp = Math.Max(0, Hp - damage);
            _hpContainer.Action?.Invoke(Hp, MaxHp);
            damagePopUp.ShowDamagePopUp(damage, isCritical);
        }

        public void TakeHeal(int heal)
        {
            Hp = Math.Min(MaxHp, Hp + heal);
            _hpContainer.Action?.Invoke(Hp, MaxHp);
        }

        public (int, bool) CalculateDamage(EntityController target)
        {
            int damage = BaseAtk;
            bool isCritical = Random.Range(0f, 1f) > BaseCritRate;
            if (isCritical) damage = (int)(damage * BaseCritDmg);

            return (damage, isCritical);
        }

        public void Init(string entityName, int maxHp, int hp, int baseAtk, float attackRange, float baseMS,
            float baseCritRate, float baseCritDmg)
        {
            Name = entityName;
            MaxHp = maxHp;
            Hp = hp;
            BaseAtk = baseAtk;
            AttackRange = attackRange;
            BaseMS = baseMS;
            BaseCritRate = baseCritRate;
            BaseCritDmg = baseCritDmg;

            _agent = gameObject.GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        public void MoveToPosition(Vector2 position)
        {
            _agent.SetDestination(position);
        }


        public bool CanReachPosition(Vector2 position)
        {
            var path = new NavMeshPath();
            return _agent.CalculatePath(position, path) && path.status == NavMeshPathStatus.PathComplete;
        }

        public abstract Task Attack(EntityController targetController);
    }
}