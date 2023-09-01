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
        // �÷��̾� ���� �Լ� ȣ��
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        // ���� �÷��̾ ĳ���� Prefab ����
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, rectTransform.anchoredPosition, Quaternion.identity);
    }

    [PunRPC]
    private void SpawnPlayerRPC()
    {
        // ���� �÷��̾��� ��쿡�� ����
        if (photonView.IsMine && PhotonNetwork.IsMasterClient == false)
        {
            Debug.Log("SPawnPlayerRPC ȣ��");
            // ���� �÷��̾ ĳ���� Prefab ����
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, rectTransform.anchoredPosition, Quaternion.identity);
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        // �÷��̾� ���� �Լ� ȣ��
        SpawnPlayerRPC();
    }
}
