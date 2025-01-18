using _Game.Scripts.Infrastructure;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class EnemyView : EntityView
    {
        public Color Color { get; private set; }
        private SpriteRenderer _spriteRenderer;
        public void Init(Color color)
        {
            Color = color;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = color;
        }
    }
}