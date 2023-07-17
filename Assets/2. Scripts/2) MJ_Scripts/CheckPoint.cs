using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 현재 위치에서 레이를 발사하여 밑에 땅에 닿았을 때만 위치를 저장한다.
public class CheckPoint : MonoBehaviour
{
    // 플레이어가 땅에 닿았을 때 계속해서 위치를 저장하고 싶다.
    // 플레이어 체크 포인트
    public GameObject player;
    public GameObject checkPoint;
    // 레이 전역변수
    private Ray ray;
    private RaycastHit hitInfo;

    void Update()
    {
        //플레이어 기준으로 밑에 레이를 생성한다.
        ray = new Ray(player.transform.position, -player.transform.up);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 부딪힌 물체가 땅이면 위치를 계속 기억한다, 땅에 닿았다면 포지션을 저장한다.
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && Physics.Raycast(ray, out hitInfo, LayerMask.NameToLayer("Ground")))
        {
            // 체크포인트의 위치는 플레이어의 위치이다.
            checkPoint.transform.position = player.transform.position;
        }
    }
}
