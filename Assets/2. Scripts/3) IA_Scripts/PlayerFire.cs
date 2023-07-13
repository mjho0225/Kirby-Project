using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(LineRenderer))]
public class PlayerFire : MonoBehaviour
{

    public enum AttackState
    {
        shot,
        chargeShot
    }

    AttackState attackState;

    public GameObject bulletFactory;
    public GameObject bulletFactory02;
    public Transform firePos;
    public bool isCharge = false;

    float currTime = 0;
    public RawImage img;
    public RawImage starImg;

    LineRenderer lineRenderer;
    float lineWidth = 0.1f;

    float chargeWaitTime = 1f;

    bool fireD;
    bool fireU;
    bool fireS;
    bool fire2D;
    Rigidbody rb;

    Vector3 firePos02;
    public GameObject chargeEffect;
    GameObject particle;
    int particleCount;


    public GameObject fire01Effect;

    public GameObject bubleGun;
    void Start()
    {
        OffAbsorbCollider();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.widthMultiplier = lineWidth;
        attackState = AttackState.shot;
        rb = GetComponentInParent<Rigidbody>();
        rb.isKinematic = false;
        img.enabled = false;
        starImg.enabled = false;
        GetComponentInParent<PlayerController>().speed = 7f;

        
    }

    void GetInput()
    {
        fireD = Input.GetButtonDown("Fire1");
        fireU = Input.GetButtonUp("Fire1");
        fireS = Input.GetButton("Fire1");
        fire2D = Input.GetButtonDown("Fire2");
    }

    void Update()
    {
        GetInput();


        if (fireS)
        {
            currTime += Time.deltaTime;
            if (currTime > chargeWaitTime)
            {
                ChargeShot();
            }
        }
        if (fireD && !isCharge)
        {
            Destroy(particle, 1f);
            Shot();

        }
        if (fireU && isCharge)
        {
            Destroy(particle, 1f);
            Shot2();
            //UpdateClear();
        }

        //쏘는 중에는 공격기 못 뱉음
        if (!(isCharge) && fire2D)
        {
            Vector3 posZ = transform.position;
            posZ.z += 2;
            GameObject go = Instantiate(bubleGun, posZ, Quaternion.identity);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 6f, ForceMode.Impulse);

            GetComponentInParent<PlayerController>().ChangeAbsorb();
        }


    }
    void OffAbsorbCollider()
    {
        SphereCollider[] mesh_origin = GetComponentsInParent<SphereCollider>();
        for (int i = 0; i < mesh_origin.Length; i++)
        {
            mesh_origin[i].enabled = true;
        }

        SphereCollider mesh_child = GetComponent<SphereCollider>();
        mesh_child.enabled = false;
    }

    void StartLockOn()
    {

    }

    private void ResetLockOn()
    {

    }
    private void ChargeShot()
    {
        print("ChargeShot");

        if (particleCount < 1)
        {
            particle = Instantiate(chargeEffect);
            particle.transform.position = transform.position;
            particleCount++;
        }

        isCharge = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        lineRenderer.enabled = true;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);

        int layerMask = (1 << LayerMask.NameToLayer("Wall"));
        layerMask = ~layerMask;
        print(layerMask);
        if (Physics.Raycast(ray, out hit, 200f, layerMask))
        {
            print("hitInfo" + hit.point);
            Vector3 v3Pos = ray.GetPoint(hit.distance);

            Transform playerPos = transform;
            lineRenderer.SetPosition(0, playerPos.position);
            // lineRenderer 1번째 target position 설정
            //img.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            //라곤
            if (hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 6)
            {
                //과녁 이펙트 에너미 가운데
                lineRenderer.SetPosition(1, hit.collider.gameObject.transform.position);
                img.enabled = false;

                starImg.enabled = true;
                starImg.transform.position = hit.collider.gameObject.transform.position;

            }
            else
            {
                lineRenderer.SetPosition(1, hit.point);
                img.enabled = true;
                starImg.enabled = false;
                img.transform.position = lineRenderer.GetPosition(1);
            }

            firePos02 = hit.point;
        }


    }
    public void UpdateClear()
    {
        print("Clear");
        print("폭탄발사 임시");
        currTime = 0;
        lineRenderer.enabled = false;
        isCharge = false;
        img.enabled = false;
        starImg.enabled = false;
        particleCount = 0;


    }
    void Shot()
    {

        Instantiate(fire01Effect, firePos.position, firePos.rotation);
       
        Destroy(particle, 1f);
        print("발사");
     
        GameObject bullet = Instantiate(bulletFactory, firePos.position, firePos.rotation);
        
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = firePos.forward * 50;
        currTime = 0;
       
        transform.localScale = new Vector3(1f, 0.6f, 1f);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 100);

    }

    void Shot2()
    {
        Destroy(particle, 2f);
        print("발사2");
        //Vector3 dir = transform.position;
        //dir.z +=  1;
        //dir.x += 1;
        GameObject bullet02 = Instantiate(bulletFactory02, firePos.position, Quaternion.LookRotation(firePos02 - transform.position));

        currTime += Time.deltaTime;
        if (currTime > 1.5f)
        {
            UpdateClear();
        }
        //currTime = 0;

    }

}

