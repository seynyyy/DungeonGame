using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.DamagePopUp
{
    public class DamagePopUpPool : MonoBehaviour
    {
        [SerializeField] private GameObject damagePopUpPrefab;

        private readonly Queue<DamagePopUp> _pool = new();

        private void Awake()
        {
            const int count = 10;
            for (var i = 0; i < count; i++)
            {
                var popUp = Instantiate(damagePopUpPrefab, gameObject.transform).GetComponent<DamagePopUp>();
                popUp.Init(this);
                _pool.Enqueue(popUp);
            }
        }

        public void ShowDamagePopUp(int damage, bool isCrit, Vector2 position)
        {
            if (_pool.TryDequeue(out var popUp))
            {
                popUp.Show(damage, isCrit, position);
                popUp.gameObject.SetActive(true);
            }
            else
            {
                popUp = Instantiate(damagePopUpPrefab, gameObject.transform).GetComponent<DamagePopUp>();
                popUp.Init(this);
            }
        }

        public void HideDamagePopUp(DamagePopUp popUp)
        {
            popUp.gameObject.SetActive(false);
            _pool.Enqueue(popUp);
        }
    }
}