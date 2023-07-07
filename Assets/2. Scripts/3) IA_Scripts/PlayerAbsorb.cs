using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbsorb : MonoBehaviour
{ 
    float speed = 10f;
    Transform playerPos;
    //enemy 흡입
    //[SerializeField]
    public bool emptyState = true;
    GameObject player;
    GameObject item;
    float currTime;

    bool isEmpty = true;
    //int layer;
    float power = 20f;
    [SerializeField] public GameObject absorbItem;
    string RName;
    GameObject absorbTrigger;
    Rigidbody rb;
    public string absorbItemTag;
    public enum AbsorbState
    {
       Ready,
       Absorbing,
       Absorbed,
    }

    public AbsorbState state = AbsorbState.Ready;


    bool getRanger = false;
    float currTime2;
    void Start()
    {
        absorbTrigger = GameObject.Find("AbsorbTrigger");
        rb = GetComponentInParent<Rigidbody>();
    }

    public void Reset()
    {
        //아이템 비우기
        absorbItem = null;
        absorbItemTag = null;
        rb.isKinematic = false;

        state = AbsorbState.Ready;
        isEmpty = true;
        currTime = 0;
        currTime2 = 0;
    
    }

    private void Update()
    {
        switch (state)
        {
            case AbsorbState.Ready:
                UpdateReady();
                break;
            case AbsorbState.Absorbing:
                UpdateAbsorbing();
                break;
            case AbsorbState.Absorbed:
                UpdateAbsorbed();
                break;
            default:
                break;
        }

        if (getRanger)
        {
            print(currTime2);
            currTime2 += Time.deltaTime;
            if (currTime2 > 1f)
            {
                getRanger = false;
                GetComponentInParent<PlayerController>().ChangeRanger();
                currTime2 = 0;
            }
        }
       
       
    }
    [SerializeField]
    private LayerMask layerMask;
    private void UpdateReady()
    {
        //콜라이더 바꾸기
        OffAbsorbCollider();

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        //wall layer만 제외하기
      
        //Debug.DrawRay(ray.origin, ray.direction, Color.blue);
        int layerMask = (1 << LayerMask.NameToLayer("Player"));
        layerMask = ~layerMask;
        //print(layerMask);
        ;
        if (Input.GetButton("Fire1"))
        {
            //
            //Physics.SphereCast
            if (Physics.SphereCast(ray.origin, 3f, ray.direction, out hitInfo, 200f, layerMask, QueryTriggerInteraction.UseGlobal))
                //if (Physics.SphereCast(ray.origin, 50f, ray.direction, out hitInfo, 200f, layerMask))
            //if (Physics.Raycast(ray, out hitInfo, 200f, layerMask))
            {


                currTime += Time.deltaTime;
              
                if (currTime > 0.5f)
                {
                    GetComponentInParent<PlayerController>().speed = 2f;
                    print(hitInfo.collider.gameObject.layer);
                    if (hitInfo.collider.gameObject.layer == 9)
                    {
                        absorbItem = hitInfo.collider.gameObject;
                        absorbItemTag = absorbItem.tag;
                    }
                    else
                    {
                        absorbItem = hitInfo.collider.gameObject.transform.parent.gameObject;
                        absorbItemTag = absorbItem.tag;
                    }
                
                    if (absorbItem.layer == 6 || absorbItem.layer == 9 || absorbItem.layer == 8)
                    {
                        print("흡입준비 => 흡입시작");
                        state = AbsorbState.Absorbing;
                    }
                    //else if(hitInfo.collider.gameObject.layer == 9)
                    //{
                    //    print("hitInfo.collider.gameObject.layer == 9");
                    //    absorbItem = hitInfo.collider.gameObject;
                    //    absorbItemTag = absorbItem.tag;
                    //}
                    
                }

            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            print("흡입멈춤");
            currTime = 0f;
        }
    }
    private void UpdateAbsorbing()
    {

    
        //콜라이더 바꾸기
        OnAbsorbCollider();
        //거리가 5f이내면 강제로 흡입완료 아닐 경우 흡입 대기로 돌아간다.
        float distance = Vector3.Distance(absorbItem.transform.position, transform.position);
        //print("distance" + distance);
        if (distance < 4f)
        {
           
            Vector3 dir = transform.position;
            absorbItem.transform.LookAt(dir);
            absorbItem.transform.position = Vector3.Lerp(absorbItem.transform.position, dir, 1f);
            rb.isKinematic = true;
            //print("state absorbing => absorbed"+ state);
        }
        else
        {
            
            absorbItem.transform.LookAt(transform.position);
            absorbItem.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //print("state absorbing => ready" + state);

        }
    }
 
    private void UpdateAbsorbed()
    {
        OffAbsorbCollider();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        rb.isKinematic = false;
            //먹는 애니메이션 진행 후
            //switch (absorbItemTag)
            //{
            //case "e_ranger":
            //    RName = "Moving_Ranger_B";
            //    break;
            //case "e_fox":
            //    RName = "MonsterFox_B";
            //    break;
            //case "e_mush":
            //    RName = "Mush_B";
            //    break;
            //case "e_ghost":
            //    RName = "GhostPos_B";
            //    break;
            //case "e_bomb":
            //    RName = "Mons_Bomb_B";
            //    break;
            //case "e_bird":
            //    RName = ""; //새 몬스터는 아직 미정
            //    break;
            //case "e_gosum":
            //    RName = "Gosum_B";
            //    break;
            //default:
            //    //공격기 없는 기본 에너미들
            //    break;
            //case "e_ranger":
            //    RName = "Moving_Ranger";
            //    break;
            //case "e_fox":
            //    RName = "MonsterFox";
            //    break;
            //case "e_mush":
            //    RName = "Mush";
            //    break;
            //case "e_ghost":
            //    RName = "GhostPos";
            //    break;
            //case "e_bomb":
            //    RName = "Mons_Bomb";
            //    break;
            //case "e_bird":
            //    RName = ""; //새 몬스터는 아직 미정
            //    break;
            //case "e_gosum":
            //    RName = "Gosum";
            //    break;
            //default:
            //    //공격기 없는 기본 에너미들
            //    break;
            
        //}
           
            if (Input.GetButtonDown("Fire1") && !(getRanger))
            {
                GetComponentInParent<PlayerController>().speed = 5f;
                print(absorbItemTag  + ": 먹은 에너미 발사");
            //GameObject obj = Resources.Load<GameObject>(RName);
                GameObject obj = Resources.Load<GameObject>("BubbleMonster");
                Vector3 posZ = transform.position;
                posZ.z += 2;

                GameObject go = Instantiate(obj, posZ, Quaternion.identity);
                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * power, ForceMode.Impulse);
            transform.localScale = new Vector3(1f, 1f, 1f);
            //아이템 비우기
            Reset();

            }
     
        
    }
    private void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            //아이템 분류 후 획득
            if (other.gameObject.tag == "item")
            {

            }
        }

        print("other check" + other.gameObject);
        print("layer check" + absorbItemTag);

        if (other.gameObject == absorbItem)
        {
         
            float distance = Vector3.Distance(absorbItem.transform.position, transform.position);
            //변신 애니메이션 시간 체크 필요
            if (distance < 3f)
            {
                print("destroy" + absorbItemTag);
                Destroy(other.gameObject);
                state = PlayerAbsorb.AbsorbState.Absorbed;
           
                if (absorbItemTag == "e_ranger")
                {
                    GetComponentInParent<PlayerController>().speed = 6f;
                    getRanger = true;
                 
                }
            }
        }
    }

    void OnAbsorbCollider()
    {
        ////print("켜짐&&&&&&&&&&&&&&");
        rb.isKinematic = false;
        SphereCollider[] mesh_origin = GetComponentsInParent<SphereCollider>();
        for (int i = 0; i < mesh_origin.Length; i++)
        {
            mesh_origin[i].enabled = false;
        }
        SphereCollider[] mesh_child = GetComponents<SphereCollider>();
        for (int i = 0; i < mesh_child.Length; i++)
        {
            mesh_child[i].enabled = true;
        }
        //SphereCollider[] mesh_origin = GetComponents<SphereCollider>();
        //absorbTrigger.GetComponent<AbsorbTrigger>().enabled = true;
        //SphereCollider[] mesh_child = absorbTrigger.GetComponents<SphereCollider>();

        //for (int i = 0; i < mesh_origin.Length; i++)
        //{
        //    mesh_origin[i].enabled = false;
        //}

        //for (int i = 0; i < mesh_child.Length; i++)
        //{
        //    mesh_child[i].enabled = true;
        //}

    }
    void OffAbsorbCollider()
    {
        SphereCollider[] mesh_origin = GetComponentsInParent<SphereCollider>();
        for (int i = 0; i < mesh_origin.Length; i++)
        {
            mesh_origin[i].enabled = true;
        }

        SphereCollider[] mesh_child = GetComponents<SphereCollider>();
        for (int i = 0; i < mesh_child.Length; i++)
        {
            mesh_child[i].enabled = false;
        }
        ////print("꺼짐&&&&&&&&&&&&&&");
        ////rb.isKinematic = true;
        //SphereCollider[] mesh_origin = GetComponents<SphereCollider>();
        //SphereCollider[] mesh_child = absorbTrigger.GetComponents<SphereCollider>();
        //absorbTrigger.GetComponent<AbsorbTrigger>().enabled = false;
        //for (int i = 0; i < mesh_origin.Length; i++)
        //{
        //    mesh_origin[i].enabled = true;
        //}
        //for (int i = 0; i < mesh_child.Length; i++)
        //{
        //    mesh_child[i].enabled = false;
        //}

    }
}