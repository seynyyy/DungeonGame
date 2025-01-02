using System;
using System.Collections.Generic;
using _Game.Scripts.Character;
using UnityEngine;

namespace _Game.Scripts.Team
{
    public class TeamController : MonoBehaviour
    {
        [field: SerializeField] private List<AdventurerDataSo> adventurersData;
        [field: SerializeField] private AdventurerFactory adventurerFactory;
        [field: SerializeField] private TeamView teamView;

        private List<AdventurerController> _adventurers = new();
        public Action OnAdventurerRegistered;

        public IEnumerable<AdventurerController> GetAdventurers()
        {
            return _adventurers;
        }

        public void RegisterAdventurer(AdventurerController adventurer)
        {
            _adventurers.Add(adventurer);
            OnAdventurerRegistered?.Invoke();
        }

        public void UnregisterAdventurer(AdventurerController adventurer)
        {
            _adventurers.Remove(adventurer);
            OnAdventurerRegistered?.Invoke();
        }

        public void CommandAdventurers()
        {
            throw new NotImplementedException();
        }

        public void CreateAdventurers()
        {
            foreach (var data in adventurersData)
            {
                var adventurerController = adventurerFactory.CreateAdventurer(data);
                RegisterAdventurer(adventurerController);
                
                teamView.CreateAdventurerCard(adventurerController.Model as AdventurerModel);
            }
        }
    }
}