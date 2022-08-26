using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _speed;

    private SpriteRenderer _spriteRenderer;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startPosition = transform.position;
        _targetPosition = _targetPoint.position;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

        if (transform.position.x == _targetPosition.x)
        {
            _targetPosition = _startPosition;
            _spriteRenderer.flipX = true;
        }
    }
}
