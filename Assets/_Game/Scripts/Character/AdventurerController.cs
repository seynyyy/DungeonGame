using System.Threading.Tasks;
using _Game.Scripts.Infrastructure;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class AdventurerController : EntityController
    {
        public override async Task Attack(EntityController target)
        {
            MoveToPosition(target.transform.position);
            while (Vector2.Distance(transform.position, target.transform.position) > Model.AttackRange)
            {
                await Task.Yield();
            }
            MoveToPosition(transform.position);
            var damage = Model.CalculateDamage(target.Model);
            target.Model.TakeDamage(damage);
        }
    }
}