using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour {
    public static SoundBank instance;

    private void Awake() {
        instance = this;
    }

    [Header("Music")]
    public AudioClip ThemeMusic;

    [Header("Items")]
    public AudioClip PickUpItem;

    [Header("Player Sounds")]
    public AudioClip PlayerFootsteps;
    public AudioClip PlayerDie;
    public AudioClip PlayerJump;
    public AudioClip PlayerLand;
    public AudioClip PlayerTakeDamage;
    public AudioClip WeaponShot;

    [Header("UI")]
    public AudioClip UISelect;
    public AudioClip UIBack;
    public AudioClip TimeRunningOut;
    public AudioClip GameOver;
}
