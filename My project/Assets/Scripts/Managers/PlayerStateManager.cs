using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    #region �̱��� ����
    private static PlayerStateManager m_instance;
    public static PlayerStateManager Instance
    {
        get 
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ PlayerStateManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<PlayerStateManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }
    #endregion

    private PlayerState temp_targetState;
    private PlayerState temp_myState;

    // Ÿ�� ������Ʈ���� �������� �ִ� �Լ�
    public void AddDamage(GameObject target, GameObject me)
    {
        // �ӽú����鿡 PlayerState�� �Ҵ�
        temp_targetState = target.GetComponent<PlayerState>();
        temp_myState = me.GetComponent<PlayerState>();

        if (temp_targetState != null)
        {
            // Ÿ���� hp�� me�� ���ݷ� ��ŭ ���ҽ�Ŵ
            temp_targetState.hp -= temp_myState.atk;
        }
    }
}
