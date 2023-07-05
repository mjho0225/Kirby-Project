using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. �÷��̾ �����Ѵ�.
// 2. �÷��̾ �����ϸ� ������ ���´�.
// 2-1. ������ ���� �� �÷��̾ �ٶ󺻴�.
// 3. ���� ���.
// 3-1. ���� ��� �ڷ� ���´�.
// 4. �ٸ� �������� ���� ���� ��� ���´�.




public class JH_MovingRanger : MonoBehaviour
{
    public GameObject targetPlayer; // ������ �÷��̾� ����
    float distPlayer; // �÷��̾���� �Ÿ�
    Vector3 dirPlayer; // �÷��̾� �ٶ󺸴� ����
    Vector3 playerPos;
    Vector3 playerPosY;
    bool isRight = false;
    bool isLeft = false;
    bool isState = false;

    float hideTime = 0; // ���� �ð�
    public List<Transform> patrol_hidePos; // ��Ʈ�� ��ġ ����

    public GameObject gunRot; // �÷��̾ �������� �� �����ϱ� ���� ���Ʒ� ȸ����
    public GameObject firePos; // �Ѿ� ��� ��ġ
    public GameObject bullet; // �Ѿ�

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
            gunRot.transform.rotation = Quaternion.LookRotation(playerPosY); // �÷��̾��� y ��ġ ���� ���� ȸ��

            if (hideTime > 3 && isRight == false && isLeft == false && isState == false)
            {
                transform.position = Vector3.MoveTowards(gameObject.transform.position, patrol_hidePos[i].transform.position, 5 * Time.deltaTime);
                if (hideTime > 5)
                {
                    //�Ѿ˹߻�
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
                    //�Ѿ� �߻�
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
            //HP����
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
