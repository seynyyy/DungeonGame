using _Game.Scripts.CommandsSystem.Model;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem.Commands.MoveCommand
{
    [CreateAssetMenu(menuName = "Commands/Move", fileName = "MoveConfig")]
    public class MoveConfig : CommandConfig
    {
        public override CommandFactory GetFactory()
        {
            return new MoveFactory(this);
        }
    }
}