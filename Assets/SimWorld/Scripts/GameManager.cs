using TMPro;
using UnityEngine;

namespace AP
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public int gold;

        [SerializeField] private TextMeshProUGUI goldTMPro;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            RefreshGoldUI();
        }

        public void GainGold(int amount)
        {
            gold += amount;
            RefreshGoldUI();
        }

        public void LoseGold(int amount)
        {
            gold -= amount;
            if (gold < 0) gold = 0;
            RefreshGoldUI();
        }

        public void RefreshGoldUI() => goldTMPro.text = gold.ToString();
    }
}