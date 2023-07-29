using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using System;

// 플레이어가 일정 위치에서 떨어지면 다시 태어나게 만든다. ( 10 위치에서 )
// 플레이어가 떨어졌을 때 빨간색으로 발광한다.
public class DeathWall : MonoBehaviour
{
    public GameObject player;
    public GameObject checkPoint;
    public Material carMaterial;

    public float colorChangedTime;
    bool isMatChanged = false;

    // 플레이어 최대 떨어지는 거리
    public float maxFallPosition = 10;

    void Start()
    {
        //초기 자동차의 색상은 흰색이다.
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
            // 카메라를 한번 쉐이킹 한다.
            StartCoroutine(IEOnDamage());
        }
    }

    private void ResetInitCarColor()
    {
        // 컬러 변경 색상 시간
        colorChangedTime += Time.deltaTime;

        // 발광 시간이 0.2초가 지나고 바뀌었다면
        if (colorChangedTime >= 0.2f && isMatChanged == true)
        {
            // 데미지를 입었다면 원래 색상으로 바꾼다.
            carMaterial.color = Color.white;
            // 레드 색상으로 바뀌지 않았다.
            isMatChanged = false;
        }
    }

    // 카메라 쉐이킹을 0.5초 후에 실행시키고 싶다.
    // 0.2초간 빠르게 발광하고 싶다.
    IEnumerator IEOnDamage()
    {
        PlayerHP.instance.HP -= 1;
        yield return new WaitForSeconds(0.18f);
        // 0.1초 뒤에 실행시키고 싶다.
        CameraShaker.Instance.ShakeOnce(5f, 5f, 0.1f, 1.5f);
        colorChangedTime = 0;

        // 발광을 3번 반복한다.
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);
            carMaterial.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            carMaterial.color = Color.white;
        }
        // 색상이 바뀌었다.
        isMatChanged = true;
    }
}
