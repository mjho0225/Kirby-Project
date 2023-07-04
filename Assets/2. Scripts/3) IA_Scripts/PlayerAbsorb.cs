using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbsorb : MonoBehaviour
{ 
    float speed = 10f;
    Transform playerPos;
    //enemy ����
    //[SerializeField]
    public bool emptyState = true;
    GameObject player;
    GameObject item;
    float currTime;
    float rayLength = 20f;

    bool isEmpty = true;
    //int layer;
    float power = 10f;
    [SerializeField] public GameObject absorbItem;
    string RName;
    //public bool isAbsorbing = false;
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
    
    void Start()
    {
        absorbTrigger = GameObject.Find("AbsorbTrigger");
        rb = GetComponent<Rigidbody>();
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
    }

    private void UpdateReady()
    {
        //�ݶ��̴� �ٲٱ�
        OffAbsorbCollider();

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
        if (Input.GetButton("Fire1"))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                currTime += Time.deltaTime;
              
                if (currTime > 1.2f)
                {
                    absorbItem = hitInfo.collider.gameObject.gameObject.transform.parent.gameObject;
                    absorbItemTag = absorbItem.tag;
                    if (absorbItem.layer == 6)
                    {
                        print("�����غ� => ���Խ���");
                        state = AbsorbState.Absorbing;
                    }
                    
                }

            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            print("���Ը���");
            currTime = 0f;
        }
    }
    private void UpdateAbsorbing()
    {

        //�ݶ��̴� �ٲٱ�
        OnAbsorbCollider();
        //�Ÿ��� 5f�̳��� ������ ���ԿϷ� �ƴ� ��� ���� ���� ���ư���.
        float distance = Vector3.Distance(absorbItem.transform.position, transform.position);
        print("distance" + distance);
        if (distance < 8f)
        {
           
            Vector3 dir = transform.position;
            absorbItem.transform.LookAt(dir);
            absorbItem.transform.position = Vector3.Lerp(absorbItem.transform.position, dir, 1f);
            rb.isKinematic = true;
            //print("state absorbing => absorbed"+ state);
        }
        else
        {

            //if (Input.GetButtonUp("Fire1"))
            //{
            //    state = AbsorbState.Ready;
            //    currTime = 0f;
            //}
            
            absorbItem.transform.LookAt(transform.position);
            absorbItem.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //print("state absorbing => ready" + state);

        }
    }

    private void UpdateAbsorbed()
    {
        currTime = 0f;
        rb.isKinematic = false;
        bool isDestroy = GetComponentInChildren<AbsorbTrigger>().isDestroy;
            //�Դ� �ִϸ��̼� ���� ��
            switch (absorbItemTag)
            {
                case "e_ranger":
                    RName = "Moving_Ranger";
                    break;
                case "e_fox":
                    RName = "MonsterFox";
                    break;
                case "e_mush":
                    RName = "Mush";
                    break;
                case "e_ghost":
                    RName = "GhostPos";
                    break;
                case "e_bomb":
                    RName = "Mons_Bomb";
                    break;
                case "e_bird":
                    RName = ""; //�� ���ʹ� ���� ����
                    break;
                case "e_gosum":
                    RName = "Gosum";
                    break;
                default:
                    //���ݱ� ���� �⺻ ���ʹ̵�
                    break;
            }
           
            if (Input.GetButtonDown("Fire1") && isDestroy)
            {

               
                print(absorbItemTag  + ": ���� ���ʹ� �߻�");
                GameObject obj = Resources.Load<GameObject>(RName);
                Vector3 posZ = transform.position;
                posZ.z += 5;

                GameObject go = Instantiate(obj, posZ, Quaternion.identity);

                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * power, ForceMode.Impulse);

                //������ ����
                absorbItem = null;
                absorbItemTag = null;
                rb.isKinematic = false;
            //state ����
                state = AbsorbState.Ready;
                GetComponentInChildren<AbsorbTrigger>().isDestroy = false;
                isEmpty = true;
            //if (absorbItem.tag == "e_ranger")
            //{
            //    GetComponent<PlayerController>().attackState = PlayerController.AttackState.ABSORB;
            //}
            currTime = 0;

            }
     
        
    }



    //private void OnTriggerEnter(Collider other)
    //{
    //    print("Ʈ���� ��ü" + other.gameObject);
    //}

    void OnAbsorbCollider()
    {
        //print("����&&&&&&&&&&&&&&");
        rb.isKinematic = false;
        SphereCollider[] mesh_origin = GetComponents<SphereCollider>();
        absorbTrigger.GetComponent<AbsorbTrigger>().enabled = true;
        SphereCollider[] mesh_child = absorbTrigger.GetComponents<SphereCollider>();
     
        for (int i = 0; i < mesh_origin.Length; i++)
        {
            mesh_origin[i].enabled = false;
        }
     
        for (int i = 0; i < mesh_child.Length; i++)
        {
            mesh_child[i].enabled = true;
        }

    }
    void OffAbsorbCollider()
    {
        //print("����&&&&&&&&&&&&&&");
        //rb.isKinematic = true;
        SphereCollider[] mesh_origin = GetComponents<SphereCollider>();
        SphereCollider[] mesh_child = absorbTrigger.GetComponents<SphereCollider>();
        absorbTrigger.GetComponent<AbsorbTrigger>().enabled = false;
        for (int i = 0; i < mesh_origin.Length; i++)
        {
            mesh_origin[i].enabled = true;
        }
        for (int i = 0; i < mesh_child.Length; i++)
        {
            mesh_child[i].enabled = false;
        }

    }
}