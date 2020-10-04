using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLoop : MonoBehaviour
{
    [SerializeField] private IntReference _levelId;
    [SerializeField] private BoolReference _isGameOver;
    [SerializeField] private BoolReference _hadPlayerSuccess;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();
    [SerializeField] private TimeDirectionReference _timeDirection;

    [SerializeField] private GameObject[] _buildModeObjects;
    [SerializeField] private GameObject[] _generalObjects;


    private AudioSource _audioSource;
    private Queue<AudioClip> _clipQueue = new Queue<AudioClip>();

    private IEnumerator Start()
    {
        _timeDirection.Value = TimeDirection.Idle;
        _levelId.Value = 0;
        _clipQueue = new Queue<AudioClip>(_clips);
        _audioSource = GetComponent<AudioSource>();
        _isGameOver.Value = false;

        PlayNextAudio();
        yield return new WaitForSeconds(7);
        PlayNextAudio();
        yield return Loop();
    }

    private IEnumerator Loop()
    {
        while (!_isGameOver.Value)
        {
            yield return BuildMode();
            yield return PlayMode();
            yield return Reverse();
            PlayNextAudio();
        }

        yield return new WaitForSeconds(6);
    }

    private IEnumerator BuildMode()
    {
        AudioManager.Instance.IsIngame = false;

        EnableBuildModeObjects(true);
        _timeDirection.Value = TimeDirection.Idle;
        BlockEditor.BlockEditor.Instance.Start();
        while (_timeDirection.Value != TimeDirection.Forward)
        {
            yield return new WaitForEndOfFrame();
        }

        EnableBuildModeObjects(false);
    }

    private void EnableBuildModeObjects(bool state)
    {
        foreach (var generalObject in _generalObjects)
        {
            generalObject.SetActive(!state);
        }

        foreach (var buildModeObject in _buildModeObjects)
        {
            buildModeObject.SetActive(state);
        }
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
        AudioManager.Instance.Direction = TimeDirection.Backward;

        if (_hadPlayerSuccess)
        {
            _levelId.Value++;
            _isGameOver.Value = _clipQueue.Count == 1;
        }
        
        if (_isGameOver)
        {
            yield break;
        }

        _timeDirection.Value = TimeDirection.Backward;
        while (_timeDirection.Value == TimeDirection.Backward)
        {
            yield return new WaitForEndOfFrame();
        }

        AudioManager.Instance.Direction = TimeDirection.Forward;
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