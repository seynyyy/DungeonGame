using System.Collections.Generic;
using _Game.Scripts.CommandsSystem;
using _Game.Scripts.CommandsSystem.Model;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Character
{
    [CreateAssetMenu(menuName = "Character", fileName = "Data")]
    public class AdventurerDataSo : ScriptableObject
    {
        public string adventurerName;
        public Vector2 position;
        public Color color;
        public int maxHp;
        public int hp;
        public int baseAtk = 1;
        [Range(0.1f, 10f)] public float attackRange = 1f;
        [Range(5f, 20f)] public float seekRange = 5f;
        public float baseMS = 1f;
        public float attackCooldown = 1f;
        [Range(0f, 1f)] public float baseCritRate = 0.1f;
        [Range(1f, 10f)] public float baseCritDmg = 1f;
        
        [field: SerializeField] public List<CommandConfig> commands;
    }
}