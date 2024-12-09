using Unity.Entities;
using Unity.Mathematics;

namespace ReactionGames.TestTask.Game.Entities.Components
{
    public struct PatrolBehavior : IComponentData
    {
        public int NextPointIndex;
    }

    public struct PatrolPoint : IBufferElementData
    {
        public float3 Value;
    }
}