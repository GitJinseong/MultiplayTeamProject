using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 플레이어의 이동 속도
    public float rotationSpeed = 5.0f; // 플레이어의 회전 속도
    public float jumpForce = 10.0f; // 플레이어의 점프 파워
    public Animator playerAnimator; 

    private bool isMotioned = false; // 플레이어가 애니메이션 재생 중인지 확인하는 변수
    private bool isGround = true; // 플레이어가 땅에 있는지 확인하는 변수
    private bool isJumped = false; // 플레이어의 점프 모션의 재생을 확인하는 변수
    private bool isAttacked = false; // 플레이어가 공격 중인지 확인하는 변수
    private float attackTime = 1.0f; // 플레이어의 공격 딜레이
    private float jumpTime = 0.1f; // 자연스러운 점프 애니메이션을 위한 변수

    private Rigidbody rb;
    private ColliderDetection colliderDetection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        colliderDetection = GetComponent<ColliderDetection>();
    }

    private void Update()
    {
        // 마우스 이동 함수 호출
        MouseMove();
        // 상하 이동 함수 호출
        Move();

        // 모션 재생중이 아닐 경우 모션 재생
        if (isMotioned == false)
        {
            // 점프 함수 호출
            Jump();
            // 공격 함수 호출
            Attack();
        }

        // 모션에 즉각 대응하기 위해 프레임 단위로 점프 모션 변경
        playerAnimator.SetBool("Jump", isJumped);
        // 모션에 즉각 대응하기 위해 프레임 단위로 공격 모션 변경
        playerAnimator.SetBool("Attack", isAttacked);
    }

    // 마우스 이동 함수
    private void MouseMove()
    {
        // 마우스 입력을 받아 플레이어의 X축 회전
        float mouseX = Input.GetAxis("Mouse X");

        // 마우스가 움직이지 않을 경우 플레이어 회전 고정
        if (mouseX != 0)
        {
            // 움직일 경우 (mouse posX 좌표 * 회전 속도)로 플레이어 회전 
            Quaternion moveRotation = Quaternion.Euler(0f,
                mouseX * rotationSpeed, 0f);
            transform.rotation *= moveRotation;
        }
    }

    // 상하 이동 함수
    private void Move()
    {
        if (isAttacked == false)
        {
            // 상하 이동
            float verticalInput = Input.GetAxis("Vertical"); // 상하 입력 (-1 ~ 1)
            // 이동 애니메이션 블랜드 값 넣기
            playerAnimator.SetFloat("Speed", verticalInput);

            // 플레이어의 입력을 통해 이동 방향 결정(상/하)
            Vector3 moveDirection = new Vector3(0.0f, 0.0f, verticalInput);
            // 플레이어의 현재 위치에 입력 방향을 이용하여 이동량 계산
            Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

            // 이동량을 현재 위치에 더하여 플레이어 이동
            transform.Translate(movement);
        }
    }

    // 점프 함수
    private void Jump()
    {
        // 땅에 있고 스페이스 바를 누를 경우
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            // 땅에 있는지 확인하는 bool 값 변경
            isGround = false;
            // 모션 재생 여부를 구분하는 bool 값 변경
            isMotioned = true;
            // 점프 모션 재생 여부를 구분하는 bool 값 변경
            isJumped = true;

            // 중력에 대항하는 힘을 가하여 점프
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // 점프 애니메이션 캔슬 코루틴 함수 호출
            StartCoroutine(JumpDelay());
        }
    }
    
    // 점프 애니메이션 캔슬 코루틴 함수
    private IEnumerator JumpDelay()
    {
        // 점프 타임만큼 대기
        yield return new WaitForSeconds(jumpTime);
        // 점프 모션 재생 여부를 구분하는 bool 값 초기화
        isJumped = false;
    }

    // 공격 함수
    private void Attack()
    {
        // 공격 중이 아니고 왼쪽 클릭시
        if (isAttacked == false && Input.GetMouseButtonDown(0))
        {
            // 모션 재생 여부를 구분하는 bool 값 변경
            isMotioned = true;
            // 중복 공격 방지용 bool 값 변경
            isAttacked = true;

            // 공격 딜레이 코루틴 함수 호출
            StartCoroutine(AttackForDelay());
        }
    }

    // 공격 딜레이 코루틴 함수
    private IEnumerator AttackForDelay()
    {
        // attackTime / 2만큼 대기
        yield return new WaitForSeconds(attackTime * 0.5f);
        // 오버랩을 사용하여 현재 오브젝트 주변에 있는 모든 콜라이더 검출
        colliderDetection.GetColliders(1.0f); // 기본범위 1.5f

        // attackTime / 2만큼 대기
        yield return new WaitForSeconds(attackTime * 0.5f);
        // 공격 가능 상태로 bool 값 초기화
        isAttacked = false;
        // 모션 재생 여부를 구분하는 bool 값 초기화
        isMotioned = false;
    }

    // 땅과 충돌 했는지 체크하는 함수
    private void OnCollisionEnter(Collision collision)
    {
        // 땅 혹은 오브젝트에 착지할 경우
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Object"))
        {
            // 땅에 있는지 확인하는 bool 값 초기화
            isGround = true;
            // 모션 재생 여부를 구분하는 bool 값 초기화
            isMotioned = false;
        }
    }
}
