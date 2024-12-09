using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ReactionGames.TestTask.Game.Entities.Systems
{
    public partial class MovementSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = UnityEngine.Time.deltaTime;

            Entities
                .ForEach((
                    Entity entity,
                    ref LocalTransform localTransform,
                    in MoveTarget moveTarget,
                    in Speed speed) =>
                {
                    var targetDirection = moveTarget.Value - localTransform.Position;
                    var velocity = math.normalize(targetDirection) * speed.Value * deltaTime;
                    var distanceDiff = math.lengthsq(targetDirection) - math.lengthsq(velocity);
                    if (math.abs(distanceDiff) > math.EPSILON)
                    {
                        if (EntityManager.HasComponent<MoveDirection>(entity) == false)
                        {
                            EntityManager.AddComponent<MoveDirection>(entity);
                        }

                        var moveDirection = EntityManager.GetComponentData<MoveDirection>(entity);
                        moveDirection.Value = distanceDiff < 0f
                            ? targetDirection / speed.Value / deltaTime
                            : math.normalize(targetDirection);

                        EntityManager.SetComponentData(entity, moveDirection);
                    }
                    else
                    {
                        EntityManager.RemoveComponent<MoveTarget>(entity);
                        EntityManager.RemoveComponent<MoveDirection>(entity);
                    }
                }).WithStructuralChanges().Run();

            Entities
                .ForEach((
                    ref LocalTransform localTransform,
                    in MoveDirection moveDirection,
                    in Speed speed) =>
                {
                    localTransform.Position += moveDirection.Value * speed.Value * deltaTime;
                }).Run();
        }
    }
}