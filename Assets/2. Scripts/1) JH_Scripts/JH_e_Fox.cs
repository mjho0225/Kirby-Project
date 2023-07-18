using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_e_Fox : MonoBehaviour
{
    public GameObject coin_Yellow;

    public int enemyHP = 100;

    public JH_w_Spawn mySpawner;

    public GameObject targetPlayer; // ������ �÷��̾� ����
    float distPlayer; // �÷��̾���� �Ÿ�
    Vector3 dirPlayer; // �÷��̾� �ٶ󺸴� ����
    Vector3 playerPos;
    // Start is called before the first frame update

    public Material mat_Fox;
    
    float changeTime = 0; 
    bool matChange = false;

    Rigidbody rb;

    public enum State
    {
        Idle,
        Attack,
        KnockBack,
        Die,
    }
    public State state;

    void Start()
    {
        mat_Fox.color = new Color(255 / 255, 255 / 255f, 255 / 255f);
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
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
            case State.Die: UpdateDie();
                break;
        }

        dirPlayer = targetPlayer.transform.position - transform.position;
        distPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);

        changeTime += Time.deltaTime;

        if(enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    #region======���� ����======

    public bool knockBack = false;

    void UpdateDie()
    {
        // ���
    }

    private void UpdateKnockBack()
    {
        if (changeTime >= 0.2f && matChange == true)
        {
            transform.GetComponent<MeshRenderer>().material.color = Color.red;
            mat_Fox.color = new Color(255 / 255, 255 / 255f, 255 / 255f);
            if (changeTime > 1.5f)
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
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPlayer.transform.position, 2f * Time.deltaTime);
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
    #endregion

    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            //OnDamage();
            enemyHP -= 50;
            state = State.KnockBack;
            knockBack = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            mat_Fox.color = new Color(255 / 255, 0 / 255f, 0 / 255f);
            changeTime = 0;
            matChange = true;

            
            rb.AddForce(-dirPlayer * 10f * Time.deltaTime, ForceMode.Impulse);
        }

        

        else if (collision.gameObject.tag == "Player")
        {
            //HP����
            enemyHP -= 20;
            if (enemyHP <= 0)
            {
                state = State.Die;
                Instantiate(coin_Yellow, transform.position, Quaternion.identity);
                JH_i_Coin_rot.instance.monsDrop = true;
                JH_ScoreManager.instance.COIN_SCORE++;
                Destroy(this.gameObject);
                
            }

            state = State.KnockBack;
            knockBack = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            mat_Fox.color = new Color(255 / 255, 0 / 255f, 0 / 255f);
            changeTime = 0;
            matChange = true;

            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10f), 10f * Time.deltaTime);
            rb.AddForce(-dirPlayer * 200f * Time.deltaTime, ForceMode.Impulse);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bubble" || other.gameObject.tag == "bullet2")
        {
            enemyHP -= 100;

            state = State.KnockBack;
            knockBack = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            mat_Fox.color = new Color(255 / 255, 0 / 255f, 0 / 255f);
            changeTime = 0;
            matChange = true;


            rb.AddForce(-dirPlayer * 10f * Time.deltaTime, ForceMode.Impulse);
        }
    }


    //void OnDamage()
    //{

    //    enemyHP -= 50;

    //    if (enemyHP <= 0)
    //    {

    //        Destroy(this.gameObject);
    //    }
    //}

    private void OnDestroy()
    {
        
        mySpawner.DestroyedFox(this);
        mat_Fox.color = new Color(255 / 255, 255 / 255f, 255 / 255f);
    }
    

}
