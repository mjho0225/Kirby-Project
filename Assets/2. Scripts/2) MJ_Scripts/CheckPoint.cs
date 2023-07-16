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
    private RaycastHit hitInfo;

    void Update()
    {
        //�÷��̾� �������� �ؿ� ���̸� �����Ѵ�.
        ray = new Ray(player.transform.position, -player.transform.up);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // �ε��� ��ü�� ���̸� ��ġ�� ��� ����Ѵ�, ���� ��Ҵٸ� �������� �����Ѵ�.
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && Physics.Raycast(ray, out hitInfo, LayerMask.NameToLayer("Ground")))
        {
            // üũ����Ʈ�� ��ġ�� �÷��̾��� ��ġ�̴�.
            checkPoint.transform.position = player.transform.position;
        }
    }
}
