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
    [SerializeField] private bool shouldBeIngame;
    public TimeDirection Direction;
    private bool _isIngame;
    private AudioMixerSnapshot _ingame;
    private AudioMixerSnapshot _editor;

    private static AudioManager _instance;

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
        _ingame = mixer.FindSnapshot("Ingame");
        _editor = mixer.FindSnapshot("Editor");

        if (_editor == null || _ingame == null)
        {
            throw new Exception("The selected snapshot either has no Ingame or no Editor channel...");
        }
    }

    private void Update()
    {
        if (shouldBeIngame != IsIngame)
        {
            IsIngame = shouldBeIngame;
        }
    }
}