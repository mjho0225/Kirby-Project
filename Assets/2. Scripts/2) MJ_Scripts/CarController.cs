using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �� �������� ������ �ʹ�.
// ����Ű �������� �� �ڷ� �����̰� �ʹ�.
// ����ڰ� ���� �������� ������ �� ������ �ʹ�.
// �÷��̾ ������ �������� �̵���Ű�� �ʹ�.
// ������ �����ϰ� �ʹ�.
public class CarController : MonoBehaviour
{

    // ������ �ٵ� �ʿ�
    public Rigidbody mainRigidbody;
    public Transform bodyTransform;
    // ������ ������, �ڷ� ���� ��, �ִ� �ӷ� ���ư��� �ӷ�, �߷��� ��
    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180, gravity = 9.81f, jumpPower = 100f, dragOnGround = 3f;

    private float speedInput;
    private float hAxis, vAxis;
    private bool isGrounded;

    // �������� ����ʹ�.
    Vector3 carMoveVector;

    private void Update()
    {
        InitInput();
        MoveNormal();
        Jump();
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
        // �ӷ��� ���� ���� 0���� ũ�� �ӷ� ��ŭ ������ ���� �ְ� �ʹ�.
        if (Mathf.Abs(speedInput) > 0)
        {
            // �� �������� ���� �ְ�ʹ�.
            mainRigidbody.AddForce(carMoveVector * speedInput);
            // ���� ����
            mainRigidbody.drag = dragOnGround;
        }


    }

    private void MoveNormal()
    {
        speedInput = 0;
        // 0 ���� ũ�� ������ ����.
        if (carMoveVector.magnitude > 0)
        {

            // ���� �̵��Ϸ��� ����� ���� ��ü�� ���� ���� ���Ѵ�.
            float angle = Vector3.Angle(carMoveVector, bodyTransform.right);
            if (angle < 90)
            {
                // 90�� ���� �۴ٸ� y������ ������ ȸ���Ѵ�.
                bodyTransform.Rotate(0, 250 * Time.deltaTime, 0);
            }
            else
            {
                // 90���� ũ�� y������ ���� ȸ���Ѵ�.
                bodyTransform.Rotate(0, -250 * Time.deltaTime, 0);
            }
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
        carMoveVector = Vector3.left * vAxis + Vector3.forward * hAxis;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

}
