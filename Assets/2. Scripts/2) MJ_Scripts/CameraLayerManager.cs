using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 메인 카메라의 culling mask를 제어하고 싶다.
// 하위 카메라의 culling mask를 제어하고 싶다.
public class CameraLayerManager : MonoBehaviour
{
    // 카메라의 객체를 가져온다.
    public Camera backGroundCamera;

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

    }

}
