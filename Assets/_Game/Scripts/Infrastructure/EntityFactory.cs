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

            var characterGo = Instantiate(entityPrefab, position, Quaternion.identity);
            characterGo.name = adventurerName;

            var slider = characterGo.GetComponentInChildren<Slider>();
            var commandStorage = characterGo.AddComponent<CommandStorage>();
            var healthBar = characterGo.AddComponent<EntityHealthBar>();
            var damagePopUp = characterGo.AddComponent<EntityDamagePopUp>();
            var adventurerController = characterGo.AddComponent<AdventurerController>();

            adventurerController.Init(adventurerName, maxHp, hp, baseAtk, attackRange, baseMs, baseCritRate, baseCritDmg, commandStorage);
            commandStorage.Init(adventurerData.commands);
            healthBar.Init(adventurerController.GetHpContainer(), slider, color);
            damagePopUp.Init(damagePopUpPool);

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

            var enemyGo = Instantiate(entityPrefab, position, Quaternion.identity);
            enemyGo.name = adventurerName;

            /*
            var commandStorage = characterGO.AddComponent<CommandStorage>();
            commandStorage.Init(enemyData.commands);
            */

            var slider = enemyGo.GetComponentInChildren<Slider>();
            
            var healthBar = enemyGo.AddComponent<EnemyHealthBar>();
            var entityDamagePopUp = enemyGo.AddComponent<EntityDamagePopUp>();
            var enemyController = enemyGo.AddComponent<EnemyController>();

            healthBar.Init(enemyController.GetHpContainer(), slider, color);
            entityDamagePopUp.Init(damagePopUpPool);
            enemyController.Init(adventurerName, maxHp, hp, baseAtk, attackRange, baseMs, baseCritRate, baseCritDmg);

            return enemyController;
        }
    }
}