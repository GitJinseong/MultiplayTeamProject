using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject player;
    public TMP_Text txt_Hp;
    private PlayerMoveController moveController;
    private AudioSource deadSound;
    public float hp = 50f; // 플레이어의 현재체력
    public float max_Hp = 50f; // 플레이어의 최대체력
    public float atk = 10f; // 플레이어의 공격력
    private float respawnDelay = 5f; // 리스폰 대기시간
    private bool isDead = false; // 플레이어가 죽었는지 체크하는 변수

    private void Awake()
    {
        moveController = GetComponent<PlayerMoveController>();
        deadSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 사망여부 함수 호출
        CheckIsDead();
        // UI 상태 정보 갱신
        RefreshPlayerStateForUI();
    }

    // 플레이어가 죽었는지를 체크하는 함수
    private void CheckIsDead()
    {
        // 체력이 0 이하이고 죽지 않았을 경우
        if (hp <= 0 && isDead == false)
        {
            // 죽음 처리 함수 호출
            Dead();
        }
    }

    // UI 플레이어 상태 정보 갱신 함수
    private void RefreshPlayerStateForUI()
    {
        txt_Hp.text = "HP : " + hp.ToString();
    }

    // 죽음 처리 함수
    private void Dead()
    {
        // 사망 여부 bool 값 설정
        isDead = true;
        // 죽음 애니메이션 호출
        playerAnimator.SetTrigger("Dead");
        // 리스폰 코루틴 함수 호출
        StartCoroutine(RespawnForDelay());
        // 플레이어 이동 가능여부 전환 함수 호출
        PlayerMoveConvert();
        // 사망 사운드 재생
        deadSound.Play();
    }

    // 플레이어 이동 가능여부(true/false) 전환 함수
    private void PlayerMoveConvert()
    {
        // 디버그용 if문
        if (moveController != null)
        {
            // 플레이어 무브의 상태를 전환
            moveController.enabled = !moveController.enabled;
        }
    }

    // 리스폰 함수
    private void Respawn()
    {
        // 현재 체력을 최대 체력으로 변경
        hp = max_Hp;
        // 사망 여부 false로 초기화
        isDead = false;
    }

    // 리스폰 코루틴 함수
    private IEnumerator RespawnForDelay()
    {
        // 리스폰 대기시간 만큼 대기
        yield return new WaitForSeconds(respawnDelay);
        // 리스폰 함수 호출
        Respawn();
        // 플레이어 오브젝트 애니메이션 초기화
        player.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        player.SetActive(true);
        // 플레이어 이동 가능여부 전환 함수 호출
        PlayerMoveConvert();
    }
}
