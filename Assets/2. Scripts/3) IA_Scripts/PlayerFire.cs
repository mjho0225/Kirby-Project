using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

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
    public GameObject chergeEffect02;
    public GameObject fire01Effect;
   
    
    public GameObject flareGun;
    public GameObject bubleGun;
    public GameObject kirbyModel;
    public GameObject kirbyCar;
    
    
    GameObject particle;
    int particleCount;

    public Animator anim;



    
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

    bool isAbsorb = false;

    Coroutine runningCoroutine = null;
    void Update()
    {
        GetInput();
        checkCar();
        
        if (fireS)
        {
            if (isAbsorb)
            {
                absorbCar();
            }
            else
            {
              
                currTime += Time.deltaTime;
                if (currTime > chargeWaitTime)
                {
                    //SoundPlay("SFX_Charge");
                    //audioSource.Stop();
                    //audioSource.clip = Resources.Load("Audio/Player/SFX_Charge2") as AudioClip;
                    //audioSource.Play();
                    ChargeShot();
                }
            }

        }
       
        if (fireD && !isCharge && !isAbsorb)
        {
            audioSource.Stop();
            Destroy(particle, 1f);
            if (runningCoroutine == null)
            {
                runningCoroutine = StartCoroutine(ShotWating());
            }
           

        }
        if (fireU && isCharge && !isAbsorb)
        {
            audioSource.Stop();
            //SoundPlay("SFX_Charging_Complete");
            Destroy(particle, 1f);
            Shot2();
           
            //UpdateClear();
        }

        //쏘는 중에는 공격기 못 뱉음
        if (!(isCharge) && fire2D)
        {
            Vector3 posZ = transform.position;
            //스피어 앞방향
            posZ.y += 2.2f;
            posZ += transform.forward * 2;
            GameObject go = Instantiate(bubleGun, posZ, Quaternion.identity);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(posZ * 4f, ForceMode.Acceleration);
            //Vector3 pos = transform.position;
            //pos = transform.forward * 5;
            //go.transform.position = Vector3.Lerp(transform.position, pos, 1f);

            GetComponentInParent<PlayerController>().ChangeAbsorb();
            flareGun.SetActive(false);
        }


    }
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    void SoundPlay(string str)
    {
        
        audioSource.clip = Resources.Load("Audio/Player/"+str) as AudioClip;
        audioSource.Play();
    }




