using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class PlayerMove : MonoBehaviour
{
    [Header("플레이어 속도 관련")]
    public float Speed = 3f;
    private float _currentSpeed;
    [SerializeField]
    private float _speedAcceleration = 1.5f;
    [Space (10f)]
    [SerializeField]
    private float _minSpeed = 1f;
    [SerializeField]
    private float _maxSpeed = 10f;
    [SerializeField]
    private float _speedChangeAmount = 0.5f;
    [Header("플레이어 시작 위치")]
    [SerializeField]
    private Vector2 _initPosition = Vector2.zero;
    [Header("적 감지 범위")]
    [SerializeField]
    private float _detectAttackRange = 8f;
    [SerializeField]
    private float _detectAvoidRange = 4f;

    private bool _isMoveSpeedUp = false;
    private float _speedUpAmount = 1.5f;
    private float _moveSpeedUpTimer = 5f;
    private float _startMoveSpeed;

    private bool _autoMove = true;

    private float _camHalfWidth;
    private float _camHalfHeight;

    private void Start()
    {
        transform.position = _initPosition;
        _startMoveSpeed = Speed;
        _currentSpeed = Speed;
        _camHalfWidth = GameManager.Instance.CameraHalfWidth;
        _camHalfHeight = GameManager.Instance.CameraHalfHeight;
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
    // 자동 이동 로직
    private void AutoMoving()
    {
        // 이동 속도
        float moveSpeed = Speed;
        Vector2 currentPosition = transform.position;
        // 가장 가까운 회피할 적 오브젝트
        GameObject nearestAvoidEnemy = null;
        // 회피를 위한 감지 거리
        float nearestAvoidDist = _detectAvoidRange;
        // 플레이어 기준으로 원 생성 (그 안에 들어오는 적 오브젝트 감지)
        Collider2D[] avoidDetections = Physics2D.OverlapCircleAll(currentPosition, _detectAvoidRange);
        foreach (var detection in avoidDetections)
        {
            if (detection.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(currentPosition, detection.transform.position);
                if (distance < nearestAvoidDist)
                {
                    // 가장 가까운 적 구분
                    nearestAvoidDist = distance;
                    nearestAvoidEnemy = detection.gameObject;
                }
            }
        }
        // 가장 가까운 적이 있을 때
        if (nearestAvoidEnemy != null)
        {
            // 플레이어의 위치를 적 위치의 x 값보다 1만큼 크거나 작게 설정할 것임
            Vector2 enemyPos = nearestAvoidEnemy.transform.position;
            float leftPosX = enemyPos.x - 1f;
            float rightPosX = enemyPos.x + 1f;
            // 피해야할 거리
            float distToLeft = Mathf.Abs(currentPosition.x - leftPosX);
            float distToRight = Mathf.Abs(currentPosition.x - rightPosX);
            // 적과의 거리가 오른쪽이 멀면 왼쪽으로, 왼쪽이 멀면 오른쪽으로 피하기 (최대한 피하기 쉬운 방향 구분)
            float selectDir = distToLeft < distToRight ? leftPosX : rightPosX;
            // 그렇게 움직이면 화면 밖으로 나가는가? 만약 나간다면 눈물을 머금고 피하기 어려운 방향으로 피하기
            if (selectDir > _camHalfWidth || selectDir < -_camHalfWidth)
            {
                selectDir = (selectDir == leftPosX) ? rightPosX : leftPosX;

                if (selectDir > _camHalfWidth)
                    selectDir = -_camHalfWidth;
                else if (selectDir < -_camHalfWidth)
                    selectDir = _camHalfWidth;
            }
            // 최종 이동
            Vector2 targetPos = new Vector2(selectDir, currentPosition.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            // 회피 했으면 공격 로직은 돌 필요가 없음
            return;
        }
        
        GameObject nearestAttackEnemy = null;
        float nearestAttackDist = _detectAttackRange;

        Collider2D[] attackDetections = Physics2D.OverlapCircleAll(currentPosition, _detectAttackRange);
        foreach (var detection in attackDetections)
        {
            if (detection.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(currentPosition, detection.transform.position);
                if (distance < nearestAttackDist)
                {
                    nearestAttackDist = distance;
                    nearestAttackEnemy = detection.gameObject;
                }
            }
        }
        // 공격 범위에 들어온 적의 x좌표로 이동하여 메인 Bullet을 최대한 때려넣기
        if (nearestAttackEnemy != null)
        {
            Vector2 enemyPos = nearestAttackEnemy.transform.position;
            Vector2 targetPos = new Vector2(enemyPos.x, currentPosition.y);

            targetPos.x = Mathf.Clamp(targetPos.x, -_camHalfWidth, _camHalfWidth);
            targetPos.y = Mathf.Clamp(targetPos.y, -_camHalfHeight, 0f);

            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            return;
        }
        // 공격도 회피도 할 적이 없다면 중심으로 돌아가기
        Vector2 centerPos = new Vector2(0, currentPosition.y);
        transform.position = Vector2.MoveTowards(transform.position, centerPos, moveSpeed * 0.5f * Time.deltaTime);
    }
    // 적 탐지 기즈모 (기즈모에서 확인 가능)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectAttackRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _detectAvoidRange);
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

        if (newPosition.x > _camHalfWidth)
        {
            newPosition.x = -_camHalfWidth;
        }
        else if (newPosition.x < -_camHalfWidth)
        {
            newPosition.x = _camHalfWidth;
        }

        if (newPosition.y > 0)
        {
            newPosition.y = (-1) * _camHalfHeight;
        }
        else if (newPosition.y < (-1) * _camHalfHeight)
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
