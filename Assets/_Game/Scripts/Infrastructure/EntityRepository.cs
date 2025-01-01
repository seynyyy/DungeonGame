using System.Collections.Generic;

namespace _Game.Scripts.Infrastructure
{
    public static class EntityRepository
    {
        private static List<EntityController> _entities;
        
        public static IEnumerable<EntityController> GetEntities()
        {
            return _entities;
        }

        public static void RegisterEntity(EntityController entity)
        {
            _entities.Add(entity);
        }
        
        public static void UnRegisterEntity(EntityController entity)
        {
            _entities.Remove(entity);
        }
    }
}