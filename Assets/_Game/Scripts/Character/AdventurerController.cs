using System;
using System.Threading.Tasks;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.Infrastructure;
using _Game.Scripts.Infrastructure.Entity;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class AdventurerController : EntityController
    {
        private CommandStorage _commandStorage;
        public CommandStorage GetCommandStorage()
        {
            return _commandStorage;
        }
        
        public void Init(string entityName, int maxHp, int hp, int baseAtk, float attackRange, float seekRange,float baseMS, float baseCritRate, float baseCritDmg, CommandStorage commandStorage)
        {
            base.Init(entityName, maxHp, hp, baseAtk, attackRange, seekRange, baseMS, baseCritRate, baseCritDmg);
            _commandStorage = commandStorage;
        }
        
        public override void Attack()
        {
            EntityController target = GetTarget();
            var (damage, isCritical) = CalculateDamage(target);
            target.TakeDamage(damage, isCritical);
        }
    }
}