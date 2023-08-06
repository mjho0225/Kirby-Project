using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

// ���� �� �������� ������ �ʹ�.
// ����Ű �������� �� �ڷ� �����̰� �ʹ�.
// ����ڰ� ���� �������� ������ �� ������ �ʹ�.
// �÷��̾ ������ �������� �̵���Ű�� �ʹ�.
// ������ �����ϰ� �ʹ�.
// shiftŰ�� ������ �ӷ��� ���̰� �ʹ�.
// ī�޶� ����ŷ�ϰ� �ʹ�.
// �÷��̾ shift�� ������ ������ �̵��ϰ� ����� �ʹ�.
// �浹�� �÷��̾��� ȸ���� ���߰� �ʹ�.
// �ڵ����� �뽬�� �ϰ� ����� �ʹ�.
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
    // ������ ������, �ڷ� ���� ��, �ִ� �ӷ� ���ư��� �ӷ�, �߷��� ��
    public float forwardAccel = 15f, jumpPower = 1000f, dashMaxSpeed = 25f, normalMaxSpeed = 15f, gravity = 9.81f, dragOnGround = 3f;

    private float speedInput, hAxis, vAxis;
    private bool isGrounded, autoDashing;
    private bool jumpButtonDown, dashKeyDown, dashKeyUp;

    // ������ �ٵ� �ʿ�
    public Rigidbody mainRigidbody;
    public Transform bodyTransform;
    // ��ƼŬ�� �����ϰ� �ʹ�.
    public ParticleSystem[] NormalParticle;
    // �������� ����ʹ�.
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

        // ����Ű�� �������ٸ� 1�� ����Ѵ�.
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
        // ���࿡ ������ ���� ���Ͱ� zero �� �� ������ ���� ���⿡ ���� �ش�.
        if (dashKeyDown)
        {
            // �ڵ� ���� ����
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
            // �ڵ� ���� ����
            autoDashing = false;
            // �Ϲ� ���·� �����Ѵ�.
            carState = CarState.Move;
            // �뽬 ��ƼŬ�� �������� �ʴ´�.
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
        // �巡�׸� �ּ�ȭ
        mainRigidbody.drag = 0.1f;
    }


    private void FixedUpdate()
    {
        FreezeRotation();
    }

    private void AutoDash()
    {
        // ���� �뽬�� �߰�, �ִ� ���� �ӵ� ũ�� ���� ���� �� ���� �ش�.
        if (autoDashing && mainRigidbody.velocity.magnitude < dashMaxSpeed)
        {
            mainRigidbody.AddForce(bodyTransform.forward * forwardAccel * 40000f * Time.deltaTime);
        }
    }

    private void FreezeRotation()
    {
        //�÷��̾��� angluar ������ Zero ������
        mainRigidbody.angularVelocity = Vector3.zero;
    }

    private void MoveNormal()
    {
        // �ӷ��� ���� ���� 0���� ũ�� �ӷ� ��ŭ ������ ���� �ְ� �ʹ�. (�Ϲ� �ӵ��� �����ϰ� �ʹ�)
        if (Mathf.Abs(speedInput) > 0 && !autoDashing && mainRigidbody.velocity.magnitude < normalMaxSpeed)
        {
            // �� �������� ���� �ְ�ʹ�.
            mainRigidbody.AddForce(carMoveVector.normalized * speedInput * Time.deltaTime * 100);
            mainRigidbody.drag = dragOnGround;

        }
        // ���� �뽬�� �ߴٸ� �뽬���·� �����Ѵ�.
        else if (autoDashing)
        {
            carState = CarState.Dash;
        }
    }


    private void MoveRotation()
    {
        speedInput = 0;
        // ũ�Ⱑ 0���� ũ�ų� // auto dashing �߿� ���� ��
        if (carMoveVector.magnitude > 0)
        {

            // ���� �̵��Ϸ��� ����� ���� ��ü�� ���� �� x,z Atan2�� �̿��Ͽ� ���� ���ϰ� ���� �����.
            float targetAngle = Mathf.Atan2(carMoveVector.x, carMoveVector.z) * Mathf.Rad2Deg;

            // �ε巯�� ���� ȸ���� �̿��Ѵ�.���� y�࿡�� �� ��ŭ�� �ӵ��� Ÿ������ ���ư����� �����Ѵ�.
            float smoothAngle = Mathf.SmoothDampAngle(bodyTransform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationTime);

            // �׸�ŭ ������ ȸ���Ѵ�.
            bodyTransform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            // �ӷ� ���� �����.
            speedInput = forwardAccel * 700f;
        }


        //�÷��̾� ��ġ�� �ٸ� ��ü�� ��ġ�� ����� �ʹ�.
        transform.position = mainRigidbody.transform.position;

    }



    private void MoveCameraDir()
    {
        Vector3 dir = Vector3.right * hAxis + Vector3.forward * vAxis;
        // �÷��̾��� ������ ������ ���� �������� �Ѵ�.
        dir = Camera.main.transform.TransformDirection(dir);
        // y���� �������� �ʴ´�.
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
