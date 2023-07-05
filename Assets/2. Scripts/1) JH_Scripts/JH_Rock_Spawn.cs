using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  ���� �����Ѵ�.
//  ������ ������ ����Ʈ�� �־��ش�.
//  ���� �ð��� �ٸ��� ���� �ӵ��� ���� �ϴ�.

public class JH_Rock_Spawn : MonoBehaviour
{


    
    public float currentTime1 = 0;
    float currentTime2 = 0;
    bool bigRock = false;
    float currentTime3 = 0;
    bool finishRock = false;

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


    // Start is called before the first frame update
    void Start()
    {
        bigRock = true;
        finishRock = true;
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

        if (bigRock == true)
        {
            Instantiate(area3Rock, RockArea3.transform.position, Quaternion.identity);
            bigRock = false;
        }

        currentTime3 += Time.deltaTime;
        if(currentTime3 > 2.5f)
        {
            Invoke("Area4RockSpawn1", 2);
            Invoke("Area4RockSpawn2", 3);
            currentTime3 = 0;
        }

        if(finishRock == true)
        {
            //Instantiate(area5Rock, RockArea5[0].transform.position, Quaternion.identity);
            //Instantiate(area5Rock, RockArea5[1].transform.position, Quaternion.identity);
            //Instantiate(area5Rock, RockArea5[2].transform.position, Quaternion.identity);
            area5Rock[0].GetComponent<Rigidbody>().useGravity = true;
            area5Rock[1].GetComponent<Rigidbody>().useGravity = true;
            area5Rock[2].GetComponent<Rigidbody>().useGravity = true;
            finishRock = false;
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

    void Area4RockSpawn1()
    {
        Instantiate(area4Rock[0], RockArea4[0].transform.position, Quaternion.identity);

    }
    void Area4RockSpawn2()
    {
        Instantiate(area4Rock[1], RockArea4[1].transform.position, Quaternion.identity);

    }

}
