using UnityEngine;

namespace ECS.Trigger
{
    public sealed class TriggerInitialize : MonoBehaviour
    {
        public void Execute()
        {
            for (int i = 0; i < transform.childCount; i++)
            { 
                Transform child = transform.GetChild(i);
                
                if (child && child.TryGetComponent(out TriggerOnEnter trigger))
                {
                    trigger.gameObject.SetActive(true);
                }
            }
        }
    }
}
