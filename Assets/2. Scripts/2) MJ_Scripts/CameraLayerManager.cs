using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//메인 카메라의 culling Mask를 vfx, car, player 빼고 다 실행시키고 싶다.
// 백그라운드 카메라 vfx, car, player를 키고 나머지를 끄고싶다.
public class CameraLayerManager : MonoBehaviour
{
    // 카메라의 객체를 가져온다.
    public Camera backGroundCamera;

    private int VFX_Car_Player_MaskNumber;

    void Start()
    {
        VFX_Car_Player_MaskNumber = ((1 << LayerMask.NameToLayer("VFX") | (1 << LayerMask.NameToLayer("Car")) | (1 << LayerMask.NameToLayer("Player"))));
    }
    public void OnMainCamera_VFX_Car_Player_Layer()
    {
        // 레이어마스크 이름을 기준으로 제외하고 모든 것을 키고싶다.
        Camera.main.cullingMask = ~VFX_Car_Player_MaskNumber;
    }

    public void OnBackgroundCamera_VFX_Car_Player_Layer()
    {
        // 레이어마스크 이름을 선택한 것을 모두 키고싶다.
        backGroundCamera.cullingMask = VFX_Car_Player_MaskNumber;
    }
}
