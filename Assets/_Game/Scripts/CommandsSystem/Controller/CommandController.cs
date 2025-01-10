using System;
using System.Collections.Generic;
using _Game.Scripts.CommandsSystem.Model;
using _Game.Scripts.CommandsSystem.View;
using _Game.Scripts.Infrastructure;
using _Game.Scripts.Team;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.CommandsSystem.Controller
{
    public class CommandController : MonoBehaviour
    {
        private EntityController _ownerController;
        private EntityController _targetController;
        private Vector2 _targetPosition = Vector2.zero;

        [Header("Controllers")] [SerializeField]
        private TeamController teamController;

        [SerializeField] private CommandsView commandsView;

        private Command _currentCommand;

        public void Init()
        {
            commandsView.Init(ref teamController.OnAdventurerSelected);
        }

        public void SelectCommand(Command command)
        {
            _ownerController = teamController.selectedAdventurer;

            switch (command.TargetType)
            {
                case CommandTargetType.None:
                case CommandTargetType.Self:
                    break;
                case CommandTargetType.Enemy:
                    teamController.SetSelectionState(SelectionState.TargetSelection);
                    break;
                case CommandTargetType.Ally:
                    teamController.SetSelectionState(SelectionState.AllySelection);
                    break;
                case CommandTargetType.Position:
                    teamController.SetSelectionState(SelectionState.PositionSelection);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _currentCommand = command;

            switch (command.Status)
            {
                case CommandStatus.None:
                    break;
                case CommandStatus.Ready:
                    Debug.Log("Command is ready");
                    break;
                case CommandStatus.Cooldown:
                    Debug.Log("Command is on cooldown");
                    break;
                case CommandStatus.NeedResource:
                    Debug.Log("Need more resource");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SelectPosition(Vector2 position)
        {
            if (_currentCommand.Status == CommandStatus.Cooldown) return;
            _targetPosition = position;
            teamController.SetSelectionState(SelectionState.None);
        }
        
        public void SelectTarget(EntityController target)
        {
            _targetController = target;
            teamController.SetSelectionState(SelectionState.None);
        }

        private void FixedUpdate()
        {
            if (_currentCommand == null) return;

            if (teamController.SelectionState == SelectionState.None &&
                _currentCommand.CheckCondition(_ownerController, _targetController, _targetPosition))
            {
                _currentCommand.ApplyCommand();
                _currentCommand = null;
            }
            
        }
    }
}