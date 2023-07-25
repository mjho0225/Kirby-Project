using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ī�޶��� culling mask�� �����ϰ� �ʹ�.
// ���� ī�޶��� culling mask�� �����ϰ� �ʹ�.
// �÷��̾ ī�޶� �߾ӿ� ��ġ��Ű�� �ʹ�.
// ī�޶��� ����Ʈ ���� ���� ��ġ ������ �����Ѵ�.
// �÷��̾��� ��ġ ���� ī�޶��� ��ġ ������ �����Ѵ�.
public class CameraLayerManager : MonoBehaviour
{
    // ī�޶��� ��ü�� �����´�.
    public Camera backGroundCamera;
    public GameObject player;

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

        //ī�޶��� �߾Ӱ�
        Vector3 cameraViewCenterPosition = new Vector3(0.5f, 0.5f, 0);

        // ī�޶��� ����Ʈ ���� ��ǥ ������ �����..
        Vector3 cameraWorldPosition = Camera.main.ViewportToWorldPoint(cameraViewCenterPosition);
      
        // �÷��̾� ��ġ�� ī�޶� �¿��� �߾ӿ� �ְ�, �÷��̾�� �ڱ� �ڽ��� y,z �࿡ �ִ�.
        player.transform.position = new Vector3(cameraWorldPosition.x, player.transform.position.y, player.transform.position.z);

        //�÷��̾��� �� �� �������� ����.
        player.transform.rotation = Quaternion.LookRotation(transform.forward);
    }

}
