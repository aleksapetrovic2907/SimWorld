using UnityEngine;
using UnityEngine.UI;

namespace AP.UI.TabSystem
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class TabButton : MonoBehaviour
    {
        public Button button;
        public Image image;
        public GameObject dataContainer;
    }
}