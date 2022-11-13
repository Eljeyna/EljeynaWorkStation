using ECS.Audio;
using ECS.Audio.SFX;
using Leopotam.EcsLite;

namespace Game.GameData
{
    public static class GameSound
    {
        public static void ApplyEntitySound(EcsWorld world, int entity, int data)
        {
            if (world.GetPool<AudioComponent>().Has(entity))
            {
                world.GetPool<SoundEventComponent>().Add(entity);
                world.GetPool<SoundEventComponent>().Get(entity).m_key = data;
            }
        }
    }
}
