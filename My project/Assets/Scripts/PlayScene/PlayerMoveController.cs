using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 플레이어의 이동 속도
    public float rotationSpeed = 5.0f; // 플레이어의 회전 속도

    private void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal"); // 좌우 입력 (-1 ~ 1)
        float verticalInput = Input.GetAxis("Vertical"); // 상하 입력 (-1 ~ 1)

        Vector3 moveDirection = new Vector3(0.0f, 0.0f, verticalInput);

        // 플레이어의 현재 위치에 입력 방향을 이용하여 이동량 계산
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

        // 이동량을 현재 위치에 더하여 플레이어 이동
        transform.Translate(movement);

        // 마우스 입력을 받아 플레이어의 X축 회전
        float mouseX = Input.GetAxis("Mouse X");

        if (mouseX != 0)
        {
            Quaternion moveRotation = Quaternion.Euler(0f,
                mouseX * rotationSpeed, 0f);
            transform.rotation *= moveRotation;
        }

    }
}
