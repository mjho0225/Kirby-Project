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

    // ������ �ٵ� �ʿ�
    public Rigidbody mainRigidbody;
    public Transform bodyTransform;
    // ������ ������, �ڷ� ���� ��, �ִ� �ӷ� ���ư��� �ӷ�, �߷��� ��
    public float forwardAccel = 8f, maxSpeed = 25f, normalMaxSpeed = 15f, gravity = 9.81f, jumpPower = 100f, dragOnGround = 3f;

    private float speedInput, hAxis, vAxis;
    private bool isGrounded, autoDashing;

    // �������� ����ʹ�.
    Vector3 carMoveVector;

    // ��ƼŬ�� �����ϰ� �ʹ�.
    public ParticleSystem[] NormalParticle;
    // ��ƼŬ �迭
    public float maxEmission = 25;
    // �ִ� ȿ��. ����
    private float emissionRate;
    private float rotationVelocity;
    public float rotationTime = 0.3f;


    private void Update()
    {
        InitInput();
        MoveCameraDir();
        Jump();
        MoveRotation();
        MoveDash();

        switch (carState)
        {
            case CarState.Move: MoveNormal(); break;
            case CarState.Dash: AutoDash(); break;
            default: break;
        }
    }

    private void MoveDash()
    {
        // ���࿡ ������ ���� ���Ͱ� zero �� �� ������ ���� ���⿡ ���� �ش�.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // �ڵ� ���� ����
            autoDashing = true;
            forwardAccel = 12f;
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
            // �뽬 ��ƼŬ�� �����Ѵ�
            NormalParticle[2].Play();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // �ڵ� ���� ����
            autoDashing = false;
            forwardAccel = 7f;
            // �Ϲ� ���·� �����Ѵ�.
            carState = CarState.Move;
            // �뽬 ��ƼŬ�� �������� �ʴ´�.
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
        // �巡�׸� �ּ�ȭ
        mainRigidbody.drag = 0.1f;
    }


    private void FixedUpdate()
    {
        EmissionNormalMoveParticle();
        FreezeRotation();
    }

    private void AutoDash()
    {
        // ���� �뽬�� �߰�, �ִ� ���� �ӵ� ũ�� ���� ���� �� ���� �ش�.
        if (autoDashing && mainRigidbody.velocity.magnitude < maxSpeed)
        {
            mainRigidbody.AddForce(bodyTransform.forward * forwardAccel * 500f);
        }
    }

    private void FreezeRotation()
    {
        //�÷��̾��� angluar ������ Zero ������
        mainRigidbody.angularVelocity = Vector3.zero;
    }

    private void MoveNormal()
    {
        // ó�� ������ �ʱ�ȭ �Ѵ�.
        emissionRate = 0;
        // �ӷ��� ���� ���� 0���� ũ�� �ӷ� ��ŭ ������ ���� �ְ� �ʹ�. (�Ϲ� �ӵ��� �����ϰ� �ʹ�)
        if (Mathf.Abs(speedInput) > 0 && !autoDashing && mainRigidbody.velocity.magnitude < normalMaxSpeed)
        {
            // �� �������� ���� �ְ�ʹ�.
            mainRigidbody.AddForce(carMoveVector.normalized * speedInput);
            // �ִ� ������ �����Ѵ�.
            emissionRate = maxEmission;
            // ���� ����
            mainRigidbody.drag = dragOnGround;
        }
        // ���� �뽬�� �ߴٸ� �뽬���·� �����Ѵ�.
        else if (autoDashing)
        {
            carState = CarState.Dash;
        }

    }

    private void EmissionNormalMoveParticle()
    {

        // ��ƼŬ�� ���� �����Ͽ�
        foreach (ParticleSystem part in NormalParticle)
        {
            // ������ �����ϰ�
            var emissionModule = part.emission;
            // ����� �ð��� �����Ѵ�.
            emissionModule.rateOverTime = emissionRate;
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

    private void InitInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    private void MoveCameraDir()
    {
        // ���� ������ �� ��������, ��, �� ����Ű�� �¿�� ����.
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
