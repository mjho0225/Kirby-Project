using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Flower : MonoBehaviour
{
    //Animation flowerAnimation;
    Animator flowerAnimation;

    public AnimationCurve anim;

    // Start is called before the first frame update
    void Start()
    {
        flowerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentInParent<JH_Item>().flowerAnim == true)
        {
            flowerAnimation.SetBool("flowerAnim", true);
        }
    }
}
