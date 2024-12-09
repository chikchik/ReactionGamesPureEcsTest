using Unity.Entities;
using Unity.Mathematics;

namespace ReactionGames.TestTask.Game.Entities.Components
{
    public struct ColliderVolume : IComponentData
    {
        public float Height;
        public float Radius;
        public float3 Center;
    }

    public struct CollisionContact : IBufferElementData
    {
        public Entity Entity;
    }
}