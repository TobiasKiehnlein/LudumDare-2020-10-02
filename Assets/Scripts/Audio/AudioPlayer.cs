using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioCue _audioCue;
    [SerializeField] private float _delayInSeconds;

    private void OnEnable()
    {
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(_delayInSeconds);
        _audioCue.Play(GetComponent<AudioSource>());
    }
}
