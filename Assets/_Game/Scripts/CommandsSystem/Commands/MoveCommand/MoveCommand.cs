using _Game.Scripts.Character;
using _Game.Scripts.CommandsSystem.Failure;
using _Game.Scripts.CommandsSystem.Model;
using _Game.Scripts.Infrastructure;
using _Game.Scripts.Infrastructure.Entity;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem.Commands.MoveCommand
{
    public class MoveCommand : Command
    {
        private Vector2 _targetPosition;
        private EntityController _ownerController;

        public override FailureReason CheckCondition(EntityController owner, EntityController target, Vector2 location)
        {
            if (Status == CommandStatus.Cooldown) return FailureReason.NotReady;
            if (!owner || location == Vector2.zero) return FailureReason.CantReachLocation;

            if (owner is not AdventurerController || !owner.CanReachPosition(location)) return FailureReason.CantReachLocation;
            _ownerController = owner;
            _targetPosition = location;
            return FailureReason.None;
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
            if (!(CooldownTimer <= 0)) return;
            ChangeCooldownTimer(CooldownTime);
            ChangeStatus(CommandStatus.Ready);
        }
    }
}