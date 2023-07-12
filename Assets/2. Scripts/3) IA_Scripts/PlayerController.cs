using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float h, v;
    bool space, shift, shiftUP;

    public float jumpPower = 5f;
    public float speed = 7f;
    float maxHeight = 4.5f; //현재 커비 위치에서 4.5배
    int jumpCnt;
    int maxJumpCnt = 20;
    bool isGrounded = false;
    bool isJump = false;
    bool isDumble = false;
    bool isDash = false;
    float dashCount = 0;
    [SerializeField] public bool isGuard = false;

    float currTime;
    int damage = 1;

    float rx;
    float ry;
    public float rotSpeed = 100;
    PlayerHP playerHP;
    Rigidbody rb;
    //임시
    GameObject tempDash;
    bool isLadder = false;

    bool isRanger = true;
    bool Fire2 = false;

    public GameObject absorbObj;
    public GameObject rangerObj;
    public GameObject carObj;

    public GameObject feetEffect;

    enum PlayerState
    {
        BASIC,
        GUARD
    }
    public enum AttackState
    {
        ABSORB,
        RANGER
    }


    PlayerState state;
    public AttackState attackState = AttackState.ABSORB;
    //public GameObject gun;
    PlayerFire playerFire;
    public Animator anim;
    void Start()
    {
        playerHP = GetComponent<PlayerHP>();
        rb = GetComponent<Rigidbody>();
        state = PlayerState.BASIC;
        tempDash = GameObject.Find("Temp");
        carObj.SetActive(false);
        print("anim"+ anim);

    }

    bool isAbsorbed;



    public void ChangeRanger()
    {
        attackState = AttackState.RANGER;

        print(rangerObj);
        //Destroy(absorbObj); 
        //GameObject obj = Resources.Load<GameObject>("Player_Ranger");
        //Instantiate(obj, transform.position, transform.rotation);
        rangerObj.SetActive(true);
        absorbObj.SetActive(false);
        PlayerAbsorb newAbsorb = absorbObj.GetComponent<PlayerAbsorb>();
        newAbsorb.Reset();

    }

    bool isMaking = false;
    IEnumerator makeFeetEffect()
    {
            
                isMaking = true;
            
                Vector3 posLeft = transform.position;
                posLeft.y = transform.position.y - (transform.lossyScale.y / 2) + 0.2f; //pos 값 체크
                posLeft.x = transform.position.x - 0.5f;
                GameObject left = Instantiate(feetEffect);
                left.transform.position = posLeft;
                yield return new WaitForSeconds(1);
                Vector3 posRight = transform.position;
                posRight.y = transform.position.y - (transform.lossyScale.y / 2) + 0.2f; //pos 값 체크
                posRight.x = transform.position.x + 0.5f;
                GameObject right = Instantiate(feetEffect);
                right.transform.position = posRight;
                yield return new WaitForSeconds(0.1f);
                isMaking = false;
           
    }

    void Update()
    {
        GetInput();
        if (isLadder)
        {
            rb.useGravity = false;
            MoveUp();
        }
        else
        {
            rb.useGravity = true;
            //차징샷 이동 금지
            Move();

        }

        if (state == PlayerState.BASIC)
        {
            Jump();
        }
        else if (state == PlayerState.GUARD)
        {
            Guard();
        }
    }
    public void ChangeAbsorb()
    {

        attackState = AttackState.ABSORB;
        rangerObj.SetActive(false);
        absorbObj.SetActive(true);
        //GetComponent<PlayerFire>().enabled = false;
        //GetComponent<PlayerAbsorb>().enabled = true;
        //gun.SetActive(false);
    }

    void GetInput()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        space = Input.GetButtonDown("Jump");
        if (space) state = PlayerState.BASIC;
        shift = Input.GetKey(KeyCode.LeftShift);
        if (shift) state = PlayerState.GUARD;
        shiftUP = Input.GetKeyUp(KeyCode.LeftShift);
        Fire2 = Input.GetButtonDown("Fire2");
    }


    void Move()
    {
       
        Vector3 dir = new Vector3(h, 0, v);
      
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();
        //결정된 y 속도를 dir의 y항목에 반영해야 한다.
        Vector3 velocity = dir * speed; //sp eed 값이 yvelocity에 곱해지지 않기 위해 따로 계산
        transform.position += velocity * Time.deltaTime;

        if (h != 0 || v != 0 && (!isJump))
        {
            print("test");
            anim.SetBool("isWalk", true);
            if (!isMaking)
            {
                StartCoroutine(makeFeetEffect());
            }
            //anim.SetTrigger("Walk");
        }
        else
        {
            anim.SetBool("isWalk", false);
            
        }

        if (h != 0 || v != 0 && !(isDumble))
        {
            //Mathf.Acos(Vector3.Dot(dir, body.transform.right));
            //body의 오른쪽 벡터와 가는 방향(dir) 의 각도를 구하자
            //Vector3.Dot //내적
            float angle = Vector3.Angle(transform.right, dir);
            //만약 그 각도가 90보다 작다면 오른쪽으로 회전하자
            if (angle < 90)
            {
                transform.Rotate(0, 200 * Time.deltaTime, 0); //현재각도에서 회전
            }
            else
            {
                transform.Rotate(0, -200 * Time.deltaTime, 0);
            }
            //그렇지 않으면 왼쪽으로 회전

        }
    }

    void MoveUp()
    {

        print("MoveUP");

        bool upKey = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool upKeyU = Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W);
        bool downKey = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        if (upKey)
        {

            Vector3 dir = transform.position;
            //dir = Camera.main.transform.TransformDirection(dir);
            dir.y += 0.05f;
            print("dir.y" + dir.y);
            transform.position = Vector3.Lerp(transform.position, dir, 1f);

            //transform.position += velocity * Time.deltaTime;
        }
        else if (downKey)
        {
            Vector3 dir = transform.position;
            //dir = Camera.main.transform.TransformDirection(dir);
            dir.y -= 0.01f;
            print("dir.y" + dir.y);
            transform.position = Vector3.Lerp(transform.position, dir, 1f);
            //transform.position = Vector3.Lerp(transform.position, -dir, 0.5f);
        }

        //중력이 없는 상태에서 멈추기
    }


    void Jump()
    {


        if (space && isGrounded)
        {
            currTime += Time.deltaTime;
            isJump = true;

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

            if (jumpCnt < 1)
            {
                jumpPower = 8;
                rb.drag = 0;
                print("구르기 애니메이션");
                rb.velocity = new Vector3(0, jumpPower, 0);
                jumpCnt++;
                //TIMELINE 구르기 애니메이션 만들기
                anim.SetTrigger("Jump");
                
                

                //anim.SetTrigger("Jump01");
            }
            else if (jumpCnt >= 1)
            {
                //&& !(GetComponentInChildren<PlayerAbsorb>().statec == PlayerAbsorb.AbsorbState.Absorbed)
                if (Input.GetButtonDown("Fire1"))
                {
                    //점프 중간에 마우스 좌클릭한다면
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    isGrounded = false;
                }
                //느리게 가기 
                jumpPower = 6;
                rb.drag = 3;

                print("날개 애니메이션");

                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                //점프 높이 커비의 4~5배

                rb.velocity = new Vector3(0, jumpPower, 0);
                jumpCnt++;
                //anim.SetTrigger("Jump02"); //혹은 스케일
            }
        }
    }

    void disableJump()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        isGrounded = false;
        jumpCnt = 0;
        isJump = false;
        
    }

    int hitCount;
    private void OnTriggerEnter(Collider other)
    {
        print("other.gameObject.layer"+ other);
        if (other.tag == "Ground")
        {
            isJump = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
            isGrounded = true;
            jumpCnt = 0;
        }
        else if (other.tag == "ladder")
        {
            print("사다리");
            isLadder = true;
            JH_Rock_Spawn.instance.area1Start = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            print("other.gameObject.layer");
            
            if (hitCount < 1)
            {
                rangerObj.SetActive(false);
                absorbObj.SetActive(false);
               
                Destroy(other.gameObject);

                Vector3 pos = carObj.transform.position;
                pos.y += 5;
                carObj.transform.position = pos;
                //GameObject go = Instantiate(obj, posZ, Quaternion.identity);
                carObj.SetActive(true);
                hitCount++;
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ladder")
        {
            isLadder = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //onTriggerEnter Layer Absorb로 처리에서 인식 불가 문제로 콜라이더로 변경
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            print("eTrigger%%%%%%%%%%%");

            //박치기 = 서로피격
            print("damage 함수처리");
            Vector3 dir = transform.position - collision.gameObject.transform.position;
            //넉백일 경우와 아닐경우 분리
            //흡수할 경우 넉백이 일어나면 안됨
            rb.AddForce(dir * (300f * Time.deltaTime), ForceMode.Impulse);
            //본인도 데미지
            if (!(GetComponentInChildren<PlayerAbsorb>().state == PlayerAbsorb.AbsorbState.Absorbing))
            {
                OnDamage();
            }
        }
    }

    void Die()
    {
        print("DIE");
        Destroy(gameObject, 2);

    }

    //공격받는 스크립트 따로 만들게 되면 위치 옮기기
    public void OnDamage()
    {
        //임시로 두번 맞으면 죽음
        //대쉬 상태일 때도 무적!!
        if (!isGuard || isDash) playerHP.HP -= damage;
        if (playerHP.HP < 0)
        {
            Die();
        }
    }

    void UpdateRanger()
    {
        print("ranger상태");

    }

    void Guard()
    {
        if (isRanger)
        {
            UpdateRanger();
        }

        //버튼 누르는 만큼
        //isGuard 면 데미지 함수 무력화
        if (shift)
        {
            if (h != 0 || v != 0)
            {
                isGuard = false;
                transform.localScale = new Vector3(1f, 1f, 1f);
                StartCoroutine(Dumble());
            }
            else if (space)
            {
                isGuard = false;
                tempDash.GetComponent<Light>().enabled = true;
                transform.localScale = new Vector3(1f, 1f, 1f);
                if (dashCount >= 1)
                {
                    if (isDash)
                    {
                        StartCoroutine(Dash());
                    }
                }
                else
                {
                    StartCoroutine(Dash());
                }
            }
            else
            {
                //print("가드 애니메이션 활성화");
                isGuard = true;
                if (isGuard) transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
        else if (shiftUP)
        {
            tempDash.GetComponent<Light>().enabled = false;
            //print("가드 애니메이션 비활성화");
            //if(!isDash || !isDumble)
            //{
            dashCount = 0;
            isGuard = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
            state = PlayerState.BASIC;
            //}

        }

    }


    IEnumerator Dumble()
    {
        isDumble = true;
        print("덤블링");
        jumpPower = 2;
        if (isGrounded)
        {
            rb.velocity = new Vector3(0, jumpPower, 0);
            isGrounded = false;
        }
        yield return new WaitForSeconds(0.2f);
        isDumble = false;
        yield return new WaitForSeconds(0.5f);
        StopAllCoroutines();

    }
    IEnumerator Dash()
    {
        print("대쉬");
        rb.AddForce(transform.forward * 7, ForceMode.Impulse);
        isDash = false;
        dashCount++;
        yield return new WaitForSeconds(2.5f);
        isDash = true;


        //dash는 연속으로 안됨 => 쿨타임

    }
    //rb.velocity = transform.forward * 5;
    //transform.position += transform.forward * (speed * 150) * Time.deltaTime;

    void CheckHeight()
    {
        //커비 position값 빼주기 ray값이 커비 오브젝트 가운데 있음.
        float PosDiffer = transform.position.y - (transform.lossyScale.y / 2);
        print("belowPos" + PosDiffer);
        Vector3 belowPos = transform.position;
        belowPos.y = PosDiffer;
        Ray ray = new Ray(belowPos, Vector3.down);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            print("hitInfo" + hitInfo.transform.tag);
            print("distance" + hitInfo.distance);
            if (hitInfo.transform.tag == "Ground")
            {
                // 바닥이 있다. 현재 내 위치에서 바닥까지의 거리가 천장높이보다 크다면
                if (maxHeight < hitInfo.distance)
                {
                    rb.velocity = Vector3.zero;
                    print("maxHeight: " + maxHeight);
                    //isGrounded = false;
                    // 천장에 닿았다
                    Vector3 pos = transform.position;
                    //최대 높이 + 바뀌는 땅의 높이 y값
                    pos.y = maxHeight + hitInfo.transform.position.y;
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



}
