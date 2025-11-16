using UnityEngine;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerFire _playerFire;
    private bool _isPressed = false;

    private void Start()
    {
        _playerFire = FindObjectOfType<PlayerFire>();
    }

    private void Update()
    {
        // 버튼을 누르고 있는 동안 계속 플래그 설정
        if (_isPressed)
        {
            _playerFire?.OnFireButtonPressed();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }
}
