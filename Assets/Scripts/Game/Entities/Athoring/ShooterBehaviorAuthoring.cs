using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class ShooterBehaviourAuthoring : MonoBehaviour
    {
        public class Baker : Baker<ShooterBehaviourAuthoring>
        {
            public override void Bake(ShooterBehaviourAuthoring authoring)
            {
                var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
                AddComponent(entity, new ShooterBehaviour());
            }
        }
    }
}