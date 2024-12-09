using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ReactionGames.TestTask.Game.Entities.Systems
{
    public partial class CollisionContactSystem : SystemBase
    {
        private EntityQuery _colliderVolumeQuery;

        protected override void OnCreate()
        {
            base.OnCreate();

            _colliderVolumeQuery = Entities.WithAll<ColliderVolume, LocalTransform>().ToQuery();
        }

        protected override void OnUpdate()
        {
            var colliderVolumeEntities = _colliderVolumeQuery.ToEntityArray(Allocator.Temp);

            Entities
                .ForEach((
                    Entity entity,
                    in ColliderVolume colliderVolume,
                    in LocalTransform localTransform) =>
                {
                    if (EntityManager.HasBuffer<CollisionContact>(entity))
                    {
                        EntityManager.GetBuffer<CollisionContact>(entity).Clear();
                    }

                    var center = localTransform.Position + colliderVolume.Center;
                    for (int i = 0; i < colliderVolumeEntities.Length; i++)
                    {
                        var otherEntity = colliderVolumeEntities[i];
                        if (otherEntity == entity)
                            continue;

                        var otherColliderVolume = EntityManager.GetComponentData<ColliderVolume>(otherEntity);
                        var otherLocalTransform = EntityManager.GetComponentData<LocalTransform>(otherEntity);
                        var otherCenter = otherLocalTransform.Position + otherColliderVolume.Center;

                        if (colliderVolume.Radius + otherColliderVolume.Radius > math.length(otherCenter.xz - center.xz) &&
                            colliderVolume.Height + otherColliderVolume.Height > math.abs(otherCenter.y - center.y))
                        {
                            var collisionContactBuffer = EntityManager.AddBuffer<CollisionContact>(entity);
                            collisionContactBuffer.Add(new CollisionContact() { Entity = otherEntity });
                        }
                    }
                }).WithStructuralChanges().Run();

            colliderVolumeEntities.Dispose();
        }
    }
}