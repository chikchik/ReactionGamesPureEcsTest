using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Collections;
using Unity.Entities;

namespace ReactionGames.TestTask.Game.Entities.Systems
{
    public partial class ContactDamageSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

            Entities
                .ForEach((
                    Entity entity,
                    in ContactDamage contactDamage,
                    in Party party) =>
                {
                    if (EntityManager.HasBuffer<CollisionContact>(entity))
                    {
                        var collisionContactBuffer = EntityManager.GetBuffer<CollisionContact>(entity);
                        for (int i = 0; i < collisionContactBuffer.Length; i++)
                        {
                            var otherEntity = collisionContactBuffer[i].Entity;
                            if (EntityManager.HasComponent<Health>(otherEntity) &&
                                EntityManager.HasComponent<Party>(otherEntity))
                            {
                                var otherParty = EntityManager.GetComponentData<Party>(otherEntity);
                                if (party.Value != otherParty.Value)
                                {
                                    var damageBuffer = EntityManager.AddBuffer<Damage>(otherEntity);
                                    damageBuffer.Add(new Damage() { Value = contactDamage.Damage });

                                    entityCommandBuffer.DestroyEntity(entity);
                                    break;
                                }
                            }
                        }
                    }
                }).WithStructuralChanges().Run();

            entityCommandBuffer.Playback(EntityManager);
        }
    }
}