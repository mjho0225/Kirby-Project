using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float h, v;
    bool space, shift, shiftUP;

    public float jumpPower = 5f;
    public float speed = 7f;
    float maxHeight = 4.5f; //���� Ŀ�� ��ġ���� 4.5��
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
    //�ӽ�
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
                posLeft.y = transform.position.y - (transform.lossyScale.y / 2) + 0.2f; //pos �� üũ
                posLeft.x = transform.position.x - 0.5f;
                GameObject left = Instantiate(feetEffect);
                left.transform.position = posLeft;
                yield return new WaitForSeconds(1);
                Vector3 posRight = transform.position;
                posRight.y = transform.position.y - (transform.lossyScale.y / 2) + 0.2f; //pos �� üũ
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
            //��¡�� �̵� ����
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
        //������ y �ӵ��� dir�� y�׸� �ݿ��ؾ� �Ѵ�.
        Vector3 velocity = dir * speed; //sp eed ���� yvelocity�� �������� �ʱ� ���� ���� ���
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
            //body�� ������ ���Ϳ� ���� ����(dir) �� ������ ������
            //Vector3.Dot //����
            float angle = Vector3.Angle(transform.right, dir);
            //���� �� ������ 90���� �۴ٸ� ���������� ȸ������
            if (angle < 90)
            {
                transform.Rotate(0, 200 * Time.deltaTime, 0); //���簢������ ȸ��
            }
            else
            {
                transform.Rotate(0, -200 * Time.deltaTime, 0);
            }
            //�׷��� ������ �������� ȸ��

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

        //�߷��� ���� ���¿��� ���߱�
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
                print("������ �ִϸ��̼�");
                rb.velocity = new Vector3(0, jumpPower, 0);
                jumpCnt++;
                //TIMELINE ������ �ִϸ��̼� �����
                anim.SetTrigger("Jump");
                
                

                //anim.SetTrigger("Jump01");
            }
            else if (jumpCnt >= 1)
            {
                //&& !(GetComponentInChildren<PlayerAbsorb>().statec == PlayerAbsorb.AbsorbState.Absorbed)
                if (Input.GetButtonDown("Fire1"))
                {
                    //���� �߰��� ���콺 ��Ŭ���Ѵٸ�
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    isGrounded = false;
                }
                //������ ���� 
                jumpPower = 6;
                rb.drag = 3;

                print("���� �ִϸ��̼�");

                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                //���� ���� Ŀ���� 4~5��

                rb.velocity = new Vector3(0, jumpPower, 0);
                jumpCnt++;
                //anim.SetTrigger("Jump02"); //Ȥ�� ������
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
            print("��ٸ�");
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
        //onTriggerEnter Layer Absorb�� ó������ �ν� �Ұ� ������ �ݶ��̴��� ����
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            print("eTrigger%%%%%%%%%%%");

            //��ġ�� = �����ǰ�
            print("damage �Լ�ó��");
            Vector3 dir = transform.position - collision.gameObject.transform.position;
            //�˹��� ���� �ƴҰ�� �и�
            //����� ��� �˹��� �Ͼ�� �ȵ�
            rb.AddForce(dir * (300f * Time.deltaTime), ForceMode.Impulse);
            //���ε� ������
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

    //���ݹ޴� ��ũ��Ʈ ���� ����� �Ǹ� ��ġ �ű��
    public void OnDamage()
    {
        //�ӽ÷� �ι� ������ ����
        //�뽬 ������ ���� ����!!
        if (!isGuard || isDash) playerHP.HP -= damage;
        if (playerHP.HP < 0)
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
                    //�ִ� ���� + �ٲ�� ���� ���� y��
                    pos.y = maxHeight + hitInfo.transform.position.y;
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
