using System.Threading.Tasks;
using _Game.Scripts.CommandsSystem.Failure;
using _Game.Scripts.CommandsSystem.Model;
using _Game.Scripts.Enemy;
using _Game.Scripts.Infrastructure;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem.Commands.AttackCommand
{
    public class AttackCommand : Command
    {
        private EntityController _targetController;
        private EntityController _ownerController;
        
        public override FailureReason CheckCondition(EntityController owner, EntityController target, Vector2 location)
        {
            if (Status == CommandStatus.Cooldown) return FailureReason.NotReady;
            if (!target) return FailureReason.TargetNotFound;
            
            if (target is not EnemyController) return FailureReason.CantUseOnAllies;
            _ownerController = owner;
            _targetController = target;
            return FailureReason.None;
        }

        public override void ApplyCommand()
        {
            StartCommand();
        }
        
        private async Task Attack()
        {
            _ownerController.MoveToPosition(_targetController.transform.position);
            while (Vector2.Distance(_ownerController.transform.position, _targetController.transform.position) > _ownerController.AttackRange)
            {
                await Task.Yield();
            }
            _ownerController.MoveToPosition(_ownerController.transform.position);
            var (damage, isCritical) = _ownerController.CalculateDamage(_targetController);
            EndCommand();
            _targetController.TakeDamage(damage, isCritical);
        }

        public override void EventTick(float deltaTime)
        {
            if (Status != CommandStatus.Cooldown) return;
            ChangeCooldownTimer(CooldownTimer - deltaTime);
            if (!(CooldownTimer <= 0)) return;
            ChangeCooldownTimer(CooldownTime);
            ChangeStatus(CommandStatus.Ready);
        }
        
        public override void StartCommand()
        {
            _ = Attack();
        }

        public override void EndCommand()
        {
            ChangeCooldownTimer(CooldownTime);
            ChangeStatus(CommandStatus.Cooldown);
        }
    }
}