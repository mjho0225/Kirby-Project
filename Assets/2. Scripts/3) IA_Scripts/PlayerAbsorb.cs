using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbsorb : MonoBehaviour
{

    float speed = 10f;
    Transform playerPos;
    //enemy ����
    //[SerializeField]
    public bool emptyState = false;
    GameObject player;
    GameObject item;
    float currTime;
    bool isEmpty = false;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckItem();
    }

    void CheckItem()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (Input.GetButton("Fire1"))
            {
                currTime += Time.deltaTime;
                print("hitInfo.collider" + hitInfo.collider);
                item = hitInfo.collider.gameObject;
                print("collider" + item);

                if (hitInfo.collider.gameObject.layer == 6)
                {
                    print(currTime);
                    UpdateGetItem(item);
                }
            }
        }
    }

    void UpdateGetItem(GameObject item)

    {
        //switch (item.tag)
        //{
        //    case "e_fox":
        //        print("e_fox ����");
        //        break;
        //    case "e_ranger":
        //        break;
        //}

        print("item" + item);
        //���� üũ
        if (currTime > 2f)
        {
            float distance = Vector3.Distance(item.transform.position, playerPos.transform.position); //�ڼ��� �Ÿ��� ��� �ڵ�.
           
            print("distance" + distance);

            if (distance <= 10.0f)
            {
                item.transform.LookAt(playerPos.transform.position);
                item.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
               //��ƼŬ�� �۵�
               //������ �ָ� �۵� �ȵ�.
            }
            //item.transform.position = Vector3.MoveTowards(playerPos.position, item.transform.position, Time.deltaTime * speed);
            //item.transform.position = Vector3.MoveTowards(item.transform.position, playerPos.position, Time.deltaTime* speed);
            emptyState = true;
            print("Particle");
            //Destroy(other.gameObject, 5);

        }

    }

    //attack ��� ���� üũ
    //itemIventory();
    void Attack()
    {
        
    }
        
}
