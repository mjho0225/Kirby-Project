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
    // ������ �ٵ� �ʿ�
    public Rigidbody mainRigidbody;
    public Transform bodyTransform;
    // ������ ������, �ڷ� ���� ��, �ִ� �ӷ� ���ư��� �ӷ�, �߷��� ��
    public float forwardAccel = 8f, maxSpeed = 25f, gravity = 9.81f, jumpPower = 100f, dragOnGround = 3f;

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
        Jump();
        MoveRotation();
        MoveDash();

    }

    private void MoveDash()
    {
        // ���࿡ ������ ���� ���Ͱ� zero �� �� ������ ���� ���⿡ ���� �ش�.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // �ڵ� ���� ����
            autoDashing = true;
            forwardAccel = 9f;
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
            // �뽬 ��ƼŬ�� �����Ѵ�
            NormalParticle[2].Play();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // �ڵ� ���� ����
            autoDashing = false;
            forwardAccel = 7f;
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
        MoveNormal();
        EmissionNormalMoveParticle();
        FreezeRotation();
        AutoDash();
    }

    private void AutoDash()
    {
        // ���� �뽬�� �߰�, �ִ� ���� �ӵ� ũ�� ���� ���� �� ���� �ش�.
        if (autoDashing && mainRigidbody.velocity.magnitude < maxSpeed)
        {
            mainRigidbody.AddForce(bodyTransform.forward * forwardAccel * 1000f);
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
        // �ӷ��� ���� ���� 0���� ũ�� �ӷ� ��ŭ ������ ���� �ְ� �ʹ�.
        if (Mathf.Abs(speedInput) > 0 && !autoDashing)
        {
            // �� �������� ���� �ְ�ʹ�.
            mainRigidbody.AddForce(carMoveVector * speedInput);
            // �ִ� ������ �����Ѵ�.
            emissionRate = maxEmission;
            // ���� ����
            mainRigidbody.drag = dragOnGround;
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
        print(carMoveVector.magnitude);
        if (carMoveVector.magnitude > 0)
        {

            // ���� �̵��Ϸ��� ����� ���� ��ü�� ���� �� x,z Atan2�� �̿��Ͽ� ���� ���ϰ� ���� �����.
            float targetAngle = Mathf.Atan2(carMoveVector.x, carMoveVector.z) * Mathf.Rad2Deg;

            // �ε巯�� ���� ȸ���� �̿��Ѵ�.���� y�࿡�� �� ��ŭ�� �ӵ��� Ÿ������ ���ư����� �����Ѵ�.
            float smoothAngle = Mathf.SmoothDampAngle(bodyTransform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationTime);

            // �׸�ŭ ������ ȸ���Ѵ�.
            bodyTransform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            // �ӷ� ���� �����.
            speedInput = forwardAccel * 1000f;
        }


        //�÷��̾� ��ġ�� �ٸ� ��ü�� ��ġ�� ����� �ʹ�.
        transform.position = mainRigidbody.transform.position;

    }

    private void InitInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        // ���� ������ �� ��������, ��, �� ����Ű�� �¿�� ����.
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
