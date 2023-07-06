using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_e_Target : MonoBehaviour
{
    public GameObject hideWall;

    public List<GameObject> guideWay;

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
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            // Destroy(this);
            StartCoroutine("wayGuide");
        }
    }

    private void OnDestroy()
    {
        Destroy(hideWall, guideWay.Count * 0.2f);
    }


    IEnumerator wayGuide()
    {
        for(int i = 0; i <= guideWay.Count; i++)
        {
            guideWay[i].gameObject.SetActive(true);

            yield return new WaitForSeconds(0.2f);

            guideWay[i].gameObject.SetActive(false);
            i++;

        }
        

    }
}
