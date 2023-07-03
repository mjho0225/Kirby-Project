using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))] 
public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public Transform firePos;
    bool isCharge = false;

    float currTime = 0;
    public RawImage img;

    LineRenderer lineRenderer;
    float lineWidth = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = false;
        //lineRenderer.material.color = Color.blue;
        lineRenderer.widthMultiplier = lineWidth;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButton("Fire1"))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            lineRenderer.enabled = true;
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);

            if (Physics.Raycast(ray, out hit))
            {
           

                print("hitInfo" + hit.point);
                Vector3 v3Pos = ray.GetPoint(hit.distance);

                Transform playerPos = transform;
                lineRenderer.SetPosition(0, playerPos.position);
                lineRenderer.SetPosition(1, hit.point); // lineRenderer 1번째 target position 설정
                //img.transform.localScale =new Vector3(0.5f, 0.5f, 0.5f);
                img.transform.position = lineRenderer.GetPosition(1);
                


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
            lineRenderer.enabled = false;

        }

    }

    void Shot()
    {
        GameObject bullet = Instantiate(bulletFactory, firePos.position, firePos.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = firePos.forward * 50;
    }

    
}
