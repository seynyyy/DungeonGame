using System;
using _Game.Scripts.DamagePopUp;
using _Game.Scripts.Infrastructure._Game.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Infrastructure
{
    public abstract class EntityView : MonoBehaviour
    {
        private Slider _healthBar;
        private ActionContainer<Action<int, int>> _onHealthChanged;
        private SpriteRenderer _spriteRenderer;
        private DamagePopUpPool _damagePopUpPool;

        protected internal void Init(ActionContainer<Action<int, int>> onHealthChanged, Slider healthBar, Color color, DamagePopUpPool damagePopUpPool)
        {
            _onHealthChanged = onHealthChanged;
            onHealthChanged.Subscribe(UpdateHealthBar);
            _healthBar = healthBar;
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = color;
            _damagePopUpPool = damagePopUpPool;
        }

        protected internal void ShowDamagePopUp(int damage, bool isCritical) => _damagePopUpPool.ShowDamagePopUp(damage, isCritical, transform.position);
        
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