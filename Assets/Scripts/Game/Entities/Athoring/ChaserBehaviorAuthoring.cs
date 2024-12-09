using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class ChaserBehaviorAuthoring : MonoBehaviour
    {
        public float Range;

        public class Baker : Baker<ChaserBehaviorAuthoring>
        {
            public override void Bake(ChaserBehaviorAuthoring authoring)
            {
                var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
                AddComponent(entity, new ChaserBehavior { Range = authoring.Range });
            }
        }
    }
}