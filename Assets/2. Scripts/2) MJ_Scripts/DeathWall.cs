using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

// �÷��̾ �ؿ� �ϴܿ��� ���� ĳ��Ʈ�� �R���� �� ���鿡 ��Ҵٸ� �߻���Ų��.
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
            // ī�޶� �ѹ� ����ŷ �Ѵ�.
            StartCoroutine(IEOnDamage());

        }
    }

    // ī�޶� ����ŷ�� 0.5�� �Ŀ� �����Ű�� �ʹ�.
    IEnumerator IEOnDamage()
    {
        PlayerHP.instance.HP -= 5;
        yield return new WaitForSeconds(0.18f);
        // 0.1�� �ڿ� �����Ű�� �ʹ�.
        CameraShaker.Instance.ShakeOnce(5f, 5f, 0.1f, 1.5f);

    }
}
