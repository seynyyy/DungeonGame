using _Game.Scripts.Character;
using _Game.Scripts.CommandsSystem.Model;
using _Game.Scripts.Infrastructure;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem.Commands.MoveCommand
{
    public class MoveCommand : Command
    {
        private Vector2 _targetPosition;
        private EntityController _ownerController;

        public override bool CheckCondition(EntityController owner, EntityController target, Vector2 location)
        {
            if(Status == CommandStatus.Cooldown) return false;
            if (!owner || location == Vector2.zero) return false;
            
            if (owner is AdventurerController && owner.CanReachPosition(location))
            {
                _ownerController = owner;
                _targetPosition = location;
                return true;
            }
            return false;
        }
        
        public override void ApplyCommand()
        {
            _ownerController.MoveToPosition(_targetPosition);
            ChangeCooldownTimer(CooldownTime);
            ChangeStatus(CommandStatus.Cooldown);
        }

        public override void EventTick(float deltaTime)
        {
            if (Status != CommandStatus.Cooldown) return;
            ChangeCooldownTimer(CooldownTimer - deltaTime);
            if (CooldownTimer <= 0)
            {
                ChangeCooldownTimer(CooldownTime);
                ChangeStatus(CommandStatus.Ready);
            }
        }
    }
}