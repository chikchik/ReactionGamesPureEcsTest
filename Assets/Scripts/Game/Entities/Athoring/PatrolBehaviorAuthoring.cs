using ReactionGames.TestTask.Game.Entities.Components;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class PatrolBehaviorAuthoring : MonoBehaviour
    {
        public List<Vector3> Points;

        public class Baker : Baker<PatrolBehaviorAuthoring>
        {
            public override void Bake(PatrolBehaviorAuthoring authoring)
            {
                var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
                AddComponent(entity, new PatrolBehavior() { NextPointIndex = -1 });

                var pointBuffer = AddBuffer<PatrolPoint>(entity);
                foreach (var point in authoring.Points)
                {
                    pointBuffer.Add(new PatrolPoint() { Value = point });
                }
            }
        }
    }
}