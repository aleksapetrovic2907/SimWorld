using System.Collections.Generic;
using UnityEngine;

namespace AP.UI.TabSystem
{
    public class TabGroup : MonoBehaviour
    {
        public int SelectedTabIndex { get; private set; } = 0;

        [SerializeField] private List<TabButton> tabButtons;
        [SerializeField] private Sprite selectedTabSprite;
        [SerializeField] private Sprite unselectedTabSprite;

        private void Start()
        {
            SubscribeButtons();
            OpenDefaultTab();
        }

        private void SubscribeButtons()
        {
            for (int i = 0; i < tabButtons.Count; i++)
            {
                var index = i;
                tabButtons[index].button.onClick.AddListener(() => OpenTab(index));
            }
        }

        private void OpenDefaultTab()
        {
            tabButtons[0].dataContainer.SetActive(true);
            tabButtons[0].image.sprite = selectedTabSprite;
            SelectedTabIndex = 0;
        }

        private void OpenTab(int index)
        {
            if (SelectedTabIndex == index) { return; }
            CloseTab(SelectedTabIndex);

            tabButtons[index].dataContainer.SetActive(true);
            tabButtons[index].image.sprite = selectedTabSprite;
            SelectedTabIndex = index;
        }

        private void CloseTab(int index)
        {
            tabButtons[index].dataContainer.SetActive(false);
            tabButtons[index].image.sprite = unselectedTabSprite;
        }
    }
}