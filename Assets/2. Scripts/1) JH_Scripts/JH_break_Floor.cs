using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_break_Floor : MonoBehaviour
{

    public GameObject[] breakFloor;

    bool doitBreak;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (doitBreak == true)
        {
            for(int i = 0; i < breakFloor.Length;)
            {
                
                if(i == breakFloor.Length)
                {
                    doitBreak = false;
                }
            }
           
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rock")
        {
            doitBreak = true;
        }
    }
}
