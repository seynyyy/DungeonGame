using _Game.Scripts.CommandsSystem.Model;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem.Commands.AttackCommand
{
    [CreateAssetMenu(menuName = "Commands/Attack", fileName = "AttackConfig")]
    public class AttackConfig : CommandConfig
    {
        public override CommandFactory GetFactory()
        {
            return new AttackFactory(this);
        }
    }
}