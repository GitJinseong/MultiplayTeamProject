using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera 참조

    private void Start()
    {
        // 가정: virtualCamera는 Inspector에서 연결되었다고 가정합니다.
    }

    private void Update()
    {
        // 원하는 위치와 회전 값 설정
        Vector3 desiredPosition = new Vector3(0f, 5f, -7f); // 원하는 위치
        Quaternion desiredRotation = Quaternion.Euler(30f, 0f, 0f); // 원하는 회전

        // Cinemachine Virtual Camera의 Transform 설정
        virtualCamera.transform.position = desiredPosition;
        virtualCamera.transform.rotation = desiredRotation;
    }
}
