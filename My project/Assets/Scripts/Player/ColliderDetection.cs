using Photon.Pun;
using UnityEngine;

public class ColliderDetection : MonoBehaviourPun
{
    public Transform attackTransform;
    private Collider[] temp_Colliders;
    private Attack attack;

    private void Awake()
    {
        attack = GetComponent<Attack>();
    }

    public void GetColliders(float range)
    {
        // 현재 콜라이더(공격 범위용 콜라이더) 주변에 있는 모든 콜라이더들을 검출
        temp_Colliders = Physics.OverlapSphere(attackTransform.position, range);

        foreach (Collider collider in temp_Colliders)
        {
            // 찾아낸 콜라이더의 태그가 "Monster" 혹은 "Player" 일 경우
            if (collider.CompareTag("Monster") || collider.CompareTag("Player"))
            {
                Debug.Log("현재 콜라이더 주변에 있는 콜라이더: " + collider.name);

                // 대상 photonViewId 검색
                int[] photonViewIds = {
                    collider.gameObject.GetComponent<PhotonView>().ViewID,
                    photonView.ViewID
                };
                //int targetPhotonViewId = collider.gameObject.GetComponent<PhotonView>().ViewID;
                //int myPhotonViewId = photonView.ViewID;

                // 대상 콜라이더에게 공격 물리 처리
                //attack.ReceiveAttack(photonViewID);
                photonView.RPC("ReceiveAttack", RpcTarget.MasterClient, photonViewIds[0]);

                // 대상에게 HP 데미지 처리
                photonView.RPC("ReceiveAddDamage", RpcTarget.MasterClient, photonViewIds);

                // 맨 처음 나온 Monster 오브젝트만 처리하고 종료
                break;
            }
        }
    }
}
