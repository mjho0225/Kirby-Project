using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    // ����� ���带 �����´�
    public Sound[] sounds;

    void Awake()
    {
        // ���� �Űܵ� �ı����� �Ȱ� �Ѵ�.
        DontDestroyOnLoad(gameObject);

        // �ν��Ͻ��� ����ִٸ� �̰��� �ְ� �ƴϸ� �� ���ӿ�����Ʈ�� �����Ѵ�.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //��� ����� �ҽ��� �����Ͽ� �����´�.
        foreach (Sound s in sounds)
        {
            //Ŭ��, ����, �� �����̿� �����ϱ� ���� �ش� ������Ʈ �ҽ��� �߰��Ѵ�.
            s.source = gameObject.AddComponent<AudioSource>();
            //�ڱ� �ڽ��� �����ϰ� �˸´� �����͸� ����
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // �÷��̾ ���� ���� ���ڿ��� �����Ͽ� �Ҹ��� �÷��� �Ѵ�.
    public void PlayOneShotSound(string name)
    {
        // �迭�ȿ� �� �Ҹ� �̸��� ���� �̸��� ���ٸ� �÷��� �Ѵ�.
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // ���尡 ������ ���ٰ� ���ϱ�
        if (s == null)
        {
            Debug.LogWarning("���尡 �����ϴ�.");
            return;
        }
        // �Ҹ��� �÷����Ѵ�.
        s.source.PlayOneShot(s.source.clip);
    }

    public void PlaySound(string name)
    {
        // �迭�ȿ� �� �Ҹ� �̸��� ���� �̸��� ���ٸ� �÷��� �Ѵ�.
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // ���尡 ������ ���ٰ� ���ϱ�
        if (s == null)
        {
            Debug.LogWarning("���尡 �����ϴ�.");
            return;
        }
        // �Ҹ��� �÷����Ѵ�.
        s.source.Play();
    }
}
