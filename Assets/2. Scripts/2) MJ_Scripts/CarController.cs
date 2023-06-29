using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �� �������� ������ �ʹ�.
// ����Ű �������� �� �ڷ� �����̰� �ʹ�.
// ����ڰ� ���� �������� ������ �� ������ �ʹ�.
// �÷��̾ ������ �������� �̵���Ű�� �ʹ�.
public class CarController : MonoBehaviour
{

    // ������ �ٵ� �ʿ�
    public Rigidbody rb;
    // ������ ������, �ڷ� ���� ��, �ִ� �ӷ� ���ư��� �ӷ�
    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180;

    // �ӷ�, ���ư��� �Է°�
    private float speedInput, turnInput;

    // ���Ⱚ
    private float hAxis, vAxis;

    private void Update()
    {
        InitInput();

        speedInput = 0;


        // 0 ���� ũ�� ������ ����.
        if (vAxis > 0)
        {
            // �ӷ� ���� �����.
            speedInput = vAxis * forwardAccel * 1000f;
        }
        else if (vAxis < 0)
        {
            // 0 ���� ������ �ڷ� ����.
            speedInput = vAxis * reverseAccel * 1000f;

        }

        // ���� ���� �ִ´�.
        turnInput = hAxis;

        // ȸ������ ������ ���� ���� ���� �����Ѵ�.
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime, 0f));

        //�÷��̾� ��ġ�� �ٸ� ��ü�� ��ġ�� ����� �ʹ�.
        transform.position = rb.transform.position;
    }

    private void InitInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // �ӷ��� ���� ���� 0���� ũ�� �ӷ� ��ŭ ������ ���� �ְ� �ʹ�.
        if (Mathf.Abs(speedInput) > 0)
        {
            // �� �������� ���� �ְ�ʹ�.
            rb.AddForce(transform.forward * speedInput);
        }
    }

   
}
