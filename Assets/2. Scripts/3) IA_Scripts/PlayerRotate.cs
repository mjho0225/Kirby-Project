using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // 발사체가 적에 맞지 않는다면
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
            float angle = Vector3.Angle(transform.right, dir);
            //만약 그 각도가 90보다 작다면 오른쪽으로 회전하자
            if (angle < 90)
            {
                transform.Rotate(0, 400 * Time.deltaTime, 0); //현재각도에서 회전
            }
            else
            {
                transform.Rotate(0, -400 * Time.deltaTime, 0);
            }
            //그렇지 않으면 왼쪽으로 회전

        }
    }
}
