using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_e_isKn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponentInParent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<Rigidbody>().isKinematic = true;
        }
    }
}
