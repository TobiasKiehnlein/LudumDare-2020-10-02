using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private BoolReference _isGameOver;
    [SerializeField] private BoolReference _hadPlayerSuccess;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();
    [SerializeField] private TimeDirectionReference _timeDirection;


    private AudioSource _audioSource;
    private Queue<AudioClip> _clipQueue = new Queue<AudioClip>();

    private IEnumerator Start()
    {
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
            Debug.Log("Build Mode");
            yield return BuildMode();
            Debug.Log("Play Mode");
            yield return PlayMode();
            Debug.Log("Reverse Mode");
            yield return Reverse();
            CheckForVictory();
        }

        yield return new WaitForSeconds(6);
    }

    private IEnumerator BuildMode()
    {
        yield break;
        
        _timeDirection.Value = TimeDirection.Idle;
        while (_timeDirection.Value != TimeDirection.Forward)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator PlayMode()
    {
        _hadPlayerSuccess.Value = false;
        _timeDirection.Value = TimeDirection.Forward;
        while (_timeDirection.Value == TimeDirection.Forward)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Reverse()
    {
        _timeDirection.Value = TimeDirection.Backward;
        while (_timeDirection.Value == TimeDirection.Backward)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private void CheckForVictory()
    {
        if (!_hadPlayerSuccess.Value)
        {
            return;
        }

        PlayNextAudio();
        _isGameOver.Value = _clipQueue.Count == 0;
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