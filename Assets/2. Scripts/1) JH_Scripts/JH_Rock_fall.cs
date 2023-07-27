using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Rock_fall : MonoBehaviour
{
    public ParticleSystem fall;
    
    public bool fallen = false;

    //Rigidbody rb;
    public List<Transform> patrolPos; // ��Ʈ�� ��ġ�� ��̳Ŀ� ���� ���� �ø��� �ֵ��� �ۺ�����
    int listCount; // ��Ʈ�� ����Ʈ�� ���� �ľ�
    public int i = 0;

    public float rockSpeed;
    float fallSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rockSpeed = 4.5f;
        listCount = patrolPos.Count; // ��Ʈ�� ����Ʈ�� ���� �ľ�
        
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = -Vector3.forward * 100f * Time.deltaTime;
        //rb.AddForce(transform.forward * 100 * Time.deltaTime, ForceMode.Acceleration) ;
        if (JH_Rock_Spawn.instance.bigRock == true)
        {
            print("�ӵ� ����");
            rockSpeed = 20f;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, patrolPos[i].transform.position, rockSpeed * Time.deltaTime);
        }

        if (i <= listCount && JH_Rock_Spawn.instance.bigRock == false)
        {
            //// ����Ʈ(��Ʈ�� ��ġ)�� �̵��Ҷ� ȸ���Ͽ� ������ �ٶ󺸰� �ϰ�ʹ�.
            transform.LookAt(patrolPos[i]);
            // ����Ʈ�� �̵��ϰ�ʹ�.
            if (i > 0 && i <= 1)
            {
                rockSpeed = fallSpeed;
            }
            if (i >= 2)
            {
                rockSpeed = 4.5f;
                

            }
            

            if (i > 12 && i <= 13)
            {
                this.fallen = false;
                rockSpeed = fallSpeed;
            }
            if (i >= 14)
            {
                
                rockSpeed = 4.5f;
            }
            if (i >= 15)
            {
                
                rockSpeed = fallSpeed;
            }
            transform.position = Vector3.MoveTowards(gameObject.transform.position, patrolPos[i].transform.position, rockSpeed * Time.deltaTime);

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

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾� HP����
        if(collision.gameObject.tag == "Player")
        {
            PlayerHP.instance.HP -= 5;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground" && this.fallen == false)
        {
            //print("����");
            Vector3 fallPos = transform.position;
            fallPos = new Vector3(fallPos.x, fallPos.y - 2.2f, fallPos.z);
            Instantiate(fall, fallPos, Quaternion.identity);
            
            this.fallen = true;
        }
        if (other.gameObject.tag == "Player")
        {
            PlayerHP.instance.HP -= 5;
        }
    }

}
