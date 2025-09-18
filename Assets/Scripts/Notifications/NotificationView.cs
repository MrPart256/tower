using DG.Tweening;
using Localization;
using UnityEngine;

namespace Notifications
{
    public class NotificationView : MonoBehaviour
    {
        private const float Visible = 1;
        private const float Hidden = 0;
        
        [SerializeField] private LocalizedText _localizedText;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [SerializeField] private float _showDuration;
        [SerializeField] private float _hideDuration;
        [SerializeField] private float _delay;
        
        [SerializeField] private Ease _showEase = Ease.InSine;
        
        private Sequence _showTween;

        public void Show(string notification)
        {
            _localizedText.Init(notification);

            _canvasGroup.alpha = Hidden;
            
            if(_showTween != null)
                _showTween.Kill();

            _showTween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(Visible, _showDuration))
                .AppendInterval(_delay)
                .Append(_canvasGroup.DOFade(Hidden, _hideDuration))
                .SetEase(_showEase);
        }
    }
}