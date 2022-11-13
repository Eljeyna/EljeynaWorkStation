using ECS.Entity;
using Game.GameData;
using UnityEngine;

namespace ECS.Trigger
{
    public abstract class TriggerOnEnter : Trigger
    {
        public EntityReference triggerEntity;
        public Collider triggerObject;
        public bool destroyOnExecute;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != GameManager.EntityLayer)
            {
                return;
            }
            
            triggerObject = other;
            Execute();
        }
    }
}
