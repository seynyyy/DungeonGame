using System.Collections.Generic;
using _Game.Scripts.CommandsSystem;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.Infrastructure;

namespace _Game.Scripts.Character
{
    public class AdventurerModel : EntityModel
    {
        private readonly CommandStorage _commandStorage;

        public AdventurerModel(EntityView view, string name, int maxHp, int hp, int baseAtk, float baseMS,
            float baseCritRate, float baseCritDmg, CommandStorage commandStorage) : base(view,
            name, maxHp, hp, baseAtk, baseMS, baseCritRate,
            baseCritDmg)
        {
            _commandStorage = commandStorage;
        }
        
        public CommandStorage GetCommandStorage()
        {
            return _commandStorage;
        }
    }
}