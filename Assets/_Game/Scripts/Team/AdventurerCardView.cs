using System;
using _Game.Scripts.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Team
{
    public class AdventurerCardView : MonoBehaviour
    {
        [SerializeField] private Slider healthBar;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image portraitImage;

        private bool _isSelected;

        private TeamController _teamController;
        private AdventurerController _adventurerController;

        public void Init(TeamController teamController, AdventurerController adventurerController, string adventurerName,
            Sprite portrait)
        {
            _teamController = teamController;
            _adventurerController = adventurerController;
            _teamController.OnAdventurerSelected += CardSelected;
            
            nameText.text = adventurerName;
            portraitImage.sprite = portrait;
            
            GetComponent<Button>().onClick.AddListener(() => _teamController.SelectAdventurer(_adventurerController));;
        }
        
        public void UpdateHealthBar(int hp, int maxHp)
        {
            healthBar.value = hp / (float)maxHp;
        }

        private void CardSelected(AdventurerController adventurerController)
        {
            if (_adventurerController == adventurerController)
            {
                HighlightCard();
            }
            else
            {
                UnhighlightCard();
            }
        }

        private void HighlightCard()
        {
            var color = Color.yellow;
            GetComponent<Image>().color = color;
        }

        private void UnhighlightCard()
        {
            var color = Color.white;
            GetComponent<Image>().color = color;
        }
    }
}