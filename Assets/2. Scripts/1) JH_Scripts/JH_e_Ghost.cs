using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���ڸ� ȸ��
// ȸ���ϸ鼭 �̵�
// �÷��̾� ����
// 



public class JH_e_Ghost : MonoBehaviour
{

    public List<Transform> patrol_ghostPos;

    public GameObject ghostRot; // ȸ���ؾ��ϴ� ��Ʈ ��������

    public GameObject targetPlayer; // ������ �÷��̾� ����
    float distPlayer; // �÷��̾���� �Ÿ�
    Vector3 dirPlayer; // �÷��̾� �ٶ󺸴� ����
    Vector3 playerPos;
    // Start is called before the first frame update


    float changeTime = 0; // ���� �� ���
    bool matChange = false;


    int listCount; // ��Ʈ�� ����Ʈ�� ���� �ľ�
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
            //// ����Ʈ(��Ʈ�� ��ġ)�� �̵��Ҷ� ȸ���Ͽ� ������ �ٶ󺸰� �ϰ�ʹ�.
            
            // ����Ʈ�� �̵��ϰ�ʹ�.
            transform.position = Vector3.MoveTowards(gameObject.transform.position, patrol_ghostPos[i].transform.position, 3f * Time.deltaTime);

            //// patrolPos[i] i�� �ٲ��� ��
            if (transform.position == patrol_ghostPos[i].position) // ����, ���� ��ġ�� ������ ��ġ���� ���ԵǸ�
            {
                transform.LookAt(patrol_ghostPos[i]);
                i++; // i�� 1�� �����־� ���� �������� ������ ����
                if (i >= listCount) // ����, ����Ʈ �������� �����ϸ� �ٽ� ù��°�� �̵�
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
