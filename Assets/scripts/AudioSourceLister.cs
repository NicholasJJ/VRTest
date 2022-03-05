using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioSourceLister : ScriptableObject
{
    List<AudioSource> soundMakers;
    List<AudioSource> loopingSounds;

    public void resetSounds() {
        soundMakers = new List<AudioSource>();
        loopingSounds = new List<AudioSource>();
    }
    public void AddSoundMaker(AudioSource a) {
        soundMakers.Add(a);
    }
    public void AddLoopingSound(AudioSource a) {
        loopingSounds.Add(a);
    }

    public List<AudioSource> GetLoopingSounds() => loopingSounds;
    public List<AudioSource> GetSoundMakers() => soundMakers;
}
