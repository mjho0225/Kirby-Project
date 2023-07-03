using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
