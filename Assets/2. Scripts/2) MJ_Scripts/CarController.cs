using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �� �������� ������ �ʹ�.
public class CarController : MonoBehaviour
{

    // ������ �ٵ� �ʿ�
    public Rigidbody rb;
    // ������ ������, �ڷ� ���� ��, �ִ� �ӷ� ���ư��� �ӷ�
    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed, turnStrength;

    private void Update()
    {
        //�÷��̾� ��ġ�� �ٸ� ��ü�� ��ġ�� ����� �ʹ�.
        transform.position = rb.transform.position;
    }
    private void FixedUpdate()
    {
        // �� �������� ���� �ְ�ʹ�.
        rb.AddForce(transform.forward * forwardAccel, ForceMode.Impulse);
    }
}
