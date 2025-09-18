using Cubes.Presenter;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Cubes.View
{
    public class CubePlacementView : MonoBehaviour , IPointerDownHandler
    {
        private const float Faded = 0;
        private const float Visible = 1;
        
        [Header("Jump Animation")]
        [SerializeField] private float _jumpPower = 1;
        [SerializeField] private float _jumpAnimationDuration = .2f;
        [SerializeField] private Ease _jumpAnimationEase = Ease.OutBack;

        [Header("Hide Animation")] 
        [SerializeField] private float _hideDuration = .2f;
        [SerializeField] private Ease _hideEase = Ease.InSine;
        
        [Header("Drop Animation")]
        [SerializeField] private float _dropDistance = 10;
        [SerializeField] private float _dropDuration = .2f;
        [SerializeField] private Ease _dropEase = Ease.InBack;
        
        [Header("Fields")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _icon;
        
        private CubePlacementPresenter _cubePlacementPresenter;

        private State _state;
        
        private Tween _placementTween;
        private Tween _hideTween;
        private Sequence _dropTween;
        
        [Inject]
        private void Construct(CubePlacementPresenter presenter)
        {
            _cubePlacementPresenter = presenter;
        }
        
        public void Set(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void PlayPlaceAnimation(Vector2 position)
        {
            ResetAnimations();
            _placementTween = _rectTransform.DOMove(position, _jumpAnimationDuration)
                .SetEase(_jumpAnimationEase);
        }

        public void UpdatePosition(Vector2 position)
        {
            _rectTransform.position = position;
        }

        public Vector2 GetPosition()
            => _rectTransform.position;

        public Vector2 GetSize()
            => _rectTransform.rect.size * _rectTransform.lossyScale;
        
        public void SetState(State state)
        {
            _state = state;

            switch (state)
            {
                case State.Active:
                    _canvasGroup.alpha = Visible;
                    gameObject.SetActive(true);
                    break;
                case State.Inactive:
                    Hide();
                    break;
            }
        }

        public void PlayDropInHoleAnimatio(Vector2 holePosition)
        {
            ResetAnimations();
            _dropTween = DOTween.Sequence();
            _dropTween
                .Append(_rectTransform.DOMove(holePosition, _dropDuration))
                .Join(_canvasGroup.DOFade(Faded,_dropDuration))
                .SetEase(_dropEase)
                .OnComplete(()=> gameObject.SetActive(false));
        }
        
        private void Hide()
        {
            ResetAnimations();
            _hideTween = _canvasGroup.DOFade(Faded, _hideDuration)
                .OnComplete(()=> gameObject.SetActive(false))
                .SetEase(_hideEase);
        }
        
        private void ResetAnimations()
        {
            if (_placementTween != null)
                _placementTween.Kill();
            if (_hideTween != null)
                _hideTween.Kill();
            if(_dropTween != null)
                _dropTween.Kill();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(_state != State.Active)
                return;
            
            ResetAnimations();
            _cubePlacementPresenter.InitiatePlacement(this);
        }
        public enum State
        {
            Active,
            Inactive,
        }
    }
}