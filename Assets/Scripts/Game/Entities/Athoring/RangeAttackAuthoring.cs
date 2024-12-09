using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class RangeAttackAuthoring : MonoBehaviour
    {
        public float Range;
        public float AttackInterval;
        public Vector3 AttackPositionOffset;
        public GameObject ProjectilePrefab;

        public class Baker : Baker<RangeAttackAuthoring>
        {
            public override void Bake(RangeAttackAuthoring authoring)
            {
                var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
                var projectilePrefab = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic);
                AddComponent(entity, new RangeAttack()
                {
                    Range = authoring.Range,
                    AttackInterval = authoring.AttackInterval,
                    AttackPositionOffset = authoring.AttackPositionOffset,
                    ProjectilePrefab = projectilePrefab
                });
            }
        }
    }
}