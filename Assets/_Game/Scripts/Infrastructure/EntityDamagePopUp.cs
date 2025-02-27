using _Game.Scripts.DamagePopUp;
using UnityEngine;

namespace _Game.Scripts.Infrastructure
{
    public class EntityDamagePopUp : MonoBehaviour
    {
        protected internal void Init(DamagePopUpPool damagePopUpPool)
        {
            _damagePopUpPool = damagePopUpPool;
        }
        
        private DamagePopUpPool _damagePopUpPool;
        protected internal void ShowDamagePopUp(int damage, bool isCritical) => _damagePopUpPool.ShowDamagePopUp(damage, isCritical, transform.position);
    }
}