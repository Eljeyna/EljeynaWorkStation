using System.Text;
using Game.UserInterface;
using UnityEditor;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace ECS.Trigger
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class TriggerSphereCollect : TriggerOnEnter
    {
        private static int m_sphereCount = 0;
        private static int m_sphereCountMax;

        private static StringBuilder m_stringBuilder = new StringBuilder();
        private const string SPHERE_COLLECTED = "Сфер собрано: ";
        private const string SPHERE_COLLECTED_ALL = "Все сферы собраны";
        private const string SPHERE_SEPARATOR = " / ";

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            m_sphereCount = 0;
            m_sphereCountMax = 0;
            m_stringBuilder.Clear();
        }
#endif

        public override void Execute()
        {
            var world = WorldHandler.GetMainWorld();
            var pool = world.GetPool<TriggerEnterEventSendComponent>();

            if (pool.Has(triggerEntity.m_entity))
            {
                return;
            }

            pool.Add(triggerEntity.m_entity);
        }

        public static void SetSphereMaxCount(int count)
        {
            if (count == 0)
            {
                SphereCountUpdate();

                return;
            }

            m_sphereCountMax = count;
        }

        private void Awake()
        {
            SphereInstantiated();
        }

        private static void SphereInstantiated()
        {
            m_sphereCount++;
        }

        private static void SphereCollected()
        {
            m_sphereCount--;
            SphereCountUpdate();
        }

        private static void SphereCountUpdate()
        {
            if (m_sphereCount == 0)
            {
                UserInterfaceData.Instance.m_questProgress.text = SPHERE_COLLECTED_ALL;

                return;
            }

            m_stringBuilder.Append(SPHERE_COLLECTED);
            m_stringBuilder.Append(m_sphereCountMax - m_sphereCount);
            m_stringBuilder.Append(SPHERE_SEPARATOR);
            m_stringBuilder.Append(m_sphereCountMax);

            UserInterfaceData.Instance.m_questProgress.text = m_stringBuilder.ToString();
            m_stringBuilder.Clear();
        }

        private void OnDestroy()
        {
            SphereCollected();
        }
    }
}
