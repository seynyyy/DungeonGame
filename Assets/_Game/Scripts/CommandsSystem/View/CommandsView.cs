using System;
using _Game.Scripts.Character;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.Infrastructure._Game.Scripts.Infrastructure;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.CommandsSystem.View
{
    public class CommandsView : MonoBehaviour
    {
        [SerializeField] private CommandController commandController;
        [SerializeField] private Transform commandPanelContent;
        [SerializeField] private GameObject commandCardPrefab;

        public void Init(ActionContainer<Action<AdventurerController>> onAdventurerSelected)
        {
            onAdventurerSelected.Subscribe(UpdateCommandPanel);
        }

        private void UpdateCommandPanel(AdventurerController selectedAdventurer)
        {
            if (!selectedAdventurer)
            {
                ClearCommandPanel();
                return;
            }

            ClearCommandPanel();
            var model = selectedAdventurer.Model as AdventurerModel;

            foreach (var command in model?.GetCommandStorage().GetCommands()!)
            {
                var commandCard = Instantiate(commandCardPrefab, commandPanelContent);
                var commandCardView = commandCard.GetComponent<CommandCardView>();
                commandCardView.Init(commandController, command, command.DisplayImage, command.CommandTimerContainer);
            }
        }

        private void ClearCommandPanel()
        {
            foreach (Transform child in commandPanelContent) Destroy(child.gameObject);
        }
    }
}