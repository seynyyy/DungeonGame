using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Team
{
    public class AdventurerCardView : MonoBehaviour
    {
        [SerializeField] private Slider healthBar;
        [SerializeField] private TMP_Text nameText;

        public TMP_Text NameText
        {
            get => nameText;
            set => nameText = value;
        }

        public Image PortraitImage
        {
            get => portraitImage;
            set => portraitImage = value;
        }

        [SerializeField] private Image portraitImage;
        public void UpdateHealthBar(int hp, int maxHp)
        {
            healthBar.value = hp/(float)maxHp;
        }
    }
}