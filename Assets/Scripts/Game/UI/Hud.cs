using ReactionGames.TestTask.Game.Entities.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace ReactionGames.TestTask.Game
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Slider _healthBar;

        private EntityManager _entityManager;
        private EntityQuery _playerHealthQuery;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _playerHealthQuery = _entityManager.CreateEntityQuery(
                ComponentType.ReadOnly<PlayerInput>(),
                ComponentType.ReadOnly<Health>());
        }

        private void OnDestroy()
        {
            _playerHealthQuery.Dispose();
        }

        private void Update()
        {
            bool playerExist = false;

            var entities = _playerHealthQuery.ToEntityArray(Allocator.Temp);
            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var health = _entityManager.GetComponentData<Health>(entity);
                _healthBar.value = health.Value / health.MaxValue;
                playerExist = true;
            }

            _healthBar.gameObject.SetActive(playerExist);
            _playerHealthQuery.ResetFilter();
        }
    }
}