using Unity.Entities;

namespace ReactionGames.TestTask.Game.Entities.Components
{
    public struct ChaserBehavior : IComponentData
    {
        public float Range;
        public Entity Target;
    }
}