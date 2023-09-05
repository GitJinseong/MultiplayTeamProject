using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class Attack : MonoBehaviourPun
{
    private float pushForce = 20.0f; // �о�� ��
    private Rigidbody temp_TargetRb;
    private Vector3 temp_PushDirection;

    [PunRPC]
    public void ReceiveAttack(int id)
    {
        photonView.RPC("DOAttack", RpcTarget.All, id);
    }

    [PunRPC]
    // ���(Ÿ��)���� �����ϴ� �Լ�
    public void DOAttack(int id)
    {
        PhotonView targetPhotonView = PhotonView.Find(id);
        GameObject target = targetPhotonView.gameObject;

        // Ÿ���� ������ �ٵ� ������
        temp_TargetRb = target.GetComponent<Rigidbody>();

        // Ÿ�� ������Ʈ�� ��ġ�� ������ ������Ʈ�� ��ġ ��
        temp_PushDirection = target.transform.position - transform.position;

        // Ÿ�� ������Ʈ�� ���� ���� �о
        temp_TargetRb.AddForce(temp_PushDirection.normalized * pushForce, ForceMode.Impulse);

        // Ÿ�ٿ��� ������ ó��
        //PlayerStateManager.Instance.AddDamage(target, gameObject);
    }
}
