using _Game.Scripts.Infrastructure.Entity;
using _Game.Scripts.Infrastructure.Finite_State_Machine;
using _Game.Scripts.Infrastructure.Finite_State_Machine.States;

namespace _Game.Scripts.Enemy
{
    public class EnemyController : EntityController
    {
        private StateMachine _stateMachine;
        public new void Init(string entityName, int maxHp, int hp, int baseAtk, float attackRange, float seekRange,float baseMS,
            float baseCritRate, float baseCritDmg)
        {
            base.Init(entityName, maxHp, hp, baseAtk, attackRange, seekRange,baseMS,
                baseCritRate, baseCritDmg);
            _stateMachine = new StateMachine(new IState[]
            {
                new Idle(),
                new FindEnemy(this),
                new MoveToEnemy(this),
                new Attack(this)
            }, new ITransition[]
            {
                new Transition<Idle, FindEnemy>(() => EntityRepository.HasEntityInSeekRange(this)),
                new Transition<FindEnemy, MoveToEnemy>(() => !IsTargetInAttackRange),
                new Transition<MoveToEnemy, Attack>(() => GetTarget() && IsTargetInAttackRange && !IsInAttackCooldown),
                new Transition<Attack, FindEnemy>(() => !GetTarget() && !IsTargetInAttackRange),
                new Transition<Attack, MoveToEnemy>(() => GetTarget() && (!IsTargetInAttackRange || IsInAttackCooldown)),
                new Transition<MoveToEnemy, Idle>(() => !GetTarget()),
                new Transition<FindEnemy, Attack>(() => GetTarget() && IsTargetInAttackRange && !IsInAttackCooldown)
            });
        }
        
        public override void Attack()
        {
            EntityController target = GetTarget();
            var (damage, isCritical) = CalculateDamage(target);
            target.TakeDamage(damage, isCritical);
            
            attackCooldownTime = 0f;
        }
        
        private void FixedUpdate() => _stateMachine.Update();
    }
}