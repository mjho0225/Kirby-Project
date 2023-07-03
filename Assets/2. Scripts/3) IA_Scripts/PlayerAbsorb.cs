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
                //float.MaxValue, layer => raycast 거리 제한 가능
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
                //마우스 포인터 keyMapping
                
                //커비가 아이템을 가지고 있다면
                attackState = true;
                absorbItemTag = null;
                
                print("발사" + absorbItemTag);

                
                //1. 로드 위치 임시
                GameObject obj = Resources.Load<GameObject>("MonsterPos");

                print("##########obj" + obj);
                Vector3 posZ = transform.position;
                posZ.z += 1;
                GameObject go = Instantiate(obj, posZ, Quaternion.identity);

                //2. 발사체 몬스터 따로 구현..?

                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * power, ForceMode.Impulse);

                emptyState = true;
            }
            
        }

    }

    void UpdateGetItem(GameObject item)

    {
        print("Game" + item);
        //상태 체크
        if (currTime > 2f)
        {
            float distance = Vector3.Distance(item.transform.position, transform.position); //자석의 거리를 재는 코드.

            print("distance" + distance);

            //if (distance <= 10.0f)
            //{
                item.transform.LookAt(transform.position);
                item.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                absorbItem = item;


            //}
            //else
            //{
            //파티클만 작동
            //실제로 멀면 작동 안됨.
            //}
            //item.transform.position = Vector3.MoveTowards(playerPos.position, item.transform.position, Time.deltaTime * speed);

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            //아이템 분류 후 획득
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
            //게임 오브젝트 흡입 후 소멸
            Destroy(other.gameObject, 0.2f);
            currTime = 0;
            emptyState = false;
        }
    }

    //attack 사용 상태 체크
    //itemIventory();
    void Attack()
    {

    }

    //switch (item.tag)
    //{
    //    case "e_fox":
    //        print("e_fox 흡입");
    //        break;
    //    case "e_ranger":
    //        break;
    //}

}
