using System;
using System.Collections.Generic;
using _Game.Scripts.CommandsSystem;
using _Game.Scripts.CommandsSystem.Controller;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class AdventurerFactory : MonoBehaviour
    {
        [SerializeField] private GameObject adventurerPrefab;

        public AdventurerController CreateAdventurer(AdventurerDataSo adventurerData)
        {
            string adventurerName = adventurerData.adventurerName;
            Vector2 position = adventurerData.position;
            Color color = adventurerData.color;
            int maxHp = adventurerData.maxHp;
            int hp = adventurerData.hp;
            int baseAtk = adventurerData.baseAtk;
            float baseMs = adventurerData.baseMS;
            float baseCritRate = adventurerData.baseCritRate;
            float baseCritDmg = adventurerData.baseCritDmg;

            GameObject characterGO = Instantiate(adventurerPrefab, position, Quaternion.identity);
            characterGO.name = adventurerName;

            CommandStorage commandStorage = characterGO.AddComponent<CommandStorage>();
            commandStorage.Init(adventurerData.commands);
            
            AdventurerView view = characterGO.AddComponent<AdventurerView>();
            view.Init(color);

            AdventurerModel model = new AdventurerModel(view, adventurerName, maxHp, hp, baseAtk, baseMs, baseCritRate,
                baseCritDmg, commandStorage);

            AdventurerController adventurerController = characterGO.AddComponent<AdventurerController>();
            adventurerController.Init(model, view);

            return adventurerController;
        }
    }
}