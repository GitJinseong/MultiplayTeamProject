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
    public float hp = 50f; // �÷��̾��� ����ü��
    public float max_Hp = 50f; // �÷��̾��� �ִ�ü��
    public float atk = 10f; // �÷��̾��� ���ݷ�
    private float respawnDelay = 5f; // ������ ���ð�
    private bool isDead = false; // �÷��̾ �׾����� üũ�ϴ� ����

    private void Awake()
    {
        moveController = GetComponent<PlayerMoveController>();
        deadSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾� ������� �Լ� ȣ��
        CheckIsDead();
        // UI ���� ���� ����
        RefreshPlayerStateForUI();
    }

    // �÷��̾ �׾������� üũ�ϴ� �Լ�
    private void CheckIsDead()
    {
        // ü���� 0 �����̰� ���� �ʾ��� ���
        if (hp <= 0 && isDead == false)
        {
            // ���� ó�� �Լ� ȣ��
            Dead();
        }
    }

    // UI �÷��̾� ���� ���� ���� �Լ�
    private void RefreshPlayerStateForUI()
    {
        txt_Hp.text = "HP : " + hp.ToString();
    }

    // ���� ó�� �Լ�
    private void Dead()
    {
        // ��� ���� bool �� ����
        isDead = true;
        // ���� �ִϸ��̼� ȣ��
        playerAnimator.SetTrigger("Dead");
        // ������ �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(RespawnForDelay());
        // �÷��̾� �̵� ���ɿ��� ��ȯ �Լ� ȣ��
        PlayerMoveConvert();
        // ��� ���� ���
        deadSound.Play();
    }

    // �÷��̾� �̵� ���ɿ���(true/false) ��ȯ �Լ�
    private void PlayerMoveConvert()
    {
        // ����׿� if��
        if (moveController != null)
        {
            // �÷��̾� ������ ���¸� ��ȯ
            moveController.enabled = !moveController.enabled;
        }
    }

    // ������ �Լ�
    private void Respawn()
    {
        // ���� ü���� �ִ� ü������ ����
        hp = max_Hp;
        // ��� ���� false�� �ʱ�ȭ
        isDead = false;
    }

    // ������ �ڷ�ƾ �Լ�
    private IEnumerator RespawnForDelay()
    {
        // ������ ���ð� ��ŭ ���
        yield return new WaitForSeconds(respawnDelay);
        // ������ �Լ� ȣ��
        Respawn();
        // �÷��̾� ������Ʈ �ִϸ��̼� �ʱ�ȭ
        player.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        player.SetActive(true);
        // �÷��̾� �̵� ���ɿ��� ��ȯ �Լ� ȣ��
        PlayerMoveConvert();
    }
}
