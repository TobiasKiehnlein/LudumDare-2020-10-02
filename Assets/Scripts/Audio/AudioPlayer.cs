using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioCue _audioCue;
    [SerializeField] private float _delayInSeconds;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(_delayInSeconds);
        _audioCue.Play(_audioSource);
    }
}