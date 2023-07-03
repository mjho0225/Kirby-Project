using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Test_Bullet : MonoBehaviour
{

    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {

        var otherRB = collision.gameObject.GetComponent<Rigidbody>();
        // �� �չ������� ���� 10 ���ϰ��ʹ�.
        if (otherRB != null)
        {
            otherRB.AddForce(transform.forward * otherRB.mass * 5, ForceMode.Impulse);
        }
        Destroy(this.gameObject);
    }
}