using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Systems
{
    public partial class PlayerInputSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Entities
                .WithAll<PlayerInput>()
                .ForEach((Entity entity) =>
                {
                    if (math.abs(horizontalInput) > math.EPSILON || math.abs(verticalInput) > math.EPSILON)
                    {
                        if (EntityManager.HasComponent<MoveDirection>(entity) == false)
                        {
                            EntityManager.AddComponent<MoveDirection>(entity);
                        }

                        var moveDirection = EntityManager.GetComponentData<MoveDirection>(entity);
                        moveDirection.Value = math.normalize(new float3(horizontalInput, 0f, verticalInput));
                        EntityManager.SetComponentData(entity, moveDirection);
                    }
                    else
                    {
                        if (EntityManager.HasComponent<MoveDirection>(entity))
                        {
                            EntityManager.RemoveComponent<MoveDirection>(entity);
                        }
                    }
                }).WithStructuralChanges().Run();
        }
    }
}