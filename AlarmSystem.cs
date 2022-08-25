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
    private Coroutine _coroutineUpVolumeInWork;
    private Coroutine _coroutineLowVolumeInWork;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _coroutineUpVolumeInWork = StartCoroutine(UpVolume());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _coroutineLowVolumeInWork = StartCoroutine(LowVolume());
        }
    }

    private IEnumerator UpVolume()
    { 
        if (_coroutineLowVolumeInWork != null)
        {
            StopCoroutine(_coroutineLowVolumeInWork);
        }
            
        _audioSource.volume = _minVolume;
        _audioSource.Play();

        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, (_maxVolume / _timeTurnOn) * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator LowVolume()
    {
        if (_coroutineUpVolumeInWork != null)
        {
            StopCoroutine(_coroutineUpVolumeInWork);
        }
            
        while (_audioSource.volume > _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, (_maxVolume / _timeTurnOff) * Time.deltaTime);
            yield return null;
        }

        _audioSource.Stop();
    }
}
