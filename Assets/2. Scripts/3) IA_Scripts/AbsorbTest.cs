using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbTest : MonoBehaviour
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
        print("흡수 동작");
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
            //float.MaxValue, layer => raycast 거리 제한 가능
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

        if (Input.GetButtonDown("Fire1"))
        {
            if (!(emptyState))
            {
                //마우스 포인터 keyMapping



                print("발사" + absorbItemTag);


                //1. 로드 위치 임시
                GameObject obj = Resources.Load<GameObject>(absorbItemTag);

                print("##########obj" + obj);
                Vector3 posZ = transform.position;
                posZ.z += 1;
                GameObject go = Instantiate(obj, posZ, Quaternion.identity);

                //2. 발사체 몬스터 따로 구현..?

                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * power, ForceMode.Impulse);

                emptyState = true;
                absorbItemTag = null;

            }
        }
        //if (Input.GetButtonUp("Fire1") && isAbsorb)
        //{
        //    print("FIRE");

        //}


    }

    void UpdateGetItem(GameObject item)

    {
        isAbsorb = true;
        absorbItem = item;
        print("Game" + item);
        //상태 체크
        float distance = Vector3.Distance(item.transform.position, transform.position); //자석의 거리를 재는 코드.
        print("distance" + distance);
        if (currTime > 1.5f && distance > 5f)
        {
            item.transform.LookAt(transform.position);
            item.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (currTime > 1.5f && distance < 5f)
        {
            Vector3 dir = transform.position;
            item.transform.LookAt(transform.position);
            item.transform.position = Vector3.Lerp(item.transform.position, dir, 1f);

        }


    }

    void OnAbsorbCollider()
    {
        SphereCollider[] mesh_origin = GetComponents<SphereCollider>();
        SphereCollider mesh_child = absorbTrigger.GetComponent<SphereCollider>();
        for (int i = 0; i < mesh_origin.Length; i++)
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

    //attack 사용 상태 체크
    //itemIventory();
    public void IsAttackItem(string absorbItemTag)
    {
        currTime = 0;
        emptyState = false;
        OffAbsorbCollider();
        switch (absorbItemTag)
        {
            case "e_ranger":
                //PlayerFire 상태로 변경
                absorbItemTag = "MonsterPos";
                if (isAbsorb)
                {
                    isAbsorb = false;
                    print("변신 애니메이션 + 시간 흐른 후 ranger로 변경 + 현재 총알 나가는 오류 있음");
                    GetComponent<PlayerController>().attackState = PlayerController.AttackState.RANGER;
                }
                else
                {
                    GetComponent<PlayerController>().attackState = PlayerController.AttackState.ABSORB;
                }
                //매번해주기
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
    //        print("e_fox 흡입");
    //        break;
    //    case "e_ranger":
    //        break;
    //}

}

    //private void OnTriggerEnter(Collider other)
    //{
 
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
    //    {
    //        //아이템 분류 후 획득
    //        if(other.gameObject.tag == "item")
    //        {
             
    //        }
    //    }

    //    //print("other check" + other.gameObject);
    //    //print("layer check" + layer);

    //    if (other.gameObject == absorbItem)
    //    {
          
    //        absorbItemTag = other.gameObject.tag;
            
    //        //게임 오브젝트 흡입 후 소멸
    //        Destroy(other.gameObject);
    //        currTime = 0;
    //        emptyState = false;
            
    //        IsAttackItem(absorbItemTag, isAbsorb);

    //    }
    //}
