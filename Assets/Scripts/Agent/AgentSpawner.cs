using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    private Bounds _playArea;
    private float _nextSpawnTime;
    private int _agentCount = 0;
    private bool _isGamePaused = false;

    [SerializeField] private GameObject agentPrefab;

    [SerializeField] private int maxAgentCount;

    [SerializeField] private int minStartingAgentCount = 3;
    [SerializeField] private int maxStartingAgentCount = 5;

    [SerializeField] private float minSpawnDelay = 2f;
    [SerializeField] private float maxSpawnDelay = 6f;

    private void Start()
    {
        _playArea = GameManager.Instance.PlayArea.PlayAreaBounds;

        int startingAgentCount = Random.Range(minStartingAgentCount, maxStartingAgentCount);
        for (int i = 0; i < startingAgentCount; i++)
            SpawnAgent();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPauseStateChanged += PauseStateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPauseStateChanged -= PauseStateChanged;
    }

    private void PauseStateChanged(bool isPaused)
    {
        _isGamePaused = isPaused;
    }

    private void SpawnAgent()
    {
        Vector3 randomSpawnPoint = RandomPointInBounds(_playArea);
        Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        GameObject prefabInstance = Instantiate(agentPrefab, randomSpawnPoint, randomRotation, gameObject.transform);

        if (!prefabInstance.TryGetComponent<Agent>(out Agent agent))
            throw new System.Exception($"Couldn't access Agent Component on {prefabInstance.name}.");

        agent.OnAgentDestroyed += AgentDestroyed;

        _nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);

        _agentCount++;
    }

    private void AgentDestroyed(Agent agent)
    {
        agent.OnAgentDestroyed -= AgentDestroyed;

        _agentCount--;
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            1f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void Update()
    {
        if (_isGamePaused) 
            return;

        if (_agentCount >= maxAgentCount)
            return;

        if (_nextSpawnTime < Time.time)
            SpawnAgent();
    }
}
