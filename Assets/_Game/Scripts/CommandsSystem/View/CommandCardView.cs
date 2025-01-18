using System;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.CommandsSystem.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.CommandsSystem.View
{
    public class CommandCardView : MonoBehaviour
    {
        [SerializeField] private Image displayImage;
        [SerializeField] private Image cooldownImage;
        [SerializeField] private TMP_Text costText;

        private Command _command;
        private CommandController _commandController;
        private CommandTimer _commandTimer;

        public void Init(CommandController commandController, Command command, Sprite image, CommandTimer commandTimer)
        {
            _commandController = commandController;
            _command = command;

            GetComponent<Button>().onClick.AddListener(() => _commandController.SelectCommand(command));

            displayImage.sprite = image;
            _commandTimer = commandTimer;
            commandTimer.OnChangeCooldownTimer += UpdateCardView;

            UpdateCardView(_command.CooldownTime, _command.CooldownTimer);
        }

        private void UpdateCardView(float cooldownTime, float cooldownTimer)
        {
            cooldownImage.fillAmount = cooldownTime / cooldownTimer;
        }

        private void OnDestroy()
        {
            _commandTimer.OnChangeCooldownTimer -= UpdateCardView;
            _commandController = null;
            _command = null;
        }
    }
}