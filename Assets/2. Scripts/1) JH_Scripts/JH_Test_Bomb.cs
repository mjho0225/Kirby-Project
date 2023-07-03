using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Test_Bomb : MonoBehaviour
{

    public float speed = 2000f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed * Time.deltaTime;
        //rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = rb.velocity.normalized;
    }



    private void OnCollisionEnter(Collision collision)
    {
        // ���� �ε��� ��ü�� Rigidbody�� �ִٸ�
        var otherRB = collision.gameObject.GetComponent<Rigidbody>();
        // �� �չ������� ���� 10 ���ϰ�ʹ�.
        if (otherRB != null)
        {
            otherRB.AddForce(transform.forward * otherRB.mass * 5, ForceMode.Impulse);
        }
        Destroy(this.gameObject);
    }
}
