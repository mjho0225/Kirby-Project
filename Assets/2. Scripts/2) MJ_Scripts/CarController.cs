using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

// 차를 앞 방향으로 날리고 싶다.
// 방향키 방향으로 앞 뒤로 움직이고 싶다.
// 사용자가 왼쪽 오른쪽을 눌렀을 때 돌리고 싶다.
// 플레이어가 벡터의 방향으로 이동시키고 싶다.
// 점프를 구현하고 싶다.
// shift키를 누르면 속력을 높이고 싶다.
// 카메라를 쉐이킹하고 싶다.
// 플레이어가 shift를 누르면 앞으로 이동하게 만들고 싶다.
// 충돌시 플레이어의 회전을 멈추고 싶다.
// 자동으로 대쉬를 하게 만들고 싶다.
public class CarController : MonoBehaviour
{
    // 리지드 바디가 필요
    public Rigidbody mainRigidbody;
    public Transform bodyTransform;
    // 앞으로 가는힘, 뒤로 가는 힘, 최대 속력 돌아가는 속력, 중력의 힘
    public float forwardAccel = 8f, maxSpeed = 25f, gravity = 9.81f, jumpPower = 100f, dragOnGround = 3f;

    private float speedInput, hAxis, vAxis;
    private bool isGrounded, autoDashing;

    // 방향으로 가고싶다.
    Vector3 carMoveVector;

    // 파티클을 생성하고 싶다.
    public ParticleSystem[] NormalParticle;
    // 파티클 배열
    public float maxEmission = 25;
    // 최대 효과. 비율
    private float emissionRate;
    private float rotationVelocity;
    public float rotationTime = 0.3f;

    private void Update()
    {
        InitInput();
        Jump();
        MoveRotation();
        MoveDash();

    }

    private void MoveDash()
    {
        // 만약에 앞으로 가는 벡터가 zero 일 때 앞으로 가는 방향에 힘을 준다.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // 자동 오토 실행
            autoDashing = true;
            forwardAccel = 9f;
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
            // 대쉬 파티클을 실행한다
            NormalParticle[2].Play();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // 자동 오토 정지
            autoDashing = false;
            forwardAccel = 7f;
            // 대쉬 파티클을 실행하지 않는다.
            NormalParticle[2].Stop();
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            mainRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
        }
        mainRigidbody.AddForce(Vector3.up * -gravity * 25);
        // 드래그를 최소화
        mainRigidbody.drag = 0.1f;
    }


    private void FixedUpdate()
    {
        MoveNormal();
        EmissionNormalMoveParticle();
        FreezeRotation();
        AutoDash();
    }

    private void AutoDash()
    {
        // 만약 대쉬를 했고, 최대 제한 속도 크기 보다 작을 때 힘을 준다.
        if (autoDashing && mainRigidbody.velocity.magnitude < maxSpeed)
        {
            mainRigidbody.AddForce(bodyTransform.forward * forwardAccel * 1000f);
        }
    }

    private void FreezeRotation()
    {
        //플레이어의 angluar 방향을 Zero 만들자
        mainRigidbody.angularVelocity = Vector3.zero;
    }

    private void MoveNormal()
    {
        // 처음 비율을 초기화 한다.
        emissionRate = 0;
        // 속력의 절댓 값이 0보다 크면 속력 만큼 앞으로 힘을 주고 싶다.
        if (Mathf.Abs(speedInput) > 0 && !autoDashing)
        {
            // 앞 방향으로 힘을 주고싶다.
            mainRigidbody.AddForce(carMoveVector * speedInput);
            // 최대 배출을 생성한다.
            emissionRate = maxEmission;
            // 마찰 지정
            mainRigidbody.drag = dragOnGround;
        }
    }

    private void EmissionNormalMoveParticle()
    {

        // 파티클에 각각 접근하여
        foreach (ParticleSystem part in NormalParticle)
        {
            // 배출을 지정하고
            var emissionModule = part.emission;
            // 모듈을 시간을 생성한다.
            emissionModule.rateOverTime = emissionRate;
        }
    }

    private void MoveRotation()
    {
        speedInput = 0;
        // 크기가 0보다 크거나 // auto dashing 중에 했을 때
        print(carMoveVector.magnitude);
        if (carMoveVector.magnitude > 0)
        {

            // 내가 이동하려는 방향과 현재 물체의 방향 각 x,z Atan2를 이용하여 각을 구하고 도로 만든다.
            float targetAngle = Mathf.Atan2(carMoveVector.x, carMoveVector.z) * Mathf.Rad2Deg;

            // 부드러운 각도 회전을 이용한다.나의 y축에서 얼마 만큼의 속도로 타겟으로 돌아가는지 조정한다.
            float smoothAngle = Mathf.SmoothDampAngle(bodyTransform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationTime);

            // 그만큼 각도를 회전한다.
            bodyTransform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            // 속력 값을 만든다.
            speedInput = forwardAccel * 1000f;
        }


        //플레이어 위치를 다른 물체의 위치로 만들고 싶다.
        transform.position = mainRigidbody.transform.position;

    }

    private void InitInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        // 왼쪽 방향일 때 왼쪽으로, 앞, 뒤 방향키는 좌우로 간다.
        carMoveVector = Vector3.left * hAxis + Vector3.back * vAxis;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

}
