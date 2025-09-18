using Cubes.Presenter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Cubes.View
{
    public class CubeSelectionView : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
    {
        private int _cubeMapperId;
        [SerializeField] private Image _icon;
        private CubeSelectionPresenter _cubeSelectionPresenter;
        
        [Inject]
        private void Construct(CubeSelectionPresenter cubeSelectionPresenter)
        {
            _cubeSelectionPresenter = cubeSelectionPresenter;
        }
        
        public void Set(int cubeMapperId, Sprite icon)
        {
            _cubeMapperId = cubeMapperId;
            _icon.sprite = icon;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _cubeSelectionPresenter.InitiateSelection(_cubeMapperId);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _cubeSelectionPresenter.EndSelection();
        }
    }
}