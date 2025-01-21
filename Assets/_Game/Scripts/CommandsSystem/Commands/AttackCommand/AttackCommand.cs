using _Game.Scripts.Character;
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
        
        public override bool CheckCondition(EntityController owner, EntityController target, Vector2 location)
        {
            if(Status == CommandStatus.Cooldown) return false;
            if (!owner || !target) return false;

            if (owner is not AdventurerController || target is not EnemyController) return false;
            _ownerController = owner;
            _targetController = target;
            return true;
        }

        public override void ApplyCommand()
        {
            _ownerController.Attack(_targetController);
            ChangeCooldownTimer(CooldownTime);
            ChangeStatus(CommandStatus.Cooldown);
        }
        
        public override void EventTick(float deltaTime)
        {
            if (Status != CommandStatus.Cooldown) return;
            ChangeCooldownTimer(CooldownTimer - deltaTime);
            if (!(CooldownTimer <= 0)) return;
            ChangeCooldownTimer(CooldownTime);
            ChangeStatus(CommandStatus.Ready);
        }
    }
}