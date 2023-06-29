using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{


    public float jumpPower = 5f;
    public float speed = 5f;
    float maxHeight = 5f; //현재 커비 위치에서 4.5배
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

        dir.y = 0; //들리거나 아래로 떨어지는 오류 방지
        dir.Normalize();
        //결정된 y 속도를 dir의 y항목에 반영해야 한다.
        Vector3 velocity = dir * speed; //sp eed 값이 yvelocity에 곱해지지 않기 위해 따로 계산
      //  velocity.y = yVelocity;
        transform.position += velocity * Time.deltaTime;
        UpdateJump();
      
    }

    void CheckHeight()
    {
       //커비 position값 빼주기 ray값이 커비 오브젝트 가운데 있음.
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
                // 바닥이 있다. 현재 내 위치에서 바닥까지의 거리가 천장높이보다 크다면
                if (maxHeight < hitInfo.distance)
                {
                    rb.velocity = Vector3.zero;
                    print("maxHeight: " + maxHeight);
                    //isGrounded = false;
                    // 천장에 닿았다
                    Vector3 pos = transform.position;
                    pos.y = maxHeight;
                    transform.position = pos;
                }
            }
           
        }
        else
        {
            // 부딪힌 것이 없다. 허공
            print("허공");
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
                print("구르기 애니메이션");
                rb.velocity = new Vector3(0, jumpPower, 0);
                jumpCnt++;
                
            }
            else if(jumpCnt >= 1)
            {
                print("날개 애니메이션");
                //점프 높이 커비의 4~5배
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
