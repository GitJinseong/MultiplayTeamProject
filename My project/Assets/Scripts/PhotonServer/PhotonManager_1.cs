using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun; //선언
using Photon.Realtime; //선언
using UnityEngine.UI; //선언

public class PhotonManager_1 : MonoBehaviourPunCallbacks // 클래스 상속
{
    public TMP_Text StatusText;
    public TMP_InputField nickNameInput;
    public TMP_InputField roomNameInput;
    public GameObject uiPanel;
    public byte userNum = 5;

    private bool connect = false;

    private void Update()
    {
        // 현재 상태 표시
        StatusText.text = "Server State : " + PhotonNetwork.NetworkClientState.ToString();
    }

    // 서버에 접속하는 함수
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 접속 완료");
        string nickName = PhotonNetwork.LocalPlayer.NickName;
        nickName = nickNameInput.text;
        Debug.Log("당신의 이름은 " + nickName + " 입니다.");
        connect = true;
    }

    // 연결을 끊는 함수
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    // 연결이 끊겼을 때 호출하는 함수
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("연결 끊김");
    }

    // 방에 입장하는 함수
    public void JoinRoom()
    {
        if (connect)
        {
            PhotonNetwork.JoinRandomRoom();
            uiPanel.SetActive(false);
            Debug.Log(roomNameInput.text + "방에 입장하셨습니다.");
        }
    }

    // 랜덤한 방 입장에 실패하면 새로운 방 생성 (master 방 생성)
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = userNum });
    }

    // 방에 입장 했을 때 호출
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}
