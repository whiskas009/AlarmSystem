using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _timeTurnOn;
    [SerializeField] private float _timeTurnOff;

    private AudioSource _audioSource;
    private float _minVolume = 0;
    private float _maxVolume = 1;
    private Coroutine _coroutineMoveVolumeInWork;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            ChangeVolume(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            ChangeVolume(false, true);
        }
    }

    private void ChangeVolume(bool isUpVolume = false, bool isLowVolume = false)
    { 
        if (_coroutineMoveVolumeInWork != null)
        {
            StopCoroutine(_coroutineMoveVolumeInWork);
        }

        if (isUpVolume)
        {
            _audioSource.volume = _minVolume;
            _audioSource.Play();
            _coroutineMoveVolumeInWork = StartCoroutine(MoveVolume(_maxVolume));
        }
        else if (isLowVolume)
        {
            _coroutineMoveVolumeInWork = StartCoroutine(MoveVolume(_minVolume));
            while (_audioSource.volume > _minVolume) {}
            _audioSource.Stop();
        }
    }

    private IEnumerator MoveVolume(float targetVolume)
    {
        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, (_maxVolume / _timeTurnOn) * Time.deltaTime);
            yield return null;
        }
    }
}
