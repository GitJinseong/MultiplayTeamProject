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

                // ��� �ݶ��̴����� ���� ó��
                photonView.RPC("DOAttack", RpcTarget.All, collider.gameObject);
                //attack.DOAttack(collider.gameObject);

                // �� ó�� ���� Monster ������Ʈ�� ó���ϰ� ����
                break;
            }
        }
    }
}
