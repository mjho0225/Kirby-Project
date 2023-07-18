using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ī�޶��� culling Mask�� vfx, car, player ���� �� �����Ű�� �ʹ�.
// ��׶��� ī�޶� vfx, car, player�� Ű�� �������� ����ʹ�.
public class CameraLayerManager : MonoBehaviour
{
    // ī�޶��� ��ü�� �����´�.
    public Camera backGroundCamera;

    private int VFX_Car_Player_MaskNumber;

    void Start()
    {
        VFX_Car_Player_MaskNumber = ((1 << LayerMask.NameToLayer("VFX") | (1 << LayerMask.NameToLayer("Car")) | (1 << LayerMask.NameToLayer("Player"))));
    }
    public void OnMainCamera_VFX_Car_Player_Layer()
    {
        // ���̾��ũ �̸��� �������� �����ϰ� ��� ���� Ű��ʹ�.
        Camera.main.cullingMask = ~VFX_Car_Player_MaskNumber;
    }

    public void OnBackgroundCamera_VFX_Car_Player_Layer()
    {
        // ���̾��ũ �̸��� ������ ���� ��� Ű��ʹ�.
        backGroundCamera.cullingMask = VFX_Car_Player_MaskNumber;
    }
}
