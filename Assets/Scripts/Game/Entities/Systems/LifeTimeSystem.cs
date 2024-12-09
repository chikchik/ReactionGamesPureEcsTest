using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Collections;
using Unity.Entities;

namespace ReactionGames.TestTask.Game.Entities.Systems
{
    public partial class LifeTimeSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = UnityEngine.Time.deltaTime;
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

            Entities
                .ForEach((
                    Entity entity,
                    ref LifeTime lifeTime) =>
                {
                    lifeTime.Value -= deltaTime;

                    if (lifeTime.Value <= 0f)
                    {
                        entityCommandBuffer.DestroyEntity(entity);
                    }
                }).Run();

            entityCommandBuffer.Playback(EntityManager);
        }
    }
}