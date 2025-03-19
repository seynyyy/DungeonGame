using _Game.Scripts.Infrastructure.Entity;

namespace _Game.Scripts.Infrastructure.Finite_State_Machine.States
{
    public class FindEnemy : IEnterState
    {
        private readonly EntityController _controller;
        
        public FindEnemy(EntityController controller)
        {
            _controller = controller;
        }
        
        public void Enter()
        {
            EntityController target = EntityRepository.GetClosestEntity(_controller);
            _controller.SetTarget(target);
        }
    }
}