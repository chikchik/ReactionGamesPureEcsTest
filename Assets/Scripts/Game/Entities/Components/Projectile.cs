using Unity.Entities;

namespace ReactionGames.TestTask.Game.Entities.Components
{
    public struct Projectile : IComponentData
    {
        public float LifeTime;
        public float SpawnTime;
    }
}