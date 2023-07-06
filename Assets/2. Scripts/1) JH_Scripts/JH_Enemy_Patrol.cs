using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2스테이지 배치

// ★   패트롤을 네브메쉬를 사용하지 않고 구현
// 1.   패트롤하고자 하는 위치를 저장한다. -완료
// 1-1. 배열 리스트를 만들어 패트롤 하고자하는 위치를 받는다. -완료
// 2.   리스트에 있는 패트롤 위치로 이동한다. -완료
// 2-1. 리스트에 있는 패트롤 위치로 순차적으로 이동한다. - 완료
// 2-2. 이동할때는 다음 패트롤 위치로 회전한다. - 완료
// 3.   순차적으로 이동 후 마지막 지점에서는 다시 첫번째 리스트의 위치로 이동한다. - 완료
// 4.   만약, 순차적으로 이동 중에 플레이어가 감지 되면 플레이어에게 이동한다. - 완료


public class JH_Enemy_Patrol : MonoBehaviour
{

    public int enemyHP = 100;

    public bool knockBack = false;
    Rigidbody rb;

    #region 리스트 관련
    public List<Transform> patrolPos; // 패트롤 위치가 몇개이냐에 따라 쉽게 늘릴수 있도록 퍼블릭으로
    int listCount; // 패트롤 리스트의 개수 파악
    public int i = 0;
    #endregion

    public float enemySpeed = 3f;  // 기획 팀에서 에너미 스피드 변경할 수 있도록 설정

    public GameObject targetPlayer; // 플레이어가 가까워지면(범위 안에 들어오면)
    float targetDist; // 플레이어 거리
    Vector3 dirPlayer;
    Vector3 playerPos;

    float changeTime = 0;
    bool matChange = false;

    // Start is called before the first frame update
    void Start()
    {
        listCount = patrolPos.Count; // 패트롤 리스트의 개수 파악
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 거리
        targetDist = Vector3.Distance(targetPlayer.transform.position, gameObject.transform.position);

        dirPlayer = targetPlayer.transform.position - this.transform.position;
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);

        changeTime += Time.deltaTime;

        // 순찰 도중에 플레이어가 가까이 오면 플레이어한테 가고 플레이어아 거리에 없거나 멀어지면 기존 패트롤 이동
        if (targetDist <= 4)
        {
            transform.rotation = Quaternion.LookRotation(playerPos);
            transform.Translate(-playerPos * 2f * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPlayer.transform.position, enemySpeed * Time.deltaTime);
        }

        //거리가 멀어지면 기존 패트롤 이동
        else if(targetDist > 4)
        {
            if (i <= listCount)
            {
                //// 리스트(패트롤 위치)로 이동할때 회전하여 정면을 바라보게 하고싶다.
                transform.LookAt(patrolPos[i]);
                // 리스트로 이동하고싶다.
                
                transform.position = Vector3.MoveTowards(gameObject.transform.position, patrolPos[i].transform.position, enemySpeed * Time.deltaTime);

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
        UpdateKnockBack();

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            OnDamage();
        }

        if (collision.gameObject.tag == "Player")
        {
            //HP감소
            enemyHP -= 20;
            if (enemyHP <= 0)
            {
                Destroy(this.gameObject);

            }

            knockBack = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            changeTime = 0;
            matChange = true;

            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10f), 10f * Time.deltaTime);
            rb.AddForce(-dirPlayer * 200f * Time.deltaTime, ForceMode.Impulse);
        }


    }
    private void UpdateKnockBack()
    {
        if (changeTime >= 0.2f && matChange == true)
        {
            transform.GetComponent<MeshRenderer>().material.color = Color.red;
            if (changeTime > 1.5f)
            {
                matChange = false;

                knockBack = false;
            }
        }

        if (targetDist <= 4 && knockBack == false)
        {
            //공격
        }

        if (targetDist > 4)
        {
            knockBack = false;
            
        }
    }

    void OnDamage()
    {

        enemyHP -= 50;

        if (enemyHP <= 0)
        {

            Destroy(this);
        }
    }
}
