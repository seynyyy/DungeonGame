using System;
using _Game.Scripts.Infrastructure;
using _Game.Scripts.Infrastructure._Game.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Character
{
    public class AdventurerView : EntityView
    {
        public Color Color { get; private set; }
        private SpriteRenderer _spriteRenderer;
        public void Init(ActionContainer<Action<int, int>> onHpChanged,Slider slider, Color color)
        {
            base.Init(onHpChanged, slider);
            Color = color;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = color;
        }
    }
}