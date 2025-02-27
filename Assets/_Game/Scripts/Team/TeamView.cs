using System;
using _Game.Scripts.Character;
using _Game.Scripts.Infrastructure;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Team
{
    public class TeamView : MonoBehaviour
    {
        [Header("Controllers")] 
        [SerializeField] private TeamController teamController;

        [Header("Prefabs")] 
        [SerializeField] private GameObject adventurerCardPrefab;
        [SerializeField] private GameObject commandCardPrefab;

        [Header("UI")] 
        [SerializeField] private Transform adventurersPanelContent;
        [SerializeField] private GameObject selectionPopup;
        [SerializeField] private TMP_Text selectionPopupText;

        public void Init(ActionContainer<Action<SelectionState>>  onSelectionStateChanged)
        {
            onSelectionStateChanged.Subscribe(OnSelectionStateChanged);
        }
        
        private void OnSelectionStateChanged(SelectionState selectionState)
        {
            switch (selectionState)
            {
                case SelectionState.None:
                    selectionPopup.SetActive(false);
                    break;
                case SelectionState.CommandSelection:
                    selectionPopup.SetActive(true);
                    selectionPopupText.text = "Select command";
                    break;
                case SelectionState.PositionSelection:
                    selectionPopup.SetActive(true);
                    selectionPopupText.text = "Select position";
                    break;
                case SelectionState.TargetSelection:
                    selectionPopup.SetActive(true);
                    selectionPopupText.text = "Select target";
                    break;
                case SelectionState.AllySelection:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void CreateAdventurerCardView(AdventurerController adventurerController)
        {
            var adventurerCard = Instantiate(adventurerCardPrefab, adventurersPanelContent);
            var adventurerCardView = adventurerCard.GetComponent<AdventurerCardView>();

            adventurerController.GetHpContainer().Subscribe(adventurerCardView.UpdateHealthBar);
            adventurerCardView.UpdateHealthBar(adventurerController.Hp, adventurerController.MaxHp);

            adventurerCardView.Init(teamController, adventurerController, adventurerController.Name, null); //TODO: додати портрет
        }
    }
}