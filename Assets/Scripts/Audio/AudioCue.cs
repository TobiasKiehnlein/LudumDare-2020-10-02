using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Audio Cue")]
public class AudioCue : ScriptableObject
{
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();

    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;

    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;


    public void Play(AudioSource audioSource)
    {
        var volume = Random.Range(_minVolume, _maxVolume);
        var pitch = Random.Range(_minPitch, _maxPitch);

        audioSource.volume = volume;
        audioSource.pitch = pitch;

        var clip = _clips[Random.Range(0, _clips.Count)];
        audioSource.PlayOneShot(clip);
    }
}