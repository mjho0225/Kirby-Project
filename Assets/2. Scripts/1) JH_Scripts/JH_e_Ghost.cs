using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 제자리 회전
// 회전하면서 이동
// 플레이어 감지
// 



public class JH_e_Ghost : MonoBehaviour
{

    public List<Transform> patrol_ghostPos;

    public GameObject ghostRot; // 회전해야하는 고스트 가져오기

    public GameObject targetPlayer; // 감지할 플레이어 선택
    float distPlayer; // 플레이어와의 거리
    Vector3 dirPlayer; // 플레이어 바라보는 방향
    Vector3 playerPos;
    // Start is called before the first frame update


    float changeTime = 0; // 변신 초 계산
    bool matChange = false;


    int listCount; // 패트롤 리스트의 개수 파악
    public int i = 0;

    Rigidbody rb;

    public enum State
    {
        Idle,
        Attack,
        KnockBack,
        Patrol,
    }
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();

        listCount = patrol_ghostPos.Count;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                UpdateIdle();
                break;
            case State.Attack:
                UpdateAttack();
                break;
            case State.KnockBack:
                UpdateKnockBack();
                break;
            case State.Patrol:
                UpdatePatrol();
                break;
        }

        dirPlayer = targetPlayer.transform.position - transform.position;
        distPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);

        changeTime += Time.deltaTime;
        ghostRot.transform.Rotate(Vector3.up * 200f * Time.deltaTime);
    }


    
    private void UpdatePatrol()
    {
        transform.rotation = Quaternion.Euler(0, -playerPos.y, 0);
        if (distPlayer <= 5)
        {
            state = State.Attack;
        }

        if (distPlayer > 5)
        {
            state = State.Patrol;
        }

        if (i <= listCount)
        {
            //// 리스트(패트롤 위치)로 이동할때 회전하여 정면을 바라보게 하고싶다.
            
            // 리스트로 이동하고싶다.
            transform.position = Vector3.MoveTowards(gameObject.transform.position, patrol_ghostPos[i].transform.position, 3f * Time.deltaTime);

            //// patrolPos[i] i가 바뀌어야 함
            if (transform.position == patrol_ghostPos[i].position) // 만약, 현재 위치가 목적지 위치까지 오게되면
            {
                transform.LookAt(patrol_ghostPos[i]);
                i++; // i에 1을 더해주어 다음 목적지로 가도록 설정
                if (i >= listCount) // 만약, 리스트 마지막에 도착하면 다시 첫번째로 이동
                {
                    i = 0;
                }
            }
        }

    }

    public bool knockBack = false;

    private void UpdateKnockBack()
    {
        if (changeTime >= 0.2f && matChange == true)
        {
            ghostRot.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            if (changeTime > 1.5f)
            {
                matChange = false;

                knockBack = false;
            }
        }

        if (distPlayer <= 5 && knockBack == false)
        {
            state = State.Attack;
        }

        if (distPlayer > 5)
        {
            knockBack = false;
            state = State.Idle;
        }
    }

    private void UpdateAttack()
    {
        if(distPlayer <= 5)
        {
            transform.rotation = Quaternion.LookRotation(playerPos);
            //transform.Translate(-playerPos * 0.5f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPlayer.transform.position, 2f * Time.deltaTime);
        }

        if (distPlayer > 5)
        {
            state = State.Patrol;
        }
    }

    private void UpdateIdle()
    {
        transform.rotation = Quaternion.Euler(0, -playerPos.y, 0);

        if (distPlayer > 5)
        {
            state = State.Patrol;
        }
        else if (distPlayer <= 5)
        {
            state = State.Attack;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            state = State.KnockBack;
            knockBack = true;
            ghostRot.transform.GetComponent<MeshRenderer>().material.color = Color.white;
            changeTime = 0;
            matChange = true;

            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10f), 10f * Time.deltaTime);
            rb.AddForce(-dirPlayer * 500f * Time.deltaTime, ForceMode.Impulse);
        }
    }

}
