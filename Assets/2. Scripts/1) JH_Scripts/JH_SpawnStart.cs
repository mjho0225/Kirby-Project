using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_SpawnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        if (this.gameObject.name.Contains("BigRock"))
    //        {
    //            JH_Rock_Spawn.instance.bigRock = true;
    //        }
    //        if (this.gameObject.name.Contains("Area4Rock"))
    //        {
    //            JH_Rock_Spawn.instance.area4Start = true;
    //        }
    //        if (this.gameObject.name.Contains("FinishRock"))
    //        {
    //            JH_Rock_Spawn.instance.finishRock = true;
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            if (this.gameObject.name.Contains("BigRock"))
            {
                JH_Rock_Spawn.instance.bigRock = true;
            }
            if (this.gameObject.name.Contains("Area4Rock"))
            {
                JH_Rock_Spawn.instance.area4Start = true;
            }
            if (this.gameObject.name.Contains("FinishRock"))
            {
                JH_Rock_Spawn.instance.finishRock = true;
            }
        }
    }
}
