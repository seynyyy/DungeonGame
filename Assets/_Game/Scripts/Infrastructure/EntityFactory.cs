using System;
using _Game.Scripts.Character;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.Enemy;
using _Game.Scripts.Infrastructure._Game.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game.Scripts.Infrastructure
{
    public class EntityFactory : MonoBehaviour
    {
        [SerializeField] private GameObject entityPrefab;

        public AdventurerController CreateAdventurer(AdventurerDataSo adventurerData)
        {
            var adventurerName = adventurerData.adventurerName;
            var position = adventurerData.position;
            var color = adventurerData.color;
            var maxHp = adventurerData.maxHp;
            var hp = adventurerData.hp;
            var baseAtk = adventurerData.baseAtk;
            var baseMs = adventurerData.baseMS;
            var baseCritRate = adventurerData.baseCritRate;
            var baseCritDmg = adventurerData.baseCritDmg;

            var characterGO = Instantiate(entityPrefab, position, Quaternion.identity);
            characterGO.name = adventurerName;

            var slider = characterGO.GetComponentInChildren<Slider>();
            var commandStorage = characterGO.AddComponent<CommandStorage>();
            var view = characterGO.AddComponent<AdventurerView>();
            var adventurerController = characterGO.AddComponent<AdventurerController>();
            var model = new AdventurerModel(view, adventurerName, maxHp, hp, baseAtk, baseMs, baseCritRate,
                baseCritDmg, commandStorage);
            ActionContainer<Action<int,int>> onHpChanged = new(model.OnHpChanged) ;
            
            commandStorage.Init(adventurerData.commands);
            view.Init(onHpChanged,slider, color);
            adventurerController.Init(model, view);

            return adventurerController;
        }

        public EnemyController CreateEnemy(EnemyDataSo enemyData)
        {
            var adventurerName = enemyData.enemyName;
            var position = enemyData.position;
            var color = enemyData.color;
            var maxHp = enemyData.maxHp;
            var hp = enemyData.hp;
            var baseAtk = enemyData.baseAtk;
            var baseMs = enemyData.baseMS;
            var baseCritRate = enemyData.baseCritRate;
            var baseCritDmg = enemyData.baseCritDmg;

            var characterGO = Instantiate(entityPrefab, position, Quaternion.identity);
            characterGO.name = adventurerName;

            /*
            var commandStorage = characterGO.AddComponent<CommandStorage>();
            commandStorage.Init(enemyData.commands);
            */
            
            var view = characterGO.AddComponent<EnemyView>();
            view.Init(color);

            var model = new EnemyModel(view, adventurerName, maxHp, hp, baseAtk, baseMs, baseCritRate,
                baseCritDmg/*, commandStorage*/);

            var enemyController = characterGO.AddComponent<EnemyController>();
            enemyController.Init(model, view);

            return enemyController;
        }
    }
}