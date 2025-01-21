using System;
using _Game.Scripts.Infrastructure._Game.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Infrastructure
{
    public abstract class EntityView : MonoBehaviour
    {
        private Slider _healthBar;
        private ActionContainer<Action<int, int>> _onHealthChanged;

        protected void Init(ActionContainer<Action<int, int>> onHealthChanged, Slider healthBar)
        {
            _onHealthChanged = onHealthChanged;
            _onHealthChanged.Action += UpdateHealthBar;
            _healthBar = healthBar;
        }

        private void UpdateHealthBar(int hp, int maxHp)
        {
            _healthBar.value = hp / (float)maxHp;
        }

        private void OnDestroy()
        {
            if (_onHealthChanged != null)
                _onHealthChanged.Action -= UpdateHealthBar;
        }
    }
}