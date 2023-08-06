using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPivotCheckManager : MonoBehaviour
{
    public GameObject playerPivot;
    public GameObject playerPivotCheckPoint;

    private Ray ray;
    // 물리기반은 FixedUpdate에서 사용한다.(update에서 사용하면 프레임 드랍이 나타남)
    // 업데이트 문에다가 레이를 쏴서 만야

    public void UpdatePoint()
    {
        ray = new Ray(playerPivot.transform.position, -playerPivot.transform.up);
        int groundLayer = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit hitInfo;
        // 밑에 땅에 닿으면 현재 위치를 충돌한 위치로 지정한다.
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, groundLayer))
        {
            //현재 채크포인트 위치를 부딪힌 지점의 위치에 놓고, 위에서 살짝 태어난다.
            playerPivotCheckPoint.transform.position = hitInfo.point + Vector3.up;
        }

    }
}
