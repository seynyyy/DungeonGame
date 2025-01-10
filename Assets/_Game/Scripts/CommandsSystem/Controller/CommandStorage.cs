using System;
using System.Collections.Generic;
using _Game.Scripts.CommandsSystem.Model;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem.Controller
{
    public class CommandStorage : MonoBehaviour
    {
        private readonly List<Command> _commands = new();

        public void Init(List<CommandConfig> commandConfigs)
        {
            foreach (var commandConfig in commandConfigs)
            {
                var factory = commandConfig.GetFactory();

                factory.CreateCommand();
                _commands.Add(factory.GetCommand());
            }
        }

        private void Update()
        {
            foreach (var command in _commands)
            {
                command.EventTick(Time.deltaTime);
            }
        }

        public List<Command> GetCommands() => _commands;
    }
}