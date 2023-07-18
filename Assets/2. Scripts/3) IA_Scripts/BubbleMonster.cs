using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMonster : MonoBehaviour
{

    public GameObject bulletStar;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            print("SS");
            Instantiate(bulletStar, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject,1);
        }
        //Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
    }
}
