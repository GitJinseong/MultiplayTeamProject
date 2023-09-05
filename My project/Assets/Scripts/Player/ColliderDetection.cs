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
        // ���� �ݶ��̴�(���� ������ �ݶ��̴�) �ֺ��� �ִ� ��� �ݶ��̴����� ����
        temp_Colliders = Physics.OverlapSphere(attackTransform.position, range);

        foreach (Collider collider in temp_Colliders)
        {
            // ã�Ƴ� �ݶ��̴��� �±װ� "Monster" Ȥ�� "Player" �� ���
            if (collider.CompareTag("Monster") || collider.CompareTag("Player"))
            {
                Debug.Log("���� �ݶ��̴� �ֺ��� �ִ� �ݶ��̴�: " + collider.name);

                // ��� photonViewId �˻�
                int[] photonViewIds = {
                    collider.gameObject.GetComponent<PhotonView>().ViewID,
                    photonView.ViewID
                };
                //int targetPhotonViewId = collider.gameObject.GetComponent<PhotonView>().ViewID;
                //int myPhotonViewId = photonView.ViewID;

                // ��� �ݶ��̴����� ���� ���� ó��
                //attack.ReceiveAttack(photonViewID);
                photonView.RPC("ReceiveAttack", RpcTarget.MasterClient, photonViewIds[0]);

                // ��󿡰� HP ������ ó��
                photonView.RPC("ReceiveAddDamage", RpcTarget.MasterClient, photonViewIds);

                // �� ó�� ���� Monster ������Ʈ�� ó���ϰ� ����
                break;
            }
        }
    }
}
