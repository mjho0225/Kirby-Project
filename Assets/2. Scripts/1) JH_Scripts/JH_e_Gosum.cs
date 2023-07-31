using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JH_e_Gosum : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject coin_Green;

    public int enemyHP = 100;

    public GameObject targetPlayer;
    public GameObject bullet;
    float distPlayer;
    float distBullet;
    Vector3 dirPlayer;
    Vector3 bulletDir;
    Vector3 playerPos;

    float changeTime = 0; // ���� �� ���
    //float scaleTime = 0; // ������ Ŀ���� �� ���
    public float changeColor = 0;
    public bool currentChage = false; // ���� ���� ���� false = �������� ���� ���� | true = ���� ����
    bool matChange = false;

    Rigidbody rb;
    public bool knockBack = false;

    AudioSource damge_SFX;
    public ParticleSystem damage_FX;

    public Material matGosum;


    // Start is called before the first frame update
    void Start()
    {
        damge_SFX = GetComponent<AudioSource>();

        matGosum.color = new Color(255 / 255, 255 / 255f, 255 / 255f);
        agent = GetComponent<NavMeshAgent>();

        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ŀ���� �۾����� �ϴ� ��� >> Ŀ���� 3�� �ִٰ� �۾���
        changeTime += Time.deltaTime;
        changeColor += Time.deltaTime;

        //dirPlayer = targetPlayer.transform.position - this.transform.position;
        dirPlayer = targetPlayer.transform.position - this.transform.position;
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);
        distPlayer = Vector3.Distance(targetPlayer.transform.position, gameObject.transform.position);

        if (distPlayer < 7)
        {
            if (changeTime >= 3) //3���ִٰ� Ŀ��
            {
                ChangeBig(); //ũ�� ����
                
            }

        }
        if (changeTime >= 6 && currentChage == true) // 3���ִٰ� �۾���
        {
            ChangeSmall(); // ���� Ǯ�� > �۰� ����

            changeTime = 0; // ������ Ǯ������ �ʱ�ȭ
        }


        if (changeColor >= 0.2f && matChange == true)
        {
            transform.GetComponent<MeshRenderer>().material.color = Color.red;
            matGosum.color = new Color(255 / 255, 255 / 255f, 255 / 255f);
            matChange = false;

        }

        //ry = dirPlayer.y;
        if (distPlayer <= 7)
        {
            //transform.rotation = Quaternion.LookRotation(dirPlayer); //��������
            transform.rotation = Quaternion.LookRotation(playerPos); //���� ����
            
            //transform.Translate(dirPlayer * 0.2f * Time.deltaTime);
            transform.Translate(-playerPos * 0.2f * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position,targetPlayer.transform.position,3*Time.deltaTime);
        }

        if(enemyHP <= 0)
        {
            
            Instantiate(coin_Green, transform.position, Quaternion.identity);
            JH_i_Coin_rot.instance.monsDrop = true;
            JH_ScoreManager.instance.COIN_SCORE += 5;
            Destroy(this.gameObject);

        }

        if(knockBack == true)
        {
            UpdateKnockBack();
            
        }
        

    }



    void ChangeBig()
    {
        //���� ���� ��Ÿ���� > Ŀ���ִ� ���� > �÷��̾�κ��� �ǰ��� ���� �ʱ� ���ؤ�
        currentChage = true;
        //scaleTime += Time.deltaTime;
        transform.localScale = new Vector3(4, 4, 4);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //transform.Translate(0, 2, 0);

    }

    void ChangeSmall()
    {
        currentChage = false;
        transform.localScale = new Vector3(2, 2, 2);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }


    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player" && currentChage == false)
        {
            enemyHP -= 20;
            damge_SFX.PlayOneShot(damge_SFX.clip);
            Instantiate(damage_FX, transform.position, Quaternion.identity);
            if (enemyHP <= 0)
            {
                Instantiate(coin_Green, transform.position, Quaternion.identity);
                JH_i_Coin_rot.instance.monsDrop = true;
                JH_ScoreManager.instance.COIN_SCORE += 5;
                Destroy(this.gameObject);

            }

            //print("�浹");
            knockBack = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            changeColor = 0;
            matChange = true;

            matGosum.color = new Color(255 / 255, 0 / 255f, 0 / 255f);
            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 10f), 10f * Time.deltaTime);
            rb.AddForce(-dirPlayer * 200f * Time.deltaTime, ForceMode.Impulse);
        }

        if (collision.gameObject.tag == "bullet" && currentChage == false)
        {
            //OnDamage();
            enemyHP -= 20;
            //state = State.KnockBack;
            knockBack = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            changeColor = 0;
            matChange = true;
            damge_SFX.PlayOneShot(damge_SFX.clip);
            matGosum.color = new Color(255 / 255, 0 / 255f, 0 / 255f);
            rb.AddForce(-dirPlayer * 10f * Time.deltaTime, ForceMode.Impulse);
        }

        if ((collision.gameObject.tag == "bubble" || collision.gameObject.tag == "bullet2")&&currentChage == false)
        {
            enemyHP -= 100;
            knockBack = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            changeTime = 0;
            matChange = true;
            damge_SFX.PlayOneShot(damge_SFX.clip);
            matGosum.color = new Color(255 / 255, 0 / 255f, 0 / 255f);
        }

    }

    void UpdateKnockBack()
    {
        if (changeColor >= 0.2f && matChange == true)
        {


            
            transform.GetComponent<MeshRenderer>().material.color = Color.red;
            
            if (changeColor > 1.5f)
            {
                matChange = false;

                knockBack = false;
            }
        }


    }

    private void OnDestroy()
    {
        matGosum.color = new Color(255 / 255, 255 / 255f, 255 / 255f);
    }
}
