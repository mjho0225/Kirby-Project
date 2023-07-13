using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    // 유니티 타임라인을 실행시키고 싶다.
    public PlayableDirector playableDirector;

    void Update()
    {
        // 만약 플레이어가 삼켰다면 변신하는 타임라인을 실행시키고 싶다.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //플레이한다.
            playableDirector.Play();
        }
    }
}
