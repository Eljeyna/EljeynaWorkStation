using UnityEngine;

namespace Project
{
    public sealed class DropMenu : MonoBehaviour
    {
        [SerializeField] private Canvas m_canvas;

        public void ShowHideCanvas()
        {
            m_canvas.enabled = !m_canvas.enabled;
        }
    }
}
