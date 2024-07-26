using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private Vector3 _currentMovementDirection = Vector3.zero;
    private Bounds _playArea;

    [SerializeField]
    private float _speed = 1f;

    private void OnEnable()
    {
        _currentMovementDirection = Random.insideUnitSphere.normalized;
        _currentMovementDirection.y = 0;
    }
    private void Start()
    {
        _playArea = GameManager.Instance.PlayArea.PlayAreaBounds;
    }

    private bool IsInsidePlayArea()
    {
        return _playArea.Contains(new Vector3(transform.position.x, 0f, transform.position.z));
    }

    private void ReflectCurrentMovementDirection()
    {
        float radianAngle = Random.Range(-180f, 180f) * Mathf.Rad2Deg;
        _currentMovementDirection = (_playArea.center - transform.position + new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle))).normalized;
        _currentMovementDirection.y = 0;
    }

    private void Update()
    {
        transform.position += _currentMovementDirection * _speed * Time.deltaTime;

        if (!IsInsidePlayArea())
            ReflectCurrentMovementDirection();
    }
}
