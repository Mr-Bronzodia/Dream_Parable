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

    private void ChangeMovementDirection(float degreeRange)
    {
        float radianAngle = Random.Range(-degreeRange, degreeRange) * Mathf.Rad2Deg;
        _currentMovementDirection = (_playArea.center - transform.position + new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle))).normalized;
        _currentMovementDirection.y = 0;
    
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeMovementDirection(360);
    }

    private void Update()
    {
        transform.position += _currentMovementDirection * _speed * Time.deltaTime;

        if (!IsInsidePlayArea())
            ChangeMovementDirection(180);
    }
}
