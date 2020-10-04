using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Fictions/Audio Cue")]
public class AudioCue : ScriptableObject
{
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();

    [SerializeField, HideInInspector] private float _minVolume; 
    [SerializeField, HideInInspector] private float _maxVolume;
    
    [SerializeField, HideInInspector] private float _minPitch; 
    [SerializeField, HideInInspector] private float _maxPitch;   
    
    [ShowInInspector]
    public float MinVolume
    {
        get => _minVolume;
        set => _minVolume = Mathf.Clamp(value, 0, _maxVolume);
    }

    [ShowInInspector]
    public float MaxVolume
    {
        get => _maxVolume;
        set => _maxVolume = Mathf.Clamp(value, _minVolume, 1);
    }
    
    
    [ShowInInspector]
    public float MinPitch
    {
        get => _minPitch;
        set => _minPitch = Mathf.Clamp(value, -3, _maxPitch);
    }  
    
    [ShowInInspector]
    public float MaxPitch
    {
        get => _maxPitch;
        set => _maxPitch = Mathf.Clamp(value, _minPitch, 3);
    }     
  

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
