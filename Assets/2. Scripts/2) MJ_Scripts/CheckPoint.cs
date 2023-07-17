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

    // 물리기반은 FixedUpdate에서 사용한다.(update에서 사용하면 프레임 드랍이 나타남)
    void FixedUpdate()
    {
        //플레이어 기준으로 밑에 레이저를 발생한다.
        ray = new Ray(player.transform.position, -player.transform.up);
        RaycastHit hitInfo;
        // 땅 레이아웃만 걸러서 받는다.
        int groundLayer = 1 << LayerMask.NameToLayer("Ground");
        // 밑에 땅에 닿으면 현재 위치를 충돌한 위치로 지정한다.
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, groundLayer))
        {
            //현재 채크포인트 위치를 부딪힌 지점의 위치에 놓고, 위에서 살짝 태어난다.
            checkPoint.transform.position = hitInfo.point + Vector3.up;
        }
    }
}
