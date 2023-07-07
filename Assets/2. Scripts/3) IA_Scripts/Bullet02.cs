using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet02 : MonoBehaviour
{
    public ParticleSystem HitEffect;
    int hitCount;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody bulletRb = GetComponent<Rigidbody>();
        bulletRb.velocity = transform.forward * 30;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {

        //GameObject.Find("PlayerRanger").GetComponent<PlayerFire>().UpdateClear();
       
      
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(hitCount < 1)
            {
                Instantiate(HitEffect, collision.transform.position, Quaternion.identity);
                Destroy(gameObject, 2f);
                hitCount++;
            }
            
        }
            Destroy(gameObject, 3);
    }
    private void OnTriggerExit(Collider collision)
    {
        hitCount = 0;
    }
}
