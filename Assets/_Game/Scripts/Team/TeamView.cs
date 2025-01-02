using _Game.Scripts.Character;
using UnityEngine;

namespace _Game.Scripts.Team
{
    public class TeamView : MonoBehaviour
    {
        [SerializeField] private TeamController teamController;
        [SerializeField] private Transform adventurersPanelContent;
        [SerializeField] private GameObject adventurerCardPrefab;

        public void CreateAdventurerCard(AdventurerModel adventurerModel)
        {
            var adventurerCard = Instantiate(adventurerCardPrefab, adventurersPanelContent);
            var adventurerCardView = adventurerCard.GetComponent<AdventurerCardView>();
            adventurerModel.OnHpChanged += adventurerCardView.UpdateHealthBar;
            adventurerCardView.UpdateHealthBar(adventurerModel.Hp, adventurerModel.MaxHp);
            
            adventurerCardView.NameText.text = adventurerModel.Name;
            adventurerCardView.PortraitImage.color = Color.cyan;
        }
    }
}