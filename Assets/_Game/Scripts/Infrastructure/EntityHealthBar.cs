using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Infrastructure
{
    public class EntityHealthBar : MonoBehaviour
    {
        private Slider _healthBar;
        private ActionContainer<Action<int, int>> _onHealthChanged;
        private SpriteRenderer _spriteRenderer;


        protected internal void Init(ActionContainer<Action<int, int>> onHealthChanged, Slider healthBar, Color color)
        {
            _onHealthChanged = onHealthChanged;
            onHealthChanged.Subscribe(UpdateHealthBar);
            _healthBar = healthBar;
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = color;
        }

        
        
        private void UpdateHealthBar(int hp, int maxHp)
        {
            _healthBar.value = hp / (float)maxHp;
        }

        private void OnDestroy()
        {
            _onHealthChanged?.Unsubscribe(UpdateHealthBar);
        }
    }
}