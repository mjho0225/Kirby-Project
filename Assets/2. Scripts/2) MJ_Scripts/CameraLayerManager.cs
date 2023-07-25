using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 메인 카메라의 culling mask를 제어하고 싶다.
// 하위 카메라의 culling mask를 제어하고 싶다.
// 플레이어를 카메라 중앙에 위치시키고 싶다.
// 카메라의 뷰포트 값을 월드 위치 값으로 변경한다.
// 플레이어의 위치 값에 카메라의 위치 값으로 변경한다.
public class CameraLayerManager : MonoBehaviour
{
    // 카메라의 객체를 가져온다.
    public Camera backGroundCamera;
    public GameObject player;

    private int VFX_Car_Player_MaskNumber;

    void Awake()
    {
        VFX_Car_Player_MaskNumber = ((1 << LayerMask.NameToLayer("VFX") | (1 << LayerMask.NameToLayer("Car")) | (1 << LayerMask.NameToLayer("Player"))));
    }


    public void ChangedOffCameraLayer()
    {
        //메인 카메라 레이아웃을 다 키고싶다.
        Camera.main.cullingMask = -1;
        // 하위 카메라 레이아웃을 다 끄고 싶다.
        backGroundCamera.cullingMask = ~-1;

    }


    public void ChangingOnCameraLayer()
    {
        // 메인 카메라의 다 키고 vfx, player, car 3개만 끄고싶다
        Camera.main.cullingMask = ~VFX_Car_Player_MaskNumber;
        // 하위 카메라의 다 끄고 vfx, player, car 3개만 키고싶다.
        backGroundCamera.cullingMask = VFX_Car_Player_MaskNumber;

        //카메라의 중앙값
        Vector3 cameraViewCenterPosition = new Vector3(0.5f, 0.5f, 0);

        // 카메라의 뷰포트 값을 좌표 값으로 만든다..
        Vector3 cameraWorldPosition = Camera.main.ViewportToWorldPoint(cameraViewCenterPosition);
      
        // 플레이어 위치는 카메라 좌우의 중앙에 있고, 플레이어는 자기 자신의 y,z 축에 있다.
        player.transform.position = new Vector3(cameraWorldPosition.x, player.transform.position.y, player.transform.position.z);

        //플레이어의 앞 쪽 방향으로 만다.
        player.transform.rotation = Quaternion.LookRotation(transform.forward);
    }

}
