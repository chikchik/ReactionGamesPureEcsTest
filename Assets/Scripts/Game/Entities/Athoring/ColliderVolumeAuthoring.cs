using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class BoxColliderBaker : Baker<BoxCollider>
    {
        public override void Bake(BoxCollider authoring)
        {
            var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
            AddComponent(entity, new ColliderVolume()
            {
                Height = authoring.size.y,
                Radius = math.max(authoring.size.x / 2f, authoring.size.z / 2f),
                Center = authoring.center
            });
        }
    }

    public class SphereColliderBaker : Baker<SphereCollider>
    {
        public override void Bake(SphereCollider authoring)
        {
            var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
            AddComponent(entity, new ColliderVolume()
            {
                Height = authoring.radius * 2f,
                Radius = authoring.radius,
                Center = authoring.center
            });
        }
    }
}