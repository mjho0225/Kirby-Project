using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using System;

// �÷��̾ ���� ��ġ���� �������� �ٽ� �¾�� �����. ( 10 ��ġ���� )
// �÷��̾ �������� �� ���������� �߱��Ѵ�.
public class DeathWall : MonoBehaviour
{
    public GameObject player;
    public GameObject checkPoint;
    public Material carMaterial;

    public float colorChangedTime;
    bool isMatChanged = false;

    // �÷��̾� �ִ� �������� �Ÿ�
    public float maxFallPosition = 10;

    void Start()
    {
        //�ʱ� �ڵ����� ������ ����̴�.
        carMaterial.color = Color.white;
    }

    void Update()
    {
        SaveCarPoint();
        ResetInitCarColor();
    }

    private void SaveCarPoint()
    {
        if (player.transform.position.y < maxFallPosition)
        {
            player.transform.position = checkPoint.transform.position;
            // ī�޶� �ѹ� ����ŷ �Ѵ�.
            StartCoroutine(IEOnDamage());
        }
    }

    private void ResetInitCarColor()
    {
        // �÷� ���� ���� �ð�
        colorChangedTime += Time.deltaTime;

        // �߱� �ð��� 0.2�ʰ� ������ �ٲ���ٸ�
        if (colorChangedTime >= 0.2f && isMatChanged == true)
        {
            // �������� �Ծ��ٸ� ���� �������� �ٲ۴�.
            carMaterial.color = Color.white;
            // ���� �������� �ٲ��� �ʾҴ�.
            isMatChanged = false;
        }
    }

    // ī�޶� ����ŷ�� 0.5�� �Ŀ� �����Ű�� �ʹ�.
    // 0.2�ʰ� ������ �߱��ϰ� �ʹ�.
    IEnumerator IEOnDamage()
    {
        PlayerHP.instance.HP -= 1;
        yield return new WaitForSeconds(0.18f);
        // 0.1�� �ڿ� �����Ű�� �ʹ�.
        CameraShaker.Instance.ShakeOnce(5f, 5f, 0.1f, 1.5f);
        colorChangedTime = 0;

        // �߱��� 3�� �ݺ��Ѵ�.
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);
            carMaterial.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            carMaterial.color = Color.white;
        }
        // ������ �ٲ����.
        isMatChanged = true;
    }
}
