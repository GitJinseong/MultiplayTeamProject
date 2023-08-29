using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float smoothSpeed = 10.0f; // 카메라의 부드러운 이동을 조절하는 변수
    public Vector3 offset = new Vector3(0f, 5f, -7f); // 카메라와 플레이어 간의 거리 조절 변수

    private Transform playerTransform; // 플레이어의 Transform을 저장할 변수

    private void Start()
    {
        // 플레이어 오브젝트의 Transform 가져오기
        playerTransform = transform.parent;
    }

    private void LateUpdate()
    {
        // 플레이어의 회전을 가져와서 회전에 적용
        Quaternion playerRotation = playerTransform.rotation * Quaternion.Euler(0f, Input.GetAxis("Mouse X") * smoothSpeed, 0f);

        // 플레이어의 위치와 회전을 기반으로 카메라 위치 설정
        Vector3 cameraTargetPosition = playerTransform.position - playerRotation * Vector3.forward * offset.z + Vector3.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, cameraTargetPosition, smoothSpeed * Time.deltaTime);

        // 플레이어의 정면을 바라보는 방향 벡터 계산
        Vector3 lookDirection = playerTransform.position - transform.position;
        lookDirection.y = 0f; // y 축 회전 방지

        // 카메라의 회전을 조절하여 플레이어를 중심으로 바라보게 설정
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, smoothSpeed * Time.deltaTime);
    }
}
