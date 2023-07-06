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
        bulletRb.velocity = transform.forward * 10;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {

        //GameObject.Find("PlayerRanger").GetComponent<PlayerFire>().UpdateClear();
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject, 1);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(hitCount < 1)
            {
                Instantiate(HitEffect, collision.transform.position, Quaternion.identity);
                Destroy(gameObject, 2f);
                hitCount++;
            }
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        hitCount = 0;
    }
}
