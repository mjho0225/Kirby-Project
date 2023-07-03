using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class PlayerFire : MonoBehaviour
{

    public enum AttackState
    {
        shot,
        chargeShot
    }

    AttackState attackState;

    public GameObject bulletFactory;
    public Transform firePos;
    bool isCharge = false;

    float currTime = 0;
    public RawImage img;

    LineRenderer lineRenderer;
    float lineWidth = 0.1f;

    float chargeWaitTime = 2f;

    bool fireD;
    bool fireU;
    bool fireS;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.widthMultiplier = lineWidth;
        attackState = AttackState.shot;
    }

    void GetInput()
    {
        fireD = Input.GetButtonDown("Fire1");
        fireU = Input.GetButtonUp("Fire1");
        fireS = Input.GetButton("Fire1");
    }
    
    void Update()
    {
        GetInput();
        //switch (attackState)
        //{
        //    case AttackState.shot:
        //        Shot();
        //        break;
        //    case AttackState.chargeShot:
        //        ChargeShot();
        //        break;
        //    default:
        //        break;
        //}

        

        if (fireS)
        {
            currTime += Time.deltaTime;
            if (currTime > chargeWaitTime){
                ChargeShot();           
            }
        }
        else if(fireU && !isCharge)
        {
            Shot();
            
        }else if (fireU && isCharge)
        {
            UpdateClear();
        }

    }

    private void ChargeShot()
    {
        print("ChargeShot");
        print("파티클값 커짐");
        isCharge = true;
        img.enabled = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        lineRenderer.enabled = true;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);

        if (Physics.Raycast(ray, out hit))
        {
            print("hitInfo" + hit.point);
            Vector3 v3Pos = ray.GetPoint(hit.distance);

            Transform playerPos = transform;
            lineRenderer.SetPosition(0, playerPos.position);
            lineRenderer.SetPosition(1, hit.point); // lineRenderer 1번째 target position 설정
            //img.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            img.transform.position = lineRenderer.GetPosition(1);
        }


    }
    void UpdateClear()
    {
        print("Clear");
        print("폭탄발사 임시");
        currTime = 0;
        lineRenderer.enabled = false;
        isCharge = false;
        img.enabled = false;
        
    }
    void Shot()
    {
        print("발사");
        GameObject bullet = Instantiate(bulletFactory, firePos.position, firePos.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = firePos.forward * 50;
        currTime = 0;
    }

}
