using System;
using System.Collections.Generic;
using _Game.Scripts.Infrastructure;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem
{
    public class CommandController : MonoBehaviour
    {
        [SerializeField] private CommandStorage commandStorage;
        [SerializeField] private EntityController ownerController;
        [SerializeField] private EntityController targetController;
        [SerializeField] private Vector2 targetPosition;

        private List<Command> _commands = new List<Command>();
        private Command _curentCommand;
        

        public void Init()
        {
            commandStorage.Init();
            _commands.AddRange(commandStorage.GetCommands());
        }

        public void OnClickCommandButton(int commandIndex)
        {
            _curentCommand?.CancelCommand();

            switch (_commands[commandIndex].Status)
            {
                case CommandStatus.None:
                    break;
                case CommandStatus.Ready:
                    _curentCommand = _commands[commandIndex];
                    _curentCommand.StartCommand();
                    break;
                case CommandStatus.Cooldown:
                    break;
                case CommandStatus.NeedResource:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FixedUpdate()
        { 
            for (int i = 0; i < _commands.Count; ++i)
            {
                _commands[i].EventTick(Time.deltaTime);
            }
            
            
        }
    }
}