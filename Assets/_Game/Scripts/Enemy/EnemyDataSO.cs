using UnityEngine;

namespace _Game.Scripts.Enemy
{
    [CreateAssetMenu(menuName = "Enemy", fileName = "Data")]
    public class EnemyDataSo : ScriptableObject
    {
        public string enemyName;
        public Vector2 position;
        public Color color;
        public int maxHp;
        public int hp;
        public int baseAtk = 1;
        [Range(0.1f, 10f)] public float attackRange = 1f;
        [Range(0.1f, 10f)] public float seekRange = 1f;
        public float baseMS = 1f;
        public float attackCooldown = 1f;
        [Range(0f, 1f)] public float baseCritRate = 0.1f;
        [Range(1f, 10f)] public float baseCritDmg = 1f;
    }
}