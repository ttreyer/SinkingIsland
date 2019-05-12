using System.Collections;
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
            level.loop.LoadAudioData();
        }

        PlayLevel(1);
    }

    void StartLevel(int level) {
        StopCoroutine("PlayLevel");
        StartCoroutine("PlayLevel", level);
    }

    void PlayLevel(int level) {
        audioIntro.Stop();
        audioLoop.Stop();

        audioIntro.clip = levels[level].intro;
        audioLoop.clip = levels[level].loop;

        audioIntro.Play();
        audioLoop.PlayDelayed(audioIntro.clip.length + levels[level].introOffset);
    }
}
