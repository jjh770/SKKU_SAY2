using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class PlayerMove : MonoBehaviour
{
    public float Speed = 3f;
    private float _currentSpeed;
    [SerializeField]
    private float _speedAcceleration = 1.5f;
    [SerializeField]
    private float _minSpeed = 1f;
    [SerializeField]
    private float _maxSpeed = 10f;
    [SerializeField]
    private float _speedChangeAmount = 0.5f;
    [SerializeField]
    private Vector2 _initPosition = Vector2.zero;

    private bool _isMoveSpeedUp = false;
    private float _speedUpAmount = 1.5f;
    private float _moveSpeedUpTimer = 5f;
    private float _startMoveSpeed;

    private bool _autoMove = false;

    private void Start()
    {
        transform.position = _initPosition;
        _startMoveSpeed = Speed;
        _currentSpeed = Speed;
    }

    private void Update()
    {
        UpdateMoveSpeedUp();

        if (_autoMove)
        {
            AutoMoving();
        }
        else
        {
            HandleMoveSpeed();
            if (Input.GetKey(KeyCode.R))
            {
                MovePlayer(true);
            }
            else
            {
                MovePlayer(false);
            }
        }
    }

    public void AutoMode(bool isThatAuto)
    {
        _autoMove = isThatAuto;
    }

    private void AutoMoving()
    {

    }

    public void GetMoveSpeedUp()
    {
        _moveSpeedUpTimer = 5f; 
        _isMoveSpeedUp = true;
    }

    private void UpdateMoveSpeedUp()
    {
        if (_isMoveSpeedUp)
        {
            _moveSpeedUpTimer -= Time.deltaTime;

            if (_moveSpeedUpTimer > 0f)
            {
                Speed = _startMoveSpeed + _speedUpAmount; 
            }
            else
            {
                Speed = _startMoveSpeed; 
                _isMoveSpeedUp = false;
            }
        }
    }

    private void HandleMoveSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = Speed * _speedAcceleration;
        }
        else
        {
            _currentSpeed = Speed;
        }
    }

    private void MovePlayer(bool isOrigin = false)
    {
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");   
        
        Vector2 direction = new Vector2(h, v);
        direction.Normalize();

        Vector2 position = transform.position; 

        Vector2 newPosition = position + direction * _currentSpeed * Time.deltaTime; 

        if (newPosition.x > GameManager.Instance.CameraHalfWidth)
        {
            newPosition.x = (-1) * GameManager.Instance.CameraHalfWidth;
        }
        else if (newPosition.x < (-1) * GameManager.Instance.CameraHalfWidth)
        {
            newPosition.x = GameManager.Instance.CameraHalfWidth;
        }

        if (newPosition.y > 0)
        {
            newPosition.y = (-1) * GameManager.Instance.CameraHalfHeight;
        }
        else if (newPosition.y < (-1) * GameManager.Instance.CameraHalfHeight)
        {
            newPosition.y = 0f;
        }
        if (isOrigin)
        {
            if (Mathf.Abs(transform.position.x) < 0.01f && Mathf.Abs(transform.position.y) < 0.01f)
            {
                transform.position = _initPosition;
            }
            else
            {
                transform.Translate((_initPosition - (Vector2)transform.position).normalized * _currentSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = newPosition; 
        }
    }
}
