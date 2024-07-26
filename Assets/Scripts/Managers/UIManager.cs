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
    private VisualElement _marcoPoloVE;

    private Button _pauseButton;
    private ProgressBar _healthBarSlider;
    private Label _nameLabel;
    private Label _marcoPoloLabel;
    private Button _marcoPoloButton;

    private bool _isMarcoPoloOpen = false;


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
        _pauseButton.clicked += SelectionManager.Instance.DeselectCurrentAgent;

        _healthBarVE = _uiDocument.rootVisualElement.Q("HealthBar");
        _healthBarSlider = _healthBarVE.Q<ProgressBar>();

        _nameLabelVE = _uiDocument.rootVisualElement.Q("TextBackground");
        _nameLabel = _nameLabelVE.Q<Label>();

        _marcoPoloVE = _uiDocument.rootVisualElement.Q("MarcoPoloTab");
        _marcoPoloLabel = _marcoPoloVE.Q<Label>();
        _marcoPoloButton = _marcoPoloVE.Q<Button>();
        _marcoPoloLabel.text = MarcoPolo.Solve();
        _marcoPoloButton.clicked += ToggleMarcoPolo;

        SelectionManager.Instance.OnAgentSelected += AgentSelected;
        SelectionManager.Instance.OnAgentDeSelected += AgentDeselected;
    }

    private void AgentSelected(Agent agent)
    {
        _healthBarSlider.highValue = agent.MaxHealth;
        _healthBarSlider.value = agent.Health;
        _healthBarSlider.title = $"{agent.Health}/{agent.MaxHealth}";

        _nameLabel.text = agent.name;

        _healthBarVE.RemoveFromClassList("hidden");
        _nameLabelVE.RemoveFromClassList("hidden");
        _pauseButtonVE.RemoveFromClassList("hidden");
    }

    private void ToggleMarcoPolo()
    {
        if (!_isMarcoPoloOpen)
        {
            _marcoPoloVE.AddToClassList("marco-polo");
            _marcoPoloVE.RemoveFromClassList("marco-polo-closed");
        }
        else
        {
            _marcoPoloVE.RemoveFromClassList("marco-polo");
            _marcoPoloVE.AddToClassList("marco-polo-closed");
        }
            

        _isMarcoPoloOpen = !_isMarcoPoloOpen;
    }

    private void AgentDeselected()
    {
        _pauseButtonVE.AddToClassList("hidden");
        _healthBarVE.AddToClassList("hidden");
        _nameLabelVE.AddToClassList("hidden");

    }

    private void OnDisable()
    {
        SelectionManager.Instance.OnAgentDeSelected -= AgentDeselected;
        _pauseButton.clicked -= SelectionManager.Instance.DeselectCurrentAgent;
        _marcoPoloButton.clicked -= ToggleMarcoPolo;
    }
}
