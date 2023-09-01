using Photon.Pun; // ����Ƽ�� ���� ������Ʈ��
using Photon.Realtime; // ���� ���� ���� ���̺귯��
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// ������(��ġ ����ŷ) ������ �� ������ ���
public class PhotonManager_1 : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1.0.0"; // ���� ����

    public TMP_Text connectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public Button joinButton; // �� ���� ��ư

    // ���� ����� ���ÿ� ������ ���� ���� �õ�
    private void Start()
    {
        // ���ӿ� �ʿ��� ����(���� ����) ����
        PhotonNetwork.GameVersion = gameVersion;
        // ������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();

        // �� ���� ��ư ��� ��Ȱ��ȭ
        joinButton.interactable = false;
        // ���� �õ� ������ �ؽ�Ʈ�� ǥ��
        connectionInfoText.text = "Connect To Master Server...";
    }

    // ������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
        // �� ���� ��ư Ȱ��ȭ
        joinButton.interactable = true;
        // �� ���� ���� ǥ��
        connectionInfoText.text = "Online : Connected To Master Server Succeed";
    }

    // ������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        // �� ���� ��ư ��Ȱ��ȭ
        joinButton.interactable = false;
        // ���� ���� ǥ��
        connectionInfoText.text = string.Format("{0}\n{1}",
            "Offline: Disconnected To Master Server", "Retry Connect Now...");

        // ������ �������� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    // �� ���� �õ�
    public void Connect()
    {
        // �ߺ� ���� �õ��� ���� ���� ���� ��ư ��� ��Ȱ��ȭ
        joinButton.interactable = false;

        // ������ ������ ���� ���̶��
        if (PhotonNetwork.IsConnected == true)
        {
            // �� ���� ����
            connectionInfoText.text = "Conncect To Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // ������ ������ ���� ���� �ƴ϶�� ������ ������ ���� �õ�
            connectionInfoText.text = string.Format("{0}\n{1}",
                "Offline: Disconnected To Master Server", "Retry Connect Now...");

            // ������ �������� ������ �õ�
            PhotonNetwork.ConnectUsingSettings();
        }
    }       // Connect()

    // (�� ���� ����)���� �� ������ ������ ��� �ڵ� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ���� ���� ǥ��
        connectionInfoText.text = "Nothing To Empty Room, Create New Room...";
        // �ִ� 4���� ���� ������ �� �� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }       // OnJoinRandomFailed()

    // �뿡 ���� �Ϸ�� ��� �ڵ� ����
    public override void OnJoinedRoom()
    {
        // ���� ���� ǥ��
        connectionInfoText.text = "Successes Joined Room";
        // ��� �� �����ڰ� Main ���� �ε��ϰ� ��.
        PhotonNetwork.LoadLevel("PlayScene");
    }       // OnJoinedRoom()
}

