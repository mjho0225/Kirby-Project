using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2초동안 멈추게하고 싶다. 그 이후로는 플레이 되게 한다.
public class TimeSignal : MonoBehaviour
{
    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void StartTime()
    {
        Time.timeScale = 1;
    }
}
