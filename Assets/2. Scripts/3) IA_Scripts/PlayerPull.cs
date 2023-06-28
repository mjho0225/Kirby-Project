using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPull : MonoBehaviour
{

    float speed = 5f;
    Transform playerPos;
    //enemy »Ì¿‘
    //[SerializeField]
    public bool emptyState = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    //Stay Ω√∞£ø° µ˚∂Û 1.5/5
    private void OnTriggerEnter(Collider other)
    {
        GameObject item = other.gameObject;
        print("tagName" + item.tag);
        playerPos = GameObject.FindWithTag("Player").transform;
        switch (item.tag)
        {
            case "e_fox":
                print("e_fox »Ì¿‘");
                item.transform.position = Vector3.MoveTowards(playerPos.position, item.transform.position, speed * Time.deltaTime);
                emptyState = true;
                break;
            case "e_ranger":
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

     
    }



}
