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
        // 현재 콜라이더 주변에 있는 모든 콜라이더들을 검출
        colliders = Physics.OverlapSphere(attackTransform.position, range);

        foreach (Collider collider in colliders)
        {
            // 찾아낸 콜라이더의 태기가 "Monster" 혹은 "Player" 일 경우
            if (collider.CompareTag("Monster") || collider.CompareTag("Player"))
            {
                Debug.Log("현재 콜라이더 주변에 있는 콜라이더: " + collider.name);
                
                // 대상 콜라이더에게 공격 처리
                attack.DOAttack(collider.gameObject);

                // 맨 처음 나온 Monster 오브젝트만 처리하고 종료
                break;
            }
        }
    }
}
