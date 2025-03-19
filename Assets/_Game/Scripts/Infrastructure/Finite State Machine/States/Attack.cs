using _Game.Scripts.Infrastructure.Entity;

namespace _Game.Scripts.Infrastructure.Finite_State_Machine.States
{
    public class Attack : IUpdateState
    {
        private readonly EntityController _controller;

        public Attack(EntityController controller) => _controller = controller;
        
        public void Update()
        {
            _controller.Attack();
        }
    }
}