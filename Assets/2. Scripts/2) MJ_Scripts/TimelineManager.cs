using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// Ÿ�Ӷ��� �迭�� �ʱ�ȭ�Ͽ� �ȿ��ٰ� �ְ� �ʹ�.
public class TimelineManager : MonoBehaviour
{
    // Ÿ�Ӷ��� �Ŵ����� �ν��Ͻ�ȭ �Ѵ�.
    public static TimelineManager instance;
    public PlayableDirector[] timeLines;

    void Awake()
    {
        instance = this;
        InitTimelines();
    }

    private void InitTimelines()
    {
        // �迭�� �ȿ� �ִ� �ڽĵ��� �θ� timelines�� �����´�.
        timeLines = new PlayableDirector[transform.childCount];
        // �� �ȿ� �ִ� �ڽĵ��� ��� �Ҵ��Ѵ�.
        for (int i = 0; i < transform.childCount; i++)
        {
            //�ڽ��� Ʈ�������� �ִ� PlayerDirector�� �����ͼ� �ִ´�.
            timeLines[i] = transform.GetChild(i).GetComponent<PlayableDirector>();
        }
    }
}
