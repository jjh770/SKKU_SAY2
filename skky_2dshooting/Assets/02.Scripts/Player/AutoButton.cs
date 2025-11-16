using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoButton : MonoBehaviour, IPointerUpHandler
{
    private PlayerFire _playerFire;
    private PlayerMove _playerMove;
    private bool _isAutoMode = true;
    [SerializeField] private Image _autoButtonImage;
    private Tweener _rotateTween;
    private void Start()
    {
        _playerFire = FindObjectOfType<PlayerFire>();
        _playerMove = FindObjectOfType<PlayerMove>();

        StartRotation();
    }

    private void StartRotation()
    {
        _autoButtonImage.rectTransform.DOKill(true);

        _autoButtonImage.rectTransform.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }
    private void StopRotation()
    {
        _autoButtonImage.rectTransform.DOKill(true);
        _autoButtonImage.rectTransform.rotation = Quaternion.identity;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isAutoMode = !_isAutoMode;
        _playerFire.AutoMode(_isAutoMode);
        _playerMove.AutoMode(_isAutoMode);

        if (_isAutoMode)
        {
            StartRotation();
        }
        else
        {
            StopRotation();
        }
    }
}
