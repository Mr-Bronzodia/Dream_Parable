using UnityEngine;

public class Agent : MonoBehaviour
{
    private Vector3 _currentMovementDirection = Vector3.zero;
    private Bounds _playArea;

    [SerializeField]
    private float _speed = 1f;

    private void OnEnable()
    {
        _currentMovementDirection = GetRandomDirection();
    }
    private void Start()
    {
        _playArea = GameManager.Instance.PlayArea.PlayAreaBounds;
    }

    private bool IsAgentInsidePlayArea()
    {
        return _playArea.Contains(new Vector3(transform.position.x, 0f, transform.position.z));
    }

    private void RotateAgentTowardDirection(Vector3 direction, float degreeRange)
    {
        float radianAngle = Random.Range(-degreeRange, degreeRange) * Mathf.Rad2Deg;

        Vector3 newDirection = direction + new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle));
        newDirection.y = 0f;
        newDirection.Normalize();

        _currentMovementDirection = newDirection;
    
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 direction = Random.insideUnitSphere.normalized;
        direction.y = 0;
        direction.Normalize();

        return direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 faceAwayCollisionDirection = (other.transform.position - transform.position) * -1f;

        RotateAgentTowardDirection(faceAwayCollisionDirection, 75);
    }

    private void Update()
    {
        transform.position += _currentMovementDirection * _speed * Time.deltaTime;

        if (IsAgentInsidePlayArea())
            return;

        Vector3 playAreaCentreDirection = _playArea.center - transform.position;
        RotateAgentTowardDirection(playAreaCentreDirection, 180);
    }
}
