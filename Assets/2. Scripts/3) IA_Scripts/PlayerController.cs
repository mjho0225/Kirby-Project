using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float h, v;
    bool space, shift, shiftUP;

    public float jumpPower = 5f;
    public float speed = 5f;
    float maxHeight = 5f; //���� Ŀ�� ��ġ���� 4.5��
    int jumpCnt;
    int maxJumpCnt = 20;
    bool isGrounded = false;
    bool isJump = false;
    bool isDumble = false;
    bool isDash = false;
    float dashCount = 0;
    [SerializeField] public bool isGuard = false;

    float currTime;
    int damage = 5;

    float rx;
    float ry;
    public float rotSpeed = 100;
    PlayerHP playerHP;
    Rigidbody rb;
    //�ӽ�
    GameObject tempDash;
    bool isLadder = false;

    bool isRanger = true;
    bool Fire2 = false;

    public GameObject absorbObj;
    public GameObject rangerObj;
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
    public GameObject gun;


    void Start()
    {
        playerHP = GetComponent<PlayerHP>();
        rb = GetComponent<Rigidbody>();
        state = PlayerState.BASIC;
        tempDash = GameObject.Find("Temp");
       
    }



    // Update is called once per frame 
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

        //isAbsorb = GetComponent<PlayerAbsorb>().isAbsorb;


        //if(attackState == AttackState.RANGER && Fire2)
        //{
        //    Vector3 posZ = transform.position;
        //    posZ.z += 3;
        //    GameObject go = Instantiate(bubleGun, posZ, Quaternion.identity);
        //    Rigidbody rb = go.GetComponent<Rigidbody>();
        //    rb.AddForce(transform.forward * 10f, ForceMode.Impulse);

        //    ChangeAbsorb();
        //}


    }

    public void ChangeRanger()
    {
        attackState = AttackState.RANGER;
        //GetComponent<PlayerFire>().enabled = true;
        //GetComponent<PlayerAbsorb>().enabled = false;
        //// print("�� ��� Ŀ��� ����, �ӽ� �� ������Ʈ �ѱ�");
        //gun.SetActive(true);
       
        print(rangerObj);
        //Destroy(absorbObj); 
        //GameObject obj = Resources.Load<GameObject>("Player_Ranger");
        //Instantiate(obj, transform.position, transform.rotation);
        rangerObj.SetActive(true);
        absorbObj.SetActive(false);
        PlayerAbsorb newAbsorb = absorbObj.GetComponent<PlayerAbsorb>();
        newAbsorb.Reset();

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
        //dir.y = 0; //�鸮�ų� �Ʒ��� �������� ���� ����
        dir.Normalize();
        //������ y �ӵ��� dir�� y�׸� �ݿ��ؾ� �Ѵ�.
        Vector3 velocity = dir * speed; //sp eed ���� yvelocity�� �������� �ʱ� ���� ���� ���
        transform.position += velocity * Time.deltaTime;

        if (h != 0 || v != 0 && !(isDumble))
        {
            //Mathf.Acos(Vector3.Dot(dir, body.transform.right));
            //body�� ������ ���Ϳ� ���� ����(dir) �� ������ ������
            //Vector3.Dot //����
            float angle = Vector3.Angle(transform.right, dir);
            //���� �� ������ 90���� �۴ٸ� ���������� ȸ������
            if (angle < 90)
            {
                transform.Rotate(0, 300 * Time.deltaTime, 0); //���簢������ ȸ��
            }
            else
            {
                transform.Rotate(0, -300 * Time.deltaTime, 0);
            }
            //�׷��� ������ �������� ȸ��

        }
    }

    void MoveUp()
    {

        print("MoveUP");

        bool upKey = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        bool downKey = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);

        if (upKey)
        {
            Vector3 dir = transform.position;
            dir.y += 2;
            print("dir.y" + dir.y);
            transform.position = Vector3.Lerp(transform.position, dir, 1f);

            //transform.position += velocity * Time.deltaTime;
        }
        else if (downKey)
        {
            //transform.position = Vector3.Lerp(transform.position, -dir, 0.5f);
        }

    }


    void Jump()
    {


        if (space && isGrounded)
        {
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
                jumpPower = 7;
                print("������ �ִϸ��̼�");

                rb.velocity = new Vector3(0, jumpPower, 0);
                jumpCnt++;

            }
            else if (jumpCnt >= 1)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    //���� �߰��� ���콺 ��Ŭ���Ѵٸ�
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    isGrounded = false;
                }

                print("���� �ִϸ��̼�");
                transform.localScale = new Vector3(2f, 2f, 2f);
                //���� ���� Ŀ���� 4~5��
                jumpPower = 4;
                rb.velocity = new Vector3(0, jumpPower, 0);
                jumpCnt++;
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            isJump = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
            isGrounded = true;
            jumpCnt = 0;
        }
        else if (other.tag == "ladder")
        {
            print("��ٸ�");
            isLadder = true;

        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
           
            //��ġ�� = �����ǰ�
            //print("damage �Լ�ó��");
            Vector3 dir = transform.position;
            //�˹��� ���� �ƴҰ�� �и�
            //����� ��� �˹��� �Ͼ�� �ȵ�
            rb.AddForce(-dir * (100f * Time.deltaTime), ForceMode.Impulse);
            //���ε� ������
            
            OnDamage();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ladder")
        {
            isLadder = false;
        }
    }


    void Die()
    {
        print("DIE");
        Destroy(gameObject, 2);
    }

    //���ݹ޴� ��ũ��Ʈ ���� ����� �Ǹ� ��ġ �ű��
    public void OnDamage()
    {
        //�ӽ÷� �ι� ������ ����
        //�뽬 ������ ���� ����!!
        if (!isGuard || isDash) playerHP.HP -= damage;
        if(playerHP.HP < 0)
        {
            Die();
        }
    }

    void UpdateRanger()
    {
        print("ranger����");

    }

    void Guard()
    {
        if (isRanger)
        {
            UpdateRanger();
        }

        //��ư ������ ��ŭ
        //isGuard �� ������ �Լ� ����ȭ
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
                //print("���� �ִϸ��̼� Ȱ��ȭ");
                isGuard = true;
                if (isGuard) transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
        else if (shiftUP)
        {
            tempDash.GetComponent<Light>().enabled = false;
            //print("���� �ִϸ��̼� ��Ȱ��ȭ");
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
        print("����");
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
        print("�뽬");
        rb.AddForce(transform.forward * 7, ForceMode.Impulse);
        isDash = false;
        dashCount++;
        yield return new WaitForSeconds(2.5f);
        isDash = true;


        //dash�� �������� �ȵ� => ��Ÿ��

    }
    //rb.velocity = transform.forward * 5;
    //transform.position += transform.forward * (speed * 150) * Time.deltaTime;

    void CheckHeight()
    {
        //Ŀ�� position�� ���ֱ� ray���� Ŀ�� ������Ʈ ��� ����.
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



}
