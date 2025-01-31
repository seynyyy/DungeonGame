using System;
using System.Collections.Generic;
using _Game.Scripts.CommandsSystem.Failure;
using _Game.Scripts.CommandsSystem.Model;
using _Game.Scripts.CommandsSystem.View;
using _Game.Scripts.Infrastructure;
using _Game.Scripts.Team;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.CommandsSystem.Controller
{
    public class CommandController : MonoBehaviour
    {
        [SerializeField] private TeamController teamController;
        [SerializeField] private CommandsView commandsView;
        [SerializeField] private TMP_Text commandFailureText;

        private EntityController _ownerController;
        private EntityController _targetController;
        private Vector2 _targetPosition;
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
        }

        public void SelectPosition(Vector2 position)
        {
            if (_currentCommand == null) return;
            _targetPosition = position;

            var failureReason = _currentCommand.CheckCondition(_ownerController, _targetController, _targetPosition);

            if (failureReason != FailureReason.None)
            {
                commandFailureText.text = FailureReasonHandler.GetFailureReasonString(failureReason);
                return;
            }
            commandFailureText.text = $"Successfully applied command {_currentCommand.Title}";

            _currentCommand.ApplyCommand();
            _currentCommand = null;
            teamController.SetSelectionState(SelectionState.None);
        }

        public void SelectTarget(EntityController target)
        {
            if (_currentCommand == null) return;
            _targetController = target;

            var failureReason = _currentCommand.CheckCondition(_ownerController, _targetController, _targetPosition);

            if (failureReason != FailureReason.None)
            {
                commandFailureText.text = FailureReasonHandler.GetFailureReasonString(failureReason);
                return;
            }
            commandFailureText.text = $"Successfully applied command {_currentCommand.Title}";

            _currentCommand.ApplyCommand();
            _currentCommand = null;
            _targetController = null;
            teamController.SetSelectionState(SelectionState.None);
        }
    }
}