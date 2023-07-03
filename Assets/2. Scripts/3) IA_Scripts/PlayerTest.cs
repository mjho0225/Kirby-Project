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

        //만약에 움직이고 있을 때
        //if(dir.magnitude > 0)
        if (h != 0 || v != 0)
        {
            //Mathf.Acos(Vector3.Dot(dir, body.transform.right));
            //body의 오른쪽 벡터와 가는 방향(dir) 의 각도를 구하자
            //Vector3.Dot //내적
            float angle = Vector3.Angle(body.transform.right, dir);
            //만약 그 각도가 90보다 작다면 오른쪽으로 회전하자
            if(angle < 90)
            {
                body.transform.Rotate(0, 500 * Time.deltaTime , 0); //현재각도에서 회전
            }
            else
            {
                body.transform.Rotate(0, -500 * Time.deltaTime, 0);
            }
            //그렇지 않으면 왼쪽으로 회전

        }
        ////만약에 오른쪽을 눌렀다면
        //if(h > 0)전
        //{
        //    body.transform.localEulerAngles = new Vector3(0, 90, 0);    
        //}
        ////Body 의 angle 값을 0, 90, 0 으로 한다
    }
}
