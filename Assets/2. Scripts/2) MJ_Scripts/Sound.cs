using UnityEngine.Audio;
using UnityEngine;
using System;

// ���带 ����ȭ�Ͽ� �ٸ� �������� ����ϰ� �ʹ�.
[Serializable]
public class Sound
{
    public string name;
    // ���� Ŭ��
    public AudioClip clip;

    // ����(0~1)
    [Range(0f, 3f)]
    public float volume;

    //���� ������ (1 ~ 3)
    [Range(0.1f, 3f)]
    public float pitch;

    // �ݺ� üũ
    public bool loop;

    // ����� �ҽ��� �ִ� ��, �ν����Ϳ��� �Ⱥ��̰� �����
    [HideInInspector]
    public AudioSource source;
}
