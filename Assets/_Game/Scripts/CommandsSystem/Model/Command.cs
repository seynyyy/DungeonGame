using System;
using _Game.Scripts.CommandsSystem.Failure;
using _Game.Scripts.Infrastructure;
using _Game.Scripts.Infrastructure.Entity;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem.Model
{
    public class Command
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Sprite DisplayImage { get; private set; }

        public float CooldownTime { get; private set; }
        public float CooldownTimer { get; private set; }

        public float CommandResourceCost { get; private set; }
        public CommandTargetType TargetType { get; private set; }

        public CommandStatus Status { get; private set; }
        public readonly ActionContainer<Action<float, float>> CommandTimerContainer = new();

        public void SetDescription(string title, string description, Sprite displayImage)
        {
            Title = title;
            Description = description;
            DisplayImage = displayImage;
        }

        public void SetCooldown(float cooldown) => CooldownTime = cooldown;

        public void SetCommandResourceCost(float commandResourceCost) => CommandResourceCost = commandResourceCost;

        public void SetTargetType(CommandTargetType targetType) => TargetType = targetType;
        
        public void ChangeStatus(CommandStatus status) => Status = status;

        public void ChangeCooldownTimer(float timer)
        {
            CooldownTimer = Mathf.Clamp(timer, 0f, CooldownTime);
            CommandTimerContainer.Action?.Invoke(CooldownTimer, CooldownTime);
        }
        
        public virtual void StartCommand()
        {
        }
        
        public virtual void EndCommand()
        {
        }

        public virtual FailureReason CheckCondition(EntityController owner, EntityController target, Vector2 location)
        {
            return FailureReason.None;
        }

        public virtual void ApplyCommand()
        {
        }

        public virtual void EventTick(float deltaTime)
        {
        }

        public virtual void CancelCommand()
        {
        }
    }
}