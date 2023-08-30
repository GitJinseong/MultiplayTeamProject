using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun; //����
using Photon.Realtime; //����
using UnityEngine.UI; //����

public class PhotonManager_1 : MonoBehaviourPunCallbacks // Ŭ���� ���
{
    public TMP_Text StatusText;
    public TMP_InputField nickNameInput;
    public TMP_InputField roomNameInput;
    public GameObject uiPanel;
    public byte userNum = 5;

    private bool connect = false;

    private void Update()
    {
        // ���� ���� ǥ��
        StatusText.text = "Server State : " + PhotonNetwork.NetworkClientState.ToString();
    }

    // ������ �����ϴ� �Լ�
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("���� ���� �Ϸ�");
        string nickName = PhotonNetwork.LocalPlayer.NickName;
        nickName = nickNameInput.text;
        Debug.Log("����� �̸��� " + nickName + " �Դϴ�.");
        connect = true;
    }

    // ������ ���� �Լ�
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    // ������ ������ �� ȣ���ϴ� �Լ�
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("���� ����");
    }

    // �濡 �����ϴ� �Լ�
    public void JoinRoom()
    {
        if (connect)
        {
            PhotonNetwork.JoinRandomRoom();
            uiPanel.SetActive(false);
            Debug.Log(roomNameInput.text + "�濡 �����ϼ̽��ϴ�.");
        }
    }

    // ������ �� ���忡 �����ϸ� ���ο� �� ���� (master �� ����)
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = userNum });
    }

    // �濡 ���� ���� �� ȣ��
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}
