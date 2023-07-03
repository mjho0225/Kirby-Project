using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_e_Fox : MonoBehaviour
{
    public GameObject targetPlayer; // ������ �÷��̾� ����
    float distPlayer; // �÷��̾���� �Ÿ�
    Vector3 dirPlayer; // �÷��̾� �ٶ󺸴� ����
    Vector3 playerPos;
    // Start is called before the first frame update

    
    float changeTime = 0; // ���� �� ���
    bool currentChage = false; // ���� ���� ���� false = �������� ���� ���� | true = ���� ����
    bool matChange = false;

    Rigidbody rb;

    public enum State
    {
        Idle,
        Attack,
        KnockBack,
    }
    public State state;

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
            case State.KnockBack: UpdateKnockBack();
                break;
        }

        dirPlayer = targetPlayer.transform.position - transform.position;
        distPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);

        changeTime += Time.deltaTime;

        //if (distPlayer <= 7)
        //{
        //    StartCoroutine("Jump");

        //    transform.rotation = Quaternion.LookRotation(playerPos);
        //    //transform.Translate(-playerPos * 0.5f * Time.deltaTime);
        //    transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPlayer.transform.position, 5f * Time.deltaTime);
        //}


        //if (changeTime >= 0.2f && matChange == true)
        //{
        //    transform.GetComponent<MeshRenderer>().material.color = Color.red;
        //    matChange = false;
        //}
    }

    public bool knockBack = false;

    private void UpdateKnockBack()
    {
        if (changeTime >= 0.2f && matChange == true)
        {
            transform.GetComponent<MeshRenderer>().material.color = Color.red;
            if(changeTime > 1.5f)
            {
            matChange = false;

            knockBack = false;
            }
        }

        if (distPlayer <= 7 && knockBack == false)
        {
            state = State.Attack;
        }

        if (distPlayer > 7 )
        {
            knockBack = false;
            state = State.Idle;
        }
    }

    private void UpdateAttack()
    {
        
        if (distPlayer <= 7 && knockBack == false)
        {
            StartCoroutine("Jump");

            transform.rotation = Quaternion.LookRotation(playerPos);
            //transform.Translate(-playerPos * 0.5f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPlayer.transform.position, 5f * Time.deltaTime);
        }
        else if(distPlayer > 7 && knockBack == false)
        {
            state = State.Idle;
        }
    }

    private void UpdateIdle()
    {
        transform.rotation = Quaternion.Euler(0, -playerPos.y, 0);

        if (distPlayer <= 7)
        {
            state = State.Attack;
        }

        else if (distPlayer > 7)
        {
            state = State.Idle;
        }
    }


    //���� ���

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.1f);

        rb.AddForce(transform.up * 5f * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //HP����
            state = State.KnockBack;
            knockBack = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            changeTime = 0;
            matChange = true;

            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10f), 10f * Time.deltaTime);
            rb.AddForce(-dirPlayer * 500f * Time.deltaTime, ForceMode.Impulse);
        }
        //if (distPlayer <= 7)
        //{
        //    state = State.Attack;
        //}

        //else if (distPlayer > 7)
        //{
        //    state = State.Idle;
        //}
    }
}
