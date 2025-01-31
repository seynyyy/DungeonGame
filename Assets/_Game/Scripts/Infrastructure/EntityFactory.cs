using _Game.Scripts.Character;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.DamagePopUp;
using _Game.Scripts.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Infrastructure
{
    public class EntityFactory : MonoBehaviour
    {
        [SerializeField] private GameObject entityPrefab;
        [SerializeField] private DamagePopUpPool damagePopUpPool;

        public AdventurerController CreateAdventurer(AdventurerDataSo adventurerData)
        {
            var adventurerName = adventurerData.adventurerName;
            var position = adventurerData.position;
            var color = adventurerData.color;
            var maxHp = adventurerData.maxHp;
            var hp = adventurerData.hp;
            var baseAtk = adventurerData.baseAtk;
            var attackRange = adventurerData.attackRange;
            var baseMs = adventurerData.baseMS;
            var baseCritRate = adventurerData.baseCritRate;
            var baseCritDmg = adventurerData.baseCritDmg;

            var characterGO = Instantiate(entityPrefab, position, Quaternion.identity);
            characterGO.name = adventurerName;

            var slider = characterGO.GetComponentInChildren<Slider>();
            var commandStorage = characterGO.AddComponent<CommandStorage>();
            var view = characterGO.AddComponent<AdventurerView>();
            var adventurerController = characterGO.AddComponent<AdventurerController>();
            var model = new AdventurerModel(view, adventurerName, maxHp, hp, baseAtk, attackRange, baseMs, baseCritRate,
                baseCritDmg, commandStorage);

            commandStorage.Init(adventurerData.commands);
            view.Init(model.GetHpContainer(), slider, color, damagePopUpPool);
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
            var attackRange = enemyData.attackRange;
            var baseMs = enemyData.baseMS;
            var baseCritRate = enemyData.baseCritRate;
            var baseCritDmg = enemyData.baseCritDmg;

            var characterGO = Instantiate(entityPrefab, position, Quaternion.identity);
            characterGO.name = adventurerName;

            /*
            var commandStorage = characterGO.AddComponent<CommandStorage>();
            commandStorage.Init(enemyData.commands);
            */

            var slider = characterGO.GetComponentInChildren<Slider>();
            
            var view = characterGO.AddComponent<EnemyView>();
            var enemyController = characterGO.AddComponent<EnemyController>();
            var model = new EnemyModel(view, adventurerName, maxHp, hp, baseAtk, attackRange, baseMs, baseCritRate,
                baseCritDmg /*, commandStorage*/);

            view.Init(model.GetHpContainer(), slider, color, damagePopUpPool);
            enemyController.Init(model, view);

            return enemyController;
        }
    }
}