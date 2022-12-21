using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    string bgmName = "";
    public AudioMixer audioMixer;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        AudioMixerGroup[] audioMixerGroups = audioMixer.FindMatchingGroups("Master");
        foreach (Sound s in sounds)
        {
            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.source.loop)
            {
                s.source.outputAudioMixerGroup = audioMixerGroups[1];
            }
            else
            {
                s.source.outputAudioMixerGroup = audioMixerGroups[2];
            }
        }
    }
    public void Play(string name)
    {
        Sound sound = null;
        foreach (Sound s in sounds)
        {
            if (s.name==name)
            {
                sound = s;
                break;
            }
        }
        if (sound == null)
        {
            print("Sound : " + name + "File Not Found!");
        }
        sound.source.Play();
    }
    public void StopBgm()
    {
        Sound sound = null;
        foreach (Sound s in sounds)
        {
            if (s.name==bgmName)
            {
                sound = s;
                break;
            }
        }
        if (sound == null)
        {
            print("Sound : " + name + "File Not Found!");
            return;
        }
        sound.source.Stop();
        bgmName = "";
    }
    public void PlayBgm(string name)
    {
        if (bgmName==name)
        {
            return;
        }
        StopBgm();
        Sound sound = null;
        foreach (Sound s in sounds)
        {
            if (s.name==name)
            {
                sound = s;
                break;
            }
        }
        if (sound == null)
        {
            print("Sound : " + name + "File Not Found!");
        }
        bgmName = sound.name;
        sound.source.Play();
    }
}
