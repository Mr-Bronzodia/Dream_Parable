using UnityEngine;

public class Agent : MonoBehaviour, IDamageable
{
    private Vector3 _currentMovementDirection = Vector3.zero;
    private Bounds _playArea;
    private float _currentHealth = 0;
    private float _nextDamageTime = 0;
    private bool _isGamePaused = false;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _maxHealth = 3f;
    [SerializeField] private float _damageGracePeriod = 0.1f;

    public System.Action<Agent> OnAgentDestroyed;
    public float Health { get { return _currentHealth; } }
    public float MaxHealth { get { return _maxHealth; } }


    private void OnEnable()
    {
        _currentMovementDirection = GetRandomDirection();
        transform.forward = _currentMovementDirection;

        GameManager.Instance.OnPauseStateChanged += PauseStateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPauseStateChanged -= PauseStateChanged;
    }

    private void Start()
    {
        _playArea = GameManager.Instance.PlayArea.PlayAreaBounds;
        _currentHealth = _maxHealth;
    }

    private void PauseStateChanged(bool isPaused)
    {
        _isGamePaused = isPaused;
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
        transform.forward = _currentMovementDirection;
    }

    private static Vector3 GetRandomDirection()
    {
        Vector3 direction = Random.insideUnitSphere.normalized;
        direction.y = 0;
        direction.Normalize();

        return direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            return;

        damageable.TakeDamage(1f);
        this.TakeDamage(1f);

        Vector3 faceAwayCollisionDirection = (other.transform.position - transform.position) * -1f;

        RotateAgentTowardDirection(faceAwayCollisionDirection, 75);
    }

    private void Update()
    {
        if (_isGamePaused)
            return;

        transform.position += _currentMovementDirection * _speed * Time.deltaTime;

        if (IsAgentInsidePlayArea())
            return;

        Vector3 playAreaCentreDirection = _playArea.center - transform.position;
        RotateAgentTowardDirection(playAreaCentreDirection, 180);
    }

    private void DestroyAgent()
    {
        OnAgentDestroyed?.Invoke(this);

        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (Time.time < _nextDamageTime)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
            DestroyAgent();

        _nextDamageTime = Time.time + _damageGracePeriod;
    }
}
