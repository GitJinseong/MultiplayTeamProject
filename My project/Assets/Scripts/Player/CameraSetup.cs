using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraSetup : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        // ���� �ڽ��� ���� �÷��̾���
        if (photonView.IsMine)
        {
            // ���� �ִ� �ó׸ӽ� ���� ī�޶� ã��
            CinemachineVirtualCamera followCam = FindAnyObjectByType<CinemachineVirtualCamera>();
            // ���� ī�޶��� ���� ����� �ڽ��� Ʈ���������� ����
            followCam.Follow = transform;
            followCam.LookAt = transform;
        }
    }

    // ������Ʈ Ÿ���� �˻��ϴ� �޼���
    private T FindAnyObjectByType<T>() where T : Object
    {
        T[] objectsOfType = FindObjectsOfType<T>();
        if (objectsOfType.Length > 0)
        {
            return objectsOfType[0];
        }
        return null;
    }
}
