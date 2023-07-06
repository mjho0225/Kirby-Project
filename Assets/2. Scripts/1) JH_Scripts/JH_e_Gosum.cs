using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_e_Gosum : MonoBehaviour
{
    int enemyHP = 100;

    public GameObject targetPlayer;
    public GameObject bullet;
    float distPlayer;
    float distBullet;
    Vector3 dirPlayer;
    Vector3 bulletDir;
    Vector3 playerPos;

    float changeTime = 0; // ���� �� ���
    //float scaleTime = 0; // ������ Ŀ���� �� ���
    float changeColor = 0;
    bool currentChage = false; // ���� ���� ���� false = �������� ���� ���� | true = ���� ����
    bool matChange = false;

    Rigidbody rb;
    public bool knockBack = false;


    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ŀ���� �۾����� �ϴ� ��� >> Ŀ���� 3�� �ִٰ� �۾���
        changeTime += Time.deltaTime;
        changeColor += Time.deltaTime;

        if (changeTime >= 3) //3���ִٰ� Ŀ��
        {
            ChangeBig(); //ũ�� ����
            if (changeTime >= 6) // 3���ִٰ� �۾���
            {
                ChangeSmall(); // ���� Ǯ�� > �۰� ����

                changeTime = 0; // ������ Ǯ������ �ʱ�ȭ
            }
        }

        if (changeTime >= 0.2f && matChange == true)
        {
            transform.GetComponent<MeshRenderer>().material.color = Color.red;
            matChange = false;
        }

        //dirPlayer = targetPlayer.transform.position - this.transform.position;
        dirPlayer = targetPlayer.transform.position - this.transform.position;
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);
        distPlayer = Vector3.Distance(targetPlayer.transform.position, gameObject.transform.position);

        //ry = dirPlayer.y;
        if (distPlayer <= 4)
        {
            //transform.rotation = Quaternion.LookRotation(dirPlayer); //��������
            transform.rotation = Quaternion.LookRotation(playerPos); //���� ����
            //transform.Translate(dirPlayer * 0.2f * Time.deltaTime);
            transform.Translate(-playerPos * 0.2f * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position,targetPlayer.transform.position,3*Time.deltaTime);
        }

        UpdateKnockBack();

    }



    void ChangeBig()
    {
        //���� ���� ��Ÿ���� > Ŀ���ִ� ���� > �÷��̾�κ��� �ǰ��� ���� �ʱ� ���ؤ�
        currentChage = true;
        //scaleTime += Time.deltaTime;
        transform.localScale = new Vector3(4, 4, 4);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    void ChangeSmall()
    {
        currentChage = false;
        transform.localScale = new Vector3(2, 2, 2);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && currentChage == false)
        {
            //HP����

            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            changeTime = 0;
            matChange = true;

            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 2), 10 * Time.deltaTime);
            rb.AddForce(-dirPlayer * 250f * Time.deltaTime, ForceMode.Impulse);
        }
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
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

    void UpdateKnockBack()
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


    }
}
