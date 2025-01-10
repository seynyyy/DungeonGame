using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Infrastructure
{
    public abstract class EntityController : MonoBehaviour
    {
        public EntityModel Model { get; private set; }
        private EntityView _view;
        private NavMeshAgent _agent;

        public void Init(EntityModel model, EntityView view)
        {
            Model = model;
            _view = view;
            
            _agent = gameObject.GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }
        
        public void MoveToPosition(Vector2 position)
        {
            _agent.SetDestination(position);
        }
        
        public bool CanReachPosition(Vector2 position)
        {
            var path = new NavMeshPath();
            return _agent.CalculatePath(position, path) && path.status == NavMeshPathStatus.PathComplete;
        }

        public void AttackTarget(EntityController target)
        {
            throw new NotImplementedException();
        }
    }
}
