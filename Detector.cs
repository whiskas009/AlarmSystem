using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Detector : MonoBehaviour
{
    [SerializeField] private float _speedChangeVolume;

    private AudioSource _audioSource;
    private float _minVolume = 0;
    private float _maxVolume = 1;
    private Coroutine _coroutineMoveVolumeInWork;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Change(bool isUpVolume = true)
    {
        if (_coroutineMoveVolumeInWork != null)
        {
            StopCoroutine(_coroutineMoveVolumeInWork);
        }

        if (isUpVolume)
        {
            _audioSource.Play();
            _coroutineMoveVolumeInWork = StartCoroutine(MoveValue(_maxVolume));
        }
        else
        {
            _coroutineMoveVolumeInWork = StartCoroutine(MoveValue(_minVolume));
        }
    }

    private IEnumerator MoveValue(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedChangeVolume * Time.deltaTime);
            yield return null;
        }

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }
}
