using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    // 사용할 사운드를 가져온다
    public Sound[] sounds;

    void Awake()
    {
        // 씬을 옮겨도 파괴되지 안게 한다.
        DontDestroyOnLoad(gameObject);

        // 인스턴스가 비어있다면 이것을 넣고 아니면 이 게임오브젝트를 삭제한다.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //모든 오디오 소스에 접근하여 가져온다.
        foreach (Sound s in sounds)
        {
            //클립, 볼륨, 음 높낮이에 접근하기 위한 해당 컴포넌트 소스를 추가한다.
            s.source = gameObject.AddComponent<AudioSource>();
            //자기 자신을 인지하게 알맞는 데이터를 넣음
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // 플레이어가 직접 넣은 문자열에 반응하여 소리를 플레이 한다.
    public void PlayOneShotSound(string name)
    {
        // 배열안에 들어간 소리 이름과 적은 이름이 같다면 플레이 한다.
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // 사운드가 없을때 없다고 말하기
        if (s == null)
        {
            Debug.LogWarning("사운드가 없습니다.");
            return;
        }
        // 소리를 플레이한다.
        s.source.PlayOneShot(s.source.clip);
    }

    public void PlaySound(string name)
    {
        // 배열안에 들어간 소리 이름과 적은 이름이 같다면 플레이 한다.
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // 사운드가 없을때 없다고 말하기
        if (s == null)
        {
            Debug.LogWarning("사운드가 없습니다.");
            return;
        }
        // 소리를 플레이한다.
        s.source.Play();
    }
}
