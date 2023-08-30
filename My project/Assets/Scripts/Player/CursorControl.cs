using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{

    void Start()
    {
        // 게임 시작 시 마우스 포인트터를 숨김
        Cursor.visible = false;
    }


    void Update()
    {
        // 게임 중에 Esc 키를 누를 경우 마우스 포인터를 표시/숨김 전환
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            // 현재 상태를 반대로 전환
            Cursor.visible = !Cursor.visible;
        }
    }
}
