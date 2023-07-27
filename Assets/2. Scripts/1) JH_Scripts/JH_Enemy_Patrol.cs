using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2�������� ��ġ

// ��   ��Ʈ���� �׺�޽��� ������� �ʰ� ����
// 1.   ��Ʈ���ϰ��� �ϴ� ��ġ�� �����Ѵ�. -�Ϸ�
// 1-1. �迭 ����Ʈ�� ����� ��Ʈ�� �ϰ����ϴ� ��ġ�� �޴´�. -�Ϸ�
// 2.   ����Ʈ�� �ִ� ��Ʈ�� ��ġ�� �̵��Ѵ�. -�Ϸ�
// 2-1. ����Ʈ�� �ִ� ��Ʈ�� ��ġ�� ���������� �̵��Ѵ�. - �Ϸ�
// 2-2. �̵��Ҷ��� ���� ��Ʈ�� ��ġ�� ȸ���Ѵ�. - �Ϸ�
// 3.   ���������� �̵� �� ������ ���������� �ٽ� ù��° ����Ʈ�� ��ġ�� �̵��Ѵ�. - �Ϸ�
// 4.   ����, ���������� �̵� �߿� �÷��̾ ���� �Ǹ� �÷��̾�� �̵��Ѵ�. - �Ϸ�


public class JH_Enemy_Patrol : MonoBehaviour
{

    public int enemyHP = 100;

    public bool knockBack = false;
    Rigidbody rb;

    AudioSource damge_SFX;
    public ParticleSystem damage_FX;

    #region ����Ʈ ����
    public List<Transform> patrolPos; // ��Ʈ�� ��ġ�� ��̳Ŀ� ���� ���� �ø��� �ֵ��� �ۺ�����
    int listCount; // ��Ʈ�� ����Ʈ�� ���� �ľ�
    public int i = 0;
    #endregion

    public float enemySpeed = 2f;  // ��ȹ ������ ���ʹ� ���ǵ� ������ �� �ֵ��� ����

    public GameObject targetPlayer; // �÷��̾ ���������(���� �ȿ� ������)
    float targetDist; // �÷��̾� �Ÿ�
    Vector3 dirPlayer;
    Vector3 playerPos;

    float changeTime = 0;
    bool matChange = false;

    public Material mat_Mush;

    // Start is called before the first frame update
    void Start()
    {
        mat_Mush.color = new Color(255 / 255, 255 / 255f, 255 / 255f);
        listCount = patrolPos.Count; // ��Ʈ�� ����Ʈ�� ���� �ľ�
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        damge_SFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾� �Ÿ�
        targetDist = Vector3.Distance(targetPlayer.transform.position, gameObject.transform.position);

        dirPlayer = targetPlayer.transform.position - this.transform.position;
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);

        changeTime += Time.deltaTime;

        // ���� ���߿� �÷��̾ ������ ���� �÷��̾����� ���� �÷��̾�� �Ÿ��� ���ų� �־����� ���� ��Ʈ�� �̵�
        if (targetDist <= 4)
        {
            transform.rotation = Quaternion.LookRotation(playerPos);
            transform.Translate(-playerPos * 2f * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPlayer.transform.position, enemySpeed * Time.deltaTime);
        }

        //�Ÿ��� �־����� ���� ��Ʈ�� �̵�
        else if(targetDist > 4)
        {
            if (i <= listCount)
            {
                //// ����Ʈ(��Ʈ�� ��ġ)�� �̵��Ҷ� ȸ���Ͽ� ������ �ٶ󺸰� �ϰ�ʹ�.
                transform.LookAt(patrolPos[i]);
                // ����Ʈ�� �̵��ϰ�ʹ�.
                
                transform.position = Vector3.MoveTowards(gameObject.transform.position, patrolPos[i].transform.position, enemySpeed * Time.deltaTime);

                //// patrolPos[i] i�� �ٲ��� ��
                if (transform.position == patrolPos[i].position) // ����, ���� ��ġ�� ������ ��ġ���� ���ԵǸ�
                {
                    i++; // i�� 1�� �����־� ���� �������� ������ ����

                    
                    if (i >= listCount) // ����, ����Ʈ �������� �����ϸ� �ٽ� ù��°�� �̵�
                    {
                        i = 0;
                    }
                }
            }
        }
        UpdateKnockBack();

        if(enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            //OnDamage();
            enemyHP -= 50;
            //state = State.KnockBack;
            knockBack = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            mat_Mush.color = new Color(255 / 255, 0/ 255f, 255 / 255f);
            changeTime = 0;
            matChange = true;


            rb.AddForce(-dirPlayer * 10f * Time.deltaTime, ForceMode.Impulse);
        }

        if (collision.gameObject.tag == "bubble" || collision.gameObject.tag == "bullet2")
        {
            enemyHP -= 100;
        }

        if (collision.gameObject.tag == "Player")
        {
            //HP����
            enemyHP -= 20;
            damge_SFX.PlayOneShot(damge_SFX.clip);
            Instantiate(damage_FX, transform.position, Quaternion.identity);

            if (enemyHP <= 0)
            {
                Destroy(this.gameObject);

            }

            knockBack = true;
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
            mat_Mush.color = new Color(255 / 255, 0 / 255f, 255 / 255f);
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
            mat_Mush.color = new Color(255 / 255, 255 / 255f, 255 / 255f);
            if (changeTime > 1.5f)
            {
                matChange = false;

                knockBack = false;
            }
        }

        if (targetDist <= 4 && knockBack == false)
        {
            //����
        }

        if (targetDist > 4)
        {
            knockBack = false;
            
        }
    }

    void OnDamage()
    {

        enemyHP -= 50;

        if (enemyHP <= 0)
        {

            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        mat_Mush.color = new Color(255 / 255, 255 / 255f, 255 / 255f);
    }
}
