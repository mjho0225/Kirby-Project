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
    #region ����Ʈ ����
    public List<Transform> patrolPos; // ��Ʈ�� ��ġ�� ��̳Ŀ� ���� ���� �ø��� �ֵ��� �ۺ�����
    int listCount; // ��Ʈ�� ����Ʈ�� ���� �ľ�
    public int i = 0;
    #endregion

    public float enemySpeed = 3f;  // ��ȹ ������ ���ʹ� ���ǵ� ������ �� �ֵ��� ����

    public GameObject targetPlayer; // �÷��̾ ���������(���� �ȿ� ������)
    float targetDist; // �÷��̾� �Ÿ�
    Vector3 dirPlayer;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        listCount = patrolPos.Count; // ��Ʈ�� ����Ʈ�� ���� �ľ�
        targetPlayer = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾� �Ÿ�
        targetDist = Vector3.Distance(targetPlayer.transform.position, gameObject.transform.position);

        dirPlayer = targetPlayer.transform.position - this.transform.position;
        playerPos = new Vector3(dirPlayer.x, 0, dirPlayer.z);

        // ���� ���߿� �÷��̾ ������ ���� �÷��̾����� ���� �÷��̾�� �Ÿ��� ���ų� �־����� ���� ��Ʈ�� �̵�
        if (targetDist <= 4)
        {
            transform.rotation = Quaternion.LookRotation(playerPos);
            transform.Translate(-playerPos * 1f * Time.deltaTime);
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

    }
}
