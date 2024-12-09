using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class PartyAuthoring : MonoBehaviour
    {
        public PartyType Value;

        public class Baker : Baker<PartyAuthoring>
        {
            public override void Bake(PartyAuthoring authoring)
            {
                var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
                AddComponent(entity, new Party() { Value = authoring.Value });
            }
        }
    }
}