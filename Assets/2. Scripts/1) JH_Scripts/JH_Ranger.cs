using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Ranger : MonoBehaviour
{
    public GameObject targetPlayer; // 감지할 플레이어 선택
    float distPlayer; // 플레이어와의 거리
    Vector3 dirPlayer; // 플레이어 바라보는 방향
    Vector3 playerPos;
    Vector3 playerPosY;


    public GameObject gunRot; // 플레이어가 점프했을 때 조준하기 위해 위아래 회전값
    public GameObject firePos; // 총알 쏘는 위치
    public GameObject bullet; // 총알

    float shootingTime = 0;

    public int enemyHP = 100;
    public int damageHP = 50;

    public bool knockBack = false;
    float changeTime = 0;
    bool matChange = false;

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
        dirPlayer = targetPlayer.transform.position - transform.position;
        distPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);
        playerPosY = new Vector3(dirPlayer.x, dirPlayer.y, dirPlayer.z);


        if(distPlayer < 5)
        {
            shootingTime += Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(playerPos);
            gunRot.transform.rotation = Quaternion.LookRotation(playerPosY); // 플레이어의 y 위치 값에 따라 회전

            if (shootingTime > 3)
            {
                bullet.transform.position = firePos.transform.position;
                bullet.transform.forward = firePos.transform.forward;
                Instantiate(bullet);

                shootingTime = 0;
            }
        }
        changeTime += Time.deltaTime;
        UpdateKnockBack();



    }



    void OnDamage()
    {

        enemyHP -= damageHP;

        if (enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            //OnDamage();
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


    }
}
