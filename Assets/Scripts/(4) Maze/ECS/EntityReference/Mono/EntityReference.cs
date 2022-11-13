using UnityEngine;

namespace ECS.Entity
{
    public sealed class EntityReference : MonoBehaviour
    {
        public int m_entity;

        public void DelEntity()
        {
            gameObject.SetActive(false);

            Destroy(gameObject);
        }
    }
}
