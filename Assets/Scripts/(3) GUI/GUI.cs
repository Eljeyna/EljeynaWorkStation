using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    public class GUI : MonoBehaviour
    {
#region === VARIABLES ===
        [SerializeField] private Canvas m_interface;
        [SerializeField] private GameObject m_barPrefab;
        [SerializeField] private Transform m_contentTransform;
        [SerializeField] private RectTransform m_scrollViewTransform;

        [field: SerializeField] public Sprite m_spriteSelected { get; private set; }
        [field: SerializeField] public Sprite m_spriteDeselected { get; private set; }

        private Dictionary<int, CanvasGroup> m_dictionaryBars = new Dictionary<int, CanvasGroup>();
        private bool m_isSelected;
        private int m_countObject;

        private const float SCROLL_VIEW_STEP_HEIGHT = 92f;
        private const float SCROLL_VIEW_STEP_ADD_HEIGHT = 36;
        private const float SCROLL_VIEW_MAX_HEIGHT = 888f;
        private const float SCROLL_VIEW_MIN_HEIGHT = 16f;

#endregion

#region === DO SOME CODE ===
        private void RecalculateScrollViewHeight()
        {
            m_scrollViewTransform.sizeDelta = new Vector2(m_scrollViewTransform.rect.width,
                    Mathf.Clamp(SCROLL_VIEW_MIN_HEIGHT + ((SCROLL_VIEW_STEP_HEIGHT + SCROLL_VIEW_STEP_ADD_HEIGHT) * m_countObject), SCROLL_VIEW_MIN_HEIGHT, SCROLL_VIEW_MAX_HEIGHT));
        }

        public void AddBar(int key, CanvasGroup value)
        {
            m_dictionaryBars.Add(key, value);
        }

        public void AddBar()
        {
            GameObject go = Instantiate(m_barPrefab, m_contentTransform, true);
            go.SetActive(true);
            m_countObject++;
            RecalculateScrollViewHeight();
        }

        public void RemoveBar(int key)
        {
            m_dictionaryBars.Remove(key);
        }

        public void RemoveBars()
        {
            foreach (var bars in m_dictionaryBars)
            {
                bars.Value.gameObject.SetActive(false);
                Destroy(bars.Value.gameObject);
                m_countObject--;
            }

            m_isSelected = false;
            m_dictionaryBars.Clear();
            RecalculateScrollViewHeight();
        }

        public void SelectDeselectAllBars()
        {
            if (m_contentTransform.childCount <= 1)
            {
                return;
            }

            m_isSelected = !m_isSelected;

            if (m_isSelected)
            {
                Transform bar;

                for (int i = 1; i < m_contentTransform.childCount; i++)
                {
                    bar = m_contentTransform.GetChild(i);

                    if (bar.GetChild(1).TryGetComponent(out SelectBarButton script))
                    {
                        script.SelectBar();
                    }
                }

                return;
            }

            SelectBarButton[] scripts = new SelectBarButton[m_dictionaryBars.Count];
            int count = 0;

            foreach (var bars in m_dictionaryBars)
            {
                if (bars.Value.transform.GetChild(1).TryGetComponent(out SelectBarButton script))
                {
                    scripts[count] = script;
                    count++;
                }
            }

            for (count = 0; count < scripts.Length; count++)
            {
                scripts[count].SelectBar();
            }
        }

        public void ChangeBarTransparency(float value)
        {
            foreach (var canvasGroup in m_dictionaryBars.Values)
            {
                canvasGroup.alpha = value;

                if (canvasGroup.alpha <= 0.01d)
                {
                    canvasGroup.interactable = false;
                    continue;
                }

                canvasGroup.interactable = true;
            }
        }

        public void ShowHideInterface()
        {
            m_interface.enabled = !m_interface.enabled;
        }
#endregion
    }
}
