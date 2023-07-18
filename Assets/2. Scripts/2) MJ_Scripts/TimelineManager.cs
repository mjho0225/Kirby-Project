using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// 타임라인 배열을 초기화하여 안에다가 넣고 싶다.
public class TimelineManager : MonoBehaviour
{
    // 타임라인 매니저를 인스턴스화 한다.
    public static TimelineManager instance;
    public PlayableDirector[] timeLines;

    void Awake()
    {
        instance = this;
        InitTimelines();
    }

    private void InitTimelines()
    {
        // 배열에 안에 있는 자식들의 부모에 timelines를 가져온다.
        timeLines = new PlayableDirector[transform.childCount];
        // 그 안에 있는 자식들을 모두 할당한다.
        for (int i = 0; i < transform.childCount; i++)
        {
            //자식의 트랜스폼에 있는 PlayerDirector을 가져와서 넣는다.
            timeLines[i] = transform.GetChild(i).GetComponent<PlayableDirector>();
        }
    }
}
