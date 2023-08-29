using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float smoothSpeed = 10.0f; // ī�޶��� �ε巯�� �̵��� �����ϴ� ����
    public Vector3 offset = new Vector3(0f, 5f, -7f); // ī�޶�� �÷��̾� ���� �Ÿ� ���� ����

    private Transform playerTransform; // �÷��̾��� Transform�� ������ ����

    private void Start()
    {
        // �÷��̾� ������Ʈ�� Transform ��������
        playerTransform = transform.parent;
    }

    private void LateUpdate()
    {
        // �÷��̾��� ȸ���� �����ͼ� ȸ���� ����
        Quaternion playerRotation = playerTransform.rotation * Quaternion.Euler(0f, Input.GetAxis("Mouse X") * smoothSpeed, 0f);

        // �÷��̾��� ��ġ�� ȸ���� ������� ī�޶� ��ġ ����
        Vector3 cameraTargetPosition = playerTransform.position - playerRotation * Vector3.forward * offset.z + Vector3.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, cameraTargetPosition, smoothSpeed * Time.deltaTime);

        // �÷��̾��� ������ �ٶ󺸴� ���� ���� ���
        Vector3 lookDirection = playerTransform.position - transform.position;
        lookDirection.y = 0f; // y �� ȸ�� ����

        // ī�޶��� ȸ���� �����Ͽ� �÷��̾ �߽����� �ٶ󺸰� ����
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, smoothSpeed * Time.deltaTime);
    }
}
