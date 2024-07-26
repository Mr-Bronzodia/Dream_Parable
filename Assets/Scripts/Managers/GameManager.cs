using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayArea playArea;

    public static GameManager Instance { get; private set; }
    public PlayArea PlayArea { get { return playArea; } }

    public System.Action<bool> OnPauseStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SelectionManager.Instance.OnAgentSelected += AgentSelected;
    }

    private void AgentSelected(Agent agent)
    {
        OnPauseStateChanged?.Invoke(true);
    }

    public void UnPauseGame()
    {
        OnPauseStateChanged?.Invoke(false);
    }

    private void OnDisable()
    {
        SelectionManager.Instance.OnAgentSelected -= AgentSelected;
    }
}
