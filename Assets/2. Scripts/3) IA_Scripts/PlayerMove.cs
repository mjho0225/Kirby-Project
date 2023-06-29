using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{


    public float jumpPower = 5f;
    public float speed = 5f;
    float maxHeight = 5f; //���� Ŀ�� ��ġ���� 4.5��
    int jumpCnt;
    int maxJumpCnt = 20;
    bool isGrounded = false;
    
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame 
    void Update()
    {


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        dir.y = 0; //�鸮�ų� �Ʒ��� �������� ���� ����
        dir.Normalize();
        //������ y �ӵ��� dir�� y�׸� �ݿ��ؾ� �Ѵ�.
        Vector3 velocity = dir * speed; //sp eed ���� yvelocity�� �������� �ʱ� ���� ���� ���
      //  velocity.y = yVelocity;
        transform.position += velocity * Time.deltaTime;
        UpdateJump();
      
    }

    void CheckHeight()
    {
       //Ŀ�� position�� ���ֱ� ray���� Ŀ�� ������Ʈ ��� ����.
       float PosDiffer = transform.position.y - (transform.lossyScale.y/2);
       print("belowPos" + PosDiffer);
       Vector3 belowPos = transform.position;
       belowPos.y = PosDiffer;
       Ray ray = new Ray(belowPos, Vector3.down);

       RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo))
        {
            print("hitInfo" + hitInfo.transform.tag);
            print("distance"+hitInfo.distance);
            if(hitInfo.transform.tag == "Ground")
            {
                // �ٴ��� �ִ�. ���� �� ��ġ���� �ٴڱ����� �Ÿ��� õ����̺��� ũ�ٸ�
                if (maxHeight < hitInfo.distance)
                {
                    rb.velocity = Vector3.zero;
                    print("maxHeight: " + maxHeight);
                    //isGrounded = false;
                    // õ�忡 ��Ҵ�
                    Vector3 pos = transform.position;
                    pos.y = maxHeight;
                    transform.position = pos;
                }
            }
           
        }
        else
        {
            // �ε��� ���� ����. ���
            print("���");
        }

    }


    void UpdateJump()
    {
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (jumpCnt >= maxJumpCnt)
            {
                CheckHeight();
                disableJump();
                return;
            }
            else
            {
                CheckHeight();
            }

            print("jumpCnt: " + jumpCnt);
            if(jumpCnt < 1){
                jumpPower = 7;
                print("������ �ִϸ��̼�");
                rb.velocity = new Vector3(0, jumpPower, 0);
                jumpCnt++;
                
            }
            else if(jumpCnt >= 1)
            {
                print("���� �ִϸ��̼�");
                //���� ���� Ŀ���� 4~5��
                jumpPower = 2;
                rb.velocity = new Vector3(0, jumpPower, 0);
                jumpCnt++;
            }
  
        }

    }

    void disableJump()
    {
        isGrounded = false;
        jumpCnt = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            isGrounded = true;
            jumpCnt = 0;
        }

    }
}
