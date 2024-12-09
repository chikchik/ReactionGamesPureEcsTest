using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class HealthAuthoring : MonoBehaviour
    {
        public float Value;

        public class Baker : Baker<HealthAuthoring>
        {
            public override void Bake(HealthAuthoring authoring)
            {
                var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
                AddComponent(entity, new Health() { Value = authoring.Value, MaxValue = authoring.Value });
            }
        }
    }
}