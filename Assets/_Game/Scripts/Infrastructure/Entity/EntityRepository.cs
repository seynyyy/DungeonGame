using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Character;

namespace _Game.Scripts.Infrastructure.Entity
{
    public static class EntityRepository
    {
        private static readonly List<EntityController> Entities;
        private static readonly List<AdventurerController> Adventurers;
        public static Action<EntityController> OnEntityRegistered;
        
        static EntityRepository()
        {
            Entities = new List<EntityController>();
            Adventurers = new List<AdventurerController>();
        }
        
        public static void Add(EntityController entityController)
        {
            Entities.Add(entityController);
            if (entityController is AdventurerController adventurerController)
            {
                Adventurers.Add(adventurerController);
            }
            OnEntityRegistered?.Invoke(entityController);
        }
        
        public static void Remove(EntityController entityController) {
            Entities.Remove(entityController);
            if (entityController is AdventurerController adventurerController)
                Adventurers.Remove(adventurerController);
        }

        public static IEnumerable<EntityController> GetEntities() => Entities;
        
        public static IEnumerable<AdventurerController> GetAdventurers() => Adventurers;
        
        public static EntityController GetClosestEntity(EntityController entityController)
        {
            
            EntityController closestEntity = null;
            var minDistance = float.MaxValue;
            foreach (var entity in Entities)
            {
                if (entity == entityController || entity.GetType() == entityController.GetType())
                {
                    continue;
                }
                var distance = (entity.transform.position - entityController.transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEntity = entity;
                }
            }
            return closestEntity;
        }
        
        public static bool HasEntityInAttackRange(EntityController entityController)
        {
            return (from entity in Entities where entity != entityController && entity.GetType() != entityController.GetType() select (entity.transform.position - entityController.transform.position).sqrMagnitude).Any(distance => distance <= entityController.SqrAttackRange);
        }
        
        public static bool HasEntityInSeekRange(EntityController entityController)
        {
            return (from entity in Entities where entity != entityController && entity.GetType() != entityController.GetType() select (entity.transform.position - entityController.transform.position).sqrMagnitude).Any(distance => distance <= entityController.SqrSeekRange);
        }
    }
}