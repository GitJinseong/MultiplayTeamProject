using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    public Transform attackTransform;
    private Collider[] colliders;
    private Attack attack;

    private void Awake()
    {
        attack = GetComponent<Attack>();
    }

    public void GetColliders(float range)
    {
        // ���� �ݶ��̴� �ֺ��� �ִ� ��� �ݶ��̴����� ����
        colliders = Physics.OverlapSphere(attackTransform.position, range);

        foreach (Collider collider in colliders)
        {
            // ã�Ƴ� �ݶ��̴��� �±Ⱑ "Monster" Ȥ�� "Player" �� ���
            if (collider.CompareTag("Monster") || collider.CompareTag("Player"))
            {
                Debug.Log("���� �ݶ��̴� �ֺ��� �ִ� �ݶ��̴�: " + collider.name);
                
                // ��� �ݶ��̴����� ���� ó��
                attack.DOAttack(collider.gameObject);

                // �� ó�� ���� Monster ������Ʈ�� ó���ϰ� ����
                break;
            }
        }
    }
}
