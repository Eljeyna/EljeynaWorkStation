using ECS.EntityTags;
using ECS.Movement;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.GameData
{
    public static class GameEntity
    {
		public static void ApplyPlayerInputZero(EcsWorld world)
		{
			EcsFilter playerFilter = world.Filter<PlayerTagComponent>()
				.Inc<DirectionComponent>()
				.End();

			foreach (int entity in playerFilter)
			{
				ref var directionComponent = ref world.GetPool<DirectionComponent>().Get(entity).m_direction;
				directionComponent = Vector2.zero;
			}
		}
    }
}
