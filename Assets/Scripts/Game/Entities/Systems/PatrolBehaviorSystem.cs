using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;

namespace ReactionGames.TestTask.Game.Entities.Systems
{
    public partial class PatrolBehaviorSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach((
                    Entity entity,
                    ref PatrolBehavior patrolBehavior) =>
                {
                    if (EntityManager.HasComponent<MoveTarget>(entity) == false)
                    {
                        EntityManager.AddComponent<MoveTarget>(entity);

                        var patrolPoints = EntityManager.GetBuffer<PatrolPoint>(entity);

                        patrolBehavior.NextPointIndex++;
                        if (patrolBehavior.NextPointIndex >= patrolPoints.Length)
                        {
                            patrolBehavior.NextPointIndex = 0;
                        }

                        var moveTarget = EntityManager.GetComponentData<MoveTarget>(entity);
                        moveTarget.Value = patrolPoints[patrolBehavior.NextPointIndex].Value;
                        EntityManager.SetComponentData(entity, moveTarget);
                    }
                }).WithStructuralChanges().Run();
        }
    }
}