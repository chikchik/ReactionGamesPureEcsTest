using Unity.Entities;

namespace ReactionGames.TestTask.Game.Entities.Components
{
    public struct Party : IComponentData
    {
        public PartyType Value;
    }
}