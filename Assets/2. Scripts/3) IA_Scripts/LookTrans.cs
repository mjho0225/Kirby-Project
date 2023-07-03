using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTrans : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)

    {

        // other.gameObject == target.gameObject

        if (other.tag == "Player")

        {
            Vector3 dir = transform.position;
            
            other.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10000);

        }

    }
}
