using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class LifeTimeAuthoring : MonoBehaviour
    {
        public float Value;

        public class Baker : Baker<LifeTimeAuthoring>
        {
            public override void Bake(LifeTimeAuthoring authoring)
            {
                var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
                AddComponent(entity, new LifeTime() { Value = authoring.Value });
            }
        }
    }
}