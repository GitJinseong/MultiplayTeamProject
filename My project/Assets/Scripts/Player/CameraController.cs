using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera ����

    private void Start()
    {
        // ����: virtualCamera�� Inspector���� ����Ǿ��ٰ� �����մϴ�.
    }

    private void Update()
    {
        // ���ϴ� ��ġ�� ȸ�� �� ����
        Vector3 desiredPosition = new Vector3(0f, 5f, -7f); // ���ϴ� ��ġ
        Quaternion desiredRotation = Quaternion.Euler(30f, 0f, 0f); // ���ϴ� ȸ��

        // Cinemachine Virtual Camera�� Transform ����
        virtualCamera.transform.position = desiredPosition;
        virtualCamera.transform.rotation = desiredRotation;
    }
}
