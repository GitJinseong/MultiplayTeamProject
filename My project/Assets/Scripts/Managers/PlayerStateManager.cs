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

    //데미지 처리를 모든 클라이언트에게 전송하는 함수
    [PunRPC]
    public void ReceiveAddDamage(int[] targetIds)
    {
        Debug.Log($"타겟 id : {targetIds[0]}");
        Debug.Log($"나의 id : {targetIds[1]}");
        photonView.RPC("AddDamage", RpcTarget.All, targetIds);
    }

    [PunRPC]
    // 타겟 오브젝트에게 데미지를 주는 함수
    public void AddDamage(int[] targetIds)
    {
        // 임시변수에 PhotonView 할당
        temp_targetPhotonView = PhotonView.Find(targetIds[0]);
        temp_myPhotonView = PhotonView.Find(targetIds[1]);

        //// 임시변수들에 PlayerState를 할당
        temp_targetState = temp_targetPhotonView.gameObject.GetComponent<PlayerState>();
        temp_myState = temp_myPhotonView.gameObject.GetComponent<PlayerState>();

        Debug.Log($"타겟 id : {targetIds[0]}");
        Debug.Log($"나의 id : {targetIds[1]}");


        if (temp_targetState != null)
        {
            // 타겟의 hp를 me의 공격력 만큼 감소시킴
            temp_targetState.hp -= temp_myState.atk;
        }
    }
}
