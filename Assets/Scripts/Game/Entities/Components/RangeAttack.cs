using Unity.Entities;
using Unity.Mathematics;

namespace ReactionGames.TestTask.Game.Entities.Components
{
    public struct RangeAttack : IComponentData
    {
        public float Range;
        public float AttackInterval;
        public float3 AttackPositionOffset;
        public Entity ProjectilePrefab;
        public float LastAttackTime;
    }
}