using UnityEngine;

namespace _Game.Scripts.Character
{
    [CreateAssetMenu(menuName = "Character", fileName = "Data")]
    public class AdventurerDataSo : ScriptableObject
    {
        [field:SerializeField] public string Name { get; set; } 
        [field:SerializeField] public Vector2 Position { get; set; } 
        [field:SerializeField] public Color Color { get; set; }
        [field:SerializeField] public int MaxHp { get; set; }
        [field:SerializeField] public int Hp { get; set; }
        [field: SerializeField] public int BaseAtk { get; set; } = 1;
        [field: SerializeField] public float BaseMS { get; set; } = 1f;
        [field: SerializeField, Range(0f, 1f)] public float BaseCritRate { get; set; } = 0.1f;
        [field: SerializeField, Range(1f, 10f)] public float BaseCritDmg { get; set; } = 1f;
    }
}