//void OnDrawGizmosSelected()
//{
//    Vector3 posZ = transform.position;
//    //스피어 앞방향
//    posZ += transform.forward * 4;
//    Gizmos.color = Color.green;
//    Gizmos.DrawSphere(posZ, 4.5f);
//}
Collider[] cols;
    void checkCar()
    {

        int layer = 1 << LayerMask.NameToLayer("Car");
        Vector3 posZY = transform.position;
        //스피어 앞방향
        posZY += transform.forward * 4;
        //posZY.z += 10;
        posZY.y += 1;
        cols = Physics.OverlapSphere(posZY, 5f, layer);
        //print("cols" + cols);
        if (cols.Length > 0)
        {
            isAbsorb = true;
        }
    }

    void absorbCar()
    {
        //car mesh 합치기, Layer 중복 상위 오브젝트에만 CarLayer처리 + rigidbody
        for (int i = 0; i < cols.Length; i++)
        {
            //print("cols" + cols[i]);
            float dist = Vector3.Distance(transform.position, cols[i].gameObject.transform.position);
            Vector3 dir = transform.position;
            if (dist < 8f)
            {

                cols[i].gameObject.transform.LookAt(dir);
                cols[i].gameObject.transform.position = Vector3.Lerp(cols[i].gameObject.transform.position, dir, Time.deltaTime * 10);
                //rb.isKinematic = true;
            }
            if(dist < 2f)
            {
                isAbsorb = false;
                //Destroy(cols[0].gameObject);
                // 커비 자동차의 위치를 자식의 위치에 존재한다.
                kirbyCar.transform.position = transform.parent.GetChild(0).transform.position;

                TimelineManager.instance.timeLines[1].Play();
            }
           
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //print("collision" + collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            isAbsorb = false;
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
            kirbyModel.SetActive(false);
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


    IEnumerator PlayChargeSound()
    {
        SoundPlay("SFX_Charge");
        yield return new WaitForSeconds(0.8f);
        audioSource.Stop();
        SoundPlay("SFX_Charge2");
    }

    private void ChargeShot()
    {
        //print("ChargeShot");

        if (!isCharge)
        {
            StartCoroutine(PlayChargeSound());
        }
        isCharge = true;
        anim.SetBool("isReadyFire", true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        lineRenderer.enabled = true;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);

        int layerMask = (1 << LayerMask.NameToLayer("Wall"));
        layerMask = ~layerMask;
        //print(layerMask);
        if (Physics.Raycast(ray, out hit, 200f, layerMask))
        {
            //print("hitInfo" + hit.point);
            Vector3 v3Pos = ray.GetPoint(hit.distance);

            Transform playerPos = transform;
            lineRenderer.SetPosition(0, firePos.position);
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

           
            if (particleCount < 1)
            {
                Vector3 pos = lineRenderer.GetPosition(0);
                pos.z += 1f;

                particle = Instantiate(chargeEffect, firePos.position, Quaternion.LookRotation(firePos02 - transform.position));
                particleCount++;
            }

            
            particle.transform.position = firePos.position;


            Vector3 pointTolook = ray.GetPoint(200f);
         
            //flareGun.transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
            transform.parent.gameObject.transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }


    }
    public void UpdateClear()
    {
      
        //print("Clear");
        //print("폭탄발사 임시");
        currTime = 0;
        lineRenderer.enabled = false;
        StopAllCoroutines();
        isCharge = false;
        img.enabled = false;
        starImg.enabled = false;
        particleCount = 0;
        flareGun.transform.localRotation = Quaternion.Euler(0, 0, 0);

    }
    //void Shot()
    //{
    //    SoundPlay("SFX_ChargingShot");
    //    CameraShaker.Instance.ShakeOnce(3f, 3f, 0.1f, 0.5f);
    //    //anim.SetBool("isFire", true);
    //    anim.SetTrigger("Fire");
    //    Instantiate(fire01Effect, firePos.position, firePos.rotation);

    //    Destroy(particle, 1f);
    //    print("발사");

    //    GameObject bullet = Instantiate(bulletFactory, firePos.position, firePos.rotation);

    //    Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
    //    bulletRb.velocity = firePos.forward * 50;
    //    currTime = 0;

    //    transform.localScale = new Vector3(1f, 0.6f, 1f);
    //    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 100);
    //    flareGun.transform.localRotation = Quaternion.Euler(0, 0, 0);
    //}

    IEnumerator ShotWating()
    {
       
        Shot();
        yield return new WaitForSeconds(0.6f);
        runningCoroutine = null;
    }
    void Shot()
    {
       
        SoundPlay("SFX_ChargingShot");
        CameraShaker.Instance.ShakeOnce(3f, 3f, 0.1f, 0.5f);
        //anim.SetBool("isFire", true);
 
        anim.SetTrigger("Fire");
        Instantiate(fire01Effect, firePos.position, firePos.rotation);

        Destroy(particle, 1f);
        //print("발사");

        GameObject bullet = Instantiate(bulletFactory, firePos.position, firePos.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = firePos.forward * 50;
        currTime = 0;

        transform.localScale = new Vector3(1f, 0.6f, 1f);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 100);
        flareGun.transform.localRotation = Quaternion.Euler(0, 0, 0);
      
    }

    void Shot2()
    {
        audioSource.Stop();
       
        anim.SetBool("isReadyFire", false);
        anim.SetTrigger("Fire");
        Instantiate(chergeEffect02, firePos.position, firePos.rotation);
        Destroy(particle);
        CameraShaker.Instance.ShakeOnce(3f, 3f, 0.1f, 0.5f);

        Instantiate(fire01Effect, firePos.position, firePos.rotation);
        Destroy(particle, 2f);
        //print("발사2");
        GameObject bullet02 = Instantiate(bulletFactory02, firePos.position, Quaternion.LookRotation(firePos02 - transform.position));
        flareGun.transform.localRotation = Quaternion.Euler(0, 0, 0);
        SoundPlay("SFX_ChargingShot");
        UpdateClear();


    }

}

