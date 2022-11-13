using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class SelectBarButton : MonoBehaviour
    {
        [SerializeField] private Image m_image;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private GUI m_gui;

        private bool m_selected;

        public void SelectBar()
        {
            m_selected = !m_selected;

            if (m_selected)
            {
                m_gui.AddBar(this.transform.parent.gameObject.GetHashCode(), m_canvasGroup);
                m_image.sprite = m_gui.m_spriteSelected;

                return;
            }

            m_gui.RemoveBar(this.transform.parent.gameObject.GetHashCode());
            m_image.sprite = m_gui.m_spriteDeselected;
        }
    }
}
