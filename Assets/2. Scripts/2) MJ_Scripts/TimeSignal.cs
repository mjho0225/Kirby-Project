using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2�ʵ��� ���߰��ϰ� �ʹ�. �� ���ķδ� �÷��� �ǰ� �Ѵ�.
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
