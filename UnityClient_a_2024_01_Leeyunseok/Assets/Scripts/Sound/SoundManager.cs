using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]

public class Sound
{
    public string name;     //사운드의 이름
    public AudioClip clip;  //사운드 클립
    [Range(0f, 1f)]
    public float voiume = 1f;    //사운드 볼륨

    [Range(0.1f, 3f)]
    public float pitch = 1f;        //사운드 피치
    public bool loop;               //반복 재생 여부
    public AudioMixerGroup mixerGroup;      //오디오 믹서 그룹

    [HideInInspector]               //Inspector 창에서 안보이게 
    public AudioSource source;      //오디오 소스
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;            //싱글톤 (어디서든 접근 가능하게)

    public List<Sound> sounds = new List<Sound>();  //사운드 리스트 선언
    public AudioMixer audioMixer;                   //오디오 믹서 참조

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);      //Scene이 이동되도 파괴 되지 않게 하기 위해서
        }
        else
        {
            Destroy(gameObject);
        }

        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.voiume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.mixerGroup;  //오디오 믹서 그룹 설정
        }

    }

    public void PlaySound(string name)
    {
        Sound soundToPlay = sounds.Find(sound => sound.name == name);

        if (soundToPlay != null)
        {
            soundToPlay.source.Play();
        }
        else
        {
            Debug.LogWarning("사운드 " + name + " 찾을 수 없습니다.");
        }
    }
    
}
