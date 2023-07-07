using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_w_Spawn : MonoBehaviour
{


    //���ҽ����� �ε� ( null �� �Ǹ� ���� �ð� �� ���� )
    public GameObject e_Fox;
    

    public List<GameObject> foxCount;

    public float spawnTime = 0;
    float currentTime;

    private void Awake()
    {

    }
    GameObject distPlayer;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 5;

        distPlayer = GameObject.FindGameObjectWithTag("Player");

        //�켱 ó�� ���� ����
        e_Fox = Resources.Load<GameObject>("MonsterFox");
        GameObject fox = Instantiate(e_Fox, this.transform.position, Quaternion.identity);
        fox.GetComponent<JH_e_Fox>().mySpawner = this;
        foxCount.Add(fox);
    }

    float dist;
    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(distPlayer.transform.position, transform.position);


        if (foxCount.Count == 0)
        {
            currentTime += Time.deltaTime;
            
            if (currentTime >= spawnTime)
            {
                GameObject fox = Instantiate(e_Fox, this.transform.position, Quaternion.identity);
                fox.GetComponent<JH_e_Fox>().mySpawner = this;
                foxCount.Add(fox);

            }
        }
        
    }


    void FoxSpawn()
    {
        GameObject fox = Instantiate(e_Fox, this.transform.position, Quaternion.identity);
        fox.GetComponent<JH_e_Fox>().mySpawner = this;
        foxCount.Add(fox);
    }


    internal void DestroyedFox(JH_e_Fox fox)
    {
        if (foxCount.Contains(fox.gameObject))
            foxCount.Remove(fox.gameObject);
        currentTime = 0;
    }
}
