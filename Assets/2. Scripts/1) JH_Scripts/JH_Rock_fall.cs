using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Rock_fall : MonoBehaviour
{
    public ParticleSystem fall;
    
    public bool fallen = false;

    //Rigidbody rb;
    public List<Transform> patrolPos; // 패트롤 위치가 몇개이냐에 따라 쉽게 늘릴수 있도록 퍼블릭으로
    int listCount; // 패트롤 리스트의 개수 파악
    public int i = 0;

    public float rockSpeed;
    float fallSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rockSpeed = 4.5f;
        listCount = patrolPos.Count; // 패트롤 리스트의 개수 파악
        
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = -Vector3.forward * 100f * Time.deltaTime;
        //rb.AddForce(transform.forward * 100 * Time.deltaTime, ForceMode.Acceleration) ;
        if (JH_Rock_Spawn.instance.bigRock == true)
        {
            print("속도 변경");
            rockSpeed = 20f;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, patrolPos[i].transform.position, rockSpeed * Time.deltaTime);
        }

        if (i <= listCount && JH_Rock_Spawn.instance.bigRock == false)
        {
            //// 리스트(패트롤 위치)로 이동할때 회전하여 정면을 바라보게 하고싶다.
            transform.LookAt(patrolPos[i]);
            // 리스트로 이동하고싶다.
            if (i > 0 && i <= 1)
            {
                rockSpeed = fallSpeed;
            }
            if (i >= 2)
            {
                rockSpeed = 4.5f;
                

            }
            

            if (i > 12 && i <= 13)
            {
                this.fallen = false;
                rockSpeed = fallSpeed;
            }
            if (i >= 14)
            {
                
                rockSpeed = 4.5f;
            }
            if (i >= 15)
            {
                
                rockSpeed = fallSpeed;
            }
            transform.position = Vector3.MoveTowards(gameObject.transform.position, patrolPos[i].transform.position, rockSpeed * Time.deltaTime);

            //// patrolPos[i] i가 바뀌어야 함
            if (transform.position == patrolPos[i].position) // 만약, 현재 위치가 목적지 위치까지 오게되면
            {
                i++; // i에 1을 더해주어 다음 목적지로 가도록 설정


                if (i >= listCount) // 만약, 리스트 마지막에 도착하면 다시 첫번째로 이동
                {
                    i = 0;
                }
            }
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어 HP감소
        if(collision.gameObject.tag == "Player")
        {
            PlayerHP.instance.HP -= 5;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground" && this.fallen == false)
        {
            //print("낙하");
            Vector3 fallPos = transform.position;
            fallPos = new Vector3(fallPos.x, fallPos.y - 2.2f, fallPos.z);
            Instantiate(fall, fallPos, Quaternion.identity);
            
            this.fallen = true;
        }
        if (other.gameObject.tag == "Player")
        {
            PlayerHP.instance.HP -= 5;
        }
    }

}
