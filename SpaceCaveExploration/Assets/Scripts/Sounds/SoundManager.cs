using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;

    [Header("Music")]
    public AudioSource MusicSource;


    [Header("Sound Effects")]
    public AudioSource EffectsSource;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    private void Start() {
        PlayMusic(SoundBank.instance.ThemeMusic);
    }

    public void PlayMusic(AudioClip _clip) {
        if (_clip == null) return;

        MusicSource.Stop();
        MusicSource.clip = _clip;
        MusicSource.loop = true;
        MusicSource.Play();
    }

    public void PlayEffect(AudioClip _clip) {
        if (_clip == null) return;

        EffectsSource.PlayOneShot(_clip);
    }
}
