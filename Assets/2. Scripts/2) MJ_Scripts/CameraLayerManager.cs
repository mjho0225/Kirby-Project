using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ī�޶��� culling mask�� �����ϰ� �ʹ�.
// ���� ī�޶��� culling mask�� �����ϰ� �ʹ�.
public class CameraLayerManager : MonoBehaviour
{
    // ī�޶��� ��ü�� �����´�.
    public Camera backGroundCamera;

    private int VFX_Car_Player_MaskNumber;

    void Awake()
    {
        VFX_Car_Player_MaskNumber = ((1 << LayerMask.NameToLayer("VFX") | (1 << LayerMask.NameToLayer("Car")) | (1 << LayerMask.NameToLayer("Player"))));
    }


    public void ChangedOffCameraLayer()
    {
        //���� ī�޶� ���̾ƿ��� �� Ű��ʹ�.
        Camera.main.cullingMask = -1;
        // ���� ī�޶� ���̾ƿ��� �� ���� �ʹ�.
        backGroundCamera.cullingMask = ~-1;

    }


    public void ChangingOnCameraLayer()
    {
        // ���� ī�޶��� �� Ű�� vfx, player, car 3���� ����ʹ�
        Camera.main.cullingMask = ~VFX_Car_Player_MaskNumber;
        // ���� ī�޶��� �� ���� vfx, player, car 3���� Ű��ʹ�.
        backGroundCamera.cullingMask = VFX_Car_Player_MaskNumber;

    }

}
