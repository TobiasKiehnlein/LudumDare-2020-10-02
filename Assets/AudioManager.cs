using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float timeToReach;
    [SerializeField] private float pitchChangingSpeed = 5;
    public TimeDirection Direction;
    private bool _isIngame;
    private AudioMixerSnapshot _ingame;
    private AudioMixerSnapshot _editor;

    private static AudioManager _instance;
    private AudioSource[] _audioSources;

    public static AudioManager Instance => _instance;

    public bool IsIngame
    {
        get => _isIngame;
        set
        {
            _isIngame = value;
            if (value)
            {
                _ingame.TransitionTo(timeToReach);
            }
            else
            {
                _editor.TransitionTo(timeToReach);
            }
        }
    }


    private void Awake()
    {
        transform.parent = null;
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        _audioSources = GetComponents<AudioSource>();

        _ingame = mixer.FindSnapshot("Ingame");
        _editor = mixer.FindSnapshot("Editor");

        if (_editor == null || _ingame == null)
        {
            throw new Exception("The selected snapshot either has no Ingame or no Editor channel...");
        }
    }

    private void Update()
    {
        foreach (var audioSource in _audioSources)
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, TimeDirection.Backward == Direction ? -1 : 1, Time.deltaTime * pitchChangingSpeed);
        }
    }
}