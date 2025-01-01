using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class AdventurerFactory : MonoBehaviour
    {
        [SerializeField] private GameObject characterPrefab;
        
        [field: SerializeField] private List<AdventurerDataSo> charactersDataSo;

        private void Awake()
        {
            CreateCharacters();
        }

        private void CreateCharacters()
        {
            foreach (var characterData in charactersDataSo)
            {
                string characterName = characterData.Name;
                Vector2 position = characterData.Position;
                Color color = characterData.Color;
                int maxHp = characterData.MaxHp;
                int hp = characterData.Hp;
                int baseAtk = characterData.BaseAtk;
                float baseMs = characterData.BaseMS;
                float baseCritRate = characterData.BaseCritRate;
                float baseCritDmg = characterData.BaseCritDmg;

                GameObject characterGO = Instantiate(characterPrefab, position, Quaternion.identity);
                characterGO.name = characterName;

                AdventurerView view = characterGO.AddComponent<AdventurerView>();
                view.Init(color);
                
                AdventurerModel model = new AdventurerModel(view, maxHp, hp, baseAtk, baseMs, baseCritRate, baseCritDmg);
                
                characterGO.AddComponent<AdventurerController>().Init(model, view);
            }
        }
    }
}