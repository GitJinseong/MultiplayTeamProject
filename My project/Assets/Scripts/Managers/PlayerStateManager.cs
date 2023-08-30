using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    #region 싱글톤 선언
    private static PlayerStateManager m_instance;
    public static PlayerStateManager Instance
    {
        get 
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 PlayerStateManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<PlayerStateManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    #endregion

    private PlayerState temp_targetState;
    private PlayerState temp_myState;

    // 타겟 오브젝트에게 데미지를 주는 함수
    public void AddDamage(GameObject target, GameObject me)
    {
        // 임시변수들에 PlayerState를 할당
        temp_targetState = target.GetComponent<PlayerState>();
        temp_myState = me.GetComponent<PlayerState>();

        if (temp_targetState != null)
        {
            // 타겟의 hp를 me의 공격력 만큼 감소시킴
            temp_targetState.hp -= temp_myState.atk;
        }
    }
}
