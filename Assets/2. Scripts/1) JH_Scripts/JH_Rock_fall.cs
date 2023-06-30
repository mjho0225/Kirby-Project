using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Rock_fall : MonoBehaviour
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
        //rb.velocity = -Vector3.forward * 100f * Time.deltaTime;
        rb.AddForce(transform.forward * 100 * Time.deltaTime, ForceMode.Acceleration) ;
    }
}
