using System.Threading.Tasks;
using _Game.Scripts.Infrastructure;

namespace _Game.Scripts.Enemy
{
    public class EnemyController : EntityController
    {
        public override async Task Attack(EntityController targetController)
        {
            await Task.Delay(1000);
        }
    }
}