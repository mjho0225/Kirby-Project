using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPull : MonoBehaviour
{

    float speed = 10f;
    Transform playerPos;
    //enemy ����
    //[SerializeField]
    public bool emptyState = false;
    GameObject player;
    GameObject item;
    float currTime;
    bool isPull = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    
    //Stay �ð��� ���� 1.5/5
    private void OnTriggerStay(Collider other)
    {
       

        item = other.gameObject;
        print("tagName" + item.tag);
        
        switch (item.tag)
        {
            case "e_fox":
                isPull = true;
                print("e_fox ����");
                break;
            case "e_ranger":
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            player = GameObject.FindWithTag("Player");
            playerPos = player.transform;
            currTime += Time.deltaTime;
            print(currTime);
            UpdateGetItem(item);
        }else
        {
            currTime = 0;
        }

    }


    void UpdateGetItem(GameObject item)

    {
        print("item"+ item);
            if (currTime > 2f && isPull)
            {
                float distance = Vector3.Distance(item.transform.position, playerPos.transform.position); //�ڼ��� �Ÿ��� ��� �ڵ�.
                print("distance" + distance);

                //if (distance <= 10.0f)
                //{
                    item.transform.LookAt(playerPos.transform.position);
                    item.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                //}
                //else
                //{
                    //��ƼŬ�� �۵�
                    //������ �ָ� �۵� �ȵ�.
                //}

                //item.transform.position = Vector3.MoveTowards(playerPos.position, item.transform.position, Time.deltaTime * speed);
                //item.transform.position = Vector3.MoveTowards(item.transform.position, playerPos.position, Time.deltaTime* speed);
                emptyState = true;
                print("Particle");
            //Destroy(other.gameObject, 5);

            }


    }
}
