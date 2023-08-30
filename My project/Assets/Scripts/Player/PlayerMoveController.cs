using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // �÷��̾��� �̵� �ӵ�
    public float rotationSpeed = 5.0f; // �÷��̾��� ȸ�� �ӵ�
    public float jumpForce = 10.0f; // �÷��̾��� ���� �Ŀ�
    public Animator playerAnimator; 

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

        // ��ǿ� �ﰢ �����ϱ� ���� ������ ������ ���� ��� ����
        playerAnimator.SetBool("Jump", isJumped);
        // ��ǿ� �ﰢ �����ϱ� ���� ������ ������ ���� ��� ����
        playerAnimator.SetBool("Attack", isAttacked);
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
            playerAnimator.SetFloat("Speed", verticalInput);

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
    }

    // ���� �Լ�
    private void Attack()
    {
        // ���� ���� �ƴϰ� ���� Ŭ����
        if (isAttacked == false && Input.GetMouseButtonDown(0))
        {
            // ��� ��� ���θ� �����ϴ� bool �� ����
            isMotioned = true;
            // �ߺ� ���� ������ bool �� ����
            isAttacked = true;

            // ���� ������ �ڷ�ƾ �Լ� ȣ��
            StartCoroutine(AttackForDelay());
        }
    }

    // ���� ������ �ڷ�ƾ �Լ�
    private IEnumerator AttackForDelay()
    {
        // attackTime / 2��ŭ ���
        yield return new WaitForSeconds(attackTime * 0.5f);
        // �������� ����Ͽ� ���� ������Ʈ �ֺ��� �ִ� ��� �ݶ��̴� ����
        colliderDetection.GetColliders(1.0f); // �⺻���� 1.5f

        // attackTime / 2��ŭ ���
        yield return new WaitForSeconds(attackTime * 0.5f);
        // ���� ���� ���·� bool �� �ʱ�ȭ
        isAttacked = false;
        // ��� ��� ���θ� �����ϴ� bool �� �ʱ�ȭ
        isMotioned = false;
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
}
