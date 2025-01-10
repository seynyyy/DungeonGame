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
        private Command _command;
        private CommandController _commandController;
        [SerializeField] private Image displayImage;
        [SerializeField] private Image cooldownImage;
        [SerializeField] private TMP_Text costText;

        public void Init(CommandController commandController, Command command, Sprite image,
            ref Action<float, float> onUpdateCardView)
        {
            _commandController = commandController;
            _command = command;

            GetComponent<Button>().onClick.AddListener(() => _commandController.SelectCommand(command));

            displayImage.sprite = image;
            onUpdateCardView += UpdateCardView;

            UpdateCardView(_command.CooldownTime, _command.CooldownTimer);
        }

        private void UpdateCardView(float cooldownTime, float cooldownTimer)
        {
            if (!cooldownImage) return;
            cooldownImage.fillAmount = cooldownTime / cooldownTimer;
        }

        private void OnDestroy()
        {
            _commandController = null;
            _command = null;
        }
    }
}