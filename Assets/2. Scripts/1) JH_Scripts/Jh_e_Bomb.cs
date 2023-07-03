using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 1. 플레이어 감지
// 1-1. 플레이어를 바라본다.
// 2.폭탄을 쏜다.



public class Jh_e_Bomb : MonoBehaviour
{

    enum State
    {
        Idle,
        Attack,
    }
    State state;

    public GameObject targetPlayer; // 감지할 플레이어 선택
    float distPlayer; // 플레이어와의 거리
    Vector3 dirPlayer; // 플레이어 바라보는 방향
    Vector3 playerPos;

    public int attackRange = 5;

    public GameObject bomb;
    public GameObject firePos;

    float changeTime = 0;
    float attackTime = 0;

    bool matChange = false;

    public bool knockBack = false;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle: UpdateIdle();
                break;
            case State.Attack: UpdateAttack();
                break;
        }


        dirPlayer = targetPlayer.transform.position - transform.position;
        distPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);

        changeTime += Time.deltaTime;
        attackTime += Time.deltaTime;

    }




    private void UpdateIdle()
    {
        

        if(distPlayer <= attackRange)
        {
            state = State.Attack;
        }



    }

    private void UpdateAttack()
    {
        if(distPlayer > attackRange)
        {
            state = State.Idle;
        }

        if(attackTime > 3)
        {
            //폭탄 발사
            transform.rotation = Quaternion.LookRotation(playerPos);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPlayer.transform.position, 1f * Time.deltaTime);

            bomb.transform.position = firePos.transform.position;
            bomb.transform.forward = firePos.transform.forward;
            Instantiate(bomb);

            attackTime = 0;
        }

    }
}
