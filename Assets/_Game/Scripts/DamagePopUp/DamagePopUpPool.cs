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
            for (int i = 0; i < 10; i++)
            {
                _pool.Enqueue(CreatePopUp());
            }
        }

        public void ShowDamagePopUp(int damage, bool isCrit, Vector2 position)
        {
            var popUp = _pool.Count > 0 ? _pool.Dequeue() : CreatePopUp();
            popUp.Show(damage, isCrit, position);
            popUp.gameObject.SetActive(true);
        }

        public void HideDamagePopUp(DamagePopUp popUp)
        {
            popUp.gameObject.SetActive(false);
            _pool.Enqueue(popUp);
        }

        private DamagePopUp CreatePopUp()
        {
            var popUp = Instantiate(damagePopUpPrefab, transform).GetComponent<DamagePopUp>();
            popUp.Init(this);
            return popUp;
        }
    }
}