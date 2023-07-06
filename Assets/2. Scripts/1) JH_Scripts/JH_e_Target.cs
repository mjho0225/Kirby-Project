using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_e_Target : MonoBehaviour
{
    public GameObject hideWall;

    public List<GameObject> guideWay;

    public bool hit = false;

    enum TargetStage
    {
        targetS1,
        targetS2,
    }

    TargetStage targetStage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (targetStage)
        {
            case TargetStage.targetS1:
                break;
            case TargetStage.targetS2:
                break;
        }


        if (hit == true)
        {
            //WayGuide();
            Destroy(this.transform.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            hit = true;
             Destroy(this);
            //StartCoroutine("WayGuide");
        }
    }

    private void OnDestroy()
    {
        if (hit == true)
        {

            Destroy(hideWall, guideWay.Count * 0.5f);
        }
    }

    float currentTime = 0;

    void  WayGuide()
    {
        currentTime += Time.deltaTime;
        for (int i = 0; i <= guideWay.Count; i++)
        {



            if (currentTime > 0.5f)
            {
                guideWay[i].gameObject.SetActive(true);
                
                if (i >= guideWay.Count)
                {
                    Destroy(this);
                }

                if (i >= guideWay.Count)
                {
                    //guideWay[i].gameObject.SetActive(false);
                    //Destroy(guideWay[i], 0.1f);
                    currentTime = 0;
                    hit = false;

                }
            }

        }
        

    }
}
