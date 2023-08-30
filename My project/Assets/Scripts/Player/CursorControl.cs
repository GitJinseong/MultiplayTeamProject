using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{

    void Start()
    {
        // ���� ���� �� ���콺 ����Ʈ�͸� ����
        Cursor.visible = false;
    }


    void Update()
    {
        // ���� �߿� Esc Ű�� ���� ��� ���콺 �����͸� ǥ��/���� ��ȯ
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            // ���� ���¸� �ݴ�� ��ȯ
            Cursor.visible = !Cursor.visible;
        }
    }
}
