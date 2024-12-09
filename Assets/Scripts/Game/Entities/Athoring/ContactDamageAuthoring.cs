using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace ReactionGames.TestTask.Game.Entities.Authoring
{
    public class ContactDamageAuthoring : MonoBehaviour
    {
        public float Damage;

        public class Baker : Baker<ContactDamageAuthoring>
        {
            public override void Bake(ContactDamageAuthoring authoring)
            {
                var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
                AddComponent(entity, new ContactDamage() { Damage = authoring.Damage });
            }
        }
    }
}