using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Ranger : MonoBehaviour
{
    public GameObject targetPlayer; // ������ �÷��̾� ����
    float distPlayer; // �÷��̾���� �Ÿ�
    Vector3 dirPlayer; // �÷��̾� �ٶ󺸴� ����
    Vector3 playerPos;
    Vector3 playerPosY;


    public GameObject gunRot; // �÷��̾ �������� �� �����ϱ� ���� ���Ʒ� ȸ����
    public GameObject firePos; // �Ѿ� ��� ��ġ
    public GameObject bullet; // �Ѿ�

    float shootingTime = 0;

    public int enemyHP = 100;
    public int damageHP = 50;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dirPlayer = targetPlayer.transform.position - transform.position;
        distPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);
        playerPosY = new Vector3(dirPlayer.x, dirPlayer.y, dirPlayer.z);


        if(distPlayer > 5)
        {
            shootingTime += Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(playerPos);
            gunRot.transform.rotation = Quaternion.LookRotation(playerPosY); // �÷��̾��� y ��ġ ���� ���� ȸ��

            if (shootingTime > 3)
            {
                bullet.transform.position = firePos.transform.position;
                bullet.transform.forward = firePos.transform.forward;
                Instantiate(bullet);

                shootingTime = 0;
            }
        }
        

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            OnDamage();
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
}
