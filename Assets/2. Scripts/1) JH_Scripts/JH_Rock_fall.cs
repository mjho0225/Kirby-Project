using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Rock_fall : MonoBehaviour
{
    //Rigidbody rb;
    public List<Transform> patrolPos; // ��Ʈ�� ��ġ�� ��̳Ŀ� ���� ���� �ø��� �ֵ��� �ۺ�����
    int listCount; // ��Ʈ�� ����Ʈ�� ���� �ľ�
    public int i = 0;

    float rockSpeed = 4.5f;
    float fallSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        listCount = patrolPos.Count; // ��Ʈ�� ����Ʈ�� ���� �ľ�
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = -Vector3.forward * 100f * Time.deltaTime;
        //rb.AddForce(transform.forward * 100 * Time.deltaTime, ForceMode.Acceleration) ;


        if (i <= listCount)
        {
            //// ����Ʈ(��Ʈ�� ��ġ)�� �̵��Ҷ� ȸ���Ͽ� ������ �ٶ󺸰� �ϰ�ʹ�.
            transform.LookAt(patrolPos[i]);
            // ����Ʈ�� �̵��ϰ�ʹ�.
            if(i>0 && i <= 1)
            {
                rockSpeed = fallSpeed;
            }
            if (i >= 2)
            {
                rockSpeed = 20f;
            }

            if (i>12 && i <= 13)
            {
                rockSpeed = fallSpeed;
            }
            if (i >= 14)
            {
                rockSpeed = 20f;
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
}
