using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_break_Floor : MonoBehaviour
{

    public GameObject breakFloor;

    bool doitBreak;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (doitBreak == true)
        {
            breakFloor.GetComponent<Rigidbody>().useGravity = true;
            Destroy(breakFloor,1f);
        
        }
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Rock")
    //    {
    //        doitBreak = true;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
            print("dhkT");
            doitBreak = true;
        }
    }
}
