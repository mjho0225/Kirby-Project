using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPivotCheckManager : MonoBehaviour
{
    public GameObject playerPivot;
    public GameObject playerPivotCheckPoint;

    private Ray ray;
    // ��������� FixedUpdate���� ����Ѵ�.(update���� ����ϸ� ������ ����� ��Ÿ��)
    // ������Ʈ �����ٰ� ���̸� ���� ����

    public void UpdatePoint()
    {
        ray = new Ray(playerPivot.transform.position, -playerPivot.transform.up);
        int groundLayer = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit hitInfo;
        // �ؿ� ���� ������ ���� ��ġ�� �浹�� ��ġ�� �����Ѵ�.
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, groundLayer))
        {
            //���� äũ����Ʈ ��ġ�� �ε��� ������ ��ġ�� ����, ������ ��¦ �¾��.
            playerPivotCheckPoint.transform.position = hitInfo.point + Vector3.up;
        }

    }
}
