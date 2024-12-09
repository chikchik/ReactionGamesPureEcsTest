using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ReactionGames.TestTask.Game.Entities.Systems
{
    public partial class ShooterBehaviorSystem : SystemBase
    {
        private EntityQuery _targetsQuery;

        protected override void OnCreate()
        {
            base.OnCreate();

            _targetsQuery = Entities.WithAll<Health, Party, LocalTransform>().ToQuery();
        }

        protected override void OnUpdate()
        {
            float time = UnityEngine.Time.time;

            var targets = _targetsQuery.ToEntityArray(Allocator.Temp);

            Entities
                .WithAll<ShooterBehaviour>()
                .ForEach((
                    Entity entity,
                    ref RangeAttack rangeAttack,
                    in Party party,
                    in LocalTransform localTransform) =>
                {
                    for (int i = 0; i < targets.Length; i++)
                    {
                        var target = targets[i];
                        var targetLocalTransform = EntityManager.GetComponentData<LocalTransform>(target);
                        var targetParty = EntityManager.GetComponentData<Party>(target);
                        if (targetParty.Value != party.Value &&
                            math.lengthsq(targetLocalTransform.Position - localTransform.Position) < math.square(rangeAttack.Range) &&
                            time > rangeAttack.LastAttackTime + rangeAttack.AttackInterval)
                        {
                            var projectileEntity = EntityManager.Instantiate(rangeAttack.ProjectilePrefab);
                            var projectileLocalTransform = EntityManager.GetComponentData<LocalTransform>(projectileEntity);
                            projectileLocalTransform.Position = localTransform.Position + rangeAttack.AttackPositionOffset;
                            EntityManager.AddComponent<MoveDirection>(projectileEntity);
                            EntityManager.AddComponent<Party>(projectileEntity);
                            EntityManager.SetComponentData(projectileEntity, projectileLocalTransform);
                            EntityManager.SetComponentData(projectileEntity, new Party() { Value = party.Value });
                            EntityManager.SetComponentData(projectileEntity, new MoveDirection()
                            {
                                Value = math.normalize(targetLocalTransform.Position - localTransform.Position)
                            });
                            rangeAttack.LastAttackTime = time;
                            break;
                        }
                    }
                }).WithStructuralChanges().Run();

            targets.Dispose();
        }
    }
}