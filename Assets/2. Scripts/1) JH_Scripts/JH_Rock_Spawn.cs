using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  돌을 생성한다.
//  생성한 돌에게 리스트를 넣어준다.
//  시작 시간은 다르되 생성 속도는 동일 하다.

public class JH_Rock_Spawn : MonoBehaviour
{


    public float startTime = 0; 
    public float currentTime = 0;

    public GameObject RockArea1;
    public GameObject RockArea2;
    GameObject RockArea3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        currentTime += Time.deltaTime;
        if (currentTime > startTime)
        {
            RockSpawn();
        }
    }

    void RockSpawn()
    {
        
        
        if (currentTime > 3)
        {
            
            if (this.gameObject.name.Contains("Area1"))
            {
                Instantiate(RockArea1);
                RockArea1.transform.position = transform.position;
                currentTime = 0;
            }
            if (this.gameObject.name.Contains("Area2"))
            {
                Instantiate(RockArea2);
                RockArea2.transform.position = transform.position;
                currentTime = 0;
            }
            if (this.gameObject.name.Contains("Area3"))
            {
                Instantiate(RockArea3);
                RockArea3.transform.position = transform.position;
                currentTime = 0;
            }
            
        }
    }

    
}
