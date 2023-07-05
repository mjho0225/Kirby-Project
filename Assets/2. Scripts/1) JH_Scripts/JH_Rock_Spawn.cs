using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  돌을 생성한다.
//  생성한 돌에게 리스트를 넣어준다.
//  시작 시간은 다르되 생성 속도는 동일 하다.

public class JH_Rock_Spawn : MonoBehaviour
{


    
    public float currentTime1 = 0;
    float currentTime2 = 0;

    public List<GameObject> RockArea1;
    public List<GameObject> RockArea2;
    public List<GameObject> RockArea3;

    public List<GameObject> area1Rock;
    public List<GameObject> area2Rock;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        currentTime1 += Time.deltaTime;
        if(currentTime1 > 2)
        {
            
            Invoke("Area1RockSpawn1", 2);
            Invoke("Area1RockSpawn2", 3);
            currentTime1 = 0;
        }

        currentTime2 += Time.deltaTime;
        if(currentTime2 > 4) 
        {
            Invoke("Area2RockSpawn1", 2);
            Invoke("Area2RockSpawn2", 4);
            Invoke("Area2RockSpawn3", 3);
            currentTime2 = 0;
        }
    }

    void RockSpawn1()
    {
 
            
 


            Invoke("Area2RockSpawn1",2);
            Invoke("Area2RockSpawn2",4);
            Invoke("Area2RockSpawn3",3);
 

            //Instantiate(RockArea3[0]);
            //RockArea3.transform.position = transform.position;
            //currentTime = 0;
 

    }


    void Area1RockSpawn1()
    {
        Instantiate(area1Rock[0], RockArea1[0].transform.position, Quaternion.identity);
    }

    void Area1RockSpawn2()
    {
        Instantiate(area1Rock[1], RockArea1[1].transform.position, Quaternion.identity);
    }

    void Area2RockSpawn1()
    {
        
        Instantiate(area2Rock[0], RockArea2[0].transform.position, Quaternion.identity);

    }
    void Area2RockSpawn2()
    {
        Instantiate(area2Rock[1], RockArea2[1].transform.position, Quaternion.identity);

    }
    void Area2RockSpawn3()
    {
        Instantiate(area2Rock[2], RockArea2[2].transform.position, Quaternion.identity);

    }

}
