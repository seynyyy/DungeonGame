using System;
using System.Collections.Generic;
using _Game.Scripts.Character;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.Enemy;
using _Game.Scripts.Infrastructure;
using _Game.Scripts.Team;
using UnityEngine;

namespace _Game.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private TeamController teamController;
        [SerializeField] private EnemyTeamController enemyTeamController;
        [SerializeField] private CommandController commandController;
        
        private void Awake()
        {
            teamController.Init();
            enemyTeamController.Init();
            commandController.Init();
        }
    }
}
