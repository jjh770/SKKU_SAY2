using UnityEngine;

public class PlayerMove_Copilot : MonoBehaviour
{
    // 목표
    // 키보드 입력에 따라 방향을 구하고 그 방향으로 이동시키고 싶다.

    // 1. 키보드 입력
    // 2. 방향 구하는 방법
    // 3. 이동

    public float Speed = 3f; // 기본 이동 속도

    [SerializeField]
    private float _minSpeed = 1f; // 최소 이동 속도
    [SerializeField]
    private float _maxSpeed = 10f; // 최대 이동 속도
    [SerializeField]
    private float _speedChangeAmount = 0.5f; // 속도 변경량

    private float _boundaryX; // X축 이동 제한 경계
    private float _boundaryY; // Y축 이동 제한 경계

    // 게임 오브젝트가 생성될 때 (단 한번)
    private void Start()
    {
        // 카메라의 시야를 기준으로 화면 경계 계산
        // orthographicSize는 카메라 뷰의 절반 높이를 월드 단위로 나타냅니다.
        // aspect는 화면 비율 (너비 / 높이) 입니다.
        float worldHeightHalf = Camera.main.orthographicSize;
        float worldWidthHalf = Camera.main.orthographicSize * Camera.main.aspect;

        // "화면 반을 넘어가지 않게" 조작하기 위해 계산된 월드 경계의 절반을 사용합니다.
        // 원점을 기준으로 움직이므로 -_boundaryX ~ +_boundaryX, -_boundaryY ~ +_boundaryY 범위가 됩니다.
        _boundaryX = worldWidthHalf / 2f;
        _boundaryY = worldHeightHalf / 2f;
    }

    // 게임 오브젝트가 게임을 시작한 후 최대한 많이 실행 (지속적으로)
    private void Update()
    {
        HandleSpeedInput(); // 스피드 조작 처리
        MovePlayer();       // 플레이어 이동 처리
    }

    private void HandleSpeedInput()
    {
        // Q 키를 누르면 스피드 업
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed = Mathf.Min(Speed + _speedChangeAmount, _maxSpeed); // 최대 스피드를 넘지 않도록 제한
            Debug.Log($"Speed increased to: {Speed}");
        }

        // E 키를 누르면 스피드 다운
        if (Input.GetKeyDown(KeyCode.E))
        {
            Speed = Mathf.Max(Speed - _speedChangeAmount, _minSpeed); // 최소 스피드를 넘지 않도록 제한
            Debug.Log($"Speed decreased to: {Speed}");
        }
    }

    private void MovePlayer()
    {
        // 1. 키보드 입력을 감지한다.
        // 유니티에서는 Input이라는 모듈이 입력에 관한 모든 것을 담당함.
        // GetAxtis -> 원하는 축을 -1 ~ 1 로 가져온다. (입력 했냐 안했냐 판별)
        float h = Input.GetAxis("Horizontal"); // 수평 입력에 대한 값을 -1 ~ 1 로 가져옴
        float v = Input.GetAxis("Vertical");   // 수직 입력에 대한 값을 -1 ~ 1 로 가져옴

        // Debug.Log($"{h} {v}"); // 필요 없으므로 주석 처리

        // 2. 입력으로부터 방향을 구한다.
        // Vector : 크기와 방향을 표현하는 물리 개념
        Vector2 direction = new Vector2(h, v);
        // Debug.Log($"{direction.x} {direction.y}"); // 필요 없으므로 주석 처리

        // 입력이 없을 경우 정규화하지 않아도 되지만, 대각선 이동 시 속도 보정을 위해 정규화하는 것이 좋습니다.
        // 예를 들어 (1,1) 방향은 길이가 sqrt(2) 이므로 그냥 사용하면 속도가 빨라집니다.
        if (direction.sqrMagnitude > 1f) // 벡터의 제곱 크기가 1보다 크면 (대각선 입력)
        {
            direction.Normalize(); // 벡터의 길이를 1로 만들어 실제 이동 속도를 일정하게 유지
        }


        // 3. Vector 방향으로 이동한다.
        Vector2 position = transform.position; // 현재 위치 (Vector3로 해도 되지만 z축은 사용하지 않기 때문에 Vector2)

        // 새로운 위치 = 현재 위치 + (방향 * 속력) * 시간
        // 새로운 위치 = 현재 위치 + (속도)        * 시간
        //      새로운 위치 = 현재 위치 + (방향)   *  속도      * 시간
        Vector2 newPosition = position + direction * Speed * Time.deltaTime;  // 새로운 위치

        // Time.deltaTime : 이전 프레임으로부터 현재 프레임까지 시간이 얼마나 흘렀는지 나타내는 값 (delta : 얼마나 변했는가)
        // 각 PC 사양에 따라 다른 FPS 값의 차이를 메꿔줄 수 있음.
        // 1초 / FPS 의 값과 비슷함.

        // 이동속도 : 10
        // PC1 :  50FPS : Update -> 초당  50번 실행 -> 10 *  50 =  500 * (Time.deltaTime)
        // PC2 : 100FPS : Update -> 초당 100번 실행 -> 10 * 100 = 1000 * (Time.deltaTime) // PC1, PC2 두 값이 같아짐

        // 새로 계산된 위치가 경계를 넘지 않도록 제한 (clamp)
        newPosition.x = Mathf.Clamp(newPosition.x, -_boundaryX, _boundaryX);
        newPosition.y = Mathf.Clamp(newPosition.y, -_boundaryY, _boundaryY);

        transform.position = newPosition;      // 새로운 위치로 갱신
    }
}