using Unity.Entities;

namespace ReactionGames.TestTask.Game.Entities.Components
{
    public struct ContactDamage : IComponentData
    {
        public float Damage;
    }

    public struct Damage : IBufferElementData
    {
        public float Value;
    }
}