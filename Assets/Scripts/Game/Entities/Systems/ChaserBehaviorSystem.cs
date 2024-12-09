using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ReactionGames.TestTask.Game.Entities.Systems
{
    public partial class ChaserBehaviorSystem : SystemBase
    {
        private EntityQuery _targetsQuery;

        protected override void OnCreate()
        {
            base.OnCreate();

            _targetsQuery = Entities.WithAll<Health, Party, LocalTransform>().ToQuery();
        }

        protected override void OnUpdate()
        {
            var targets = _targetsQuery.ToEntityArray(Allocator.Temp);

            Entities
                .ForEach((
                    Entity entity,
                    ref ChaserBehavior chaserBehavior,
                    in Party party,
                    in LocalTransform localTransform) =>
                {
                    if (EntityManager.Exists(chaserBehavior.Target) &&
                        EntityManager.HasComponent<Party>(chaserBehavior.Target) &&
                        EntityManager.HasComponent<LocalTransform>(chaserBehavior.Target))
                    {
                        var targetLocalTransform = EntityManager.GetComponentData<LocalTransform>(chaserBehavior.Target);
                        var targetParty = EntityManager.GetComponentData<Party>(chaserBehavior.Target);

                        if (targetParty.Value != party.Value &&
                            math.length(targetLocalTransform.Position - localTransform.Position) < chaserBehavior.Range)
                        {
                            if (EntityManager.HasComponent<MoveTarget>(entity) == false)
                            {
                                EntityManager.AddComponent<MoveTarget>(entity);
                            }

                            var moveTarget = EntityManager.GetComponentData<MoveTarget>(entity);
                            moveTarget.Value = targetLocalTransform.Position;
                            EntityManager.SetComponentData(entity, moveTarget);
                        }
                        else
                        {
                            chaserBehavior.Target = Entity.Null;
                        }
                    }

                    if (EntityManager.Exists(chaserBehavior.Target) == false)
                    {
                        for (int i = 0; i < targets.Length; i++)
                        {
                            var target = targets[i];
                            var targetLocalTransform = EntityManager.GetComponentData<LocalTransform>(target);
                            var targetParty = EntityManager.GetComponentData<Party>(target);
                            if (targetParty.Value != party.Value &&
                                math.length(targetLocalTransform.Position - localTransform.Position) < chaserBehavior.Range)
                            {
                                chaserBehavior.Target = target;

                                if (EntityManager.HasComponent<MoveTarget>(entity) == false)
                                {
                                    EntityManager.AddComponent<MoveTarget>(entity);
                                }

                                var moveTarget = EntityManager.GetComponentData<MoveTarget>(entity);
                                moveTarget.Value = targetLocalTransform.Position;
                                EntityManager.SetComponentData(entity, moveTarget);
                                break;
                            }
                        }
                    }
                }).WithStructuralChanges().Run();

            targets.Dispose();
        }
    }
}