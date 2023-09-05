using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class Attack : MonoBehaviourPun
{
    private float pushForce = 20.0f; // 밀어내는 힘
    private Rigidbody temp_TargetRb;
    private Vector3 temp_PushDirection;

    [PunRPC]
    public void ReceiveAttack(int id)
    {
        photonView.RPC("DOAttack", RpcTarget.All, id);
    }

    [PunRPC]
    // 대상(타겟)에게 공격하는 함수
    public void DOAttack(int id)
    {
        PhotonView targetPhotonView = PhotonView.Find(id);
        GameObject target = targetPhotonView.gameObject;

        // 타겟의 리지드 바디를 가져옴
        temp_TargetRb = target.GetComponent<Rigidbody>();

        // 타겟 오브젝트의 위치와 공격한 오브젝트의 위치 비교
        temp_PushDirection = target.transform.position - transform.position;

        // 타겟 오브젝트에 힘을 가해 밀어냄
        temp_TargetRb.AddForce(temp_PushDirection.normalized * pushForce, ForceMode.Impulse);

        // 타겟에게 데미지 처리
        //PlayerStateManager.Instance.AddDamage(target, gameObject);
    }
}
