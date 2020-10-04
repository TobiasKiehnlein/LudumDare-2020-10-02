﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameLoop : MonoBehaviour
{
    [SerializeField] private IntReference _levelId;
    [SerializeField] private BoolReference _isGameFinished;
    [SerializeField] private BoolReference _hadPlayerSuccess;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();
    [SerializeField] private TimeDirectionReference _timeDirection;

    [SerializeField] private GameObject _resetButton;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _buildModeHeader;
    [SerializeField] private Animator _playerAnimator;

    private static readonly int PlayerVelocity = Animator.StringToHash("Horizontal Velocity");


    private AudioSource _audioSource;
    private Queue<AudioClip> _clipQueue = new Queue<AudioClip>();

    private IEnumerator Start()
    {
        _hadPlayerSuccess.Value = true;
        _timeDirection.Value = TimeDirection.Idle;
        _levelId.Value = 0;
        _clipQueue = new Queue<AudioClip>(_clips);
        _audioSource = GetComponent<AudioSource>();
        _isGameFinished.Value = false;

        PlayNextAudio();
        yield return new WaitForSeconds(5);
        PlayNextAudio();
        yield return Loop();
    }

    private IEnumerator Loop()
    {
        while (!_isGameFinished.Value)
        {
            yield return BuildMode();
            yield return PlayMode();
            yield return Reverse();
            if (_hadPlayerSuccess.Value)
                PlayNextAudio();
        }

        _playerAnimator.SetFloat(PlayerVelocity, 0);
        yield return new WaitForSeconds(6);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    private IEnumerator BuildMode()
    {
        AudioManager.Instance.IsIngame = false;

        EnableBuildModeObjects(true);
        _playButton.GetComponent<Button>().interactable = false;
        _timeDirection.Value = TimeDirection.Idle;
        if (_hadPlayerSuccess.Value)
        {
            BlockEditor.BlockEditor.Instance.StartLevel();
        }

        while (_timeDirection.Value != TimeDirection.Forward)
        {
            yield return new WaitForEndOfFrame();
        }

        EnableBuildModeObjects(false);
    }

    private void EnableBuildModeObjects(bool state)
    {
        _buildModeHeader.SetActive(state);
        _playButton.SetActive(state);
        _resetButton.SetActive(state);
    }

    private IEnumerator PlayMode()
    {
        AudioManager.Instance.IsIngame = true;
        _hadPlayerSuccess.Value = false;
        _timeDirection.Value = TimeDirection.Forward;
        while (_timeDirection.Value == TimeDirection.Forward)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Reverse()
    {
        if (_hadPlayerSuccess.Value)
        {
            _levelId.Value++;
            _isGameFinished.Value = _levelId.Value >= 10;
        }


        if (!_isGameFinished.Value)
        {
            AudioManager.Instance.Direction = TimeDirection.Backward;
            _timeDirection.Value = TimeDirection.Backward;
            while (_timeDirection.Value == TimeDirection.Backward)
            {
                yield return new WaitForEndOfFrame();
            }

            AudioManager.Instance.Direction = TimeDirection.Forward;
        }
    }

    private void PlayNextAudio()
    {
        if (_clipQueue.Count == 0)
        {
            return;
        }

        _audioSource.PlayOneShot(_clipQueue.Dequeue());
    }
}