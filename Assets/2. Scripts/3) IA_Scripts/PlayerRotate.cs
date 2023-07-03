using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // �߻�ü�� ���� ���� �ʴ´ٸ�
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        transform.position += dir * 5 * Time.deltaTime;

        //���࿡ �����̰� ���� ��
        //if(dir.magnitude > 0)
        if (h != 0 || v != 0)
        {
            //Mathf.Acos(Vector3.Dot(dir, body.transform.right));
            //body�� ������ ���Ϳ� ���� ����(dir) �� ������ ������
            //Vector3.Dot //����
            float angle = Vector3.Angle(transform.right, dir);
            //���� �� ������ 90���� �۴ٸ� ���������� ȸ������
            if (angle < 90)
            {
                transform.Rotate(0, 400 * Time.deltaTime, 0); //���簢������ ȸ��
            }
            else
            {
                transform.Rotate(0, -400 * Time.deltaTime, 0);
            }
            //�׷��� ������ �������� ȸ��

        }
    }
}
