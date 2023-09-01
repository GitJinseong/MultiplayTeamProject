using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        // 플레이어 생성 함수 호출
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        // 로컬 플레이어가 캐릭터 Prefab 생성
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, rectTransform.anchoredPosition, Quaternion.identity);
    }

    [PunRPC]
    private void SpawnPlayerRPC()
    {
        // 로컬 플레이어인 경우에만 실행
        if (photonView.IsMine && PhotonNetwork.IsMasterClient == false)
        {
            Debug.Log("SPawnPlayerRPC 호출");
            // 로컬 플레이어가 캐릭터 Prefab 생성
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, rectTransform.anchoredPosition, Quaternion.identity);
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        // 플레이어 생성 함수 호출
        SpawnPlayerRPC();
    }
}
