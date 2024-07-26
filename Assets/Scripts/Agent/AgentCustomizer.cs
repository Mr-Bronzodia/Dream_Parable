using System.Collections.Generic;
using UnityEngine;

public class AgentCustomizer : MonoBehaviour
{
    private Renderer _renderer;

    [SerializeField] private List<string> names;

    private void OnEnable()
    {
        _renderer = GetComponent<Renderer>();

        _renderer.material.color = GetRandomColor();
        gameObject.name = names[Random.Range(0, names.Count)];
    }
    private static Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    private void Awake()
    {
        
    }
}
