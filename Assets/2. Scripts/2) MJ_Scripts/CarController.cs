using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 차를 앞 방향으로 날리고 싶다.
public class CarController : MonoBehaviour
{

    // 리지드 바디가 필요
    public Rigidbody rb;
    // 앞으로 가는힘, 뒤로 가는 힘, 최대 속력 돌아가는 속력
    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed, turnStrength;

    private void Update()
    {
        //플레이어 위치를 다른 물체의 위치로 만들고 싶다.
        transform.position = rb.transform.position;
    }
    private void FixedUpdate()
    {
        // 앞 방향으로 힘을 주고싶다.
        rb.AddForce(transform.forward * forwardAccel, ForceMode.Impulse);
    }
}
