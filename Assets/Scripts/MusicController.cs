﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicLevel {
    public AudioClip intro;
    public AudioClip loop;
    public float introOffset;
}

public class MusicController : MonoBehaviour {
    public MusicLevel[] levels;

    public AudioSource audioIntro;
    public AudioSource audioLoop;

    // Start is called before the first frame update
    void Start() {
        foreach (MusicLevel level in levels) {
            level.intro.LoadAudioData();
            if (level.loop)
                level.loop.LoadAudioData();
        }

        PlayLevel(0);
    }

    public void PlayLevel(int level) {
        if (level >= levels.Length) return;
        audioIntro.Stop();
        audioLoop.Stop();

        audioIntro.clip = levels[level].intro;
        if (levels[level].loop)
            audioLoop.clip = levels[level].loop;

        audioIntro.Play();
        if (levels[level].loop)
            audioLoop.PlayDelayed(audioIntro.clip.length + levels[level].introOffset);
    }
}
