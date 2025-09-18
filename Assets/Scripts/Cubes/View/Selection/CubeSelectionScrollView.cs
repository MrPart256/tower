using UnityEngine;
using UnityEngine.UI;

namespace Cubes.View
{
    public class CubeSelectionScrollView : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollView;

        public void BlockScroll()
        {
            _scrollView.enabled = false;
        }

        public void EnableScroll()
        {
            _scrollView.enabled = true;
        }
    }
}