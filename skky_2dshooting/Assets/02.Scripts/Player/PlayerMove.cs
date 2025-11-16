using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("플레이어 속도 관련")]
    public float Speed = 3f;
    private float _currentSpeed;
    [SerializeField]
    private float _speedAcceleration = 1.5f;
    [Space(10f)]
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

    [Header("회피 안전거리")]
    [SerializeField]
    private float _avoidanceEnemyThreshold = 1f;
    private float _avoidanceBulletThreshold = 0.5f;

    // 회피 방향을 유지할 시간
    [Header("회피 쿨다운 (떨림 방지)")]
    [SerializeField]
    private float _avoidCooldownDuration = 0.3f;
    // 쿨다운 타이머, 회피 방향, 회피중인가?
    private float _avoidCooldownTimer = 0f;
    private Vector2 _lockedAvoidDirection = Vector2.zero; 
    private bool _isAvoiding = false;

    private bool _isMoveSpeedUp = false;
    private float _speedUpAmount = 1.5f;
    private float _moveSpeedUpTimer = 5f;
    private float _startMoveSpeed;

    private bool _autoMove = true;

    private float _camHalfWidth;
    private float _camHalfHeight;

    private Animator _animator;

    [Header("조이스틱")]
    public Joystick JoyStick;


    private void Start()
    {
        transform.position = _initPosition;
        _startMoveSpeed = Speed;
        _currentSpeed = Speed;
        _camHalfWidth = GameManager.Instance.CameraHalfWidth;
        _camHalfHeight = GameManager.Instance.CameraHalfHeight;
        _animator = gameObject.GetComponent<Animator>();
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
            // 플래그 인수 제거 (매개변수 true/false 대신 명확한 메서드 이름 사용)
            if (Input.GetKey(KeyCode.R))
            {
                MovePlayerOrigin();
            }
            else
            {
                MovePlayer();
            }
        }
    }

    public void AutoMode(bool isThatAuto)
    {
        _autoMove = isThatAuto;
    }

    // 화면밖으로 나가면 반대편으로 나가는 메서드
    private Vector2 WrapPosition(Vector2 position)
    {
        Vector2 wrappedPos = position;

        if (wrappedPos.x > _camHalfWidth)
        {
            wrappedPos.x = -_camHalfWidth;
        }
        else if (wrappedPos.x < -_camHalfWidth)
        {
            wrappedPos.x = _camHalfWidth;
        }

        if (wrappedPos.y > 0)
        {
            wrappedPos.y = -_camHalfHeight;
        }
        else if (wrappedPos.y < -_camHalfHeight)
        {
            wrappedPos.y = 0f;
        }

        return wrappedPos;
    }
    // 자동 이동 로직
    private void AutoMoving()
    {
        // 이동 속도
        float moveSpeed = Speed;
        Vector2 currentPosition = transform.position;

        // 쿨다운 감소 
        if (_avoidCooldownTimer > 0)
        {
            _avoidCooldownTimer -= Time.deltaTime;
        }

        //  회피해야하는 가장 가까운 적 오브젝트
        GameObject nearestAvoidEnemy = null;
        // 회피 감지 거리
        float nearestAvoidDist = _detectAvoidRange;
        bool targetIsBullet = false;

        // 플레이어 기준으로 원 생성 (그 안에 들어오는 적 오브젝트 감지)
        Collider2D[] avoidDetections = Physics2D.OverlapCircleAll(currentPosition, _detectAvoidRange);
        foreach (var detection in avoidDetections)
        {
            bool isEnemy = detection.CompareTag("Enemy");
            bool isBullet = detection.CompareTag("BossDirectionalBullet") ||
                            detection.CompareTag("BossCircleBullet") ||
                            detection.CompareTag("BossDelayBullet");

            if (isEnemy || isBullet)
            {
                float distance = Vector2.Distance(currentPosition, detection.transform.position);
                if (distance < nearestAvoidDist)
                {
                    // 가장 가까운 적 구분
                    nearestAvoidDist = distance;
                    nearestAvoidEnemy = detection.gameObject;
                    targetIsBullet = isBullet;
                }
            }
        }

        // 깊은 if 중첩 방지를 위한 조기 return 적용
        // 가장 가까운 적이 없을 때 회피 모드 해제
        if (nearestAvoidEnemy == null)
        {
            _isAvoiding = false;
            _avoidCooldownTimer = 0f;
        }
        else
        {
            // 적 오브젝트와의 x축 거리 계산
            Vector2 enemyPos = nearestAvoidEnemy.transform.position;
            float xDistance = Mathf.Abs(currentPosition.x - enemyPos.x);

            // 총알과 적에 따라 다른 임계값
            float baseThreshold = targetIsBullet ? _avoidanceBulletThreshold : _avoidanceEnemyThreshold;  // ⭐ 조금 증가
            float thresholdToUse = _isAvoiding ? baseThreshold * 1.3f : baseThreshold;

            // 아직 안전거리를 확보했다면 회피 종료
            if (xDistance >= thresholdToUse)
            {
                _isAvoiding = false;
                _avoidCooldownTimer = 0f;
            }
            else
            {
                float distanceToTarget = Vector2.Distance(currentPosition, _lockedAvoidDirection);
                bool reachedTarget = distanceToTarget < 0.2f;

                // 쿨다운이 끝났을 때만 새로운 회피 방향 계산
                if (_avoidCooldownTimer <= 0 || reachedTarget)
                {
                    float leftPosX = enemyPos.x - thresholdToUse;
                    float rightPosX = enemyPos.x + thresholdToUse;
                    // 더 가까운 방향으로 회피 선택
                    float distToLeft = Mathf.Abs(currentPosition.x - leftPosX);
                    float distToRight = Mathf.Abs(currentPosition.x - rightPosX);

                    float selectDir = distToLeft < distToRight ? leftPosX : rightPosX;

                    // 회피 방향 고정 및 쿨다운 시작
                    _lockedAvoidDirection = new Vector2(selectDir, currentPosition.y);
                    _avoidCooldownTimer = _avoidCooldownDuration;
                    _isAvoiding = true;
                }

                // 최종 이동
                Vector2 newPos = Vector2.MoveTowards(transform.position, _lockedAvoidDirection, moveSpeed * Time.deltaTime);
                // 화면 밖으로 나간다? -> 반대편으로 이동
                transform.position = WrapPosition(newPos);

                return;
            }
        }

        // 회피할 적이 없을 때 공격할 적 탐지
        GameObject nearestAttackEnemy = null;
        float nearestAttackDist = _detectAttackRange;

        Collider2D[] attackDetections = Physics2D.OverlapCircleAll(currentPosition, _detectAttackRange);
        foreach (var detection in attackDetections)
        {
            if (detection.CompareTag("Enemy"))
            {
                if (nearestAvoidEnemy != null && detection.gameObject == nearestAvoidEnemy)
                {
                    continue;
                }

                float distance = Vector2.Distance(currentPosition, detection.transform.position);
                if (distance < nearestAttackDist)
                {
                    nearestAttackDist = distance;
                    nearestAttackEnemy = detection.gameObject;
                }
            }
        }
        // 공격은 굳이 반대편으로 이동할 필요없으므로 그냥 가까운 쪽으로 이동
        if (nearestAttackEnemy != null)
        {
            Vector2 enemyPos = nearestAttackEnemy.transform.position;
            Vector2 targetPos = new Vector2(enemyPos.x, currentPosition.y);

            targetPos.x = Mathf.Clamp(targetPos.x, -_camHalfWidth, _camHalfWidth);
            targetPos.y = Mathf.Clamp(targetPos.y, -_camHalfHeight, 0f);

            Vector2 newPos = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            transform.position = newPos;

            return;
        }

        // 공격도 회피도 할 필요 없을 때 중심으로 돌아가기
        Vector2 centerPos = new Vector2(0, currentPosition.y);
        Vector2 newPos2 = Vector2.MoveTowards(transform.position, centerPos, moveSpeed * 0.5f * Time.deltaTime);
        transform.position = WrapPosition(newPos2);
    }


    // 적 탐지 기즈모 (Scene에서 확인 가능)
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
        if (!_isMoveSpeedUp) return;

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


    private void HandleMoveSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = Speed * _speedAcceleration;
            return;
        }

        _currentSpeed = Speed;
    }


    private void MovePlayerOrigin()
    {
        if (Mathf.Abs(transform.position.x) < 0.01f && Mathf.Abs(transform.position.y) < 0.01f)
        {
            transform.position = _initPosition;
            return;
        }

        transform.Translate((_initPosition - (Vector2)transform.position).normalized * _currentSpeed * Time.deltaTime);
    }

    private void MovePlayer()
    {
        Vector2 newPosition = ManualDirection();
        transform.position = WrapPosition(newPosition);
    }

    private Vector2 ManualDirection()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        float h = JoyStick.Horizontal;
        float v = JoyStick.Vertical;


        Vector2 direction = new Vector2(h, v);
        direction.Normalize();

        // 첫번째 방식 : Play 메서드를 이용한 강제 애니메이션 적용
        //if (direction.x < 0) _animator.Play("Left");
        //else if (direction.x == 0) _animator.Play("Idle");
        //else if (direction.x > 0) _animator.Play("Right");
        // 이 방식의 장점은 쉽고 빠르게 구현이 가능하다.
        // 이 방식의 단점은 Fade, Timing, State가 무시되고, 남용하기 쉬워서 어디서 애니메이션을 수정하는 지 알 수 없게 됨.

        // 두번째 방식 : 파라미터를 이용한 애니메이션 상태 전환
        _animator.SetInteger("x", (int)direction.x);

        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _currentSpeed * Time.deltaTime;
        return newPosition;
    }
}
