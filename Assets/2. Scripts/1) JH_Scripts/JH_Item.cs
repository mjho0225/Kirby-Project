using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JH_Item : MonoBehaviour
{




    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어가 부딪히면
        if (collision.gameObject.tag == "Player")
        {
            JH_ScoreManager.instance.COIN_SCORE++;
        }

    }

}
