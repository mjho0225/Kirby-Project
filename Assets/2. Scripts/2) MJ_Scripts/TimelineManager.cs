using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    // ����Ƽ Ÿ�Ӷ����� �����Ű�� �ʹ�.
    public PlayableDirector playableDirector;

    void Update()
    {
        // ���� �÷��̾ ���״ٸ� �����ϴ� Ÿ�Ӷ����� �����Ű�� �ʹ�.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //�÷����Ѵ�.
            playableDirector.Play();
        }
    }
}
