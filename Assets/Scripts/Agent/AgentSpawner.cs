using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    private Bounds _playArea;
    private float _nextSpawnTime;

    [SerializeField] private GameObject agentPrefab; 

    [SerializeField] private int minStartingAgentCount = 3;
    [SerializeField] private int maxStartingAgentCount = 3;

    [SerializeField] private float minSpawnDelay = 2f;
    [SerializeField] private float maxSpawnDelay = 6f;

    private void Start()
    {
        _playArea = GameManager.Instance.PlayArea.PlayAreaBounds;

        int startingAgentCount = Random.Range(minStartingAgentCount, maxStartingAgentCount);
        for (int i = 0; i < startingAgentCount; i++)
            SpawnAgent();
    }

    private void SpawnAgent()
    {
        Vector3 randomSpawnPoint = RandomPointInBounds(_playArea);
        Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        GameObject agent = GameObject.Instantiate(agentPrefab, randomSpawnPoint, randomRotation, gameObject.transform);

        _nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
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
        if (_nextSpawnTime < Time.time)
            SpawnAgent();
    }
}
