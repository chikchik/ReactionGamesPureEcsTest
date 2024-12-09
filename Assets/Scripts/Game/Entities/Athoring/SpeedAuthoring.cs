using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class SpeedAuthoring : MonoBehaviour
    {
        public float Value;

        public class Baker : Baker<SpeedAuthoring>
        {
            public override void Bake(SpeedAuthoring authoring)
            {
                var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
                AddComponent(entity, new Speed() { Value = authoring.Value });
            }
        }
    }
}