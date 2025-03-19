using _Game.Scripts.Infrastructure.Entity;

namespace _Game.Scripts.Infrastructure.Finite_State_Machine.States
{
    public class MoveToEnemy : IUpdateState
    {
        private readonly EntityController _entityController;
        
        public MoveToEnemy(EntityController entityController)
        {
            _entityController = entityController;
        }
        
        public void Update()
        {
            _entityController.MoveToTarget(_entityController.GetTarget());
        }
    }
}