using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 차를 앞 방향으로 날리고 싶다.
// 방향키 방향으로 앞 뒤로 움직이고 싶다.
// 사용자가 왼쪽 오른쪽을 눌렀을 때 돌리고 싶다.
// 플레이어가 벡터의 방향으로 이동시키고 싶다.
public class CarController : MonoBehaviour
{

    // 리지드 바디가 필요
    public Rigidbody rb;
    // 앞으로 가는힘, 뒤로 가는 힘, 최대 속력 돌아가는 속력
    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180;

    // 속력, 돌아가는 입력값
    private float speedInput, turnInput;

    // 방향값
    private float hAxis, vAxis;

    private void Update()
    {
        InitInput();

        speedInput = 0;


        // 0 보다 크면 앞으로 간다.
        if (vAxis > 0)
        {
            // 속력 값을 만든다.
            speedInput = vAxis * forwardAccel * 1000f;
        }
        else if (vAxis < 0)
        {
            // 0 보다 작으면 뒤로 간다.
            speedInput = vAxis * reverseAccel * 1000f;

        }

        // 방향 값을 넣는다.
        turnInput = hAxis;

        // 회전각을 오릴러 각과 방향 값을 조정한다.
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime, 0f));

        //플레이어 위치를 다른 물체의 위치로 만들고 싶다.
        transform.position = rb.transform.position;
    }

    private void InitInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // 속력의 절댓 값이 0보다 크면 속력 만큼 앞으로 힘을 주고 싶다.
        if (Mathf.Abs(speedInput) > 0)
        {
            // 앞 방향으로 힘을 주고싶다.
            rb.AddForce(transform.forward * speedInput);
        }
    }

   
}
