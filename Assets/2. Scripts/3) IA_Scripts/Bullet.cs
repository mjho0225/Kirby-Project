using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���� Ground �����ϱ�
    private void OnCollisionEnter(Collision collision)
    {
        //��ƼŬ
        // GameObject bullet02 = Instantiate(bulletFactory02, firePos02.point, Quaternion.LookRotation(firePos02.normal));
        //Destroy(gameObject, 4);
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
