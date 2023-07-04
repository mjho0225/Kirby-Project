using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_e_Target : MonoBehaviour
{
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




}
