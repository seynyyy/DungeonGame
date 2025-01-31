using UnityEngine;

namespace _Game.Scripts.DamagePopUp
{
    public class DamagePopUp : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshPro text;
        private DamagePopUpPool _damagePopUpPool;

        public void Init (DamagePopUpPool damagePopUpPool) => _damagePopUpPool = damagePopUpPool;
        
        public void Show (int damage, bool isCrit, Vector2 position)
        {
            text.text = damage.ToString();
            gameObject.transform.position = new Vector3(position.x, position.y+1, -9);
            text.color = isCrit ? Color.red : Color.white;
        }
        public void Hide() => _damagePopUpPool.HideDamagePopUp(this);
    }
}