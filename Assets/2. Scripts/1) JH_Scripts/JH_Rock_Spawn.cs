using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  돌을 생성한다.
//  생성한 돌에게 리스트를 넣어준다.
//  시작 시간은 다르되 생성 속도는 동일 하다.

public class JH_Rock_Spawn : MonoBehaviour
{
    public static JH_Rock_Spawn instance;

    public float currentTime1 = 0;
    float currentTime2 = 0;
    float currentTime3 = 0;

    public bool area1Start = false;
    public bool bigRock = false;
    public bool area4Start = false;
    public bool finishRock = false;
    

    public List<GameObject> RockArea1;
    public List<GameObject> RockArea2;
    
    public GameObject RockArea3;
    public List<GameObject> RockArea4;
    //public List<GameObject> RockArea5;

    public List<GameObject> area1Rock;
    public List<GameObject> area2Rock;
    public GameObject area3Rock;
    public List<GameObject> area4Rock;
    public List<GameObject> area5Rock;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        area1Start = true;
        //bigRock = true;
        //area4Start = true;
        //finishRock = true;
    }

    // Update is called once per frame
    void Update()
    {

        // 첫번째 사다리에 도착하면 스폰 시작
        if(area1Start == true)
        {
            currentTime1 += Time.deltaTime;
            if (currentTime1 > 2)
            {

                Invoke("Area1RockSpawn1", 1);
                Invoke("Area1RockSpawn2", 2);
                currentTime1 = 0;
            }

            currentTime2 += Time.deltaTime;
            if (currentTime2 > 4)
            {
                Invoke("Area2RockSpawn1", 2);
                Invoke("Area2RockSpawn2", 4);
                Invoke("Area2RockSpawn3", 3);
                currentTime2 = 0;
            }
        }
        


        // 해당 지점에 도착하면 큰 돌 떨어지기 시작 (bigRock)
        if (bigRock == true)
        {
            Instantiate(area3Rock, RockArea3.transform.position, Quaternion.identity);
            area1Start = false;
            bigRock = false;
        }


        // area4 
        if(area4Start == true)
        {
            currentTime3 += Time.deltaTime;
            if (currentTime3 > 2.5f)
            {
                Invoke("Area4RockSpawn1", 2);
                Invoke("Area4RockSpawn2", 3);
                
                currentTime3 = 0;
            }
        }
        

        if(finishRock == true)
        {
            //Instantiate(area5Rock, RockArea5[0].transform.position, Quaternion.identity);
            //Instantiate(area5Rock, RockArea5[1].transform.position, Quaternion.identity);
            //Instantiate(area5Rock, RockArea5[2].transform.position, Quaternion.identity);
            area5Rock[0].GetComponent<Rigidbody>().useGravity = true;
            area5Rock[1].GetComponent<Rigidbody>().useGravity = true;
            area5Rock[2].GetComponent<Rigidbody>().useGravity = true;
            area4Start = false;
            finishRock = false;
        }

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

    void Area4RockSpawn1()
    {
        Instantiate(area4Rock[0], RockArea4[0].transform.position, Quaternion.identity);

    }
    void Area4RockSpawn2()
    {
        Instantiate(area4Rock[1], RockArea4[1].transform.position, Quaternion.identity);

    }

}
