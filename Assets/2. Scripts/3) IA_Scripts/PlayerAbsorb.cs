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

    bool attackState = false;

    //int layer;
    float power = 10f;
    GameObject absorbItem;
    string absorbItemTag;
    // Start is called before the first frame update
    void Start()
    {
        
        emptyState = true;
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
                    //print("hitInfo.collider" + hitInfo.collider);
                    item = hitInfo.collider.gameObject.transform.parent.gameObject;
                if (hitInfo.collider.gameObject.layer == 8)
                    {
                        UpdateGetItem(item);
                     }
                }
         }
         
        if(Input.GetButtonDown("Fire1"))
        {
            if (!(emptyState))
            {
                //���콺 ������ keyMapping
                
                //Ŀ�� �������� ������ �ִٸ�
                attackState = true;
                absorbItemTag = null;
                
                print("�߻�" + absorbItemTag);

                
                //1. �ε� ��ġ �ӽ�
                GameObject obj = Resources.Load<GameObject>("MonsterPos");

                print("##########obj" + obj);
                Vector3 posZ = transform.position;
                posZ.z += 1;
                GameObject go = Instantiate(obj, posZ, Quaternion.identity);

                //2. �߻�ü ���� ���� ����..?

                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * power, ForceMode.Impulse);

                emptyState = true;
            }
            
        }

    }

    void UpdateGetItem(GameObject item)

    {
        print("Game" + item);
        //���� üũ
        if (currTime > 2f)
        {
            float distance = Vector3.Distance(item.transform.position, transform.position); //�ڼ��� �Ÿ��� ��� �ڵ�.

            print("distance" + distance);

            //if (distance <= 10.0f)
            //{
                item.transform.LookAt(transform.position);
                item.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                absorbItem = item;


            //}
            //else
            //{
            //��ƼŬ�� �۵�
            //������ �ָ� �۵� �ȵ�.
            //}
            //item.transform.position = Vector3.MoveTowards(playerPos.position, item.transform.position, Time.deltaTime * speed);

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            //������ �з� �� ȹ��
            if(other.gameObject.tag == "item")
            {
             
            }
        }

        //print("other check" + other.gameObject);
        //print("layer check" + layer);
        // if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))//&& !emptyState
        if (other.gameObject == absorbItem)
        {
            absorbItemTag = other.gameObject.tag;
            //���� ������Ʈ ���� �� �Ҹ�
            Destroy(other.gameObject, 0.2f);
            currTime = 0;
            emptyState = false;
        }
    }

    //attack ��� ���� üũ
    //itemIventory();
    void Attack()
    {

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
