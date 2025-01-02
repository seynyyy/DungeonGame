using _Game.Scripts.Infrastructure;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class AdventurerModel : EntityModel
    {
        public AdventurerModel(EntityView view, string name, int maxHp, int hp, int baseAtk, float baseMS,
            float baseCritRate, float baseCritDmg) : base(view, name, maxHp, hp, baseAtk, baseMS, baseCritRate,
            baseCritDmg)
        {
        }
    }
}