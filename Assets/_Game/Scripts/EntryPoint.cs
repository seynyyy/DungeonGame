using System;
using System.Collections.Generic;
using _Game.Scripts.Character;
using _Game.Scripts.Team;
using UnityEngine;

namespace _Game.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [field: SerializeField] private TeamController teamController;
        
        private void Awake()
        {
            teamController.CreateAdventurers();
        }
    }
}
