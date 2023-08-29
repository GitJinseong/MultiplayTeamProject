using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // �÷��̾��� �̵� �ӵ�
    public float rotationSpeed = 5.0f; // �÷��̾��� ȸ�� �ӵ�

    private void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal"); // �¿� �Է� (-1 ~ 1)
        float verticalInput = Input.GetAxis("Vertical"); // ���� �Է� (-1 ~ 1)

        Vector3 moveDirection = new Vector3(0.0f, 0.0f, verticalInput);

        // �÷��̾��� ���� ��ġ�� �Է� ������ �̿��Ͽ� �̵��� ���
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

        // �̵����� ���� ��ġ�� ���Ͽ� �÷��̾� �̵�
        transform.Translate(movement);

        // ���콺 �Է��� �޾� �÷��̾��� X�� ȸ��
        float mouseX = Input.GetAxis("Mouse X");

        if (mouseX != 0)
        {
            Quaternion moveRotation = Quaternion.Euler(0f,
                mouseX * rotationSpeed, 0f);
            transform.rotation *= moveRotation;
        }

    }
}
