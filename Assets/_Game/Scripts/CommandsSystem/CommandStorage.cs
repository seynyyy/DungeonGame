using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem
{
    public class CommandStorage : MonoBehaviour
    {
        [field: SerializeField] private CommandConfig[] commandConfigs;

        private List<Command> _commands = new List<Command>();

        public void Init()
        {
            for (int i = 0; i < commandConfigs.Length; i++)
            {
                var factory = commandConfigs[i].GetFactory();

                factory.CreateCommand();
                _commands.Add(factory.GetCommand());
            }
        }

        public Command[] GetCommands() => _commands.ToArray();
    }
}