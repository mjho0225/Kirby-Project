using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public GameObject body;
    // Start is called before the first frame update
    void Start()
    {
       
    }

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
            float angle = Vector3.Angle(body.transform.right, dir);
            //���� �� ������ 90���� �۴ٸ� ���������� ȸ������
            if(angle < 90)
            {
                body.transform.Rotate(0, 500 * Time.deltaTime , 0); //���簢������ ȸ��
            }
            else
            {
                body.transform.Rotate(0, -500 * Time.deltaTime, 0);
            }
            //�׷��� ������ �������� ȸ��

        }
        ////���࿡ �������� �����ٸ�
        //if(h > 0)��
        //{
        //    body.transform.localEulerAngles = new Vector3(0, 90, 0);    
        //}
        ////Body �� angle ���� 0, 90, 0 ���� �Ѵ�
    }
}
