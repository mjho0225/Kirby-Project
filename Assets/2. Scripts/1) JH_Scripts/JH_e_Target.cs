using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_e_Target : MonoBehaviour
{
    public GameObject hideWall;

    public List<GameObject> guideWay;

    public bool hit = false;
    float guideTime = 0;

    enum TargetStage
    {
        targetS1,
        targetS2,
    }

    TargetStage targetStage;

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < guideWay.Count; i++)
        {
            guideWay[i].gameObject.SetActive(false);
        }
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

            guideTime += Time.deltaTime;

            //다시 살릴예정
            //for (int i = 0; i < guideWay.Count;)
            //{
            //    if (guideTime >= 0.5f)
            //    {
            //        guideWay[i].gameObject.SetActive(true);
            //        i++;
            //        guideTime = 0;
            //    }

            //    if (i >= guideWay.Count)
            //    {
            //        Destroy(hideWall, guideWay.Count * 0.5f);
            //        Destroy(gameObject);
            //        hit = false;
            //    }

            //}


        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet" || collision.gameObject.tag == "bullet2")
        {
            hit = true;
            gameObject.GetComponent<Renderer>().enabled = false;
            //Destroy(this.gameObject);
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
