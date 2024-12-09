using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Collections;
using Unity.Entities;

namespace ReactionGames.TestTask.Game.Entities.Systems
{
    public partial class HealthSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

            Entities
                .ForEach((
                    Entity entity,
                    ref Health health) =>
                {
                    if (EntityManager.HasBuffer<Damage>(entity))
                    {
                        var damageBuffer = EntityManager.GetBuffer<Damage>(entity);
                        for (int i = 0; i < damageBuffer.Length; i++)
                        {
                            var damage = damageBuffer[i];
                            health.Value -= damage.Value;

                            if (health.Value <= 0f)
                            {
                                entityCommandBuffer.DestroyEntity(entity);
                                break;
                            }
                        }
                        damageBuffer.Clear();
                    }
                }).WithStructuralChanges().Run();

            entityCommandBuffer.Playback(EntityManager);
        }
    }
}