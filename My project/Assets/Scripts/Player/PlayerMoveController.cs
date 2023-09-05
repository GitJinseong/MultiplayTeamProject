using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveController : MonoBehaviourPun
{
    public float moveSpeed = 5.0f; // �÷��̾��� �̵� �ӵ�
    public float rotationSpeed = 5.0f; // �÷��̾��� ȸ�� �ӵ�
    public float jumpForce = 10.0f; // �÷��̾��� ���� �Ŀ�
    public Animator playerAnimator;
    //public PhotonView PV;

    private bool isMotioned = false; // �÷��̾ �ִϸ��̼� ��� ������ Ȯ���ϴ� ����
    private bool isGround = true; // �÷��̾ ���� �ִ��� Ȯ���ϴ� ����
    private bool isJumped = false; // �÷��̾��� ���� ����� ����� Ȯ���ϴ� ����
    private bool isAttacked = false; // �÷��̾ ���� ������ Ȯ���ϴ� ����
    private float attackTime = 1.0f; // �÷��̾��� ���� ������
    private float jumpTime = 0.1f; // �ڿ������� ���� �ִϸ��̼��� ���� ����

    private Rigidbody rb;
    private ColliderDetection colliderDetection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        colliderDetection = GetComponent<ColliderDetection>();
    }

    private void Update()
    {
        // ������ Ŭ���̾�Ʈ���� ��ġ ���� ������
        SendPositionToMasterClient(transform.position);

        // ���� �÷��̾ �ƴ� ��� ������Ʈ ����
        if (!photonView.IsMine)
        {
            return;
        }

        // ���콺 �̵� �Լ� ȣ��
        MouseMove();
        // ���� �̵� �Լ� ȣ��
        Move();

        // ��� ������� �ƴ� ��� ��� ���
        if (isMotioned == false)
        {
            // ���� �Լ� ȣ��
            Jump();
            // ���� �Լ� ȣ��
            Attack();
        }
    }

    // ������ Ŭ���̾�Ʈ���� ��ġ ���� ������ RPC �Լ�
    [PunRPC]
    private void SendPositionToMasterClient(Vector3 position)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("ReceivePositionFromClient", RpcTarget.All, position);
        }
    }

    // ������ Ŭ���̾�Ʈ�� ��ġ ������ �޴� RPC �Լ�
    [PunRPC]
    private void ReceivePositionFromClient(Vector3 position, PhotonMessageInfo info)
    {

    }

    // ���콺 �̵� �Լ�

    private void MouseMove()
    {
        // ���콺 �Է��� �޾� �÷��̾��� X�� ȸ��
        float mouseX = Input.GetAxis("Mouse X");

        // ���콺�� �������� ���� ��� �÷��̾� ȸ�� ����
        if (mouseX != 0)
        {
            // ������ ��� (mouse posX ��ǥ * ȸ�� �ӵ�)�� �÷��̾� ȸ�� 
            Quaternion moveRotation = Quaternion.Euler(0f,
                mouseX * rotationSpeed, 0f);
            transform.rotation *= moveRotation;
        }
    }

    // ���� �̵� �Լ�

    private void Move()
    {
        if (isAttacked == false)
        {
            // ���� �̵�
            float verticalInput = Input.GetAxis("Vertical"); // ���� �Է� (-1 ~ 1)
            // �̵� �ִϸ��̼� ���� �� �ֱ�
            SendAnimationStatus("float", "Speed", false, verticalInput);

            // �÷��̾��� �Է��� ���� �̵� ���� ����(��/��)
            Vector3 moveDirection = new Vector3(0.0f, 0.0f, verticalInput);
            // �÷��̾��� ���� ��ġ�� �Է� ������ �̿��Ͽ� �̵��� ���
            Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

            // �̵����� ���� ��ġ�� ���Ͽ� �÷��̾� �̵�
            transform.Translate(movement);
        }
    }

    // ���� �Լ�

    private void Jump()
    {
        // ���� �ְ� �����̽� �ٸ� ���� ���
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            // ���� �ִ��� Ȯ���ϴ� bool �� ����
            isGround = false;
            // ��� ��� ���θ� �����ϴ� bool �� ����
            isMotioned = true;
            // ���� ��� ��� ���θ� �����ϴ� bool �� ����
            isJumped = true;

            // ���� ��� ���� true�� ����
            SendAnimationStatus("bool", "Jump", isJumped, 0f);

            // �߷¿� �����ϴ� ���� ���Ͽ� ����
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // ���� �ִϸ��̼� ĵ�� �ڷ�ƾ �Լ� ȣ��
            StartCoroutine(JumpDelay());
        }
    }

    // ���� �ִϸ��̼� ĵ�� �ڷ�ƾ �Լ�

    private IEnumerator JumpDelay()
    {
        // ���� Ÿ�Ӹ�ŭ ���
        yield return new WaitForSeconds(jumpTime);
        // ���� ��� ��� ���θ� �����ϴ� bool �� �ʱ�ȭ
        isJumped = false;
        // ���� ��� ���� false�� ����
        SendAnimationStatus("bool", "Jump", isJumped, 0f);
    }

    // ���� �Լ�

    private void Attack()
    {
        // ���� ���� �ƴ� ���¿��� ���� Ŭ����
        if (isAttacked == false && Input.GetMouseButtonDown(0))
        {
            // ��� ��� ���θ� �����ϴ� bool �� ����
            isMotioned = true;
            // �ߺ� ���� ������ bool �� ����
            isAttacked = true;

            // ���� ������ �ڷ�ƾ �Լ� ȣ��
            StartCoroutine(AttackForDelay());

            // ���� ��� ���� true�� ����
            SendAnimationStatus("bool", "Attack", isAttacked, 0f);
        }
    }

    // ���� ������ �ڷ�ƾ �Լ�

    private IEnumerator AttackForDelay()
    {
        // attackTime / 2��ŭ ���
        yield return new WaitForSeconds(attackTime * 0.5f);
        // �������� ����Ͽ� ���� ������Ʈ �ֺ��� �ִ� ��� �ݶ��̴��� �����ϰ� ù��° �ݶ��̴��� ������
        colliderDetection.GetColliders(1.0f); // �⺻���� 1.5f

        // attackTime / 2��ŭ ���
        yield return new WaitForSeconds(attackTime * 0.5f);
        // ���� ���� ���·� bool �� �ʱ�ȭ
        isAttacked = false;
        // ��� ��� ���θ� �����ϴ� bool �� �ʱ�ȭ
        isMotioned = false;

        // ���� ��� ���� false�� ����
        SendAnimationStatus("bool", "Attack", isAttacked, 0f);
    }

    // ���� �浹 �ߴ��� üũ�ϴ� �Լ�

    private void OnCollisionEnter(Collision collision)
    {
        // �� Ȥ�� ������Ʈ�� ������ ���
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Object"))
        {
            // ���� �ִ��� Ȯ���ϴ� bool �� �ʱ�ȭ
            isGround = true;
            // ��� ��� ���θ� �����ϴ� bool �� �ʱ�ȭ
            isMotioned = false;
        }
    }

    // �ִϸ��̼� ���¸� ������ Ŭ���̾�Ʈ���� �����ϴ� �Լ�
    private void SendAnimationStatus(string type, string name, bool isAnimating, float value)
    {
        // �ִϸ��̼� ���� ���θ� ������ Ŭ���̾�Ʈ���� ����
        photonView.RPC("ReceiveAnimationStatus", RpcTarget.MasterClient, type, name, isAnimating, value);
    }
}
