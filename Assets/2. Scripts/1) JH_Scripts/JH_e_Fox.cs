using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_e_Fox : MonoBehaviour
{
    public GameObject coin_Yellow;

    public int enemyHP = 100;

    public JH_w_Spawn mySpawner;

    public GameObject targetPlayer; // 감지할 플레이어 선택
    float distPlayer; // 플레이어와의 거리
    Vector3 dirPlayer; // 플레이어 바라보는 방향
    Vector3 playerPos;
    // Start is called before the first frame update

    AudioSource audioS;
    public ParticleSystem damage_FX;
    public AudioClip death_SFX;
    public AudioClip damage_SFX;

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
        
        audioS = GetComponent<AudioSource>();
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
            audioS.clip = death_SFX;
            audioS.PlayOneShot(audioS.clip,3f);

            Destroy(this.gameObject,0.12f);
        }
    }

    #region======몬스터 상태======

    public bool knockBack = false;

    void UpdateDie()
    {
        // 사망
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


    //점프 모션

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
            audioS.clip = damage_SFX;
            audioS.PlayOneShot(audioS.clip);

            rb.AddForce(-dirPlayer * 10f * Time.deltaTime, ForceMode.Impulse);
        }

        

        else if (collision.gameObject.tag == "Player")
        {
            //HP감소
            enemyHP -= 20;
            audioS.clip = damage_SFX;
            Instantiate(damage_FX, transform.position, Quaternion.identity);
            if(enemyHP >= 20)
            {
                
                audioS.PlayOneShot(audioS.clip, 0.8f);
            }
            if (enemyHP <= 0)
            {
                //audioS.clip = death_SFX;
                //audioS.PlayOneShot(audioS.clip, 3f);
                state = State.Die;
                Instantiate(coin_Yellow, transform.position, Quaternion.identity);
                JH_i_Coin_rot.instance.monsDrop = true;
                JH_ScoreManager.instance.COIN_SCORE++;

                

                Destroy(this.gameObject,0.2f);
                
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
