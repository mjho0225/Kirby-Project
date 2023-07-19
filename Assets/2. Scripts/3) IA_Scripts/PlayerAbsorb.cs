using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbsorb : MonoBehaviour
{
    float minSpeed = 3f;
    float maxSpeed = 7f;
    Transform playerPos;
    //enemy ����
    //[SerializeField]
    public bool emptyState = true;
    GameObject player;
    GameObject item;
    float currTime;

    bool isEmpty = true;
    //int layer;
    float power = 40f;
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
        //������ ����
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

        //if(isParticling)
        //particleMove();
        particleMove();




    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.white;
    //    Gizmos.DrawWireSphere(transform.position, 2f);
    //}

    [SerializeField] private LayerMask layerMask;
    
    float min;
    GameObject gb;
    //public ParticleSystem[] particles;
    public List<GameObject> particles;
    bool isParticling = false;


    void makeParticle()
    {
       
        for (int i = 0; i < absorbParticle.Length; i++)
        {
            GameObject go = Instantiate(absorbParticle[i], effectPos.position, effectPos.rotation);
            go.GetComponent<ParticleSystem>().Play();

            particles.Add(go);
            isParticling = true;
            //particles[i] = go.GetComponent<ParticleSystem>();
            //print(particles[i]);
        }

    }

    void particleMove()
    {
        if (isParticling)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].transform.parent = transform;

            }
        }

    }

    void DestroyParticle()
    {
        isParticling = false;
        for (int i = 0; i < particles.Count; i++)
        {
            //print("particles[i]" + particles[i]);
            Destroy(particles[i], 0.5f);
           
        }
        particles.RemoveRange(0, 3);
    }
    private void UpdateReady()
    {
        //�ݶ��̴� �ٲٱ�
        OffAbsorbCollider();

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        //wall layer�� �����ϱ�
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

                   if (Physics.SphereCast(ray.origin, 2f, ray.direction, out hitInfo, 15f, layerMask, QueryTriggerInteraction.UseGlobal))
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
                    //���Ǿ� �չ���
                        posZY += transform.forward * 2;
                        //posZY.z += 2;
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

                //��������
                //���ʹ� �迭 �޾Ƽ� �Ÿ� üũ �� ���� ����� �ָ� ����Ѵ�.
                }else
                {
                    GetComponentInParent<PlayerController>().speed = maxSpeed;
                }
        }
        
       

        if (Input.GetButtonUp("Fire1"))
        {
            print("���Ը���");
            currTime = 0f;
       
            DestroyParticle();
        }
    }


    

    void OnDrawGizmosSelected()
    {
        Vector3 posZ = transform.position;
        //���Ǿ� �չ���
        posZ += transform.forward * 1.5f;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(posZ, 1.5f);
    }

    private void UpdateAbsorbing()
    {
        GetComponentInParent<PlayerController>().speed = minSpeed;

        
        //�ݶ��̴� �ٲٱ�
        OnAbsorbCollider();
        //�Ÿ��� 5f�̳��� ������ ���ԿϷ� �ƴ� ��� ���� ���� ���ư���.
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

    public GameObject pivot;
    private void UpdateAbsorbed()
    {
        OffAbsorbCollider();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        rb.isKinematic = false;
     

        if (Input.GetButtonDown("Fire1") && !(getRanger))
        {
            GetComponentInParent<PlayerController>().speed = maxSpeed;
            print(absorbItemTag + ": ���� ���ʹ� �߻�");
            //GameObject obj = Resources.Load<GameObject>(RName);
            GameObject obj = Resources.Load<GameObject>("BubbleMonster");
            // P = P0 + direction
            Vector3 posZ = transform.parent.gameObject.transform.position;
            posZ += transform.forward;
           // posZ.z += 1;
            GameObject go = Instantiate(obj, posZ, Quaternion.identity);
            go.transform.forward = transform.forward;
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * power, ForceMode.Impulse);
            transform.localScale = new Vector3(1f, 1f, 1f);
            //������ ����
            Reset();

        }


    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            //������ �з� �� ȹ��
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
            //���� �ִϸ��̼� �ð� üũ �ʿ�
            if (distance < 3f)
            {
                print("destroy" + absorbItemTag);
                Destroy(other.gameObject);
                state = PlayerAbsorb.AbsorbState.Absorbed;
                DestroyParticle();

                if (absorbItemTag == "e_ranger")
                {
                    //sTimelineManager.instance.timeLines[0].Play();
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
        ////print("����&&&&&&&&&&&&&&");
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