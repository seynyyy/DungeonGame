using UnityEngine;

namespace _Game.Scripts.CommandsSystem
{
    [CreateAssetMenu(fileName = "New Command")]
    public class CommandConfig : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite DisplayImage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float CooldownTimer { get; private set; }
        [field: SerializeField] public float CommandResourceCost { get; private set; }
        public virtual CommandFactory GetFactory() => new CommandFactory(this);
    }
}