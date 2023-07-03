using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // 플레이어가 땅에 닿았을 때 계속해서 위치를 저장하고 싶다.
    // 플레이어 체크 포인트
    public GameObject player;
    public GameObject checkPoint;

    private void OnCollisionEnter(Collision collision)
    {
        // 부딪힌 물체가 땅이면 위치를 계속 기억한다.
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // 체크포인트의 위치는 플레이어의 위치이다.
            checkPoint.transform.position = player.transform.position;
        }
    }
}
