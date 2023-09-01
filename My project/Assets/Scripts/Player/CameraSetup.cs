using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraSetup : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        // 만약 자신이 로컬 플레이어라면
        if (photonView.IsMine)
        {
            // 씬에 있는 시네머신 가상 카메라를 찾고
            CinemachineVirtualCamera followCam = FindAnyObjectByType<CinemachineVirtualCamera>();
            // 가상 카메라의 추적 대상을 자신의 트랜스폼으로 변경
            followCam.Follow = transform;
            followCam.LookAt = transform;
        }
    }

    // 오브젝트 타입을 검색하는 메서드
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
