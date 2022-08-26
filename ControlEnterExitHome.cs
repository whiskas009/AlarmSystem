using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlEnterExitHome : MonoBehaviour
{
    [SerializeField] private UnityEvent _activatedAlarm;
    [SerializeField] private UnityEvent _deactivatedAlarm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _activatedAlarm?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _deactivatedAlarm?.Invoke();
        }
    }
}
