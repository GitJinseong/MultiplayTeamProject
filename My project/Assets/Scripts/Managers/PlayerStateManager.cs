using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerStateManager : MonoBehaviourPun
{
    private PhotonView temp_targetPhotonView;
    private PhotonView temp_myPhotonView;
    private PlayerState temp_targetState;
    private PlayerState temp_myState;

    //������ ó���� ��� Ŭ���̾�Ʈ���� �����ϴ� �Լ�
    [PunRPC]
    public void ReceiveAddDamage(int[] targetIds)
    {
        Debug.Log($"Ÿ�� id : {targetIds[0]}");
        Debug.Log($"���� id : {targetIds[1]}");
        photonView.RPC("AddDamage", RpcTarget.All, targetIds);
    }

    [PunRPC]
    // Ÿ�� ������Ʈ���� �������� �ִ� �Լ�
    public void AddDamage(int[] targetIds)
    {
        // �ӽú����� PhotonView �Ҵ�
        temp_targetPhotonView = PhotonView.Find(targetIds[0]);
        temp_myPhotonView = PhotonView.Find(targetIds[1]);

        //// �ӽú����鿡 PlayerState�� �Ҵ�
        temp_targetState = temp_targetPhotonView.gameObject.GetComponent<PlayerState>();
        temp_myState = temp_myPhotonView.gameObject.GetComponent<PlayerState>();

        Debug.Log($"Ÿ�� id : {targetIds[0]}");
        Debug.Log($"���� id : {targetIds[1]}");


        if (temp_targetState != null)
        {
            // Ÿ���� hp�� me�� ���ݷ� ��ŭ ���ҽ�Ŵ
            temp_targetState.hp -= temp_myState.atk;
        }
    }
}
