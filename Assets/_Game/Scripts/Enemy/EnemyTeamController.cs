using System;
using System.Collections.Generic;
using _Game.Scripts.Infrastructure;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class EnemyTeamController : MonoBehaviour
    {
        [SerializeField] private List<EnemyDataSo> adventurersData;
        [SerializeField] private EntityFactory entityFactory;

        public IEnumerable<EnemyController> GetEnemies() => _enemies;

        public Action<EnemyController> OnEnemyRegistered;
        public Action<EnemyController> OnEnemyUnregistered;

        private readonly List<EnemyController> _enemies = new();

        private void RegisterEnemy(EnemyController enemy)
        {
            _enemies.Add(enemy);
            OnEnemyRegistered?.Invoke(enemy);
        }

        public void UnregisterEnemy(EnemyController enemy)
        {
            _enemies.Remove(enemy);
            OnEnemyUnregistered?.Invoke(enemy);
        }
        
        public void Init()
        {
            CreateEnemies();
        }

        private void CreateEnemies()
        {
            foreach (var data in adventurersData)
            {
                var enemyController = entityFactory.CreateEnemy(data);
                RegisterEnemy(enemyController);
            }
        }
    }
}