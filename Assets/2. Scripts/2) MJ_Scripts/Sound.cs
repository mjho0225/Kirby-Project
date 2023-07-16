using UnityEngine.Audio;
using UnityEngine;
using System;

// 사운드를 직렬화하여 다른 곳에서도 사용하고 싶다.
[Serializable]
public class Sound
{
    public string name;
    // 사운드 클립
    public AudioClip clip;

    // 볼륨(0~1)
    [Range(0f, 3f)]
    public float volume;

    //음의 높낮이 (1 ~ 3)
    [Range(0.1f, 3f)]
    public float pitch;

    // 반복 체크
    public bool loop;

    // 오디오 소스를 넣는 곳, 인스펙터에는 안보이게 만들기
    [HideInInspector]
    public AudioSource source;
}
