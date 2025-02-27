using System.Threading.Tasks;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.Infrastructure;
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
        
        public void Init(string entityName, int maxHp, int hp, int baseAtk, float attackRange, float baseMS, float baseCritRate, float baseCritDmg, CommandStorage commandStorage)
        {
            base.Init(entityName, maxHp, hp, baseAtk, attackRange, baseMS, baseCritRate, baseCritDmg);
            _commandStorage = commandStorage;
        }
        
        public override async Task Attack(EntityController target)
        {
            MoveToPosition(target.transform.position);
            while (Vector2.Distance(transform.position, target.transform.position) > AttackRange)
            {
                await Task.Yield();
            }
            MoveToPosition(transform.position);
            var (damage, isCritical) = CalculateDamage(target);
            target.TakeDamage(damage, isCritical);
        }
    }
}