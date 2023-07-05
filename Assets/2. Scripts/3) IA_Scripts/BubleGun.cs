using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubleGun : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        //print("ff" + collision.gameObject);
        if (collision.gameObject.tag == "Ground")
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Acceleration);
        }
    }

}
