using System;
using _Game.Scripts.Infrastructure;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem
{
    public class Command
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Sprite DisplayImage { get; private set; }

        public float CooldownTime { get; private set; }
        public float CooldownTimer { get; private set; }

        public float CommandResourceCost { get; private set; }

        public CommandStatus Status { get; private set; }
        public Action<float, float> OnChangeCooldownTimer;

        public void SetDescription(string title, string description, Sprite displayImage)
        {
            Title = title;
            Description = description;
            DisplayImage = displayImage;
        }

        public void SetCooldown(float cooldown) => CooldownTime = cooldown;

        public void SetCommandResourceCost(float commandResourceCost) => CommandResourceCost = commandResourceCost;

        public void ChangeStatus(CommandStatus status) => Status = status;

        public void ChangeCooldownTimer(float timer)
        {
            CooldownTimer = Mathf.Clamp(timer, 0f, CooldownTime);
            OnChangeCooldownTimer?.Invoke(CooldownTimer, CooldownTime);
        }

        public virtual void StartCommand()
        {
        }

        public virtual bool CheckCondition(EntityController owner, EntityController target, Vector2 location)
        {
            return false;
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