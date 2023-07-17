using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbsorb : MonoBehaviour
{
    float minSpeed = 3f;
    float maxSpeed = 7f;
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
    public Transform effectPos;
    public GameObject flaregun;
    public GameObject[] absorbParticle;

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
        flaregun.SetActive(false);
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

        //print("state" + state);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }

    [SerializeField] private LayerMask layerMask;
    
    float min;
    GameObject gb;
    //public ParticleSystem[] particles;
    public List<GameObject> particles;
    bool isParticling = false;


    void makeParticle()
    {
        isParticling = true;
        for (int i = 0; i < absorbParticle.Length; i++)
        {
            GameObject go = Instantiate(absorbParticle[i], effectPos.position, effectPos.rotation);
            go.GetComponent<ParticleSystem>().Play();

            particles.Add(go);
            //particles[i] = go.GetComponent<ParticleSystem>();

            //print(particles[i]);
        }

    }

    void DestroyParticle()
    {
        isParticling = false;
        for (int i = 0; i < particles.Count; i++)
        {
            print("particles[i]" + particles[i]);
            Destroy(particles[i], 0.5f);
        }
    }
    private void UpdateReady()
    {
        //콜라이더 바꾸기
        OffAbsorbCollider();

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        //wall layer만 제외하기
        Debug.DrawRay(ray.origin, ray.direction, Color.blue);
        int layerMask = (1 << LayerMask.NameToLayer("Player") | (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("Wall")));
        layerMask = ~layerMask;
        //print(layerMask);



        if (Input.GetButton("Fire1"))
        {
            currTime += Time.deltaTime;

           
            if (currTime > 0.5f)
                {
                if (!(isParticling)) makeParticle();
                
                   if(Physics.SphereCast(ray.origin, 2f, ray.direction, out hitInfo, 15f, layerMask, QueryTriggerInteraction.UseGlobal))
                    {
                    GetComponentInParent<PlayerController>().speed = minSpeed;
                    print("absorbItem(*((" + hitInfo.collider.gameObject.layer);
                    print("hitInfo(*((" + hitInfo);

                    if (hitInfo.collider.gameObject.layer == 8)
                    {
                        absorbItem = hitInfo.collider.gameObject.transform.parent.gameObject;
                        print("absorbItem" + absorbItem);
                        absorbItemTag = absorbItem.tag;
                        state = AbsorbState.Absorbing;

                    }
                    else if (hitInfo.collider.gameObject.layer == 6 || hitInfo.collider.gameObject.layer == 9)
                    {
                        absorbItem = hitInfo.collider.gameObject;
                        print("absorbItem" + absorbItem);
                        absorbItemTag = absorbItem.tag;
                        state = AbsorbState.Absorbing;
                       
                    }
                }
                    else{
                        
                        int layer = (1 << LayerMask.NameToLayer("Enemy")) + (1 << LayerMask.NameToLayer("BubleGun"));
                        Vector3 posZY = transform.position;
                    //스피어 앞방향
                        posZY.z += 2;
                        posZY.y += 1;
                    Collider[] cols = Physics.OverlapSphere(posZY, 1.5f, layer);
                        for (int i = 0; i < cols.Length; i++)
                    {
                        print("cols" + cols[i]);
                        float dist = Vector3.Distance(transform.position, cols[i].gameObject.transform.position);
                        min = dist;
                        gb = cols[i].gameObject;

                        if (dist < min)
                        {
                            min = dist;
                            gb = cols[i].gameObject;
                        }
                    }
                        if (min != 0)
                    {
                        absorbItem = gb;
                        print("absorbItem" + absorbItem);
                        absorbItemTag = absorbItem.tag;
                        state = AbsorbState.Absorbing;
                    }
                    }

                //범위내에
                //에너미 배열 받아서 거리 체크 후 제일 가까운 애를 흡수한다.
                }else
                {
                    GetComponentInParent<PlayerController>().speed = maxSpeed;
                }
        }
        
       

        if (Input.GetButtonUp("Fire1"))
        {
            print("흡입멈춤");
            currTime = 0f;
            //DestroyParticle();
            //for (int i = 0; i < absorbParticle.Length; i++)
            //{
            //    particles[i].GetComponent<ParticleSystem>().Stop();
            //}
            DestroyParticle();
        }
    }


    

    void OnDrawGizmosSelected()
    {
        Vector3 posZ = transform.position;
        //스피어 앞방향
        posZ.z += 1.5f;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(posZ, 1.5f);
    }

    private void UpdateAbsorbing()
    {
        GetComponentInParent<PlayerController>().speed = minSpeed;

        DestroyParticle();
        //콜라이더 바꾸기
        OnAbsorbCollider();
        //거리가 5f이내면 강제로 흡입완료 아닐 경우 흡입 대기로 돌아간다.
        float distance = Vector3.Distance(absorbItem.transform.position, transform.position);
        Vector3 dir = transform.position;
        if (distance < 8f)
        {
        
            absorbItem.transform.LookAt(dir);
            absorbItem.transform.position = Vector3.Lerp(absorbItem.transform.position, dir, Time.deltaTime * 20);
            rb.isKinematic = true;
            //print("state absorbing => absorbed"+ state);
        }
        else
        {

            absorbItem.transform.LookAt(transform.position);
            absorbItem.transform.position = Vector3.Lerp(absorbItem.transform.position, dir, Time.deltaTime);
            rb.isKinematic = true;

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
            GetComponentInParent<PlayerController>().speed = maxSpeed;
            print(absorbItemTag + ": 먹은 에너미 발사");
            //GameObject obj = Resources.Load<GameObject>(RName);
            GameObject obj = Resources.Load<GameObject>("BubbleMonster");
            Vector3 posZ = transform.position;
            posZ.z += 1;

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
            print(distance);
            //변신 애니메이션 시간 체크 필요
            if (distance < 3f)
            {
                print("destroy" + absorbItemTag);
                Destroy(other.gameObject);
                state = PlayerAbsorb.AbsorbState.Absorbed;

                if (absorbItemTag == "e_ranger")
                {
                    GetComponentInParent<PlayerController>().speed = maxSpeed;
                    getRanger = true;
                    state = PlayerAbsorb.AbsorbState.Ready;
                    flaregun.SetActive(true);

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

    }
}