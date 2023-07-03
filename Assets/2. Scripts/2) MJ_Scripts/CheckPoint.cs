using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // �÷��̾ ���� ����� �� ����ؼ� ��ġ�� �����ϰ� �ʹ�.
    // �÷��̾� üũ ����Ʈ
    public GameObject player;
    public GameObject checkPoint;

    private void OnCollisionEnter(Collision collision)
    {
        // �ε��� ��ü�� ���̸� ��ġ�� ��� ����Ѵ�.
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // üũ����Ʈ�� ��ġ�� �÷��̾��� ��ġ�̴�.
            checkPoint.transform.position = player.transform.position;
        }
    }
}
