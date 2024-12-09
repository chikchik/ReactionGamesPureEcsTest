using Unity.Entities;

namespace ReactionGames.TestTask.Game.Entities.Components
{
    public struct Health : IComponentData
    {
        public float Value;
        public float MaxValue;
    }
}