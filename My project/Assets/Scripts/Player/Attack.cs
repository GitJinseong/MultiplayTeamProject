using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float pushForce = 20.0f; // 밀어내는 힘
    Rigidbody targetRb;

    // 대상(타겟)에게 공격하는 함수
    public void DOAttack(GameObject target)
    {
        // 타겟의 리지드 바디를 가져옴
        targetRb = target.GetComponent<Rigidbody>();

        // 타겟 오브젝트의 위치와 공격한 오브젝트의 위치 비교
        Vector3 pushDirection = target.transform.position - transform.position;

        // 박스 오브젝트에 힘을 가해 밀어냄
        targetRb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);

        // 타겟에게 데미지 처리
        PlayerStateManager.Instance.AddDamage(target, gameObject);
    }
}
