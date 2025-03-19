using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Infrastructure.Entity
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
        public float SqrAttackRange { get; private set; }
        public float SeekRange { get; private set; } //seek range
        public float SqrSeekRange { get; private set; }
        public float BaseMS { get; private set; } //move speed
        public float BaseCritRate { get; private set; } //critical hit rate
        public float BaseCritDmg { get; private set; } //critical hit damage multiplier

        private EntityController _target;

        public EntityHealthBar healthBar { get; private set; }
        public EntityDamagePopUp damagePopUp { get; set; }
        
        [SerializeField, Range(1f, 10f)]private float attackCooldown = 1f;
        [SerializeField, ReadOnly] protected float attackCooldownTime = 0f;
        protected bool IsInAttackCooldown => attackCooldownTime < attackCooldown;

        public ActionContainer<Action<int, int>> GetHpContainer()
        {
            return _hpContainer;
        }

        protected bool IsTargetInAttackRange => _target && (transform.position - _target.transform.position).sqrMagnitude <= SqrAttackRange;

        public void TakeDamage(int damage, bool isCritical)
        {
            Hp = Math.Max(0, Hp - damage);
            damagePopUp.ShowDamagePopUp(damage, isCritical);
            _hpContainer.Action?.Invoke(Hp, MaxHp);
            
            if (Hp > 0) return;
            Destroy(gameObject);
            EntityRepository.Remove(this);
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

        protected void Init(string entityName, int maxHp, int hp, int baseAtk, float attackRange, float seekRange, float baseMS,
            float baseCritRate, float baseCritDmg)
        {
            Name = entityName;
            MaxHp = maxHp;
            Hp = hp;
            BaseAtk = baseAtk;
            AttackRange = attackRange;
            SeekRange = seekRange;
            SqrSeekRange = seekRange * seekRange;
            SqrAttackRange = attackRange * attackRange;
            BaseMS = baseMS;
            BaseCritRate = baseCritRate;
            BaseCritDmg = baseCritDmg;
            
            _agent = gameObject.GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        public void MoveToPosition(Vector2 position)
        {
            Vector3 destination = position;
            if (_agent.destination == destination) return;
            _agent.SetDestination(position);
        }

        public void MoveToTarget(EntityController target)
        {
            if (!target) return;
            MoveToPosition(target.transform.position);
        }

        public bool CanReachPosition(Vector2 position)
        {
            var path = new NavMeshPath();
            return _agent.CalculatePath(position, path) && path.status == NavMeshPathStatus.PathComplete;
        }

        public abstract void Attack();

        public void SetTarget(EntityController target)
        {
            _target = target;
        }

        public EntityController GetTarget()
        {
            return _target;
        }

        private void Update()
        {
            attackCooldownTime = Math.Min(attackCooldownTime + Time.deltaTime, attackCooldown);
        }
    }
}