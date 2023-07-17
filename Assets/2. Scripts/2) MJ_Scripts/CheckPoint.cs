using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾ ���� ��ġ���� ���̸� �߻��Ͽ� �ؿ� ���� ����� ���� ��ġ�� �����Ѵ�.
public class CheckPoint : MonoBehaviour
{
    // �÷��̾ ���� ����� �� ����ؼ� ��ġ�� �����ϰ� �ʹ�.
    // �÷��̾� üũ ����Ʈ
    public GameObject player;
    public GameObject checkPoint;
    // ���� ��������
    private Ray ray;

    // ��������� FixedUpdate���� ����Ѵ�.(update���� ����ϸ� ������ ����� ��Ÿ��)
    void FixedUpdate()
    {
        //�÷��̾� �������� �ؿ� �������� �߻��Ѵ�.
        ray = new Ray(player.transform.position, -player.transform.up);
        RaycastHit hitInfo;
        // �� ���̾ƿ��� �ɷ��� �޴´�.
        int groundLayer = 1 << LayerMask.NameToLayer("Ground");
        // �ؿ� ���� ������ ���� ��ġ�� �浹�� ��ġ�� �����Ѵ�.
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, groundLayer))
        {
            //���� äũ����Ʈ ��ġ�� �ε��� ������ ��ġ�� ����, ������ ��¦ �¾��.
            checkPoint.transform.position = hitInfo.point + Vector3.up;
        }
    }
}
