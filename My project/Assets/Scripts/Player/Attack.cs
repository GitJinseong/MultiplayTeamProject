using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float pushForce = 20.0f; // �о�� ��
    Rigidbody targetRb;

    // ���(Ÿ��)���� �����ϴ� �Լ�
    public void DOAttack(GameObject target)
    {
        // Ÿ���� ������ �ٵ� ������
        targetRb = target.GetComponent<Rigidbody>();

        // Ÿ�� ������Ʈ�� ��ġ�� ������ ������Ʈ�� ��ġ ��
        Vector3 pushDirection = target.transform.position - transform.position;

        // �ڽ� ������Ʈ�� ���� ���� �о
        targetRb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);

        // Ÿ�ٿ��� ������ ó��
        PlayerStateManager.Instance.AddDamage(target, gameObject);
    }
}
