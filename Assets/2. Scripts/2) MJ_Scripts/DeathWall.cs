using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

// �÷��̾ ���� ��ġ���� �������� �ٽ� �¾�� �����. ( 10 ��ġ���� )
public class DeathWall : MonoBehaviour
{
    // �÷��̾ �����´�.
    public GameObject player;
    // üũ ����Ʈ�� �����´�.
    public GameObject checkPoint;

    // �÷��̾� �ִ� �������� �Ÿ�
    public float maxFallPosition = 10;

    void Update()
    {
        // �÷��̾ ���� ��ġ���� �������� �ٽ� �¾�� �����. ( 10 ��ġ���� )
        if (player.transform.position.y < maxFallPosition)
        {
            player.transform.position = checkPoint.transform.position;
            // ī�޶� �ѹ� ����ŷ �Ѵ�.
            StartCoroutine(IEOnDamage());
        }
    }

    // ���� �÷��̾ �±� �ȴٸ� ���� ��ġ���� �¾�� �Ѵ�.
/*    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            player.transform.position = checkPoint.transform.position;
            // ī�޶� �ѹ� ����ŷ �Ѵ�.
            StartCoroutine(IEOnDamage());

        }
    }*/

    // ī�޶� ����ŷ�� 0.5�� �Ŀ� �����Ű�� �ʹ�.
    IEnumerator IEOnDamage()
    {
        PlayerHP.instance.HP -= 5;
        yield return new WaitForSeconds(0.18f);
        // 0.1�� �ڿ� �����Ű�� �ʹ�.
        CameraShaker.Instance.ShakeOnce(5f, 5f, 0.1f, 1.5f);

    }
}
