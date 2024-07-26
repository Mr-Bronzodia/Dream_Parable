using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private UIDocument _uiDocument;
    private VisualElement _pauseButtonVE;
    private VisualElement _healthBarVE;
    private VisualElement _nameLabelVE;

    private Button _pauseButton;
    private ProgressBar _healthBarSlider;
    private Label _nameLabel;


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

    private void OnEnable()
    {
        _uiDocument = GetComponent<UIDocument>();

        _pauseButtonVE = _uiDocument.rootVisualElement.Q("UnpauseButton");
        _pauseButton = _pauseButtonVE.Q<Button>();
        _pauseButton.clicked += GameManager.Instance.UnPauseGame;

        _healthBarVE = _uiDocument.rootVisualElement.Q("HealthBar");
        _healthBarSlider = _healthBarVE.Q<ProgressBar>();

        _nameLabelVE = _uiDocument.rootVisualElement.Q("TextBackground");
        _nameLabel = _nameLabelVE.Q<Label>();

        GameManager.Instance.OnPauseStateChanged += PauseStateChanged;
        SelectionManager.Instance.OnAgentSelected += AgentSelected;
    }

    private void AgentSelected(Agent agent)
    {
        _healthBarSlider.highValue = agent.MaxHealth;
        _healthBarSlider.value = agent.Health;
        _healthBarSlider.title = $"{agent.Health}/{agent.MaxHealth}";

        _nameLabel.text = agent.name;

        _healthBarVE.RemoveFromClassList("hidden");
        _nameLabelVE.RemoveFromClassList("hidden");
    }

    private void PauseStateChanged(bool isPaused)
    {
        if (isPaused)
        {
            _pauseButtonVE.RemoveFromClassList("hidden");
            return;
        }


        _pauseButtonVE.AddToClassList("hidden");
        _healthBarVE.AddToClassList("hidden");
        _nameLabelVE.AddToClassList("hidden");

    }

    private void OnDisable()
    {
        GameManager.Instance.OnPauseStateChanged -= PauseStateChanged;
        _pauseButton.clicked -= GameManager.Instance.UnPauseGame;
    }
}
