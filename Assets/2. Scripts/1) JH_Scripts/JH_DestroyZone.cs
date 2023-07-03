using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_DestroyZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollsionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }

}
