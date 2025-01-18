using System;
using System.Collections.Generic;
using _Game.Scripts.Character;
using _Game.Scripts.CommandsSystem;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.CommandsSystem.Model;
using _Game.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Team
{
    public class TeamController : MonoBehaviour
    {
        [SerializeField] private List<AdventurerDataSo> adventurersData;
        [SerializeField] private EntityFactory entityFactory;
        [SerializeField] private TeamView teamView;
        
        [HideInInspector] public AdventurerController selectedAdventurer; // TODO: Придумати показ команд для випадку null
        public Action<AdventurerController> OnAdventurerSelected;
        
        private readonly List<AdventurerController> _adventurers = new();
        public Action<AdventurerController> OnAdventurerRegistered;

        private Action<SelectionState> _onSelectionStateChanged;

        public SelectionState SelectionState { get; private set; }
        
        public IEnumerable<AdventurerController> GetAdventurers() => _adventurers;

        public void SetSelectionState(SelectionState state)
        {
            SelectionState = state;
            _onSelectionStateChanged?.Invoke(state);
        }
        
        private void RegisterAdventurer(AdventurerController adventurer)
        {
            _adventurers.Add(adventurer);
            OnAdventurerRegistered?.Invoke(adventurer);
        }

        public void UnregisterAdventurer(AdventurerController adventurer)
        {
            _adventurers.Remove(adventurer);
            OnAdventurerRegistered?.Invoke(adventurer);
        }

        public void Init()
        {
            teamView.Init(ref _onSelectionStateChanged);
            CreateAdventurers();
        }

        private void CreateAdventurers()
        {
            foreach (var data in adventurersData)
            {
                var adventurerController = entityFactory.CreateAdventurer(data);
                RegisterAdventurer(adventurerController);
                
                teamView.CreateAdventurerCardView(adventurerController.Model as AdventurerModel, adventurerController);
            }
        }
        
        public void SelectAdventurer(AdventurerController adventurerController)
        {
            if (selectedAdventurer == adventurerController)
            {
                selectedAdventurer = null;
                SetSelectionState(SelectionState.None);
            }
            else
            {
                selectedAdventurer = adventurerController;
                SetSelectionState(SelectionState.CommandSelection);
            }
            OnAdventurerSelected?.Invoke(selectedAdventurer);
        }
    }
}