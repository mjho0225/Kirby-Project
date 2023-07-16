using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

// 플레이어가 밑에 하단에서 레이 캐스트를 쏳았을 때 지면에 닿았다면 발생시킨다.
public class DeathWall : MonoBehaviour
{
    // 플레이어를 가져온다.
    public GameObject player;
    // 체크 포인트를 가져온다.
    public GameObject checkPoint;


    // 만약 플레이어가 태그 된다면 원래 위치에서 태어나게 한다.
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            player.transform.position = checkPoint.transform.position;
            // 카메라를 한번 쉐이킹 한다.
            StartCoroutine(IEOnDamage());

        }
    }

    // 카메라 쉐이킹을 0.5초 후에 실행시키고 싶다.
    IEnumerator IEOnDamage()
    {
        PlayerHP.instance.HP -= 5;
        yield return new WaitForSeconds(0.18f);
        // 0.1초 뒤에 실행시키고 싶다.
        CameraShaker.Instance.ShakeOnce(5f, 5f, 0.1f, 1.5f);

    }
}
