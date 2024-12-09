using Unity.Entities;
using Unity.Mathematics;

namespace ReactionGames.TestTask.Game.Entities.Components
{
    public struct MoveTarget : IComponentData
    {
        public float3 Value;
    }
}