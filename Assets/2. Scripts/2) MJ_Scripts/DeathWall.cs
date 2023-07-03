using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    // �÷��̾ �����´�.
    public GameObject player;
    // üũ ����Ʈ�� �����´�.
    public GameObject checkPoint;

    // ���� �÷��̾ �±� �ȴٸ� ���� ��ġ���� �¾�� �Ѵ�.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            player.transform.position = checkPoint.transform.position;
        }
    }
}
