using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class PlayerInputAuthoring : MonoBehaviour
    {
        public class Baker : Baker<PlayerInputAuthoring>
        {
            public override void Bake(PlayerInputAuthoring authoring)
            {
                var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerInput());
            }
        }
    }
}