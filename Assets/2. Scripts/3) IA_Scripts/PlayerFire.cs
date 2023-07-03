using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public Transform firePos;
    bool isCharge = false;
    
    float currTime = 0;
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButton("Fire1"))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
            if (Physics.Raycast(ray, out hit))
            {
                
                print("hitInfo" + hit.point);
                //img.transform.position = hit.point;



            }// RayCast

            currTime = Time.deltaTime;
            if (currTime > 2f)
            {
                isCharge = true;
                //레이캐스트가 생기고 발사
                print("파티클값 커짐");

            }
            else
            {
                isCharge = false;
            }
       }else if (Input.GetButtonUp("Fire1"))
            {
                if (isCharge)
                {
                    print("레이캐스트 발사");

                    //Vector3 p = Input.mousePosition;
                    isCharge = false;

                }
                else
                {
                    Shot();
                }
            currTime = 0;
                              
            }

    }

    void Shot()
    {
        GameObject bullet = Instantiate(bulletFactory, firePos.position, firePos.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = firePos.forward * 50;
    }

    
}
