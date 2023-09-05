using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationSync : MonoBehaviourPunCallbacks, IPunObservable
{
    public Animator animator;
    private string animationName;

    [PunRPC]
    public void DOAnimationSyncForBool(string name, bool isAnimating)
    {
        Debug.Log("Running DOAnimationSync");
        animationName = name;
        animator.SetBool(animationName, isAnimating);
        Debug.Log($"animator bool : {animator.GetBool(animationName)}");
    }

    [PunRPC]
    public void DOAnimationSyncForFloat(string name, float value)
    {
        Debug.Log("Running DOAnimationSync");
        animationName = name;
        animator.SetFloat(animationName, value);
        Debug.Log($"animator bool : {animator.GetFloat(animationName)}");
    }

    // PhotonView�� �����͸� �����ϰų� ������ �� �ݹ�Ǵ� �Լ�
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("Call OnPhotonSerializeView");
        // �����͸� �����ϴ� Ŭ���̾�Ʈ�� ���
        if (stream.IsWriting)
        {
            stream.SendNext(animator.GetBool(animationName)); // �ִϸ��̼� ���¸� ����
            Debug.Log("Send Info With Animation");
        }
        // �����͸� �����ϴ� Ŭ���̾�Ʈ �� ���
        else
        {
            animator.SetBool(animationName, (bool)stream.ReceiveNext());
            Debug.Log("Get Info With Animation");
        }
    }

    [PunRPC]
    private void ReceiveAnimationStatus(string type, string name, bool isAnimating, float value)
    {
        photonView.RPC("BroadcastAniamtionStatus", RpcTarget.All, type, name, isAnimating, value);
    }

    [PunRPC]
    private void BroadcastAniamtionStatus(string type, string name, bool isAnimating, float value)
    {
        switch (type)
        {
            case "bool":
                animator.SetBool(name, isAnimating);
                break;

            case "float":
                animator.SetFloat(name, value);
                break;
        }
    }

}
