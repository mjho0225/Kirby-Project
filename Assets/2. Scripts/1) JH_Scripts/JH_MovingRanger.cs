using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 플레이어를 감지한다.
// 2. 플레이어를 감지하면 옆으로 나온다.
// 2-1. 옆으로 나온 후 플레이어를 바라본다.
// 3. 총을 쏜다.
// 3-1. 총을 쏘고돌 뒤로 숨는다.
// 4. 다른 방향으로 나가 총을 쏘고 숨는다.




public class JH_MovingRanger : MonoBehaviour
{
    public GameObject targetPlayer; // 감지할 플레이어 선택
    float distPlayer; // 플레이어와의 거리
    Vector3 dirPlayer; // 플레이어 바라보는 방향
    Vector3 playerPos;
    Vector3 playerPosY;
    bool isRight = false;
    bool isLeft = false;
    bool isState = false;

    float hideTime = 0; // 숨는 시간
    public List<Transform> patrol_hidePos; // 패트롤 위치 지정

    public GameObject gunRot; // 플레이어가 점프했을 때 조준하기 위해 위아래 회전값
    public GameObject firePos; // 총알 쏘는 위치
    public GameObject bullet; // 총알

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
        playerPosY = new Vector3(dirPlayer.x, dirPlayer.y, dirPlayer.z) ;

        MovingShoot();
        changeTime += Time.deltaTime;
        UpdateKnockBack();
        
        //if (hideTime > 5 && hideTime < 5.1f)
        //{
        //    Instantiate(bullet);
        //    bullet.transform.position = firePos.transform.position;
        //    bullet.transform.forward = firePos.transform.forward;
        //}
    }


    int i = 1;

    void MovingShoot()
    {
        if (distPlayer < 10)
        {
            hideTime += Time.deltaTime;

            transform.rotation = Quaternion.LookRotation(playerPos);
            gunRot.transform.rotation = Quaternion.LookRotation(playerPosY); // 플레이어의 y 위치 값에 따라 회전

            if (hideTime > 3 && isRight == false && isLeft == false && isState == false)
            {
                transform.position = Vector3.MoveTowards(gameObject.transform.position, patrol_hidePos[i].transform.position, 5 * Time.deltaTime);
                if (hideTime > 5)
                {
                    //총알발사
                    bullet.transform.position = firePos.transform.position;
                    bullet.transform.forward = firePos.transform.forward;
                    Instantiate(bullet);
                    i = 0;
                    isRight = true;
                    hideTime = 0;
                }
            }
            if (hideTime > 3 && isRight == true && isLeft == false && isState == false)
            {
                transform.position = Vector3.MoveTowards(gameObject.transform.position, patrol_hidePos[i].transform.position, 5 * Time.deltaTime);

                if (hideTime > 5)
                {
                    isRight = false;
                    isLeft = false;
                    isState = true;
                    hideTime = 0;
                    i = 2;
                }

            }

            if (hideTime > 3 && isLeft == false && isRight == false && isState ==true)
            {
                transform.position = Vector3.MoveTowards(gameObject.transform.position, patrol_hidePos[i].transform.position, 5 * Time.deltaTime);
                
                if (hideTime > 5)
                {
                    //총알 발사
                    bullet.transform.position = firePos.transform.position;
                    bullet.transform.forward = firePos.transform.forward;
                    Instantiate(bullet);
                    i = 0;
                    isLeft = true;
                    hideTime = 0;

                }
            }
            if (hideTime > 3 && isLeft == true && isRight == false && isState == true)
            {
                i = 0;
                transform.position = Vector3.MoveTowards(gameObject.transform.position, patrol_hidePos[i].transform.position, 5 * Time.deltaTime);
                if (hideTime > 5)
                {
                    isRight = false;
                    isLeft = false;
                    isState = false;
                    hideTime = 0;
                    i = 1;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {


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



    void OnDamage()
    {

        enemyHP -= damageHP;

        if (enemyHP <= 0)
        {
            Destroy(this.gameObject);
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
