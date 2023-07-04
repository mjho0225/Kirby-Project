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


    //int layer;
    float power = 10f;
    [SerializeField] public GameObject absorbItem;
    string absorbItemTag;
    public bool isAbsorb = false;
    //public bool isAbsorbing = false;
    GameObject absorbTrigger;
    // Start is called before the first frame update
    void Start()
    {
        print("��� ����");
        emptyState = true;
        absorbTrigger = GameObject.Find("AbsorbTrigger");
       
        //print("absorbTrigger" + absorbTrigger.GetComponent<SphereCollider>());
        //OnAbsorbCollider();

        //gameObject.GetComponentInChildren<SphereCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckItem();
    }

    void CheckItem()
    {
        Vector3 pos = transform.position;
        Ray ray = new Ray(pos, transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
        RaycastHit hitInfo;

        if (Input.GetButton("Fire1") && emptyState)
        {
                //float.MaxValue, layer => raycast �Ÿ� ���� ����
            if (Physics.Raycast(ray, out hitInfo))
                {
                    currTime += Time.deltaTime;
                    print("hitInfo.collider" + hitInfo.collider.gameObject.layer);
                    item = hitInfo.collider.gameObject.transform.parent.gameObject;
                if (hitInfo.collider.gameObject.layer == 6)
                    {
                        OnAbsorbCollider();
                        UpdateGetItem(item);
                     }
                }
      
        }
         
        if(Input.GetButtonDown("Fire1"))
        {
            if (!(emptyState))
            {
                //���콺 ������ keyMapping
           
              
                
                print("�߻�" + absorbItemTag);

                
                //1. �ε� ��ġ �ӽ�
                GameObject obj = Resources.Load<GameObject>(absorbItemTag);

                print("##########obj" + obj);
                Vector3 posZ = transform.position;
                posZ.z += 1;
                GameObject go = Instantiate(obj, posZ, Quaternion.identity);

                //2. �߻�ü ���� ���� ����..?

                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * power, ForceMode.Impulse);

                emptyState = true;
                absorbItemTag = null;

            }   
        }
        if (Input.GetButtonUp("Fire1") && isAbsorb)
        {
            print("FIRE");
       
        }


    }

    void UpdateGetItem(GameObject item)

    {
        isAbsorb = true;
        absorbItem = item;
        print("Game" + item);
        //���� üũ
        if (currTime > 1.5f)
        {
            float distance = Vector3.Distance(item.transform.position, transform.position); //�ڼ��� �Ÿ��� ��� �ڵ�.

            print("distance" + distance);
                item.transform.LookAt(transform.position);
                item.transform.Translate(Vector3.forward * speed * Time.deltaTime);
              
        }

    }

    void OnAbsorbCollider()
    {
        SphereCollider[] mesh_origin = GetComponents<SphereCollider>();
        SphereCollider mesh_child = absorbTrigger.GetComponent<SphereCollider>();
        for(int i = 0; i < mesh_origin.Length; i++)
        {
            mesh_origin[i].enabled = false;
        }
        mesh_child.enabled = true;

    }
    void OffAbsorbCollider()
    {
        SphereCollider[] mesh_origin = GetComponents<SphereCollider>();
        SphereCollider mesh_child = absorbTrigger.GetComponent<SphereCollider>();
        for (int i = 0; i < mesh_origin.Length; i++)
        {
            mesh_origin[i].enabled = true;
        }
        mesh_child.enabled = false;

    }
    
    //attack ��� ���� üũ
    //itemIventory();
    public void IsAttackItem(string absorbItemTag)
    {
        currTime = 0;
        emptyState = false;
        OffAbsorbCollider();
        switch (absorbItemTag)
        {
            case "e_ranger":
                //PlayerFire ���·� ����
                absorbItemTag = "MonsterPos";
                if (isAbsorb)
                {
                    isAbsorb = false;
                    print("���� �ִϸ��̼� + �ð� �帥 �� ranger�� ���� + ���� �Ѿ� ������ ���� ����");
                    GetComponent<PlayerController>().attackState = PlayerController.AttackState.RANGER;
                }
                else
                {
                    GetComponent<PlayerController>().attackState = PlayerController.AttackState.ABSORB;
                }
                //�Ź����ֱ�
                OffAbsorbCollider();
                break;

            default:
                //absorbCollider = GameObject.Find("AbsorbTrigger");
                //absorbCollider.GetComponent<SphereCollider>().enabled = false;
                //gameObject.GetComponent<SphereCollider>().enabled = true;
                break;
        }
    }

    //switch (item.tag)
    //{
    //    case "e_fox":
    //        print("e_fox ����");
    //        break;
    //    case "e_ranger":
    //        break;
    //}

}

    //private void OnTriggerEnter(Collider other)
    //{
 
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
    //    {
    //        //������ �з� �� ȹ��
    //        if(other.gameObject.tag == "item")
    //        {
             
    //        }
    //    }

    //    //print("other check" + other.gameObject);
    //    //print("layer check" + layer);

    //    if (other.gameObject == absorbItem)
    //    {
          
    //        absorbItemTag = other.gameObject.tag;
            
    //        //���� ������Ʈ ���� �� �Ҹ�
    //        Destroy(other.gameObject);
    //        currTime = 0;
    //        emptyState = false;
            
    //        IsAttackItem(absorbItemTag, isAbsorb);

    //    }
    //}