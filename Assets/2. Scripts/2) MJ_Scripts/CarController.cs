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
    public static CarController instance;
    private void Awake()
    {
        instance = this;
    }

    public enum CarState
    {
        Move,
        Dash,
    }
    public CarState carState;
    // 앞으로 가는힘, 뒤로 가는 힘, 최대 속력 돌아가는 속력, 중력의 힘
    public float forwardAccel = 15f, jumpPower = 1000f, dashMaxSpeed = 25f, normalMaxSpeed = 15f, gravity = 9.81f, dragOnGround = 3f;

    private float speedInput, hAxis, vAxis;
    private bool isGrounded, autoDashing;
    private bool jumpButtonDown, dashKeyDown, dashKeyUp;

    // 리지드 바디가 필요
    public Rigidbody mainRigidbody;
    public Transform bodyTransform;
    // 파티클을 생성하고 싶다.
    public ParticleSystem[] NormalParticle;
    // 방향으로 가고싶다.
    Vector3 carMoveVector;

    private float rotationVelocity;
    public float rotationTime = 0.3f;

    private readonly string JUMP_NAME = "Jump";
    private readonly string HORIZONTAL_AXIS_NAME = "Horizontal", VERTICAL_AXIS_NAME = "Vertical";

    void Start()
    {
        AudioManager.instance.PlaySound("Idle");
    }

    private void Update()
    {
        InitInput();
        MoveCameraDir();
        Jump();
        MoveRotation();
        MoveDash();

        // 방향키를 움직였다면 1을 재생한다.
        switch (carState)
        {
            case CarState.Move: MoveNormal(); break;
            case CarState.Dash: AutoDash(); break;
            default: break;
        }
    }
    private void InitInput()
    {
        hAxis = Input.GetAxis(HORIZONTAL_AXIS_NAME);
        vAxis = Input.GetAxis(VERTICAL_AXIS_NAME);
        dashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
        dashKeyUp = Input.GetKeyUp(KeyCode.LeftShift);
        jumpButtonDown = Input.GetButtonDown(JUMP_NAME);
    }

    private void MoveDash()
    {
        // 만약에 앞으로 가는 벡터가 zero 일 때 앞으로 가는 방향에 힘을 준다.
        if (dashKeyDown)
        {
            // 자동 오토 실행
            autoDashing = true;
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
            AudioManager.instance.PlayOneShotSound("Booster");
            foreach (ParticleSystem ps in NormalParticle)
            {
                ps.Play();
            }

        }
        if (dashKeyUp)
        {
            // 자동 오토 정지
            autoDashing = false;
            // 일반 상태로 변경한다.
            carState = CarState.Move;
            // 대쉬 파티클을 실행하지 않는다.
            foreach (ParticleSystem ps in NormalParticle)
            {
                ps.Stop();
            }
        }
    }


    private void Jump()
    {
        if (jumpButtonDown && isGrounded)
        {
            mainRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            AudioManager.instance.PlayOneShotSound(JUMP_NAME);
            isGrounded = false;
        }
        mainRigidbody.AddForce(Vector3.up * -gravity * Time.deltaTime * 2500);
        // 드래그를 최소화
        mainRigidbody.drag = 0.1f;
    }


    private void FixedUpdate()
    {
        FreezeRotation();
    }

    private void AutoDash()
    {
        // 만약 대쉬를 했고, 최대 제한 속도 크기 보다 작을 때 힘을 준다.
        if (autoDashing && mainRigidbody.velocity.magnitude < dashMaxSpeed)
        {
            mainRigidbody.AddForce(bodyTransform.forward * forwardAccel * 40000f * Time.deltaTime);
        }
    }

    private void FreezeRotation()
    {
        //플레이어의 angluar 방향을 Zero 만들자
        mainRigidbody.angularVelocity = Vector3.zero;
    }

    private void MoveNormal()
    {
        // 속력의 절댓 값이 0보다 크면 속력 만큼 앞으로 힘을 주고 싶다. (일반 속도를 제어하고 싶다)
        if (Mathf.Abs(speedInput) > 0 && !autoDashing && mainRigidbody.velocity.magnitude < normalMaxSpeed)
        {
            // 앞 방향으로 힘을 주고싶다.
            mainRigidbody.AddForce(carMoveVector.normalized * speedInput * Time.deltaTime * 100);
            mainRigidbody.drag = dragOnGround;

        }
        // 만약 대쉬를 했다면 대쉬상태로 전이한다.
        else if (autoDashing)
        {
            carState = CarState.Dash;
        }
    }


    private void MoveRotation()
    {
        speedInput = 0;
        // 크기가 0보다 크거나 // auto dashing 중에 했을 때
        if (carMoveVector.magnitude > 0)
        {

            // 내가 이동하려는 방향과 현재 물체의 방향 각 x,z Atan2를 이용하여 각을 구하고 도로 만든다.
            float targetAngle = Mathf.Atan2(carMoveVector.x, carMoveVector.z) * Mathf.Rad2Deg;

            // 부드러운 각도 회전을 이용한다.나의 y축에서 얼마 만큼의 속도로 타겟으로 돌아가는지 조정한다.
            float smoothAngle = Mathf.SmoothDampAngle(bodyTransform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationTime);

            // 그만큼 각도를 회전한다.
            bodyTransform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            // 속력 값을 만든다.
            speedInput = forwardAccel * 700f;
        }


        //플레이어 위치를 다른 물체의 위치로 만들고 싶다.
        transform.position = mainRigidbody.transform.position;

    }



    private void MoveCameraDir()
    {
        Vector3 dir = Vector3.right * hAxis + Vector3.forward * vAxis;
        // 플레이어의 움직임 방향을 월드 기준으로 한다.
        dir = Camera.main.transform.TransformDirection(dir);
        // y축은 포함하지 않는다.
        dir.y = 0;
        dir.Normalize();
        carMoveVector = dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

}
